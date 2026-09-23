using Microsoft.Extensions.Configuration;

namespace Data.Service
{
    public static class Config
    {
        private static IConfiguration _configuration;

        static Config()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            UpdateProperties(Env);
            LoadGoogleOAuthSettings();
        }

        public static SysEnvironment Env = SysEnvironment.Local;

        public static string? LocalDefaultConnectionString { get; set; }
        public static int CodeDurationInSeconds { get; set; } = 30;
        public static double VisitorValidityDays { get; set; } = 1d;

        public static string FcmKeyFile { get; set; }
        public static string FcmProjectName { get; set; }
        public static string FcmSenderId { get; set; }

        public static string DatabaseNameOnMongo { get; set; }
        public static string WebHook_Build_Front { get; set; }
        public static string Placeholder { get; set; } = "images/front/placeholder.png";
        public static string AssetsDomain { get; set; }
        public static string AdminUrl { get; set; }
        public static string ImageflowS3Key { get; set; } = "/assets/";
        public static string ImageResizerAdmin { get; set; } = "?w=100&h=100&scale=both&mode=pad";
        public static string ImageResizerBox { get; set; } = "?w=600&h=600&scale=both&mode=pad";

        public static string ImageResizerBox_325 { get; set; } =
            "?w=325&h=325&scale=both&mode=pad&scale=both&mode=crop&bgcolor=white";

        public static string ImageResizerBanner { get; set; } = "?w=1920&scale=both&mode=pad";
        public static string VideoM3U8AssetURL { get; set; } = "https://d1sxr8c80s8tjg.cloudfront.net";
        public static string VideoM3U8AssetURL_Square { get; set; } = "https://d1sxr8c80s8tjg.cloudfront.net";
        public static string VideoM3U8AssetURL_Portrait { get; set; } = "https://d2bu9sklwzlyv0.cloudfront.net";
        public static string VideoM3U8AssetURL_Landscape { get; set; } = "https://d2g5mjyakwoov1.cloudfront.net";

        public static string PictureBaseURL { get; set; }
        public static string MP4 { get; set; } = ".mp4";
        public static string M3U8 { get; set; } = ".m3u8";
        public static string Thumb { get; set; } = "_thumb.0000000.jpg";
        public static string BaseURL { get; set; } = "https://.com";
        public static string DeletionRequestURL { get; set; } = $"{BaseURL}/en/account/fb/deletion?code=";
        public static bool UseServerMessage { get; set; } = true; //used for unifonic same server request

        public static string UseServerSecret { get; set; } =
            "971b93a44a364952a8d7743f3b5e7a9fsdawf"; //used for unifonic same server request

        #region ticketmx

        public static string TicketMXClientId { get; set; } = "971b93a44a364952a8d7743f3b5e7a9f";

        public static string TicketMXClientToken { get; set; } =
            "ff014129 - 0fcc-4580-b4bf-5c4cd759fca9159e2c76-ca54-4819-849b-6109c89c1083";

        public static string TicketMX_XAPIKey { get; set; } = "g6g9XsvMuM5pLGN4mO8tn1aakAKLgjAR7dXqBD8v";

        #endregion

