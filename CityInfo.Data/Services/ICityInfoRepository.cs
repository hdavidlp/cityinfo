using CityInfo.Data.Entities;
using System.Collections;

namespace CityInfo.Data.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        
        Task<bool> CityExistsAsync(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsinc(int cityId);

        Task<PointOfInterest?>GetPointOfInterestForCityAsinc(int cityId, int pointOfInterestId);

        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        Task<bool> SaveChangesAsync();

    }
}
