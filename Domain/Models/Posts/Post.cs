using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Posts
{
    public class Post : BaseEntity<Guid>
    {
        public string Content { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public  PostType PostType { get; set; } = PostType.Text;
    
        public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;

        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }



        public Guid UserId { get; set; }
        public User


    }
}
