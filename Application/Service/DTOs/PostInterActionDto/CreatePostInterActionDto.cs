using Domain.Models.Conasts;

namespace Application.Service.DTOs.PostInterActionDto;

public class CreatePostInterActionDto
{
    public string PostId { get; set; }
    public ReactionType ReactionType { get; set; }
    public string CreatedById { get; private set; }

}
