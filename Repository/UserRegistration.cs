using DAL;
using Helper;
using Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRegistration : IUser
    {
        public void AddUser(UserRegistrationModel userRegisteartionModel)
        {
            using (var db = new TrainingAndMeetingEntities())
            {
                User users = new User();   //user entity
                users.FirstName = userRegisteartionModel.FirstName;
                users.LastName = userRegisteartionModel.LastName;
                users.Email = userRegisteartionModel.Email;
                users.Password = userRegisteartionModel.Password;
               // Random random = new Random();   //random class object use to generate random string
               // string code = random.Next().ToString();     //var code get random generated string 
               // users.EmailLink = code;      //save random generated string into db
                users.UpdatedAt = DateTime.Now;
                users.CreatedAt = DateTime.Now;
                users.RoleId = 2;
                db.Users.Add(users);
                db.SaveChanges();

                Mail mail = new Mail();
                mail.RegistrationMail();
            }
        }
    }
}
