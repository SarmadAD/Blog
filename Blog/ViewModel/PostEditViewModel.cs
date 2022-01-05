using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Classes.Auth;
using Blog.Models;
using Blog.View.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace Blog.ViewModel
{
    public class PostEditViewModel : BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        private NavigationManager navigationManager;
        private DialogService DialogService;
        public Post SelectedPost { get; set; }
        public bool EditMode { get; set; }
        public int StepNumber = 1;
        public IEnumerable<Category> CategorieList { get; set; } = new List<Category>();
        public IEnumerable<int> CategorieIdList { get; set; } = new List<int>();
        public IEnumerable<Tag> TagList { get; set; } = new List<Tag>();
        public IEnumerable<int> TagIdList { get; set; } = new List<int>();
        public string PostName { get; set; } = string.Empty;
        public int ReadTime { get; set; } = 1;
        public PostEditViewModel(
            IDbContextFactory<BlogContext> dbContextFactory,
            NavigationManager navigationManager,
            DialogService DialogService)
        {
            this.dbContextFactory = dbContextFactory;
            this.navigationManager = navigationManager;
            this.DialogService = DialogService;
        }


        /*
         - User automatisch festlegen, 
            - beim ändern von einen anderen User wird dieser mit hinzugefügt
         - Schritt 1 und Schritt zwei Switch als Pfeil einfügen
         - kurz beschreibung hinzufügen
         - Bug beheben wenn man eine KAtegorie oder Tag löscht das das nicht wirklich gelöscht wird (siehe Datenbank und PostCard unten)
         - 
         */
        public async Task Save()
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                var post = ctx.Posts.FirstOrDefault(x => x.Id == SelectedPost.Id);

                if (post != null && EditMode)
                {
                    //bearbeiten
                    SetDefaultValue(post);
                    post.Text = SelectedPost.Text;
                }
                else
                {
                    //Neu hinzufügen
                    post = SelectedPost;
                    SetDefaultValue(post);
                    post.Published = DateTime.Now;
                    post.Created = DateTime.Now;
                    post.Creater = "sarmad";
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

        private void SetDefaultValue(Post post)
        {
            post.Name = PostName;
            post.Readtime = ReadTime;
            post.LastEditor = "sarmad";
            post.LastEdit = DateTime.Now;
        }

        private void SetPostCategorie(Post post)
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

        private void SetPostTag(Post post)
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

        public void NextStep() 
        {
            StepNumber++;
            using var ctx = dbContextFactory.CreateDbContext();
            var post = ctx.Posts.Include(x=>x.PostCategories).Include(x=>x.PostTags).FirstOrDefault(x => x.Id == SelectedPost.Id);
            if (post != null && EditMode)
            {
                TagIdList = post.PostTags.Select(x => x.TagId);
                CategorieIdList = post.PostCategories.Select(x => x.CategoryId);
            }
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
