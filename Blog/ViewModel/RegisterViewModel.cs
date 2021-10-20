using Blog.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Blog.Classes.ObjClasses.EnumClass;

namespace Blog.ViewModel
{
    public class RegisterViewModel
    {
        public User NewUser { get; set; } = new User();
        public string RepeatPassword { get; set; } = string.Empty;
        public bool IsLoading { get; set; }

        private IDbContextFactory<BlogContext> dbContextFactory;
        private NavigationManager navigationManager;

        public RegisterViewModel(IDbContextFactory<BlogContext> dbContextFactory, NavigationManager navigationManager)
        {
            this.dbContextFactory = dbContextFactory;
            this.navigationManager = navigationManager;
        }

        public void Submit()
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                NewUser.Created = DateTime.Now;
                NewUser.Creater = NewUser.Login;
                NewUser.LastEdit = DateTime.Now;
                NewUser.LastEditor = NewUser.Login;
                NewUser.Typ = (int)UserType.Reader;
                ctx.Users.Add(NewUser);
                ctx.SaveChanges();
                ResetData();
                navigationManager.NavigateTo("/");

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

        private void ResetData() 
        {
            NewUser = new User();
            RepeatPassword = string.Empty;
        }
    }
}
