using ParentCare.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentCare.Model.Medications
{
    public class MedicationAlert
    {
        public MedicationAlert(int userId, string title, DateTime time, int[] weekdays)
        {
            UserId = userId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Time = time;
            Weekdays = weekdays ?? throw new ArgumentNullException(nameof(weekdays));
            MedicationLog = new List<MedicationLog>();
        }

        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int[] Weekdays { get; set; }
        public List<MedicationLog> MedicationLog { get; set; }
        public DateTime LastTimeTakenUtc { get; set; }

        public void Take()
        {
            var log = new MedicationLog(Id);
            MedicationLog.Add(log);
            LastTimeTakenUtc = log.DateUtc;
        }
    }
}
