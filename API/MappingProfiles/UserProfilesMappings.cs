using API.Contracts.UserProfile.Requests;
using API.Contracts.UserProfile.Responses;
using Application.Features.UserProfiles.Commands;
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
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();



        }
    }
}
