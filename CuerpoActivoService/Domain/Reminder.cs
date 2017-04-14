using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRBroadcastServiceSample.Domain
{
    public class Reminder
    {
        public int Id { get; set; }

        public String Reminder_title { get; set; }

        public String Reminder_preview_description { get; set; }

        public String Video_url { get; set; }

        public String Start_date { get; set; }

        public String End_date { get; set; }

        public String Reminder_attachment { get; set; }

        public String Reminder_description { get; set; }
    }
}
