using DAL;
using Models;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace TrainingAndMeetingProgramApplication.Controllers
{
    public class RoomDetailsController : ApiController
    {
        IRoom dt = new RoomDetails();

        // POST: api/ RoomDetails
        [ResponseType(typeof(Schedule))]
        public IHttpActionResult PostSchedule([FromBody]ScheduleM shedule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.AddRoom(shedule);
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
