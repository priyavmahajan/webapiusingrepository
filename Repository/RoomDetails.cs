using DAL;
using Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomDetails : IRoom
    {
        public void AddRoom(ScheduleM s)
        {
            using (var db = new TrainingAndMeetingEntities())
            {
                Schedule shc = new Schedule();
                var availableRooms = db.RoomDetails
                                    .Where(m => m.Schedules.All(r => r.EndTime <= s.StartTime || r.StartTime >= s.EndTime))
                                    .Where(r => r.RoomId != shc.RoomId)
                                    .Select(r => new { r.RoomId, r.RoomName });    //return all avilable rooms according to strat time and end time


            }
        }
    }
}
