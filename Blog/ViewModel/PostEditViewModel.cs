using System;
using System.Linq;
using Blog.Classes.Auth;
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
        public bool EditMode { get; set; }

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
                if (post != null && EditMode)
                {
                    post.Text = SelectedPost.Text;
                }
                else
                {
                    post.Published = DateTime.Now;
                    post.Created = DateTime.Now; 
                    ctx.Posts.Add(post);
                }
                ctx.SaveChanges();
                navigationManager.NavigateTo("/");
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
