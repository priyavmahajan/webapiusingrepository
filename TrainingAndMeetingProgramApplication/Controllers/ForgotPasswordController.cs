using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;

namespace TrainingAndMeetingProgramApplication.Controllers
{
    public class ForgotPasswordController : ApiController
    {
        private TrainingAndMeetingEntities db = new TrainingAndMeetingEntities(); //object of database entity
        // POST: api/ForgotPassword
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser([FromBody]ForgotPasswordM forgotPassword) //bind userRegistration model class
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //User = db.Users.ToList();
            User users = new User();   //user entity
            users.Email = forgotPassword.Email;
            int UserId = db.Users.Where(s => s.Email == forgotPassword.Email).Select(s => s.UserId).FirstOrDefault();
            //users.EmailLink = code;
            using (MailMessage mm = new MailMessage("priyavmahajan16@gmail.com", users.Email))  // use mail message class
                {
                    mm.Subject = "Account Activation";
                    string body = "Hello " + users.FirstName + ",";
                    body += "<br /><br />Please click the following link to Reset your password";
                    body += "<br /><a href = 'http://localhost:4200/auth/resetPassword/'" + UserId + "'>Click here to activate your account.</a>";  //append activation code with verify account url
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
            return Ok(UserId);
        }
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
