using Blazored.LocalStorage;
using Blog.Classes.API;
using Blog.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
                    .Include(x => x.PostCategories)
                    .ThenInclude(x => x.Category)
                    .Include(x=>x.PostTags)
                    .ThenInclude(x=>x.Tag)
                    .Include(x => x.PostUsers)
                    .ThenInclude(x => x.User)
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

        public string Tags(ICollection<PostTag> tags)
        {
            var tagsString = string.Empty;
            if (tags.Any())
            {
                foreach (var tag in tags)
                    tagsString += tag.Tag.Name + ", ";
            }
            else tagsString = "Keine Tags gefunden";

            return tagsString;
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
            postEditViewModel.SelectedPost = post;
            if (post.Id > 0)
                postEditViewModel.EditMode = true;
            DeleteLocalStorage();
            SetSelectedPostInLocalStorage(post);
            navigationManager.NavigateTo("/postEdit");
            //Die Logik für neue Beiträge weiter programmieren 
        }

        private async void SetSelectedPostInLocalStorage(Post post) => await localStorage.SetItemAsync(post.Id.ToString(), post.Name);

        private async void DeleteLocalStorage() => await localStorage.ClearAsync();
    }
}
