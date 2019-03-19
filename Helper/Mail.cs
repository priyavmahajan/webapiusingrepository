using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Mail
    {
       public void RegistrationMail()
        {
            User users = new User();
            Random random = new Random();   //random class object use to generate random string
            string code = random.Next().ToString();     //var code get random generated string 
            users.EmailLink = code;      //save random generated string into db
            using (MailMessage mm = new MailMessage("priyavmahajan16@gmail.com", users.Email))  // use mail message class
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + users.FirstName + ",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = 'http://localhost:61574/Users/VeryfiyUserAccount/" + code + "'>Click here to activate your account.</a>";  //append activation code with verify account url
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";   //use smtp host of gmail
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("priyavmahajan16@gmail.com", "swami@pilu");   //credential of mail senders mail account
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;   //smtp post number
                smtp.Send(mm);
            }

        }
        public void VeryfiyUserAccount(string activationCode)
        {
            string str = "";
            using (var db = new TrainingAndMeetingEntities())
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var value = db.Users.Where(a => a.EmailLink == activationCode).FirstOrDefault();
                if (value != null)
                {
                    value.IsActive = true;
                    db.SaveChanges();
                    str = "Dear user, Your email successfully activated now you can able to login";
                   
                }
                else
                {
                    str = "Dear user, Your email not activated";
                }
            }
            
          
        }



    }
}
