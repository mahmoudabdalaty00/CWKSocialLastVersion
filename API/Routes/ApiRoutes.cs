namespace API.Routes
{
    public class ApiRoutes
    {

        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public class UserProfiles
        {
            public const string IdRoute = "{id}";
        }




        public class Post
        {
            public const string GetById = "{id}";
        }
    }
}
