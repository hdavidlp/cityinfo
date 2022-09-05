using AutoMapper;
using CityInfo.API.Models;
using CityInfo.Data.Entities;
namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile: Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<PointOfInterestForCreationDto, >
        }
    }
}
