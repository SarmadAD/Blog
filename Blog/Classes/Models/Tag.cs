using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Classes.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
