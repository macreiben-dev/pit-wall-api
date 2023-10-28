using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesDocumentationController : ControllerBase
    {
        private IDocumentationLaptimeSerie _laptimeSerie;
        private IDocumentationTyresWearSerie _tyresWearSerie;
        private IDocumentationTyresTemperaturesSerie _temperatureSerie;

        public SeriesDocumentationController(
            IDocumentationLaptimeSerie laptimeSerie,
            IDocumentationTyresWearSerie tyresWearSerie,
            IDocumentationTyresTemperaturesSerie tyresTemperaturesSerie)
        {
            _laptimeSerie = laptimeSerie;
            _tyresWearSerie = tyresWearSerie;
            _temperatureSerie = tyresTemperaturesSerie;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var doc = new SeriesDocumentation()
            {
                Laptimes = new OneSerieDocumentation()
                {
                    Name = _laptimeSerie.SerieName,
                    Description = _laptimeSerie.Description,
                    Labels = _laptimeSerie.Labels,
                    
                },

                TyresWear = new OneSerieDocumentation()
                {
                    Name = _tyresWearSerie.SerieName,
                    Description = _tyresWearSerie.Description,
                    Labels = _tyresWearSerie.Labels,
                },

                TyresTemperatures = new OneSerieDocumentation()
                {
                    Name = _temperatureSerie.SerieName,
                    Description = _temperatureSerie.Description,
                    Labels = _temperatureSerie.Labels,
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            var json = JsonConvert.SerializeObject(doc, settings);

            return Ok(json);
        }
    }
}
