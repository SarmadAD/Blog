using Blog.Classes.API;
using Blog.Classes.Models;
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

        public List<Post> PostList { get; set; }
        public bool IsLoading { get; set; }

        public HomeViewModel(IDbContextFactory<BlogContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task LoadPostList()
        {
            try
            {
                IsLoading = true;
                using (var ctx = dbContextFactory.CreateDbContext())
                {
                    PostList = ctx.Posts.ToList();
                }
                //PostList = await apiHandler.APICall<List<Post>>(null, HttpMethod.Get, "Posts");

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
                    categoryString += category.Category.Name;
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
                    tagsString += tag.Tag.Name;
            }
            else tagsString = "Keine Tags gefunden";

            return tagsString;
        }
    }
}
