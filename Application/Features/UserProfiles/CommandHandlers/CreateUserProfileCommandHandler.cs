using Application.Features.UserProfiles.Commands;
using AutoMapper;
using Data.MainDb;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, UserProfile>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserProfileCommandHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var basicInfo = BasicInfo.Create(
                request.FirstName, request.LastName,
                        request.DateOfBirth, request.Bio,
                        request.Phone, request.EmailAddress, request.CurrentCity);


            var userProfile = UserProfile.Create(Guid.NewGuid().ToString(), basicInfo);

            _dbContext.UserProfiles.Add(userProfile);
            await _dbContext.SaveChangesAsync();

            return userProfile;
        }
    }
}
