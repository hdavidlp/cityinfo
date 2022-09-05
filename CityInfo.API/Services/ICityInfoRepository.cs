using CityInfo.Data.Entities;
using System.Collections;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        
        Task<bool> CityExistsAsync(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsinc(int cityId);

        Task<PointOfInterest?> GetPointOfInterestForCityAsinc(int cityId, int pointOfInterestId);

    }
}
