using AutoMapper;
using CityInfo.Data.Entities;


namespace CityInfo.API.Profiles
{
    public class CityProfile: Profile
    {
        public CityProfile()
        {
            CreateMap<City, Models.CityWithoutPointsOfInterestDto>();
            CreateMap<City, Models.CityDto>();
        }
    }
}
