using AutoMapper;
using CarSale.Data.Entities;
using CarSale.Models;

namespace CarSale.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddCarRequestModel, Car>();
            CreateMap<AddCarFeatureRequestModel, CarFeature>();
        }
    }
}
