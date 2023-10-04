using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PitWallDataGatheringApi.Models.Apis;
using PitWallDataGatheringApi.Repositories;
using System.Text.Json.Serialization;

namespace PitWallDataGatheringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesDocumentationController : ControllerBase
    {
        private IDocumentationLaptimeSerie _laptimeSerie;
        private IDocumentationTyresWearSerie _tyresWearSerie;

        public SeriesDocumentationController(
            IDocumentationLaptimeSerie laptimeSerie,
            IDocumentationTyresWearSerie tyresWearSerie)
        {
            _laptimeSerie = laptimeSerie;
            _tyresWearSerie = tyresWearSerie;
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
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;

            var json = JsonConvert.SerializeObject(doc, settings);

            return Ok(json);
        }
    }
}
