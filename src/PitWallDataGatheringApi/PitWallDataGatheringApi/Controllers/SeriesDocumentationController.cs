using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;
using PitWallDataGatheringApi.Repositories.Tyres;
using PitWallDataGatheringApi.Repositories.WeatherConditions;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesDocumentationController : ControllerBase
    {
        private IDocumentationLaptimeSerie _laptimeSerie;
        private IDocumentationTyresWearSerie _tyresWearSerie;
        private IDocumentationTyresTemperaturesSerie _temperatureSerie;
        private IDocumentationAvgWetnessSerie _avgRoadWetnessSerie;
        private IDocumentationAirTemperatureSerie _airTempSerie;

        public SeriesDocumentationController(
            IDocumentationLaptimeSerie laptimeSerie,
            IDocumentationTyresWearSerie tyresWearSerie,
            IDocumentationTyresTemperaturesSerie tyresTemperaturesSerie,
            IDocumentationAvgWetnessSerie avgWetnessSerie,
            IDocumentationAirTemperatureSerie airTempSerie)
        {
            _laptimeSerie = laptimeSerie;
            _tyresWearSerie = tyresWearSerie;
            _temperatureSerie = tyresTemperaturesSerie;
            _avgRoadWetnessSerie = avgWetnessSerie;
            _airTempSerie = airTempSerie;
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
                },

                AvgRoadWetness = new OneSerieDocumentation()
                {
                    Name = _avgRoadWetnessSerie.SerieName,
                    Description = _avgRoadWetnessSerie.Description,
                    Labels = _avgRoadWetnessSerie.Labels
                },

                AirTemperature = new OneSerieDocumentation()
                {
                    Name = _airTempSerie.SerieName,
                    Description = _airTempSerie.Description,
                    Labels = _airTempSerie.Labels
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            var json = JsonConvert.SerializeObject(doc, settings);

            return Ok(json);
        }
    }
}
