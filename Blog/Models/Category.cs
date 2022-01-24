using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class Category
    {
        public Category()
        {
            PostCategories = new HashSet<PostCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public string Creater { get; set; }
        public DateTime? Created { get; set; }
        public string LastEditor { get; set; }
        public DateTime? LastEdit { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }
    }
}
