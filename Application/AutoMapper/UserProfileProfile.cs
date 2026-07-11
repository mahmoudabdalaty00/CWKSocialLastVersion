using Application.UserProfiles.Commands;
using AutoMapper;
using Domain.Models.UserProfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AutoMapper
{
    public class UserProfileProfile :Profile
    {

        public UserProfileProfile()
        {
            CreateMap<CreateUserProfileCommand, BasicInfo>();
            CreateMap<UpdateUserProfileCommand, BasicInfo>();





        }


    }
}
