using Application.UserProfiles.Commands;
using Data.MainDb;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand,Unit>
    {
        private readonly DataContext _db;

        public UpdateUserProfileCommandHandler(DataContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile =await _db.UserProfiles.FirstOrDefaultAsync(up =>
                     up.Id == request.Id);
            
               
            var basicInfo = BasicInfo.Create(
                request.FirstName, request.LastName,
                request.DateOfBirth,
                request.Bio,
                request.Phone,
                request.EmailAddress,
                request.CurrentCity);

            if (userProfile == null)
                return new Unit();

            userProfile.UpdateBasicInfo(basicInfo);

            userProfile.UpdatedAt = DateTime.UtcNow;
            _db.UserProfiles.Update(userProfile);
            await _db.SaveChangesAsync(cancellationToken);

            return new Unit();  
        }
    }
}
