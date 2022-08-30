using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]  // Controller Atribute
    [Route("api/cities")]
    public class CitiesController:ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;

        public CitiesController(CitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(_citiesDataStore.Cities);
        }


        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            // find a city
            var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();   
            }

            return Ok(cityToReturn);   // Include a 200 status

           
        }

    }
}
