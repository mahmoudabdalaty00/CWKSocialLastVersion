using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
namespace Domain.Models.Posts
{
    public class PostInterAction : BaseEntity<int>
    {
        private PostInterAction()
        {

        }

        public int PostId { get; private set; }
        public ReactionType ReactionType { get; private set; }



        public static PostInterAction Create(int postId, ReactionType reaction)
        {
            {
                return new PostInterAction
                {
                    PostId = postId,
                    ReactionType = reaction,
                };
            }
        }


        public static void Update(PostInterAction interaction, ReactionType reaction)
        {
            interaction.ReactionType = reaction;
            interaction.UpdatedAt = DateTime.UtcNow;
        }

        public static PostInterAction Update(ReactionType reaction)
        {
            return new PostInterAction
            {
                ReactionType = reaction,
            };
        }


         

    }
}
