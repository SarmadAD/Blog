using Blog.Classes.ObjClasses;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                UserList = ctx.Users.Where(x=>x.State==(int)EnumClass.State.Active).ToList();
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
        public void DeleteUser(User deletedUser)
        {
            using var ctx = dbContextFactory.CreateDbContext();
            ctx.Users.FirstOrDefault(x => x.Id == deletedUser.Id).State = (int)EnumClass.State.Deleted;
            ctx.SaveChanges();
        }
        public async void EditUser(User updatedUser)
        {
            await UserGrid.EditRow(updatedUser);
            //Debug.WriteLine(ConvertObject.ConvertObjectToJson(updatedUser));
        }
        public void CancelEdit(User cancelUser) => UserGrid.CancelEditRow(cancelUser);

        public void OnEditUser(User updatedUser)
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                var dbUser = ctx.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
                dbUser.Lastname = updatedUser.Lastname;
                dbUser.Firstname = updatedUser.Firstname;
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        public void OnCreateUser()
        {
            //Context.Save usw
        }
    }
}
