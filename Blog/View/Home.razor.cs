using Blog.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.View
{
    public class HomeBase: ComponentBase
    {
        [Inject] public HomeViewModel ViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
           await ViewModel.LoadPostList();
        }
    }
}