        //paytabs
        public static string SuccessURL { get; set; } = "/Booking/PaymentDone/";
        public static string IPNDomain { get; set; }
        public static string PTPayUrl { get; set; }
        public static string PTCurrency { get; set; }
        public static string PayTabsProfileId { get; set; }
        public static string PayTabsServerKey { get; set; }
        public static string CallbackUrl { get; set; }
        public static string IpnKey { get; set; } = "a0faab2a-6feb-416e-88e8-48bb6855c100";
        public static string PTAddress { get; set; } = "address street";
        public static string PTState { get; set; } = "01";
        public static string PTZip { get; set; } = "12345";
        public static string PTCountry { get; set; } = "SA";
        public static string PTMobile { get; set; } = "0522222222";
        public static string PTIP { get; set; } = "1.1.1.1";
        public static string ReturnUrl { get; set; } = $"{SuccessURL}";
        public static string WebViewReturnUrl { get; set; } = $"/Booking/WebViewPaymentDone/";
        public static string WebViewReturnUrlParam { get; set; } = "&WebView=true";
        //public static RegionEndpoint RegionEndpoint { get; set; } = RegionEndpoint.EUCentral1;
        public static string AdminUserId { get; set; } = "53330dc2-2b27-4f30-8676-88d43c6d48b8";
        public static string Domain { get; set; }
        public static string TicketMX_Domain { get; set; } = "ticketmx.com";
        public static string TicketMX_API { get; set; }
        public static string API { get; set; }
        public static string API_SSO { get; set; }
        public static string Website { get; set; }
        public static string Schema { get; set; } = "https://";
        public static string JWTWebIssuer { get; set; }
        public static string JWTWebAudience { get; set; }
        public static string JWTWebKey { get; set; }
        public static string JWTTicketMXWebIssuer { get; set; }
        public static string JWTTicketMXWebAudience { get; set; }
        public static string JWTTicketMXWebKey { get; set; }
        public static string AppleSandboxSubscriptionURL { get; set; } = "api.storekit-sandbox.itunes.apple.com";
        public static string AppleSandboxVerifyReceiptURL { get; set; } = "sandbox.itunes.apple.com";
        public static string AppleSubscriptionURL { get; set; } = "api.storekit.itunes.apple.com";
        public static string AppleVerifyReceiptURL { get; set; } = "buy.itunes.apple.com";
        public static string AppleVerifyReceiptPassword { get; set; } = "";
        public static bool AppleVerifyReceiptExcludeOldTransactions { get; set; } = true;
        public static string AppleBundleId { get; set; } = "sa.sela.";
        public static string Write_DefaultConnection { get; set; }
        public static string Read_DefaultConnection { get; set; }
        public static string? DocumentDbDefaultConnection { get; set; }
        public static int AbsoluteExpirationSecond { get; set; }
        public static int SlidingExpirationSecond { get; set; }
        public static int AbsoluteExpirationMinute { get; set; }
        public static int SlidingExpirationMinute { get; set; }
        public static int YearMin { get; set; }
        public static int MonthMin { get; set; }
        public const string UnifonicUrl = "test";
        public const int SaudiTimeZone = 3;
        public static bool TestMode = false;

        //Moyassar Setting
        public const string IFrameCookie = "_sh";
        public const string MoyasarDefaultCountry = "SA";
        public const string MoyasarTransactionType = "payment_paid";
        public const string MoyasarCurrency = "SAR";
        public const string PaymentTransaction = "payment_paid";
        public const string MoyassarPaid = "paid";


        public static int DefaultWaitingCartValidityMinutes = 360;
        public static int PayMarginMinutes = 15;
        public static string MoyasarUrl { get; set; }
        public static string MoyasarAppleValidateUrl { get; set; }

        #region Wala Plus

        public static string XClientCode { get; } = "sela";
        public static string WalaPlusPrivateKeyPassword { get; } = "wala@sela_prod";
        public static string WalaPlusBaseUrl { get; } = "https://b2b-dt.walaplus.com";
        public static string UnblockEndpoint { get; } = "/v1/offer/unblock";
        public static string RetailersEndpoint { get; } = "/v1/retailers";
        public static string CategoryEndpoint { get; } = "/v1/categories";
        public static string RegisterEndpoint { get; } = "/v1/partners/users";

        #endregion


        #region Google settings

        public static string Google_RevokeURL { get; set; } = "https://oauth2.googleapis.com/revoke";

        public static string Google_ClientId { get; set; }

        public static string Google_ClientSecret { get; set; }
        public static string Google_Map_API_KEY { get; set; }

        #endregion

        #region Apple settings

        public static string Apple_RevokeURL { get; set; } = "https://appleid.apple.com/auth/revoke";
        public static string Apple_AUD { get; set; } = "https://appleid.apple.com";
        public static string Apple_ISS { get; set; } = "S7R5BE6E92";
        public static string Apple_KID { get; set; } = "9BZN3PB95K";
        public static string Apple_ALG { get; set; } = "ES256";

