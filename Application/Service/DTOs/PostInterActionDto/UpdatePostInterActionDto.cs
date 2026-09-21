using Domain.Models.Conasts;

namespace Application.Service.DTOs.PostInterActionDto;

public class UpdatePostInterActionDto
{
    public ReactionType ReactionType { get; set; }
    public string PostId { get; set; }
}
