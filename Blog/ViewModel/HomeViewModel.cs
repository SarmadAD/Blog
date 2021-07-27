using Blog.Classes.API;
using Blog.Classes.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.ViewModel
{
    public class HomeViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        private NavigationManager navigationManager;
        private PostViewModel postViewModel;

        public List<Post> PostList { get; set; }
        public bool IsLoading { get; set; }
        public bool ShowPost { get; set; }

        public HomeViewModel(IDbContextFactory<BlogContext> dbContextFactory, NavigationManager navigationManager, PostViewModel postViewModel)
        {
            this.dbContextFactory = dbContextFactory;
            this.navigationManager = navigationManager;
            this.postViewModel = postViewModel;
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
            navigationManager.NavigateTo("/post");
        }
    }
}
