using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Blog;
using Blog.Shared;
using Radzen;
using Radzen.Blazor;
using Blog.View.Components;
using Blog.View.Components.SVG;
using Blog.Models;
using Blog.ViewModel;

namespace Blog.View
{
    public partial class LoginBase: ComponentBase
    {
        [Inject] public LoginViewModel ViewModel { get; set; }

        protected override void OnInitialized()
        {
            ViewModel.Message = string.Empty;
            base.OnInitialized();
        }
    }
}