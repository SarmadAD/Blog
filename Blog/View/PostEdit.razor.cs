using Blog.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.View
{
    public partial class PostEdit : ComponentBase
    {
        [Inject] public PostEditViewModel ViewModel { get; set; }   
    }
}