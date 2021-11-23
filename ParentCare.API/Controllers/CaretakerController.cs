using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParentCare.Model;
using ParentCare.Model.Medications.Commands;
using ParentCare.API.Helpers;
using ParentCare.Model.Medications;
using Kuvio.Kernel.Core;
using ParentCare.Model.Users;
using System.Linq.Dynamic.Core;

namespace ParentCare.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaretakerController : ControllerBase
    {
        private readonly ILogger<CaretakerController> _logger;

        public CaretakerController(ILogger<CaretakerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Caretaker Get([FromServices] IRepository<Caretaker> caretakerRepository)
        {
            var caretaker = caretakerRepository.Query.Where(x => x.ElderId == User.ID()).FirstOrDefault();
            return caretaker;
        }

        [HttpPost]
        public async Task<Caretaker> Post([FromServices] IRepository<Caretaker> caretakerRepository, [FromBody] Caretaker model)
        {
            model.ElderId = User.ID();
            model.CreateDateUtc = DateTime.UtcNow;

            await caretakerRepository.AddAsync(model);
            await caretakerRepository.CommitAsync();
            return model;
        }

        [HttpPost]
        public async Task<Caretaker> Put([FromServices] IRepository<Caretaker> caretakerRepository, [FromBody] Caretaker model)
        {
            model.ElderId = User.ID();
            model.CreateDateUtc = DateTime.UtcNow;

            await caretakerRepository.UpdateAsync(model);
            await caretakerRepository.CommitAsync();
            return model;
        }

        [HttpDelete]
        public async Task<MedicationAlert> Delete([FromServices] IRepository<MedicationAlert> medicationRepository, int id)
        {
            var entity = await medicationRepository.FindAsync(id);
            await medicationRepository.DeleteAsync(entity);
            await medicationRepository.CommitAsync();
            return entity;
        }

        
    }

    
}
