﻿using Blazored.LocalStorage;
using Blog.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blog.Classes.ObjClasses.EnumClass;

namespace Blog.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        private NavigationManager navigationManager;
        private PostViewModel postViewModel;
        private PostEditViewModel postEditViewModel;
        private ILocalStorageService localStorage;

        public List<Post> PostList { get; set; }
        public bool ShowPost { get; set; }
        public bool DeletePopUpVisible { get; set; }

        public HomeViewModel(
            IDbContextFactory<BlogContext> dbContextFactory, 
            NavigationManager navigationManager,
            PostViewModel postViewModel,
            PostEditViewModel postEditViewModel,
            ILocalStorageService localStorage)
        {
            this.dbContextFactory = dbContextFactory;
            this.navigationManager = navigationManager;
            this.postViewModel = postViewModel;
            this.postEditViewModel = postEditViewModel;
            this.localStorage = localStorage;
        }

        public async Task LoadPostList()
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                PostList = ctx.Posts
                    .Where(x=>x.State == (int)State.Active)
                    .Include(x => x.PostCategories)
                    .ThenInclude(x => x.Category)
                    .Include(x=>x.PostTags)
                    .ThenInclude(x=>x.Tag)
                    .Include(x => x.PostUsers)
                    .ThenInclude(x => x.User)
                    .OrderByDescending(x=>x.Published)
                    .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public string Categorys(ICollection<PostCategory> categories)
        {
            var categoryString = string.Empty;
            if (categories.Any())
            {
                foreach (var category in categories)
                    categoryString += category.Category.Name + ", ";
            }
            else categoryString = "Keine Kategorien gefunden";

            return categoryString;
        }

        public void OpenSelectedPost(Post post)
        {
            postViewModel.SelectedPost = post;
            DeleteLocalStorage();
            SetSelectedPostInLocalStorage(post);
            navigationManager.NavigateTo("/post");
        }

        public void EditMode(Post post)
        {
            postEditViewModel.EditMode = false;
            postEditViewModel.StepNumber = 1;
            postEditViewModel.SelectedPost = post;
            if (post.Id > 0)
                postEditViewModel.EditMode = true;
            DeleteLocalStorage();
            SetSelectedPostInLocalStorage(post);
            navigationManager.NavigateTo("/postEdit");
        }

        public async void DeletePost(Post post)
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                var getPost = ctx.Posts.FirstOrDefault(x=>x.Id == post.Id);
                if (getPost != null)
                {
                    PostList.Remove(post);
                    getPost.State = (int)State.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void SetSelectedPostInLocalStorage(Post post) => await localStorage.SetItemAsync(post.Id.ToString(), post.Name);

        private async void DeleteLocalStorage() => await localStorage.ClearAsync();
    }
}
