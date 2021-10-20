using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class Group
    {
        public Group()
        {
            AccessRightGroups = new HashSet<AccessRightGroup>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Creater { get; set; }
        public DateTime? Created { get; set; }
        public string LastEditor { get; set; }
        public DateTime? LastEdit { get; set; }

        public virtual ICollection<AccessRightGroup> AccessRightGroups { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
