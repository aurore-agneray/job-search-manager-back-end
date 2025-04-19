using AutoMapper;
using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.Configuration;

/// <summary>
/// Defines the profile used for configuring the AutoMapper package into the Program.cs file
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Main constructor of the class AutoMapperProfile
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<JobApplication, JobApplicationGetDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Guid.ToString()))
            .ForMember(
                dest => dest.Date,
                opt =>
                    opt.MapFrom(src =>
                        src.Date.HasValue ? src.Date.Value.ToString("yyyy-MM-dd") : string.Empty
                    )
            )
            .ForMember(
                dest => dest.StatusId,
                opt => opt.MapFrom(src => src.Status.Guid.ToString())
            );

        CreateMap<Status, StatusGetDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Guid.ToString()));
    }
}
