using FluentValidation.Results;
using Kuvio.Kernel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParentCare.Model.Medications.Commands
{
    public class MedicationAlertModel
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int[] Weekdays { get; set; }
    }

    public class CreateMedicationAlertCommand
    {
        readonly IRepository<MedicationAlert> _medicationRepository;

        public CreateMedicationAlertCommand(IRepository<MedicationAlert> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<MedicationAlert> Execute(int userId, MedicationAlertModel model)
        {
            var entity = new MedicationAlert(userId, model.Title, model.Time, model.Weekdays);

            await _medicationRepository.AddAsync(entity);
            await _medicationRepository.CommitAsync();

            return entity;
        }
    }
}
