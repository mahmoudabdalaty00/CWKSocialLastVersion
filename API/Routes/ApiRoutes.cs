namespace API.Routes
{
    public class ApiRoutes
    {

        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public class UserProfiles
        {
            public const string IdRoute = "{id}";
            public const string CreateUserProfile = "create_profile";
            public const string UpdateUserProfile = "update_profile";
            public const string DeleteUserProfile = "delete_profile";
            public const string GetUserProfile = "get_user";
            public const string GetAllUsers = "get_all_users";
        }




        public class Post
        {
            public const string IdRoute = "{id}";
            public const string GetAll  = "get_all";
            public const string GetById = "get_post/{id}";
            public const string Create  = "create_post";
            public const string Update  = "update_post";
            public const string Delete  = "delete_post";
        }

        public class PostComment
        {
            public const string IdRoute   = "{id}";
            public const string GetByPost = "by_post/{postId}";
            public const string Create    = "create_postcomment";
            public const string Update    = "update_postcomment";
            public const string Delete    = "delete_postcomment";
        }

        public class PostInteraction
        {
            public const string IdRoute   = "{id}";
            public const string GetByPost = "by_post/{postId}";
            public const string Create    = "create_postinteraction";
            public const string Update    = "update_postinteraction";
            public const string Delete    = "delete_postinteraction";
        }
    }
}
