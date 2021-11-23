using System;
using System.Linq;
using Blog.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Blog.ViewModel
{
    public class PostEditViewModel : BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        private NavigationManager navigationManager;
        public Post SelectedPost { get; set; }

        public PostEditViewModel(
            IDbContextFactory<BlogContext> dbContextFactory,
            NavigationManager navigationManager)
        {
            this.dbContextFactory = dbContextFactory;
            this.navigationManager = navigationManager;
        }

        public void Save()
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                var post = ctx.Posts.FirstOrDefault(x => x.Id == SelectedPost.Id);
                if (post != null)
                {
                    post.Text = SelectedPost.Text;
                    ctx.SaveChanges();
                    navigationManager.NavigateTo("/");
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
    }
}
