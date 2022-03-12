using Blog.Models;
using Blog.Classes.ObjClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;

namespace Blog.ViewModel
{
    public class PostViewModel : BaseViewModel
    {
        private ILocalStorageService localStorage;
        private IDbContextFactory<BlogContext> dbContextFactory;
        public PostViewModel(
            ILocalStorageService localStorage,
            IDbContextFactory<BlogContext> dbContextFactory)
        {
            this.localStorage = localStorage;
            this.dbContextFactory = dbContextFactory;
        }

        public Post SelectedPost { get; set; }

        public string LoadAuthor()
        {
            var authorString = string.Empty;
            if (SelectedPost.PostUsers.Any())
            {
                var authorList = SelectedPost.PostUsers.Where(x => x.User.Typ == (int)EnumClass.UserType.Author);
                if (authorList.Count() == 1)
                {
                    authorString = 
                        $"Von {authorList.FirstOrDefault().User.Firstname} " +
                        $"{authorList.FirstOrDefault().User.Lastname} am " +
                        $"{SelectedPost.Published} veröffentlicht";
                }
                else if (authorList.Count() > 1)
                {
                    var authors = string.Empty;
                    foreach (var author in authorList)
                    {
                        var fullName = $"{author.User.Firstname} {author.User.Lastname}";
                        authors += fullName + ", ";
                    }
                    authorString = $"Von {authors} am {SelectedPost.Published.ToShortDateString()} veröffentlicht";
                }
            }
            else authorString = "Kein Author gefunden.";
            return authorString;
        }

        public async void LoadSelectedPost()
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                var posts = ctx.Posts.ToList();

                foreach (var post in posts)
                {
                    var postname = await localStorage.GetItemAsync<string>(post.Id.ToString());
                    if (postname == post.Name)
                    {
                        SelectedPost = post;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        public string Categorys()
        {
            var categoryString = string.Empty;
            var categories = SelectedPost.PostCategories.ToList();
            if (categories.Any())
            {
                foreach (var category in categories)
                    categoryString += category.Category.Name + ", ";
            }
            else categoryString = "Keine Kategorien gefunden";

            return categoryString;
        }

        public string Tags()
        {
            var tagsString = string.Empty;
            var tags = SelectedPost.PostTags.ToList();
            if (tags.Any())
            {
                foreach (var tag in tags)
                    tagsString += tag.Tag.Name + ", ";
            }
            else tagsString = "Keine Tags gefunden";

            return tagsString;
        }
    }
}
