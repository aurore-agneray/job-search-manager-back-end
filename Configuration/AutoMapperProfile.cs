using AutoMapper;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;

namespace JobSearchManagerBack.Configuration;

/// <summary>
/// Defines the profile used for configuring the AutoMapper package
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Main constructor of the class AutoMapperProfile
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<JobApplication, JobApplicationGetDTO>()
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
