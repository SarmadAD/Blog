using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Classes.Models
{
    public partial class Post
    {
        public Post()
        {
            PostCategories = new HashSet<PostCategory>();
            PostTags = new HashSet<PostTag>();
            PostUsers = new HashSet<PostUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Published { get; set; }
        public int? Readtime { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Creater { get; set; }
        public DateTime? Created { get; set; }
        public string LastEditor { get; set; }
        public DateTime? LastEdit { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<PostUser> PostUsers { get; set; }
    }
}
