using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public  interface IMeeting
    {
        IEnumerable GetMeeting();
        IEnumerable GetMeetingByID(int id);
        void Insert(Models.MeetingM meeting);
        void Delete(int id);
        void Update(int id, Models.MeetingM meeting);
    }
}
