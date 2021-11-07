using System;
using System.Linq;
using Blog.Classes.Auth;
using Blog.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Blog.ViewModel
{
    public class LoginViewModel
    {
        public User LoginUser {  get; set; } = new User();
        public AuthenticationStateProvider authenticationStateProvider;
        private NavigationManager navigationManager;
        private IDbContextFactory<BlogContext> dbContextFactory;
        public bool IsLoading { get; set; }
        public string Message { get; set; } = string.Empty;

        public LoginViewModel(
            AuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager, 
            IDbContextFactory<BlogContext> dbContextFactory
            )
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.navigationManager = navigationManager;
            this.dbContextFactory = dbContextFactory;
        }

        public void Submit()
        {
            try
            {
                IsLoading = true;
                if (LoginUser != null)
                {
                    using var ctx = dbContextFactory.CreateDbContext();
                    var user = ctx.Users
                        .Where(x=>x.Login == LoginUser.Login)
                        .Where(x=>x.Password == LoginUser.Password)
                        .FirstOrDefault();
                    if (user != null)
                    {
                        ((CustomAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(LoginUser.Login);
                        navigationManager.NavigateTo("/");
                    }
                    else Message = "Benutzername oder Password sind falsch.";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
