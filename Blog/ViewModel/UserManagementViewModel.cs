using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.ViewModel
{
    public class UserManagementViewModel : BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        public IEnumerable<User> UserList { get; set; }

        public RadzenDataGrid<User> UserGrid { get; set; }
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
        public async void SaveUser(User updatedUser)
        {
            await UserGrid.UpdateRow(updatedUser);
        }

        public void CreateUser(User createUser) { }
        public void DeleteUser(User deletedUser) { }
        public async void EditUser(User updatedUser)
        {
            await UserGrid.EditRow(updatedUser);
        }
        public void CancelEdit(User cancelUser)
        {
            UserGrid.CancelEditRow(cancelUser);
        }

        public void OnEditUser()
        {
            //Context.Save usw
        }

        public void OnCreateUser()
        {
            //Context.Save usw
        }
    }
}
