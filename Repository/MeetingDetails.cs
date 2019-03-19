using DAL;
using Models;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MeetingDetails : IMeeting
    {
        public void Delete(int id)
        {
            using (var db = new TrainingAndMeetingEntities())
            {
                MeetingsAttendee meetingsAttendee = db.MeetingsAttendees.Where(a => a.MeetingId == id).FirstOrDefault();
                Meeting meeting = db.Meetings.Find(id);
                Schedule schedule = db.Schedules.Where(a => a.ScheduleId == meeting.ScheduleId).FirstOrDefault();
               
                    db.Meetings.Remove(meeting);
                    db.Schedules.Remove(schedule);
                    db.MeetingsAttendees.Remove(meetingsAttendee);
                    db.SaveChanges();
            }
        }

        public IEnumerable GetMeeting()
        {
            List<Models.MeetingListM> list = new List<Models.MeetingListM>();
            using (var db = new TrainingAndMeetingEntities())
            {
                var data = db.Meetings.ToList();    //all list of meetings
                foreach (var item in data)
                {
                    MeetingListM mList = new MeetingListM();
                    if (item.DeletedAt == null)
                    {
                        mList.MeetingId = item.MeetingId;
                        mList.MeetingName = item.MeetingName;
                        mList.OrganizerName = item.User.FirstName;
                        mList.Agenda = item.Agenda;
                        mList.StartTime = item.Schedule.StartTime.Value;
                        mList.EndTime = item.Schedule.EndTime.Value;
                        mList.RoomName = item.Schedule.RoomDetail.RoomName;
                        List<Attendee> users = new List<Attendee>();
                        foreach (var one in item.MeetingsAttendees)
                        {
                            Attendee attend = new Attendee();
                            attend.AttendeeName = one.User.FirstName;
                            attend.AttendeeID = one.User.UserId;
                            users.Add(attend);
                        }
                        mList.MeetingAttendeesName = users;
                        list.Add(mList);
                    }
                }
            }

            return list.ToList();
        }

        public IEnumerable GetMeetingByID(int id)
        {
            using (var db = new TrainingAndMeetingEntities())
            {
                List<MeetingListM> list = new List<MeetingListM>();
                //var data = new MeetingDetails(); //annonymous object of  meetingcontroller
                //var data1 = db.Meetings.Where(m => m.MeetingId == id).ToList();
                //return data1.ToList();
                var details = db.Meetings.Where(m => m.MeetingId == id)
                           .Select(s => new MeetingListM()
                           {
                               MeetingName = s.MeetingName,
                               OrganizerName = s.User.FirstName,
                               Agenda = s.Agenda,
                               StartTime = s.Schedule.StartTime.Value,
                               EndTime = s.Schedule.EndTime.Value,
                               RoomName = s.Schedule.RoomDetail.RoomName
                             //  MeetingAttendeesName = s.MeetingsAttendees.ToList();

                           });
                return details.ToList();
             }
        }

        public void Insert(MeetingM meeting)
        {

            using (var db = new TrainingAndMeetingEntities())
            {
                Schedule sch = new Schedule();                  //object schedule entity
                sch.StartTime = meeting.StartTime;
                sch.EndTime = meeting.EndTime;
                var obj = db.Schedules.Add(sch);
                sch.RoomId = meeting.RoomId;
                db.SaveChanges();                               //save data in schedule table
                Meeting met = new Meeting();                  //object of Training entity
                met.MeetingName = meeting.MeetingName;
                met.Agenda = meeting.Agenda;
                met.ScheduleId = obj.ScheduleId;             //getting Scheduleid from schedule table
                met.CreatedAt = DateTime.Now;
                met.UpdatedAt = DateTime.Now;
                met.UserId = meeting.UserId;
                var obj1 = db.Meetings.Add(met);
                db.Meetings.Add(met);               //save data in meeting db
                db.SaveChanges();
                foreach (int userid in meeting.MeetingAttendeeId)
                {
                    MeetingsAttendee mList = new MeetingsAttendee();    //object of model class
                    mList.UserId = userid;
                    mList.MeetingId = obj1.MeetingId;
                    db.MeetingsAttendees.Add(mList);
                    db.SaveChanges();
                }
            }
        }
        public void Update(int id, MeetingM meeting)
        {
            
                using (var ctx = new TrainingAndMeetingEntities())
                {
                    Meeting tra = new Meeting();
                    Schedule sch = new Schedule();
                    RoomDetail rm = new RoomDetail();
                    MeetingListM mList = new MeetingListM();    //object of model class
                    var data = ctx.Meetings.ToList();    //all list of meetings
                    var existingmeeting = ctx.Meetings.Where(s => s.MeetingId == id)
                                                            .FirstOrDefault<Meeting>();   //get existing meeting details
                    var existingschedule = ctx.Schedules.Where(s => s.ScheduleId == existingmeeting.ScheduleId)
                                                             .FirstOrDefault<Schedule>();                    // get existing schedule
                    var existingmeetingattendee = ctx.MeetingsAttendees.Where(s => s.MeetingId == existingmeeting.MeetingId)
                                                             .FirstOrDefault<MeetingsAttendee>(); //get existing meeting attendee list  
                    if (existingmeeting != null)
                    {
                    existingschedule.StartTime = meeting.StartTime;
                    existingschedule.EndTime = meeting.EndTime;
                        existingschedule.RoomId = meeting.RoomId;
                        var obj = ctx.Schedules.Add(sch);
                        ctx.SaveChanges();

                        existingmeeting.MeetingName = meeting.MeetingName;    //add value in training table
                        existingmeeting.Agenda = meeting.Agenda;
                        // existingmeetingattendee.MeetingId = meeting.MeetingId;
                        var obj1 = ctx.Meetings.Add(tra);
                        foreach (int userid in meeting.MeetingAttendeeId)
                        {
                            MeetingsAttendee list = new MeetingsAttendee();    //object of model class
                            list.UserId = userid;
                            list.MeetingId = existingmeeting.MeetingId;
                            ctx.MeetingsAttendees.Add(list);
                            ctx.SaveChanges();
                        }
                        ctx.SaveChanges();
                    }
                   
                }
            }
           
        }
    }

