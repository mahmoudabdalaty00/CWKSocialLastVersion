using Domain.Models.Conasts;

namespace Application.Service.DTOs.PostInterActionDto;

public class PostInterActionResponseDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public ReactionType ReactionType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