        public static string Apple_TokenKey { get; set; } =
            "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQg4NNXWYWxK3mpvZT0EmyTznKtpXOahOAmayWUQA7oXZ+gCgYIKoZIzj0DAQehRANCAASb5IvaOMTElovlKnFGfrons+fsQIHHyWjv7iI0Ks2mLiOLDWGDWPsYwS9EBdQpf9NJQKOc0Vq0iTRQVy26yVnq";

        public static string Apple_TokenURL { get; set; } = "https://appleid.apple.com/auth/token";
        public static string Apple_Authorization_Endpoint { get; set; } = "https://appleid.apple.com/auth/authorize";
        public static string Apple_JWKS_URI { get; set; } = "https://appleid.apple.com/auth/keys";

        public static string Apple_RedirectURL { get; set; } =
            "intent://callback?{0}#Intent;package={1};scheme=signinwithapple;end";

        public static string PackageName_IOS { get; set; } = "sa.sela.saudievents";
        public static string PackageName_Android { get; set; } = "sa.sela.saudieventsweb";

        #endregion

        #region Facebook settings

        public static string Facebook_AppId { get; set; } = "945373523038943";
        public static string Facebook_AppSecret { get; set; } = "AppSecret";

        public static string Facebook_RevokeURL { get; set; } =
            "https://graph.facebook.com/{0}/permissions?access_token={1}";

        #endregion


        #region AppleWallet

        public static string PassbookIdentifier { get; set; } = "pass.sa.sela.superapp";
        public static string PassbookTeamId { get; set; } = "S7R5BE6E92";
        public static string PassbookOrganizationName { get; set; } = "Sela Sport Company";
        public static string PassbookCertPass { get; set; } = "spark123";

        #endregion

        #region WarehouseSetting

        public static string WmsUrl { get; set; }
        public static string ProcurementUrl { get; set; }
        public static string LogisticUrl { get; set; }

        #endregion

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
                    API = $"https://api.{Domain}{ImageflowS3Key}";
                    API_SSO = $"https://api.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";
                    IPNDomain = API;

                    break;

                case SysEnvironment.Stg:

                    Env = SysEnvironment.Stg;


                    Domain = "CWKSocial.com";
                    TicketMX_API = $"https://devapi.{TicketMX_Domain}";
                    Website = "stg.CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d639";
                    API = $"https://api-stg.{Domain}{ImageflowS3Key}";
                    API_SSO = $"https://api-stg.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";

                    BaseURL = "https://localhost:7169";
                    DeletionRequestURL = $"{BaseURL}/en/account/fb/deletion?code=";
                    DatabaseNameOnMongo = "CWKSocialStg";

                    TestMode = true;

                    
                    ProcurementUrl = "";
 
                    break;


                case SysEnvironment.Development:
               
                    Env = SysEnvironment.Development;
                  
                    Domain = "CWKSocial.com";
                    TicketMX_API = $"https://devapi.{TicketMX_Domain}";
                    Website = "dev-Ims.CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d321";
                    JWTTicketMXWebIssuer = Website;
                    JWTTicketMXWebAudience = Website;
                    JWTTicketMXWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d321";
                    API = $"https://api-dev.{Domain}{ImageflowS3Key}";
                    API_SSO = $"https://api-dev.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";
                    
                    BaseURL = "https://localhost:7169";
                   
 
                    break;

                case SysEnvironment.Local:
                
                    Env = SysEnvironment.Local;
                 
                    Domain = "CWKSocial.com";
                    TicketMX_API = $"https://devapi.{TicketMX_Domain}";
                    Website = "dev.CWKSocial.com";
                    JWTWebIssuer = Website;
                    JWTWebAudience = Website;
                    JWTWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d321";
                    JWTTicketMXWebIssuer = Website;
                    JWTTicketMXWebAudience = Website;
                    JWTTicketMXWebKey = "29453559b5dbaV61f4V9ac335f9e89d8d321";
                    API = $"https://api-dev.{Domain}{ImageflowS3Key}";
                    API_SSO = $"https://api-dev.{Domain}";
                    PictureBaseURL = $"{AssetsDomain}/";
                    PayTabsServerKey = "SWJNWRTWN6-J2GKD2DLG6-DMZMZK2DDH";
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

