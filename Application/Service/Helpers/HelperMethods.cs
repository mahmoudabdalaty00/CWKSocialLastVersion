using System.Diagnostics;
using Data.Service;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Application.DTOs.Responses;



namespace Application.Service.Helpers;

public static class HelperMethods
{
    public static string CloudFrontKeyPairId_Mobile => "K1N1GECQXZT6EX";
    public static string AsignUrlDomain { get; set; } = "https://upload.selasuperapp.com";

    public static void AddModelStateError(ModelStateDictionary ModelState, IEnumerable<ErrorRequestViewModel> Errors)
    {
        foreach (var error in Errors)
            ModelState.AddModelError(error.Code, error.Description);
    }

    public static List<string> HiddenUsers()
    {
        return new List<string>()
        {
            "494b2239-5bd6-45e0-a2e4-b1b36ffb18ef",
            "114037f2-3913-4e71-a877-ec80c57fb0f8",
            "74e5ca29-0135-4d98-8160-2a51394c8cec",
            "9588be78-3363-4a78-84c2-f7d962815aec",
            "8546cd6d-126b-4ccd-a2ec-d23dff9fddfe",
            "834cdac4-cc29-45e3-8e5e-83d3abf9e313",
            "61f90e71-9d46-47ab-bb47-62000dae2bdc",
            "fa4973c0-54a9-4322-9f01-b19ea83cd530"
        };
    }

    public static List<string> HiddenEmails()
    {
        return new List<string>()
        {
            "hussien@spark-sys.com",
            "ezz@spark-sys.com",
            "ezz@sela.sa",
            "abtharwat@spark-sys.com",
            "marwa@spark-sys.com",
            "marwan@spark-sys.com",
            "abtharwat@sela.sa",
            "toka20@gmail.com",
            "toka@spark-sys.com",
            "moaz@spark-sys.com",
            "aya@spark-sys.com",
            "azakaria@spark-sys.com",
            "azakaria@sela.sa",
            "khalifa@spark-sys.com",
            "eslam@spark-sys.com",
            "maryam@spark-sys.com",
            "maryam@sela.sa",
            "walaplus@walaplus.com",
            "asalah@sela.sa",
            "zaki@sela.sa",
            "hussein@spark-sys.com",
            "system@spark-sys.com",
            "info@spark-sys.com",
            "mramadan@spark-sys.com",
            "ashebl@spark-sys.com",
            "myasser@spark-sys.com",
            "test@sela.sa",
            "zabdelsalam@spark-sys.com",
        };
    }

    public static string FixNumber(string number)
    {
        if (number.StartsWith("00"))
        {
            number = number.ReplaceFirst("00", "+");
        }

        else if (number.StartsWith("05"))
        {
            number = number.ReplaceFirst("05", "+9665");
        }
        else if (number.StartsWith("5"))
        {
            number = number.ReplaceFirst("5", "+9665");
        }
        else if (number.StartsWith("+"))
        {
            number = number;
        }
        else
        {
            number = "+" + number;
        }

        return number;
    }

    public static string FixNumberForUnifonic(string number)
    {
        number = number.Replace(" ", "");
        number = number.Replace("(", "");
        number = number.Replace(")", "");
        number = number.Replace("?", "0");
        number = number.Replace("?", "1");
        number = number.Replace("?", "2");
        number = number.Replace("?", "3");
        number = number.Replace("?", "4");
        number = number.Replace("?", "5");
        number = number.Replace("?", "6");
        number = number.Replace("?", "7");
        number = number.Replace("?", "8");
        number = number.Replace("?", "9");
        number = number.Replace("+", "");


        if (number.StartsWith("00"))
        {
            number = number.ReplaceFirst("00", "");
        }
        else if (number.StartsWith("05"))
        {
            number = number.ReplaceFirst("05", "9665");
        }
        else if (number.StartsWith("5") && number.Length <= 9)
        {
            number = number.ReplaceFirst("5", "9665");
        }

        return number;
    }

