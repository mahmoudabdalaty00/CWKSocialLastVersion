using Domain.Models.Conasts;

namespace Application.DTOs.PostInterActionDto;

public class PostInterActionResponseDto
{
    public int Id { get; set; }
    public string PostId { get; set; }
    public ReactionType ReactionType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
