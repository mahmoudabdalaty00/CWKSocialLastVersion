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
            public const string GetById = "{id}";
        }
    }
}
