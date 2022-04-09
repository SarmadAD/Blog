using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Classes.Auth;
using Blog.Models;
using Blog.View.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Radzen;
using static Blog.Classes.ObjClasses.EnumClass;

namespace Blog.ViewModel
{
    public class PostEditViewModel : BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        private NavigationManager navigationManager;
        public Post SelectedPost { get; set; }
        public bool EditMode { get; set; }
        public int StepNumber = 1;
        public IEnumerable<Category> CategorieList { get; set; } = new List<Category>();
        public IEnumerable<int> CategorieIdList { get; set; } = new List<int>();
        public IEnumerable<Tag> TagList { get; set; } = new List<Tag>();
        public IEnumerable<int> TagIdList { get; set; } = new List<int>();
        public string PostName { get; set; } = string.Empty;
        public int ReadTime { get; set; } = 1;
        public string PostShortDescription { get; set; } = string.Empty;
        public AuthenticationStateProvider authenticationStateProvider;
        public PostEditViewModel(
            IDbContextFactory<BlogContext> dbContextFactory,
            AuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager)
        {
            this.dbContextFactory = dbContextFactory;
            this.navigationManager = navigationManager;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task Save()
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                var user = GetUser(ctx);

                var post = ctx.Posts
                    .Include(x => x.PostCategories)
                    .Include(x => x.PostTags)
                    .Include(x => x.PostUsers)
                    .FirstOrDefault(x => x.Id == SelectedPost.Id);

                if (post != null && EditMode)
                {
                    //bearbeiten
                    SetDefaultValue(post, user);
                    SetPostCategorie(post);
                    SetPostTag(post);
                    SetPostUser(post, user);
                    post.Text = SelectedPost.Text;
                    post.Description = SelectedPost.Description;
                    post.Name = SelectedPost.Name;
                }
                else
                {
                    //Neu hinzufügen
                    post = SelectedPost;
                    SetDefaultValue(post, user);
                    SetPostUser(post, user);
                    post.Published = DateTime.Now;
                    post.Created = DateTime.Now;
                    post.Creater = user.Login;
                    post.State = (int)State.Active;
                    SetPostCategorie(post);
                    SetPostTag(post);

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

        private void SetPostUser(Post post, User user)
        {
            if (user != null)
            {
                if (post.PostUsers.Any())
                {
                    if (post.PostUsers.All(x => x.UserId != user.Id))
                    {
                        var postUserItem = new PostUser
                        {
                            PostId = post.Id,
                            UserId = user.Id,
                        };
                        post.PostUsers.Add(postUserItem);
                    }
                }
                else
                {
                    var postUserItem = new PostUser
                    {
                        PostId = post.Id,
                        UserId = user.Id,
                    };
                    post.PostUsers.Add(postUserItem);
                }
            }
        }

        private void SetDefaultValue(Post post, User user)
        {
            post.Readtime = ReadTime;
            post.LastEditor = user.Login;
            post.LastEdit = DateTime.Now;
        }

        private void SetPostCategorie(Post post)
        {
            post.PostCategories = new List<PostCategory>();
            if (CategorieIdList != null && CategorieIdList.Any())
            {
                foreach (var categorieId in CategorieIdList)
                {
                    var postCategorieItem = new PostCategory
                    {
                        PostId = post.Id,
                        CategoryId = categorieId,
                    };
                    post.PostCategories.Add(postCategorieItem);
                }
            }
        }

        private void SetPostTag(Post post)
        {
            post.PostTags = new List<PostTag>();
            if (TagIdList != null && TagIdList.Any())
            {
                foreach (var tagId in TagIdList)
                {
                    var postTagItem = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tagId,
                    };
                    post.PostTags.Add(postTagItem);
                }
            }
        }

        private User GetUser(BlogContext ctx)
        {
            return ctx.Users
                .FirstOrDefault(x => x.Login == CustomAuthenticationStateProvider.CurrentUser.Login);
        }

        public void NextStep()
        {
            StepNumber++;
            using var ctx = dbContextFactory.CreateDbContext();
            var post = ctx.Posts.Include(x => x.PostCategories).Include(x => x.PostTags).FirstOrDefault(x => x.Id == SelectedPost.Id);
            if (post != null && EditMode)
            {
                TagIdList = post.PostTags.Select(x => x.TagId);
                CategorieIdList = post.PostCategories.Select(x => x.CategoryId);
            }
        }

        public void StepDown()
        {
            StepNumber--;
        }

        public void LoadDropDownData()
        {
            try
            {
                IsLoading = true;
                CategorieList = new List<Category>();
                TagList = new List<Tag>();
                using var ctx = dbContextFactory.CreateDbContext();
                CategorieList = ctx.Categories.ToList();
                TagList = ctx.Tags.ToList();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
