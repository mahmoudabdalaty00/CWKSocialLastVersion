using Application.Models;
using Domain.Models.UserProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.UserProfiles.Queries
{
    public class GetAllUserProfilesQuery:IRequest<OperationResult<IEnumerable<UserProfile>>>
    {

    }
}
