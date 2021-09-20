using Blog.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.View
{
    public class ContactBase : ComponentBase
    {
        [Inject] public ContactViewModel ViewModel { get; set; }
    }
}
