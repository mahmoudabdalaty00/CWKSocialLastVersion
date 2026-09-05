using Application.Features.UserProfiles.Commands;
using AutoMapper;
using Domain.Models.UserProfiles;
using Application.Service.DTOs.UserProfileDto;

namespace Application.AutoMapper
{
    public class UserProfileProfile :Profile
    {

        public UserProfileProfile()
        {
            CreateMap<CreateUserProfileCommand, BasicInfo>();
            CreateMap<UpdateUserProfileCommand, BasicInfo>();


            CreateMap<UserProfile, UserProfileResponseDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.BasicInfo.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.BasicInfo.LastName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.BasicInfo.DateOfBirth))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.BasicInfo.Bio))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.BasicInfo.Phone))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.BasicInfo.EmailAddress))
                .ForMember(dest => dest.CurrentCity, opt => opt.MapFrom(src => src.BasicInfo.CurrentCity));

            // Map CreateUserProfileDto to UserProfile entity
            CreateMap<CreateUserProfileDto, UserProfile>()
                .ConstructUsing((dto, context) => UserProfile.Create(dto.IdentityUserId, 
                    BasicInfo.Create(dto.FirstName, dto.LastName, dto.DateOfBirth, 
                        dto.Bio, dto.Phone, dto.EmailAddress, dto.CurrentCity)))
                .ForAllMembers(opts => opts.Ignore()); // Ignore all members; construction handles mapping

            // Map UpdateUserProfileDto to BasicInfo
            CreateMap<UpdateUserProfileDto, BasicInfo>()
                .ConstructUsing((dto, context) => BasicInfo.Create(dto.FirstName, dto.LastName, 
                    dto.DateOfBirth, dto.Bio, dto.Phone, dto.EmailAddress, dto.CurrentCity))
                .ForAllMembers(opts => opts.Ignore());
        

        }


    }
}
