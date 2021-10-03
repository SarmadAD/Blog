using Blog.Classes.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.View
{
    public class RegisterBase : ComponentBase
    {
        [Inject] public RegisterViewModel ViewModel { get; set; }
    }
}
