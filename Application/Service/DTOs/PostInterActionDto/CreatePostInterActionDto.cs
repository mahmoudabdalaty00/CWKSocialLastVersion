using Domain.Models.Conasts;

namespace Application.Service.DTOs.PostInterActionDto;

public class CreatePostInterActionDto
{
    public int PostId { get; set; }
    public ReactionType ReactionType { get; set; }
    public Guid UserProfileId { get; private set; }

}
