//using Amazon.SimpleNotificationService.Model;
//using Amazon.SimpleNotificationService;
//using Data.IRepository;
//using Data.Service;
//using System.Net;
//using System.Text;

//namespace Data.Repository
//{
//    public class SmsSender : ISmsSender
//    {

//        public async Task<bool> SendUnifonicSMS(string number, string message, string appId, string sender)
//        {
//            number = UtilityMethods.FixNumberForUnifonic(number);
//            var url = string.Format(Config.UnifonicUrl, appId, sender, message, number);

//            if (Config.UseServerMessage)
//            {
//                var body = new UnifonicSMS
//                {
//                    AppSid = appId,
//                    Body = message,
//                    Recipient = number,
//                    SenderID = sender,
//                };

//                var client = new HttpClient();
//                var httpRequestMessage = new HttpRequestMessage
//                {
//                    Method = HttpMethod.Post,
//                    RequestUri = new Uri("https://admin.boulevardworld.co/SendSms"),
//                    Headers = {
//                    { HttpRequestHeader.Accept.ToString(), "application/json" },
//                    { HttpRequestHeader.ContentType.ToString(), "application/json" },
//                    {"secret", Config.UseServerSecret }
//                    },
//                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
//                };
//                var result = await client.SendAsync(httpRequestMessage);

//                if (result.IsSuccessStatusCode)
//                {
//                    Console.WriteLine("working");
//                    return true;
//                }
//                return false;
//            }

//            else
//            {

//                var client = new HttpClient();
//                var httpRequestMessage = new HttpRequestMessage
//                {
//                    Method = HttpMethod.Post,
//                    RequestUri = new Uri(url),
//                    Headers = {
//            { HttpRequestHeader.Accept.ToString(), "application/json" },
//            { HttpRequestHeader.ContentType.ToString(), "application/json" },
//            },
//                    Content = new StringContent("", Encoding.UTF8, "application/json")
//                };

//                var result = await client.SendAsync(httpRequestMessage);
//                var t = System.Text.Json.JsonSerializer.Serialize(result);
//                if (result.IsSuccessStatusCode)
//                {
//                    return true;
//                }
//                return false;
//            }

//        }

//        public async Task<bool> SendSMS(string number, string message)
//        {
//            var client = Config.Env == SysEnvironment.Development ? new AmazonSimpleNotificationServiceClient(Config.AWSAccessKey, Config.AWSSecretKey, Config.RegionEndpoint) : new AmazonSimpleNotificationServiceClient(Config.RegionEndpoint);
//            var messageAttributes = new Dictionary<string, MessageAttributeValue>();
//            var smsType = new MessageAttributeValue
//            {
//                DataType = "String",
//                StringValue = "Transactional"
//            };

//            messageAttributes.Add("AWS.SNS.SMS.SMSType", smsType);

//            PublishRequest request = new PublishRequest
//            {
//                Message = message,
//                PhoneNumber = number,
//                MessageAttributes = messageAttributes
//            };

//            var result = await client.PublishAsync(request);

//            if (result.HttpStatusCode == HttpStatusCode.OK)
//                return true;

//            return false;
//        }


//    }

//    public class UnifonicSMS
//    {
//        public string AppSid { get; set; }
//        public string SenderID { get; set; }
//        public string Body { get; set; }
//        public string Recipient { get; set; }

//    }
//}