    public static string ReplaceFirst(this string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }

        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    public static string GenerateSignature(string passPhrase, Dictionary<string, string> data)
    {
        data.Remove("signature");
        var dataString = data.OrderBy(x => x.Key)
            .Select(x => $"{x.Key}={x.Value}")
            .Aggregate((x, y) => $"{x}{y}");

        var dataBytes = Encoding.UTF8.GetBytes($"{passPhrase}{dataString}{passPhrase}");
        var signature = SHA256.Create().ComputeHash(dataBytes);
        return BitConverter.ToString(signature).Replace("-", string.Empty).ToLower();
    }
    public static string GenerateRequestNumber()
    {
        var random = new Random();
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var numbers = random.Next(1000, 9999);
        var randomLetters = new string(Enumerable.Repeat(letters, 2)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return $"BTP_{randomLetters}{numbers}";
    }

    public static bool ValidateSignature(string signature, string passPhrase, Dictionary<string, string> data)
    {
        var generatedSignature = GenerateSignature(passPhrase, data);
        return signature == generatedSignature;
    }


    public static String GetHash(String text, String key)
    {
        // change according to your needs, an UTF8Encoding
        // could be more suitable in certain situations
        ASCIIEncoding encoding = new ASCIIEncoding();

        Byte[] textBytes = encoding.GetBytes(text);
        Byte[] keyBytes = encoding.GetBytes(key);

        Byte[] hashBytes;

        using (HMACSHA256 hash = new HMACSHA256(keyBytes))
            hashBytes = hash.ComputeHash(textBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    private static string FixBase64String(string str)
    {
        while (str.Length % 4 != 0)
        {
            str = str.PadRight(str.Length + 1, '=');
        }

        return str.Replace("-", "+").Replace("_", "/");
    }

    public static double CalucRating(int rateSum, int rateCount)
    {
        if (rateSum > 0 && rateCount > 0)
        {
            var ratingPercent = ((rateSum * 100) / rateCount) / 5;
            return (((double)ratingPercent / 100) * 5);
        }

        return 0;
    }

    public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
        return dictionary;
    }

    public static string SelectByLanguage(string language, string en, string ar, string fr)
    {
        if (language == "ar")
        {
            return ar;
        }
        else if (language == "en")
        {
            return en;
        }
        else if (language == "fr")
        {
            return fr;
        }
        else
        {
            return en;
        }
    }

    public static T GetJson<T>(string obj)
    {
        var dictionary = JsonConvert.DeserializeObject<T>(obj);
        return dictionary;
    }

    public static List<DateTime> GetWeekdayInRange(DateTime today, DateTime from, DateTime to,
        DayOfWeek day, int durationBeforeOrder, DateTime timeFrom, bool allDay)
    {
        const int daysInWeek = 7;
        var result = new List<DateTime>();
        TimeSpan time = timeFrom.TimeOfDay;
        DateTime dateTodayWithTime = today.Date.Add(time);
        var daysToAdd = ((int)day - (int)from.DayOfWeek + daysInWeek) % daysInWeek;

        do
        {
            from = from.AddDays(daysToAdd);
            if (from.Date <= to.Date)
            {
                if (from.Date == today.Date && (allDay || today <= dateTodayWithTime.AddHours(-durationBeforeOrder)))
                {
                    result.Add(today);
                }
                else if (from.Date != today.Date && from.Date >= today.Date &&
                         (allDay || today <= from.Date.Add(time).AddHours(-durationBeforeOrder)))
                {
                    result.Add(from);
                }

                daysToAdd = daysInWeek;
            }
        } while (from.Date < to.Date);

        return result;
    }

    public static string RNGCharacterMask(string? existKey = null)
    {
        if (!string.IsNullOrEmpty(existKey))
        {
            return existKey;
        }

        int maxSize = 15;
        char[] chars = new char[62];
        string a;
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length - 1)]);
        }

        return result.ToString();
    }

    public static Func<List<int>, bool> MatchRate(int? rate = 0)
    {
        return e => (Math.Round(CalucRating(e.Sum(), e.Count)) == rate);
    }

    public static decimal CalculatePriceWithTax(this decimal price, int tax)
    {
        double taxRate = (double)tax / 100;
        var res = decimal.Round(price + (price * (decimal)taxRate), 2, MidpointRounding.AwayFromZero);
        return res;
    }

    public static decimal CalculateTaxFromPriceWithTax(this decimal priceWithTax, int tax)
    {
        decimal taxRate = (decimal)tax / 100;
        var res = decimal.Round(priceWithTax - (priceWithTax / (1 + taxRate)), 2, MidpointRounding.AwayFromZero);
        return res;
    }

    public static decimal CalculatePriceWithDiscountPercent(this decimal price, decimal discount)
    {
        var res = discount / 100;
        return res <= 0 ? 0 : decimal.Round(price - (price * res), 2, MidpointRounding.AwayFromZero);
    }

    public static decimal CalculatePriceWithDiscount(this decimal price, decimal discount)
    {
        var res = price - discount;
        return res <= 0 ? 0 : decimal.Round(price - discount, 2, MidpointRounding.AwayFromZero);
    }

    public static CultureInfo GetCultureInfo(string code)
    {
        return new CultureInfo(code);
    }

    public static string GenerateRandomPassword(int length, bool numbersOnly = false)
    {
        string allowedChars = "";
        if (numbersOnly)
        {
            allowedChars = "0123456789";
        }
        else
        {
            allowedChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        }

        char[] chars = new char[length];
        Random rd = new Random();
        for (int i = 0; i < length; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }

    public static string? GetAppleUrls(bool sandbox, string urlFor)
    {
        return urlFor switch
        {
            "Subscription" => sandbox ? Config.AppleSandboxSubscriptionURL : Config.AppleSubscriptionURL,
            "VerifyReceipt" => sandbox ? Config.AppleSandboxVerifyReceiptURL : Config.AppleVerifyReceiptURL,
            _ => null
        };
    }

  

   

    public static string ConvertHtmlToPlainTxt(string text)
    {
        return Regex.Replace(text, @"<(.|\n)*?>", "");
    }

    public static double? GetDiscount(this double? price, double? discount)
    {
        if (discount != null && discount > 0)
        {
            double? discounted_price = (price - (price * discount / 100));
            return discounted_price;
        }

        return price;
    }

    public static decimal VatPrice(decimal price, int vat)
    {
        return (price / 100) * vat;
    }

    public static double CalculateIncloudVat(double price, double vat)
    {
        double CR = (vat + 100) / 100;
        return price * CR;
    }

    public static decimal VatCost(decimal incVAT, int vat)
    {
        decimal cPrice = (decimal)(vat + 100) / 100;
        decimal tCost = incVAT * cPrice;
        return tCost - incVAT;
    }

    public static decimal GetPrice(decimal vat, decimal priceIncludeVat)
    {
        return priceIncludeVat - vat;
    }

    public static double? GetAmount(this double? price, double? discount)
    {
        if (discount != null && discount > 0)
        {
            double? discounted_price = (price * discount / 100);
            return discounted_price;
        }

        return price;
    }

    public static MemoryCacheEntryOptions GetCacheEntryOption(int second = 30)
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(second),
            SlidingExpiration = TimeSpan.FromSeconds(second),
            Size = 1024,
        };
    }

    public static HtmlString StringEnumDisplayNameFor(this Enum item)
    {
        var type = item.GetType();
        var member = type.GetMember(item.ToString());
        CustomAttributeData displayName = (CustomAttributeData)member[0].CustomAttributes.FirstOrDefault();

        if (displayName != null)
        {
            return new HtmlString(displayName.ConstructorArguments.FirstOrDefault().Value.ToString());
        }

        return new HtmlString(item.ToString());
    }

    public static string GenerateHashKey(string eventId)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(eventId));
            StringBuilder sb = new StringBuilder();

            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // Convert each byte to its hexadecimal representation
            }

            return sb.ToString();
        }
    }

    public static string GetClientToken()
    {
        return Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
    }

    public static string GetClientId()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }


    public static List<SelectListItem> ToSelectList<TEnum>(this TEnum obj)
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        return new SelectList(Enum.GetValues(typeof(TEnum))
            .OfType<Enum>()
            .Select(x => new SelectListItem
            {
                Text = Enum.GetName(typeof(TEnum), x),
                Value = (Convert.ToInt32(x))
                    .ToString()
            }), "Value", "Text").ToList();
    }


    public static MemoryCacheEntryOptions GetCacheEntryOptionMinute(int value = 1)
    {
        return new MemoryCacheEntryOptions
        {
            // AbsoluteExpiration = DateTime.Now.AddMinutes(AbsoluteExpirationMinute),
            // SlidingExpiration = TimeSpan.FromMinutes(SlidingExpirationMinute),
            //
            AbsoluteExpiration = DateTime.Now.AddMinutes(value),
            SlidingExpiration = TimeSpan.FromMinutes(value),
            Size = 1024,
        };
    }

    public static MemoryCacheEntryOptions GetCacheEntryOptionHours(int hours = 1)
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddHours(hours),
            Size = 1024
        };
    }


    public static string GenerateAppleJwtToken(string packageName)
    {
        var issueTime = DateTime.Now;
        var zeroTime = new DateTime(1970, 1, 1);

        var iat = (int)issueTime.Subtract(zeroTime).TotalSeconds;
        var exp = (int)issueTime.AddDays(5).Subtract(zeroTime).TotalSeconds;

        var header = new Dictionary<string, object>()
        {
            { "alg", Config.Apple_ALG },
            { "kid", Config.Apple_KID },
            { "typ", "JWT" }
        };

        var payload = new Dictionary<string, object>()
        {
            { "iss", Config.Apple_ISS },
            { "iat", iat },
            { "exp", exp },
            { "aud", Config.Apple_AUD },
            { "bid", packageName }
        };

        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        if (isWindows)
        {
            CngKey? cngKey = CngKey.Import(Convert.FromBase64String(Config.Apple_TokenKey),
                CngKeyBlobFormat.Pkcs8PrivateBlob);
            ECDsaCng? ecdsa = new ECDsaCng(cngKey);

            byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
            byte[] claimsBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.None));

            var base64Payload = Base64UrlEncoder.Encode(headerBytes) + "." + Base64UrlEncoder.Encode(claimsBytes);

            var signature = ecdsa.SignData(Encoding.UTF8.GetBytes(base64Payload), HashAlgorithmName.SHA256);

            return base64Payload + "." + Base64UrlEncoder.Encode(signature);
        }
        else
        {
            using (ECDsa key = ECDsa.Create())
            {
                key.ImportPkcs8PrivateKey(Convert.FromBase64String(Config.Apple_TokenKey), out _);
                byte[] headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header, Formatting.None));
                byte[] claimsBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload, Formatting.None));

                var base64Payload = Base64UrlEncoder.Encode(headerBytes) + "." + Base64UrlEncoder.Encode(claimsBytes);

                var signature = key.SignData(Encoding.UTF8.GetBytes(base64Payload), HashAlgorithmName.SHA256);

                return base64Payload + "." + Base64UrlEncoder.Encode(signature);
            }
        }
    }

    public static string GetLanguage(HttpRequest request)
    {
        var language = string.IsNullOrEmpty(request.Headers["language"])
            ? "en"
            : request.Headers["language"].ToString();
        return language;
    }

    public static string? GetPictureBaseUrl(bool? apiAssets = false)
    {
        return apiAssets != null && apiAssets.Value ? Config.PictureBaseURL : Config.ImageflowS3Key;
    }

    public static string? GetIpAddress(IHttpContextAccessor _httpContextAccessor)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null || context.Request.Headers == null)
        {
            return "0:0:0:1";
        }

        var ipAddress = string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"])
            ? "0:0:0:1"
            : context.Request.Headers["X-Forwarded-For"].ToString();

        return ipAddress;
    }


   

    public static string? GetUserId(ClaimsPrincipal user)
    {
        // Retrieve the user's unique identifier (user ID)
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        // Check if the claim exists
        if (userIdClaim != null)
        {
            return userIdClaim.Value;
        }
        else
        {
            // Handle the case where the user ID claim is not found
            return null;
        }
    }

   
    public static byte[] BitmapToByteArray(this Bitmap bitmap)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }


    public static MemoryCacheEntryOptions GetCacheEntryOptionSecond()
    {
        return new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(Config.AbsoluteExpirationSecond),
            SlidingExpiration = TimeSpan.FromSeconds(Config.SlidingExpirationSecond),
            Size = 1024,
        };
    }


    #region
    //public static string ConvertStringToEnum<T>(string status)
    //{
    //    Enum name = (Enum)Enum.Parse(typeof(T), status);
    //    return name.DisplayName();
    //}



    //public static string? GetUserId(IHttpContextAccessor _httpContextAccessor,
    //    UserManager<ApplicationUser> _userManager)
    //{
    //    var updatedByUserId = _httpContextAccessor?.HttpContext?.User != null
    //        ? _userManager.GetUserId(_httpContextAccessor?.HttpContext?.User) ??
    //          Config.AdminUserId
    //        : Config.AdminUserId;
    //    return updatedByUserId;
    //}


    //public static string GenerateAppleJwtToken(IConfiguration _configuration)
    //{
    //    var header = new Dictionary<string, object>()
    //    {
    //        { "alg", "ES256" },
    //        { "kid", "QB4U6Z3WYN" },
    //        { "typ", "JWT" }
    //    };

    //    var payload = new Dictionary<string, object>
    //    {
    //        { "iss", "69a6de90-d1f9-47e3-e053-5b8c7c11a4d1" },
    //        { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
    //        { "exp", DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds() },
    //        { "aud", "appstoreconnect-v1" },
    //        { "bid", "sa.sela.datamx" }
    //    };
    //    var privateKey = _configuration["Authentication:Apple:TokenKey"];

    //    using (ECDsa key = ECDsa.Create())
    //    {
    //        key.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
    //        string token = JWT.Encode(payload, key, JwsAlgorithm.ES256, header);
    //        return token;
    //    }

    //    return null;
    //}
    //public static async Task<MemoryStream?> GetObject(string fileName, string bucket)
    //{
    //    // connecting to the client
    //    var clientInfo = new AmazonS3Client(Config.RegionEndpoint);
    //    if (Config.Env != SysEnvironment.Production)
    //    {
    //        clientInfo = new AmazonS3Client(Config.AWSAccessKey, Config.AWSSecretKey, Config.RegionEndpoint);
    //    }


    //    using (var client = clientInfo)
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
    //        }
    //    }

    //    return null;
    //}


    //public static async Task<bool> PutObject(IFormFile file, string fileName, string bucket)
    //{
    //    try
    //    {
    //        // connecting to the client
    //        var client = new AmazonS3Client(Config.RegionEndpoint);
    //        if (Config.Env == SysEnvironment.Development || Config.Env == SysEnvironment.Local)
    //        {
    //            client = new AmazonS3Client(Config.AWSAccessKey, Config.AWSSecretKey, Config.RegionEndpoint);
    //        }

    //        // get the file and convert it to the byte[]
    //        byte[] fileBytes = new byte[file.Length];
    //        file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));

    //        // create unique file name for prevent the mess
    //        //  var fileName = Guid.NewGuid() + file.FileName;

    //        PutObjectResponse response = null;
    //        using (var stream = new MemoryStream(fileBytes))
    //        {
    //            var request = new PutObjectRequest
    //            {
    //                BucketName = bucket,
    //                Key = fileName,
    //                InputStream = stream,
    //                ContentType = file.ContentType,
    //                //CannedACL = S3CannedACL.PublicRead,
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


    //public static async Task<bool> VideoPutObject(IFormFile file, string fileName, string bucket, string ration)
    //{
    //    try
    //    {
    //        // connecting to the client
    //        var client = new AmazonS3Client(Config.RegionEndpoint);
    //        if (Config.Env == SysEnvironment.Development || Config.Env == SysEnvironment.Local)
    //        {
    //            client = new AmazonS3Client(Config.AWSAccessKey, Config.AWSSecretKey, Config.RegionEndpoint);
    //        }

    //        // get the file and convert it to the byte[]
    //        byte[] fileBytes = new byte[file.Length];
    //        file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));

    //        // create unique file name for prevent the mess
    //        //  var fileName = Guid.NewGuid() + file.FileName;

    //        PutObjectResponse response = null;
    //        using (var stream = new MemoryStream(fileBytes))
    //        {
    //            var request = new PutObjectRequest
    //            {
    //                BucketName = bucket,
    //                Key = fileName,
    //                InputStream = stream,
    //                ContentType = file.ContentType,
    //                TagSet = new List<Tag>
    //                {
    //                    new Tag { Key = "AspectRatio", Value = ration },
    //                }
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


    //    public static async Task<string> PutAPIObject(IFormFile file)
    //    {
    //        if (file != null)
    //        {
    //            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
    //            var result = await PutObject(file, Config.AWSImagePath + fileName, Config.AWSBucketAssets);
    //            if (result)
    //            {
    //                var _link = Config.AWSImagePath + fileName;
    //                return _link;
    //            }
    //        }

    //        return string.Empty;
    //    }

    //    public static async Task<string> PutAPIObjectInFile(IFormFile file)
    //    {
    //        if (file != null)
    //        {
    //            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
    //            var result = await PutObject(file, Config.AWSFilePath + fileName, Config.AWSBucketAssets);
    //            if (result)
    //            {
    //                var _link = Config.AWSFilePath + fileName;
    //                return _link;
    //            }
    //        }

    //        return string.Empty;
    //    }


    //    public static async Task<UploadFiles> UploadAssetsAsync(IFormFile file)
    //    {
    //        if (file != null)
    //        {
    //            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
    //            var result = await PutObject(file, Config.AWSFilePath + fileName, Config.AWSBucketAssets);
    //            if (result)
    //            {
    //                var _link = Config.AWSFilePath + fileName;
    //                UploadFiles upload = new UploadFiles
    //                {
    //                    link = Config.PictureBaseURL + _link,
    //                    SRC = _link,
    //                    FileName = file.FileName,
    //                };
    //                return upload;
    //            }
    //        }

    //        return new UploadFiles();
    //    }


    //    public static async Task<UploadFiles> UploadStreamVideoAssetsAsync(IFormFile file, Ratio ratio)
    //    {
    //        if (file != null)
    //        {
    //            string aspectRatio = ratio switch
    //            {
    //                Ratio.Landscape => "2:1",
    //                Ratio.Portrait => "1:2",
    //                Ratio.Square => "1:1",
    //                _ => "Unknown"
    //            };

    //            var fileName = Guid.NewGuid().ToString().Replace("-", "");
    //            var fileNameWithEx = fileName + Path.GetExtension(file.FileName);
    //            var actuallypath = Config.AWSFilePath + fileNameWithEx;

    //            var result = await VideoPutObject(file, actuallypath, Config.AWSAsignURLBucketAssets, aspectRatio);

    //            var videoUr = fileName + "/hls/" + fileName;
    //            var thumbnailUrl = fileName + "/thumbnails/" + fileName;

    //            var getDomain = Config.GetVideoM3U8AssetURL(ratio);

    //            var finalVideoUrl = getDomain + "/" + videoUr + Config.M3U8;
    //            var finalThumbnailUrl = getDomain + "/" + thumbnailUrl + Config.Thumb;
    //            if (result)
    //            {
    //                UploadFiles upload = new UploadFiles
    //                {
    //                    link = finalVideoUrl,
    //                    SRC = videoUr,
    //                    ActuallyVideoPath = actuallypath,
    //                    ActuallyThumbnailPath = thumbnailUrl,
    //                    ThumbnailUrl = finalThumbnailUrl,
    //                };
    //                return upload;
    //            }
    //        }

    //        return new UploadFiles();
    //    }


    //    public static async Task<string> PutCertificateInFile(IFormFile file, string fileName)
    //    {
    //        if (file != null)
    //        {
    //            //var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
    //            var result = await PutObject(file, Config.AWSFilePath + fileName, Config.AWSBucketAssets);
    //            if (result)
    //            {
    //                var _link = Config.AWSFilePath + fileName;
    //                return _link;
    //            }
    //        } 

    //        return string.Empty;
    //    }


    //    public static async Task<string?> GenerateCertificatePDF(string userTemplate, string tempPdfPath, string guid)
    //    {
    //        try
    //        {
    //            string? s3Url = null;
    //            var wkhtmltopdfPath = Path.Combine("WkHtmlToPdf", "wkhtmltopdf.exe");

    //            string tempHtmlPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.html");
    //            userTemplate = "<meta charset=\"UTF-8\">" + userTemplate;

    //            await File.WriteAllTextAsync(tempHtmlPath, userTemplate, Encoding.UTF8);

    //            var startInfo = new ProcessStartInfo
    //            {
    //                FileName = wkhtmltopdfPath,
    //                Arguments =
    //                    $"--encoding UTF-8 --page-size A4 --orientation landscape --margin-top 0 --margin-bottom 0 --margin-left 0 --margin-right 0 \"{tempHtmlPath}\" \"{tempPdfPath}\"",
    //                UseShellExecute = false,
    //                RedirectStandardOutput = true,
    //                RedirectStandardError = true,
    //                CreateNoWindow = true
    //            };

    //            using var process = new Process { StartInfo = startInfo };
    //            process.Start();
    //            await process.WaitForExitAsync();


    //            await process.WaitForExitAsync();


    //            using var memoryStream = new MemoryStream(await File.ReadAllBytesAsync(tempPdfPath));
    //            var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", Path.GetFileName(tempPdfPath))
    //            {
    //                Headers = new HeaderDictionary(),
    //                ContentType = "application/pdf"
    //            };
    //            s3Url = await PutCertificateInFile(formFile, $"{guid}/{Path.GetFileName(tempPdfPath)}");

    //            return s3Url;
    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }


    //    public static async Task<string> GenerateEDEFPDF(string userTemplate, string tempPdfPath, string guid)
    //    {
    //        try
    //        {
    //            string? s3Url = null;
    //            var wkhtmltopdfPath = Path.Combine("WkHtmlToPdf", "wkhtmltopdf.exe");
    //            string tempHtmlPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.html");

    //            // Add CSS to control the page dimensions within the HTML
    //            string styleTag = @"<style>
    //    @page { margin: 0; }
    //    html, body { 
    //        margin: 0 !important; 
    //        padding: 0 !important;
    //        width: 420px;
    //    }
    //    table {
    //        width: 100% !important;
    //    }
    //</style>";
    //            // Insert the style tag right after the opening <head> tag, or create one if it doesn't exist
    //            if (userTemplate.Contains("<head>"))
    //            {
    //                userTemplate = userTemplate.Replace("<head>", "<head>" + styleTag);
    //            }
    //            else if (userTemplate.Contains("<html>"))
    //            {
    //                userTemplate = userTemplate.Replace("<html>", "<html><head>" + styleTag + "</head>");
    //            }
    //            else
    //            {
    //                userTemplate = "<html><head>" + styleTag + "</head><body>" + userTemplate + "</body></html>";
    //            }

    //            await File.WriteAllTextAsync(tempHtmlPath, userTemplate, Encoding.UTF8);

    //            var startInfo = new ProcessStartInfo
    //            {
    //                FileName = wkhtmltopdfPath,
    //                // Use custom page size with exact dimensions, portrait orientation, and zero margins
    //                Arguments = $"--encoding UTF-8 --disable-smart-shrinking " +
    //            $"--page-width 111mm --page-height 970mm " +  // 420px ≈ 111mm, 3648px ≈ 970mm
    //            $"--orientation Portrait " +
    //            $"--margin-top 0 --margin-bottom 0 --margin-left 0 --margin-right 0 " +
    //            $"\"{tempHtmlPath}\" \"{tempPdfPath}\"",
    //                UseShellExecute = false,
    //                RedirectStandardOutput = true,
    //                RedirectStandardError = true,
    //                CreateNoWindow = true
    //            };

    //            using var process = new Process { StartInfo = startInfo };
    //            process.Start();

    //            // You have this line twice in your original code, so I've kept only one
    //            await process.WaitForExitAsync();

    //            if (process.ExitCode != 0)
    //            {
    //                // Read the error output to help with debugging
    //                string errorOutput = await process.StandardError.ReadToEndAsync();
    //                Console.WriteLine($"wkhtmltopdf error: {errorOutput}");
    //                return string.Empty;
    //            }

    //            using var memoryStream = new MemoryStream(await File.ReadAllBytesAsync(tempPdfPath));
    //            var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", Path.GetFileName(tempPdfPath))
    //            {
    //                Headers = new HeaderDictionary(),
    //                ContentType = "application/pdf"
    //            };

    //            s3Url = await PutCertificateInFile(formFile, $"{guid}/{Path.GetFileName(tempPdfPath)}");

    //            // Clean up temporary files
    //            try
    //            {
    //                File.Delete(tempHtmlPath);
    //            }
    //            catch { /* Ignore cleanup errors */ }

    //            return s3Url;
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Error generating PDF: {ex.Message}");
    //            return string.Empty;
    //        }
    //    }

    //    public static string GeneratePreSignedUrlCloudFront(double duration, string domainKey, UserType type)
    //    {
    //        var urlString = "";
    //        var pathtoPrivate = "wwwroot/Keys/private_key_";
    //        var CloudFrontKeyPairId = "";
    //        switch (type)
    //        {
    //            case UserType.Mobile:
    //                pathtoPrivate += "upload.pem";
    //                CloudFrontKeyPairId = CloudFrontKeyPairId_Mobile;
    //                break;
    //            default:
    //                throw new ArgumentOutOfRangeException(nameof(type), type, null);
    //        }

    //        try
    //        {
    //            var privatekey = new FileInfo(pathtoPrivate);
    //            using StreamReader privateKey2 = new(File.OpenRead(privatekey.FullName));
    //            urlString = AmazonCloudFrontUrlSigner.GetCannedSignedURL(AsignUrlDomain + "/" + domainKey, privateKey2,
    //                CloudFrontKeyPairId, DateTime.UtcNow.AddHours(duration));
    //        }
    //        catch (AmazonS3Exception)
    //        {
    //            return "";
    //        }

    //        return urlString;
    //    }


    //    public static async Task<string> Compressed_GeneratePreSignedUrlCloudFront(double duration, string domainKey,
    //        UserType type, Ratio ration)
    //    {
    //        var asignUrlDomain = Config.GetVideoM3U8AssetURL(ration);
    //        var urlString = $"{asignUrlDomain}/{domainKey}";
    //        return urlString;
    //    }

    //    public static string GeneratePreSignedUrlCloudFront(double duration, string domainKey, UserType type, Ratio ration)
    //    {
    //        var asignUrlDomain = Config.GetVideoM3U8AssetURL(ration);
    //        var urlString = $"{asignUrlDomain}/{domainKey}";
    //        return urlString;
    //    }


    //        public static async Task<string?> GenerateTopPlayerPDF(string userTemplate, string tempPdfPath, string guid)
    //    {
    //        try
    //        {
    //            string? s3Url = null;
    //            var wkhtmltopdfPath = Path.Combine("WkHtmlToPdf", "wkhtmltopdf.exe");

    //            string tempHtmlPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.html");
    //            userTemplate = "<meta charset=\"UTF-8\">" + userTemplate;
    //            //--------------------------------------------------------------------------------
    //            // Add CSS to control the page dimensions within the HTML
    //            string styleTag = @"<style>
    //                   @font-face {
    //                     font-family: ""DIN-Condensed-Bold"";
    //                     src: url(""https://assets.selasuperapp.com/email/DIN-Condensed-Bold.ttf"") format(""truetype"");
    //                   }
    //                  @page { margin: 0; }
    //                  html, body { 
    //                      margin: 0 !important; 
    //                      padding: 0 !important;
    //                      width: 306px;
    //                      height:503px;
    //                  }

    //                 </style>";
    //            // Insert the style tag right after the opening <head> tag, or create one if it doesn't exist
    //            if (userTemplate.Contains("<head>"))
    //            {
    //                userTemplate = userTemplate.Replace("<head>", "<head>" + styleTag);
    //            }
    //            else if (userTemplate.Contains("<html>"))
    //            {
    //                userTemplate = userTemplate.Replace("<html>", "<html><head>" + styleTag + "</head>");
    //            }
    //            else
    //            {
    //                userTemplate = "<html><head>" + styleTag + "</head><body>" + userTemplate + "</body></html>";
    //            }
    //            //--------------------------------------------------------------------------------
    //            await File.WriteAllTextAsync(tempHtmlPath, userTemplate, Encoding.UTF8);

    //            var startInfo = new ProcessStartInfo
    //            {
    //                FileName = wkhtmltopdfPath,
    //                Arguments = $"--encoding UTF-8 --disable-smart-shrinking " +
    //                            $"--page-width 80.50mm --page-height 132.98mm " +  // 306px ≈ 80.90mm, 503px ≈ 132.98mm
    //                            $"--orientation Portrait " +
    //                            $"--margin-top 0 --margin-bottom 0 --margin-left 0 --margin-right 0 " +
    //                            $"\"{tempHtmlPath}\" \"{tempPdfPath}\"",

    //                UseShellExecute = false,
    //                RedirectStandardOutput = true,
    //                RedirectStandardError = true,
    //                CreateNoWindow = true
    //            };

    //            using var process = new Process { StartInfo = startInfo };
    //            process.Start();
    //            await process.WaitForExitAsync();




    //            using var memoryStream = new MemoryStream(await File.ReadAllBytesAsync(tempPdfPath));
    //            var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", Path.GetFileName(tempPdfPath))
    //            {
    //                Headers = new HeaderDictionary(),
    //                ContentType = "application/pdf"
    //            };
    //            s3Url = await PutCertificateInFile(formFile, $"{guid}/{Path.GetFileName(tempPdfPath)}");
    //            File.Delete(tempHtmlPath);
    //            return s3Url;


    //        }
    //        catch (Exception ex)
    //        {
    //            return string.Empty;
    //        }
    //    }
    #endregion

}