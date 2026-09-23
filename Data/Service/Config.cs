using Microsoft.Extensions.Configuration;

namespace Data.Service
{
    public static class Config
    {
        static Config()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            UpdateProperties(Env);
        }

        public static SysEnvironment Env = SysEnvironment.Local;

        public static string? LocalDefaultConnectionString { get; set; }
        public static int CodeDurationInSeconds { get; set; } = 30;
        public static double VisitorValidityDays { get; set; } = 1d;

        public static string DatabaseNameOnMongo { get; set; }
        public static string WebHook_Build_Front { get; set; }
        public static string Placeholder { get; set; } = "images/front/placeholder.png";
        public static string AssetsDomain { get; set; }
        public static string AdminUrl { get; set; }

        public static string ImageResizerAdmin { get; set; } = "?w=100&h=100&scale=both&mode=pad";
        public static string ImageResizerBox { get; set; } = "?w=600&h=600&scale=both&mode=pad";
        public static string ImageResizerBox_325 { get; set; } =
            "?w=325&h=325&scale=both&mode=pad&scale=both&mode=crop&bgcolor=white";
        public static string ImageResizerBanner { get; set; } = "?w=1920&scale=both&mode=pad";

        public static string PictureBaseURL { get; set; }
        public static string MP4 { get; set; } = ".mp4";
        public static string M3U8 { get; set; } = ".m3u8";
        public static string Thumb { get; set; } = "_thumb.0000000.jpg";
        public static string BaseURL { get; set; } = "https://.com";
        public static string DeletionRequestURL { get; set; } = $"{BaseURL}/en/account/fb/deletion?code=";

        public static string AdminUserId { get; set; } = "53330dc2-2b27-4f30-8676-88d43c6d48b8";
        public static string Domain { get; set; }
        public static string API { get; set; }
        public static string API_SSO { get; set; }
        public static string Website { get; set; }
        public static string Schema { get; set; } = "https://";
        public static string JWTWebIssuer { get; set; }
        public static string JWTWebAudience { get; set; }
        public static string JWTWebKey { get; set; }

        public static string Write_DefaultConnection { get; set; }
        public static string Read_DefaultConnection { get; set; }
        public static string? DocumentDbDefaultConnection { get; set; }
        public static int AbsoluteExpirationSecond { get; set; }
        public static int SlidingExpirationSecond { get; set; }
        public static int AbsoluteExpirationMinute { get; set; }
        public static int SlidingExpirationMinute { get; set; }
        public static int YearMin { get; set; }
        public static int MonthMin { get; set; }
        public const int SaudiTimeZone = 3;
        public static bool TestMode = false;

        private static void UpdateProperties(SysEnvironment env)
        {
            switch (env)
            {
                case SysEnvironment.Production:

                    Env = SysEnvironment.Production;
                    Domain = "CWKSocial.com";
                    Website = "CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "cqd58yu2ckd15upknzqrrhbelvgmups23f4h71";
                    API = $"https://api.{Domain}/assets/";
                    API_SSO = $"https://api.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";

                    break;

                case SysEnvironment.Stg:

                    Env = SysEnvironment.Stg;
                    Domain = "CWKSocial.com";
                    Website = "stg.CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d639";
                    API = $"https://api-stg.{Domain}/assets/";
                    API_SSO = $"https://api-stg.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";

                    BaseURL = "https://localhost:7169";
                    DeletionRequestURL = $"{BaseURL}/en/account/fb/deletion?code=";
                    DatabaseNameOnMongo = "CWKSocialStg";

                    TestMode = true;

                    break;


                case SysEnvironment.Development:

                    Env = SysEnvironment.Development;

                    Domain = "CWKSocial.com";
                    Website = "dev-Ims.CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d321";
                    API = $"https://api-dev.{Domain}/assets/";
                    API_SSO = $"https://api-dev.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";

                    BaseURL = "https://localhost:7169";


                    break;

                case SysEnvironment.Local:

                    Env = SysEnvironment.Local;

                    Domain = "CWKSocial.com";
                    Website = "dev.CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d321";
                    API = $"https://api-dev.{Domain}/assets/";
                    API_SSO = $"https://api-dev.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";
                    YearMin = 60;
                    MonthMin = 5;
                    BaseURL = "https://localhost:7169";
                    AdminUrl = "https://localhost:7169";


                    ////Mahmoud Local connection String
                    ////DatabaseName = selalocal
                    ////Password  = mahmoud
                    Write_DefaultConnection =
                        "Server=localhost:5432;User Id=postgres;Password=mahmoud;Database=SelaLocal";
                    Read_DefaultConnection =
                        "Server=localhost:5432;User Id=postgres;Password=mahmoud;Database=SelaLocal";



                    break;
            }
        }


        public static string? GetWebViewRoute(bool? webView = false)
        {
            return webView != null && webView.Value ? "&WebView=true" : "";
        }


        public static string? GetWebViewReturn(string language, bool? webView = false)
        {
            return webView != null && webView.Value
                ? $"https://{Website}/{language}/Booking/WebViewPaymentDone/"
                : $"https://{Website}/{language}/Booking/PaymentDone/";
        }


        public static string? GetPictureBaseUrl(bool? apiAssets = false)
        {
            return apiAssets != null && apiAssets.Value ? PictureBaseURL : "/assets/";
        }

        public static string? GetURL(string AssetUrl, string Video, string? videoAssets = null, string? ex = null)
        {
            string? src = null;
            if (!string.IsNullOrWhiteSpace(Video))
            {
                if (!Video.EndsWith(".mp4"))
                {
                    src = Video != null ? videoAssets + "/" + Video + ex : null;
                }
                else
                {
                    src = Video != null ? AssetUrl + "/" + Video : null;
                }
            }

            return src;
        }

    }
}
