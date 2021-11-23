using ParentCare.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParentCare.Model.Medications
{
    public class MedicationLog
    {
        public MedicationLog(int medicationId)
        {
            MedicationAlertId = medicationId;
            DateUtc = DateTime.UtcNow;
        }

        public int Id { get; protected set; }
        public MedicationAlert MedicationAlert { get; protected set; }
        public int MedicationAlertId { get; protected set; }
        public DateTime DateUtc { get; protected set; }
    }
}
