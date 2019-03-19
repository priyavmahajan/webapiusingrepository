using DAL;
using Models;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace TrainingAndMeetingProgramApplication.Controllers
{
    public class TrainingController : ApiController
    {
        ITraining dt = new TrainingDetails();
        // POST: api/Training
        [HttpPost]
        // [Authorize]
        [ResponseType(typeof(Training))]   //post data for craete new tarining
        public IHttpActionResult PostTraining(Models.TrainingModel training)  //bind training model
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.Insert(training);
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

        // GET: api/Training
        // [Authorize]
        [ResponseType(typeof(Training))]
        [HttpGet]
        public IEnumerable<TrainingListM> Get()  //bind Training list model
        {
            var details = dt.GetTraining();
            return details.Cast<TrainingListM>();
        }

        // GET: api/Training/5
        //[Authorize]
        [HttpGet]
        [ResponseType(typeof(Training))]
        public IEnumerable<TrainingListM> GetDetailsByID(int id)
        {
            var details = dt.GetTrainingByID(id);
            return details.Cast<TrainingListM>();
        }

        // DELETE: api/Training/5
        [Authorize]
        [HttpDelete]
        [ResponseType(typeof(Training))]
        public IHttpActionResult DeleteTraining(int id)
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

        // PUT: api/Training/5
       // [Authorize]
        [HttpPut]
        [ResponseType(typeof(Training))]
        public IHttpActionResult PutTraining(int id, Models.TrainingModel training)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dt.Update(id,training);
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
    }
}
