using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Classes.Models
{
    public partial class Category
    {
        public Category()
        {
            PostCategorys = new HashSet<PostCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostCategory> PostCategorys { get; set; }
    }
}
