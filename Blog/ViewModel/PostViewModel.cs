using Blog.Models;
using Blog.Classes.ObjClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModel
{
    public class PostViewModel : BaseViewModel
    {
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
    }
}
