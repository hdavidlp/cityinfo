using CityInfo.Data.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        // Original use
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();
        // it change for Injection dependency

        public CitiesDataStore()
        {
            // initiliza data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York",
                    Description = "The one with that big park",
                    PointOfInterests = new List<PointOfInterestDto> (){
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description="The mos visited urban park in US"
                        },
                        new PointOfInterestDto()
                        {
                               Id=2,
                               Name = "Emire State Building",
                               Description = "A 102-story ...."

                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name = "Antwerp",
                    Description="The one with the cathedral that was never really ..",
                    PointOfInterests= new List<PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id=3,
                            Name="Cathedral never finished",
                            Description="Never finished cathedral"
                        },
                        new PointOfInterestDto()
                        {
                            Id=4,
                            Name = "Another Part",
                            Description = "Is very nice place"
                        }
                    }
                },
                new CityDto()
                {
                    Id=3,
                    Name="Paris",
                    Description="The one with that big tower",
                    PointOfInterests =  new List <PointOfInterestDto> ()
                    {
                        new PointOfInterestDto()
                        {
                            Id=5,
                            Name="Eiffel Tower",
                            Description = "The iron tower"
                        },
                        new PointOfInterestDto()
                        {
                            Id=6,
                            Name="The Louvre",
                            Description="The world´s largest museum"
                        }
                    }
                    
                }
            };
        }
    }
}
