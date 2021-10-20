using Blog.Models;

namespace Blog.ViewModel
{
    public class LoginViewModel
    {
        public User LoginUser {  get; set; } = new User();
        
        public void Submit()
        {
            //Benuterverwaltung: Datenbank ist fertig Login einbauen, Usern schon mal REchte zuteilen und Gruppen etc. Viel Spaß
        }
    }
}
