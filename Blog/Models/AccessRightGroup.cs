using System;
using System.Collections.Generic;

#nullable disable

namespace Blog.Models
{
    public partial class AccessRightGroup
    {
        public int AccessRightId { get; set; }
        public int GroupId { get; set; }

        public virtual AccessRight AccessRight { get; set; }
        public virtual Group Group { get; set; }
    }
}
