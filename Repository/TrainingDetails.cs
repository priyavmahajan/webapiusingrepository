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
    public class TrainingDetails : ITraining
    {
        public void Delete(int id)
        {
            using (var db = new TrainingAndMeetingEntities())
            {
                Training training = db.Trainings.Find(id);
                Schedule schedule = db.Schedules.Where(a => a.ScheduleId == training.ScheduleId).FirstOrDefault();
                db.Trainings.Remove(training);    //delete training
                db.Schedules.Remove(schedule);   //delete schedule of training
                db.SaveChanges();
            }
        }
        public IEnumerable GetTraining()
        {
            List<Models.TrainingListM> list = new List<Models.TrainingListM>();
            try
            {
                using (var db = new TrainingAndMeetingEntities())
                {
                    var data = db.Trainings.ToList();    //all list of training
                    foreach (var item in data)
                    {
                        TrainingListM tList = new TrainingListM();    //object of medel class
                        if (item.DeletedAt == null)
                        {

                            tList.TrainingId = item.TrainingId;
                            tList.TrainerName = item.User.FirstName;
                            tList.Topic = item.Topic;
                            tList.Description = item.Description;
                            tList.StartTime = item.Schedule.StartTime.Value;
                            tList.EndTime = item.Schedule.EndTime.Value;
                            tList.RoomName = item.Schedule.RoomDetail.RoomName;
                            list.Add(tList);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list.ToList(); 
        }

        public IEnumerable GetTrainingByID(int id)
        {
            TrainingListM trainingListM = null;

            using (var context = new TrainingAndMeetingEntities())
            {
                trainingListM = context.Trainings.Include("TrainingId")   //to get list of training by comapring id
                    .Where(s => s.TrainingId == id)
                    .Select(s => new TrainingListM()
                    {
                        Topic = s.Topic,
                        TrainerName = s.User.FirstName,
                        StartTime = s.Schedule.StartTime.Value,
                        EndTime = s.Schedule.EndTime.Value,
                        RoomName = s.Schedule.RoomDetail.RoomName,
                        Description = s.Description
                    }).ToList().FirstOrDefault();
            }
            return trainingListM.ToString().ToList();
        }

        public void Insert(Models.TrainingModel training)
        {
            using (var db = new TrainingAndMeetingEntities())
            {
                Schedule sch = new Schedule();                  //object schedule entity
                sch.StartTime = training.StartTime;
                sch.EndTime = training.EndTime;
                var obj = db.Schedules.Add(sch);
                sch.RoomId = training.RoomId;
                db.SaveChanges();                               //save data in schedule table
                Training tra = new Training();                  //object of Training entity
                tra.Topic = training.Topic;
                tra.Description = training.Description;
                tra.ScheduleId = obj.ScheduleId;             //getting Scheduleid from schedule table
                tra.CreatedAt = DateTime.Now;
                tra.UpdatedAt = DateTime.Now;
                tra.UserId = training.UserId;
                db.Trainings.Add(tra);               //save data in training db
                db.SaveChanges();
            }

        }

        public void Update(int id, Models.TrainingModel training)
        {
            using (var ctx = new TrainingAndMeetingEntities())
            {
                Training tra = new Training();
                Schedule sch = new Schedule();
                RoomDetail rm = new RoomDetail();
                var existingtraining = ctx.Trainings.Where(s => s.TrainingId == id)
                                                        .FirstOrDefault<Training>();
                var existingschedule = ctx.Schedules.Where(s => s.ScheduleId == existingtraining.ScheduleId)
                                                         .FirstOrDefault<Schedule>();
                if (existingtraining != null)
                {
                    existingschedule.StartTime = training.StartTime;
                    existingschedule.EndTime = training.EndTime;
                    existingschedule.RoomId = training.RoomId;
                    var obj = ctx.Schedules.Add(sch);
                    ctx.SaveChanges();

                    existingtraining.Topic = training.Topic;        //add value in training table
                    existingtraining.Description = training.Description;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
