using Application.UserProfiles.Commands;
using Data.MainDb;
using MediatR;

namespace Application.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, Unit>
    {
        private readonly DataContext _db;

        public DeleteUserProfileCommandHandler(DataContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _db.UserProfiles.FirstOrDefault(x => x.Id == request.Id);

            if(profile == null) 
                return Unit.Value;


            profile.IsDeleted = true;
            profile.DeletedAt = DateTime.UtcNow;

            _db.UserProfiles.Update(profile);
            await  _db.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
