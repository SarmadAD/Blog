using Blog.Classes.Auth;
using Blog.Classes.ObjClasses;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewModel
{
    public class UserManagementViewModel : BaseViewModel
    {
        private IDbContextFactory<BlogContext> dbContextFactory;
        public IEnumerable<User> UserList { get; set; }

        public RadzenDataGrid<User> UserGrid { get; set; }
        public User UserToInsert { get; set; }
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
                UserList = ctx.Users.Where(x => x.State == (int)EnumClass.State.Active).ToList();
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
            if (updatedUser == UserToInsert)
            {
                UserToInsert = null;
            }
            await UserGrid.UpdateRow(updatedUser);
        }

        public async Task InsertRow()
        {
            UserToInsert = new User();
            await UserGrid.InsertRow(UserToInsert);
        }

        public async Task DeleteUser(User deletedUser)
        {
            if (deletedUser == UserToInsert)
            {
                UserToInsert = null;
            }

            if (UserList.Contains(deletedUser))
            {
                using var ctx = dbContextFactory.CreateDbContext();
                ctx.Users.FirstOrDefault(x => x.Id == deletedUser.Id).State = (int)EnumClass.State.Deleted;
                ctx.SaveChanges();
                await UserGrid.Reload();
            }
            else
            {
                UserGrid.CancelEditRow(deletedUser);
            }

        }

        public async void EditUser(User updatedUser)
        {
            await UserGrid.EditRow(updatedUser);
            //Debug.WriteLine(ConvertObject.ConvertObjectToJson(updatedUser));
        }

        public void CancelEdit(User cancelUser)
        {
            if (cancelUser == UserToInsert)
            {
                UserToInsert = null;
            }
            UserGrid.CancelEditRow(cancelUser);
        }

        public void OnEditUser(User updatedUser)
        {
            try
            {
                IsLoading = true;
                if (updatedUser == UserToInsert)
                    UserToInsert = null;
                using var ctx = dbContextFactory.CreateDbContext();
                ctx.Update(updatedUser);
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
        /*ToDo:
         - nach löschen, hinzufügen wird die row erst angezeigt bzw gelöscht wenn man refrehed         
         */

        public void OnCreateUser(User user)
        {
            try
            {
                IsLoading = true;
                using var ctx = dbContextFactory.CreateDbContext();
                user.Creater = CustomAuthenticationStateProvider.CurrentUser.Login;
                user.Created = DateTime.Now;
                user.LastEditor = CustomAuthenticationStateProvider.CurrentUser.Login;
                user.State = (int)EnumClass.State.Active;
                ctx.Users.Add(user);
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
    }
}
