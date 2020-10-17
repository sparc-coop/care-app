using FluentValidation.Results;
using Kuvio.Kernel.Core;
using ParentCare.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentCare.Model.Medications.Commands
{
    public class AlertMedicineNotTakenJob
    {
        readonly IRepository<MedicationAlert> _medicationRepository;
        readonly IRepository<Caretaker> _caretakerRepository;

        public AlertMedicineNotTakenJob(IRepository<MedicationAlert> medicationRepository, IRepository<Caretaker> caretakerRepository)
        {
            _medicationRepository = medicationRepository;
            _caretakerRepository = caretakerRepository;
        }

        public async Task Execute()
        {
            // TODO: Start job 
            StartJob();
            var lateMedications = await _medicationRepository.Query
                .Where(x => x.LastTimeTakenUtc < DateTime.UtcNow.Date
                && x.Weekdays.Contains((int)DateTime.UtcNow.DayOfWeek))
                .ToListAsync();

            SendAlerts(lateMedications);
            FinishJob();
        }

        private void FinishJob()
        {
            throw new NotImplementedException();
        }

        private void StartJob()
        {
            throw new NotImplementedException();
        }

        private async Task SendAlerts(List<MedicationAlert> lateMedications)
        {
            var userIds = lateMedications.Select(x => x.UserId).Distinct();

            var caretakers = await _caretakerRepository.Query.Where(x => userIds.Contains(x.ElderId)).ToListAsync();

            foreach (var item in caretakers)
            {
                var elderMedications = lateMedications.Where(x => x.UserId == item.ElderId).ToList();
                SendSmsMessages(elderMedications);

            }
        }

        private void SendSmsMessages(List<MedicationAlert> elderMedications)
        {
            string text = GetMedicationsText(elderMedications);
            // TODO: Send SMS
        }

        private string GetMedicationsText(List<MedicationAlert> medications)
        {
            List<string> texts = new List<string>();

            for (int i = 0; i < medications.Count; i++)
            {
                texts.Add($"{i+1}/{medications.Count}: Medication {medications[i].Title} wasn't taken. The last time taken was: {medications[i].LastTimeTakenUtc}");
            }

            return string.Join(" ------------------------- ", texts);
        }
    }
}
