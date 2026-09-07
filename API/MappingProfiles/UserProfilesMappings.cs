using API.Contracts.UserProfile.Requests;
using API.Contracts.UserProfile.Responses;
using Application.Features.UserProfiles.Commands;
using Application.Service.DTOs.UserProfileDto;
using AutoMapper;
using Domain.Models.UserProfiles;

namespace API.MappingProfiles
{
    public class UserProfilesMappings : Profile
    {
        public UserProfilesMappings()
        {
            CreateMap<UserProfileCreate, CreateUserProfileCommand>();
            CreateMap<UserProfileUpdate, UpdateUserProfileCommand>();

            // Map flat DTO from service layer → API response
            CreateMap<UserProfileResponseDto, UserProfileResponse>()
                .ForMember(dest => dest.BasicInfo, opt => opt.MapFrom(src => new BasicInformation
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    DateOfBirth = src.DateOfBirth,
                    Bio = src.Bio,
                    Phone = src.Phone,
                    EmailAddress = src.EmailAddress,
                    CurrentCity = src.CurrentCity
                }));

            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}
