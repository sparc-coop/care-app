using FluentValidation.Results;
using Kuvio.Kernel.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParentCare.Model.Medications.Commands
{
    public class TakeMedicationModel
    {
        public int Id { get; set; }
    }

    public class TakeMedicationCommand
    {
        readonly IRepository<MedicationAlert> _medicationRepository;

        public TakeMedicationCommand(IRepository<MedicationAlert> medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<MedicationAlert> Execute(int userId, MedicationAlertModel model)
        {
            var medication = await _medicationRepository.FindAsync(x => x.Id == model.Id && x.UserId == userId);
            medication.Take();
            await _medicationRepository.CommitAsync();

            return medication;
        }
    }
}
