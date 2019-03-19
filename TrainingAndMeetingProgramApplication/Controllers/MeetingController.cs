using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Models;
using Repository;
using Service;

namespace TrainingAndMeetingProgramApplication.Controllers
{
    public class MeetingController : ApiController
    {
        IMeeting dt = new MeetingDetails();
        // Post: api/Meeting
        //[Authorize]
        [HttpPost]
        [ResponseType(typeof(Meeting))]   //post data for craete new tarining
        public IHttpActionResult PostMeeting(Models.MeetingM meeting)  //bind training model
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.Insert(meeting);
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
        // GET: api/Meeting
        //[Authorize]
        [ResponseType(typeof(Meeting))]
        [HttpGet]
        public IEnumerable<MeetingListM> GetMeeting()  //bind Training list model
        {
            var result = dt.GetMeeting();
            return result.Cast<MeetingListM>();
        }

        // GET: api/Meeting/5
       // [Authorize]
        [HttpGet]
        [ResponseType(typeof(Meeting))]
        public IEnumerable<MeetingListM> GetMeeting(int id)
        {
            var details = dt.GetMeetingByID(id);
            return details.Cast<MeetingListM>();
        }

        // PUT: api/Meeting/5
        //[Authorize]
        [HttpPut]
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult PutMeeting(int id, [FromBody]Models.MeetingM meeting)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    dt.Update(id, meeting);
                    if (true)
                    {
                        return Ok("Updated");
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
        // DELETE: api/Meeting/5
        // [Authorize]
        [HttpDelete]
        [ResponseType(typeof(Meeting))]
        public IHttpActionResult DeleteMeeting(int id)
        {
            try
            {
                if (ModelState.IsValid)

                {
                    dt.Delete(id);
                    if (true)
                    {
                        return Ok("Deleted");
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