        private static void LoadGoogleOAuthSettings()
        {
            try
            {
                var googleSettings = _configuration.GetSection("GoogleOAuth");
                Google_ClientId = googleSettings["ClientId"] ?? Google_ClientId;
                Google_ClientSecret = googleSettings["ClientSecret"] ?? Google_ClientSecret;
                Google_Map_API_KEY = googleSettings["MapApiKey"] ?? Google_Map_API_KEY;
            }
            catch
            {
                // If configuration loading fails, use defaults (empty strings)
                Google_ClientId = Google_ClientId ?? string.Empty;
                Google_ClientSecret = Google_ClientSecret ?? string.Empty;
                Google_Map_API_KEY = Google_Map_API_KEY ?? string.Empty;
            }
        }


        public static string? GetWebViewRoute(bool? webView = false)
        {
            return webView != null && webView.Value ? WebViewReturnUrlParam : "";
        }


        public static string? GetWebViewReturn(string language, bool? webView = false)
        {
            return webView != null && webView.Value
                ? $"https://{Website}/{language}{WebViewReturnUrl}"
                : $"https://{Website}/{language}{ReturnUrl}";
        }


        public static string? GetPictureBaseUrl(bool? apiAssets = false)
        {
            return apiAssets != null && apiAssets.Value ? PictureBaseURL : ImageflowS3Key;
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

        #region Video M3U8
        //public static string GetVideoM3U8AssetURL(Ratio? ration)
        //{
        //    return ration switch
        //    {
        //        Ratio.Square => VideoM3U8AssetURL_Square,
        //        Ratio.Portrait => VideoM3U8AssetURL_Portrait,
        //        Ratio.Landscape => VideoM3U8AssetURL_Landscape,
        //        _ => throw new ArgumentException("Invalid video ratio")
        //    };
        //}

        //public static async Task<bool> PutObject(IFormFile file, string fileName, string bucket)
        //{
        //    try
        //    {
        //        // connecting to the client
        //        var client = new AmazonS3Client(RegionEndpoint);
        //        //  var client = new AmazonS3Client(AWSAccessKey, AWSSecretKey, RegionEndpoint);


        //        // get the file and convert it to the byte[]
        //        byte[] fileBytes = new Byte[file.Length];
        //        file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

        //        // create unique file name for prevent the mess
        //        //  var fileName = Guid.NewGuid() + file.FileName;

        //        PutObjectResponse? response = null;
        //        using (var stream = new MemoryStream(fileBytes))
        //        {
        //            var request = new PutObjectRequest
        //            {
        //                BucketName = bucket,
        //                Key = fileName,
        //                InputStream = stream,
        //                ContentType = file.ContentType,
        //                CannedACL = S3CannedACL.PublicRead,
        //            };

        //            response = await client.PutObjectAsync(request);
        //        }

        //        ;

        //        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            // this model is up to you, in my case I have to use it following;
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return false;
        //}

        //public static async Task<byte[]> GetCertificateAsync(string fileName, string bucket)
        //{
        //    using var response = await GetObject(fileName, bucket);
        //    return response.ToArray();
        //}

        //public static string ConvertToBase64(X509Certificate2 certificate)
        //{
        //    return Convert.ToBase64String(certificate.Export(X509ContentType.Pfx));
        //}

        //public static async Task<MemoryStream> GetObject(string fileName, string bucket)
        //{
        //    using (var client = new AmazonS3Client(RegionEndpoint))
        //        // using (var client = new AmazonS3Client(AWSAccessKey, AWSSecretKey, RegionEndpoint))
        //    {
        //        try
        //        {
        //            GetObjectRequest request = new GetObjectRequest
        //            {
        //                BucketName = bucket,
        //                Key = fileName
        //            };
        //            using (GetObjectResponse response = await client.GetObjectAsync(request))
        //            using (var responseStream = response.ResponseStream)
        //            {
        //                var stream = new MemoryStream();
        //                await responseStream.CopyToAsync(stream);
        //                stream.Position = 0;
        //                return stream;
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            throw new Exception("Read object operation failed.", exception);
        //        }
        //    }
        //}
        #endregion

    }

    public class ImageResponse
    {
        public bool IsSuccess { get; set; }
        public string? Link { get; set; }
        public string? FileName { get; set; }
        public List<string>? Message { get; set; }
    }
}