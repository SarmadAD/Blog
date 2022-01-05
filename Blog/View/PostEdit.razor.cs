using Blog.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.View
{
    public partial class PostEdit : ComponentBase
    {
        [Inject] public PostEditViewModel ViewModel { get; set; }

        protected override Task OnInitializedAsync()
        {
            ViewModel.LoadDropDownData();
            return base.OnInitializedAsync();
        }
    }
}