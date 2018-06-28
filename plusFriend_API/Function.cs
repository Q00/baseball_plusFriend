
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace plusFriend_API
{
    public class Function
    {

        /// <summary>
        /// 첫화면
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>

        public JObject FunctionHandler()
        {

            JObject JAns = new JObject();
            JArray type = new JArray();

            type.Add("오늘의 경기");
            type.Add("오늘의 팀 순위");
            type.Add("네이버 스포츠 야구 바로가기");

            JAns.Add(new JProperty("type", "buttons"));
            JAns.Add(new JProperty("buttons", type));

            return JAns;

        }
    }
}
