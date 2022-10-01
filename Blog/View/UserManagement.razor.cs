using Blog.ViewModel;
using Microsoft.AspNetCore.Components;

namespace Blog.View
{
    public class UserManagementBase:ComponentBase
    {
        [Inject] public UserManagementViewModel ViewModel { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ViewModel.LoadUser();
        }
    }
}
