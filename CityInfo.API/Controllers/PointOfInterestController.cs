using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]
    [ApiController]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,
            IMailService mailService,
            CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }


        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId)
        {
            try
            {
                //throw new Exception("Exception sample.");

                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing of interest.");
                    return NotFound();
                }

                return Ok(city.PointOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.",
                    ex);
                return StatusCode(500, "A problem happend while handling your request.");
            }
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
            int cityId, int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            // Find point of interest
            var pointOfInterest = city.PointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);

        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInteres(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)   // This parameter is [FromBody]
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            // just for demo proposes
            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
                        c => c.PointOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterests.Add(finalPointOfInterest);

            // Returns the URI to the recent inserted data
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = finalPointOfInterest.Id

                },
                finalPointOfInterest);

        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInteres(
            int cityId,
            int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();

        }

        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(
            int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> pacthDocument)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }


            var pointOfInterestToPacth =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };

            pacthDocument.ApplyTo(pointOfInterestToPacth, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPacth))
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPacth.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPacth.Description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(
            int cityId,
            int pointOfInterestId)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointOfInterests.Remove(pointOfInterestFromStore);
            _mailService.Send(
                "Point of interest deleted.",
                $"Point of interest {pointOfInterestFromStore.Name} with id " +
                $"{pointOfInterestFromStore.Id} was deleted.");

            return NoContent();
        }

    }
}
