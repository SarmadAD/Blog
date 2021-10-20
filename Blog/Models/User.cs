using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class User
    {
        public User()
        {
            PostUsers = new HashSet<PostUser>();
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public int Typ { get; set; }
        public string Creater { get; set; }
        public DateTime? Created { get; set; }
        public string LastEditor { get; set; }
        public DateTime? LastEdit { get; set; }

        public virtual ICollection<PostUser> PostUsers { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
