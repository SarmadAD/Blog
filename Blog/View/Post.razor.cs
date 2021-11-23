using Blog.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.View
{
    public class PostBase : ComponentBase
    {
        [Inject] public PostViewModel ViewModel { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (ViewModel.SelectedPost == null)
                ViewModel.LoadSelectedPost();
            StateHasChanged();
        }
    }
}
