using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.ViewModel
{
    public class UserManagementViewModel:BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        public IEnumerable<User> UserList { get; set; }
        public UserManagementViewModel(IDbContextFactory<BlogContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public void LoadUser()
        {
            try
            {
                IsLoading = true; 
                using var ctx = dbContextFactory.CreateDbContext();
                UserList = ctx.Users.ToList();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
