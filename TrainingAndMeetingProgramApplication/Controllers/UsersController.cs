using DAL;
using Models;
using Repository;
using Service;
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
    public class UsersController : ApiController
    {
        IUser dt = new UserRegistration();
        // POST: api/Users
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser([FromBody]UserRegistrationModel userRegisteartionModel) //bind userRegistration model class
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.AddUser(userRegisteartionModel);
                    if (true)
                    {
                        return Ok("Inserted");
                    }
                }
                else
                {
                    return BadRequest("Error plz check");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
