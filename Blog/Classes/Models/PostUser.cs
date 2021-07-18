using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Classes.Models
{
    public partial class PostUser
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
