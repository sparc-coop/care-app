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
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<MedicationAlert> Post([FromServices] CreateMedicationAlertCommand medicationAlertCommand, [FromBody] MedicationAlertModel model)
        {
            var entity = await medicationAlertCommand.Execute(User.Id(), model);
            return entity;
        }

        [HttpPut]
        public async Task<MedicationAlert> TakeMedication([FromServices] TakeMedicationCommand takeMedicationCommand, [FromBody] TakeMedicationModel model)
        {
            var entity = await takeMedicationCommand.Execute(User.Id(), model);
            return entity;
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
