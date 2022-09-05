﻿using CityInfo.Data.DbContexts;
using CityInfo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c=> c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest )
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointOfInterests)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsinc(
            int cityId, 
            int pointOfInterestId)
        {
            return await _context.PointsOfInteres
                .Where(p=> p.CityId == cityId && p.Id==pointOfInterestId)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsinc(int cityId)
        {
            return await _context.PointsOfInteres
                .Where(p => p.CityId == cityId).ToListAsync();
        }
    }
}