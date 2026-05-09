using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Conasts
{
    public enum PostType
    {
        Text = 0,
        Image = 1,
        Video = 2,
        Link = 3,
    
    }

    public enum PrivacySetting
    {
        Public = 0,
        FriendsOnly = 1,
        Private = 2
    }

    public enum ReactionType
    {
        Like = 6,
        Love = 1,
        Haha = 2,
        Wow = 3,
        Sad = 4,
        Angry = 5,
        None = 0
    }

}
