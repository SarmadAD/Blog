using Blog.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Classes.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        public static User CurrentUser { get; set; }
        private ClaimsIdentity indentity;

        public CustomAuthenticationStateProvider(IDbContextFactory<BlogContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            indentity = CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.Login) ?
                GetClaimsIdentity(CurrentUser.Login) : new ClaimsIdentity();
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(indentity)));
        }

        public void MarkUserAsAuthenticated(string userName)
        {
            CurrentUser = new User();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(GetClaimsIdentity(userName)))));
        }

        public void MarkUserAsLoggedOut()
        {
            CurrentUser = new User();
            indentity = new ClaimsIdentity();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

        private ClaimsIdentity GetClaimsIdentity(string login)
        {
            CurrentUser.Login = login;
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, CurrentUser.Login) };
            GetClaimOutOfTable(login).ForEach(item => claims.Add(new Claim(ClaimTypes.Role, item)));
            return new ClaimsIdentity(claims, "apiauth_type");
        }

        private List<string> GetClaimOutOfTable(string login)
        {
            using var ctx = dbContextFactory.CreateDbContext(); 
            var rolesList = new List<string>();
            var user = ctx.Users
                .Include(x=>x.UserGroups)
                .ThenInclude(x=>x.Group)
                .FirstOrDefault(x => x.Login == login);
            if (user != null)
                foreach (var userGroup in user.UserGroups.ToList())
                    rolesList.Add(userGroup.Group.Name);
            return rolesList;
        }
    }
}
