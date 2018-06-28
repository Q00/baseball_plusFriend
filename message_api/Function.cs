using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace message_api
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public JObject FunctionHandler(messageForm input, ILambdaContext context)
        {
            //request parameter
            string user_key = input.User_key;
            string type = input.Type;
            string content = input.Content;

            //return 할 json 변수 선언
            JObject jo = new JObject();
            JObject message = new JObject();

            //매 화면마다 돌아가는 버튼 선언
            JObject return_button = new JObject();
            return_button.Add("type", "buttons");

            JArray return_button_arr = new JArray();
            return_button_arr.Add("처음으로");
            return_button.Add("buttons", return_button_arr);


            GetInfo getinfo = new GetInfo();


            switch (content)
            {
                case "오늘의 경기":

                    message.Add("text", "오늘의 경기를 알려드리겠습니다. \n" + string.Join("\n", getinfo.getInfoList("match").ToArray()));

                    break;

                case "오늘의 팀 순위":

                    message.Add("text", "팀순위를 알려드리겠습니다. \n" + string.Join("\n", getinfo.getInfoList("rate").ToArray()));

                    break;

                case "네이버 스포츠 야구 바로가기":
                    message.Add("text", "네이버 스포츠 야구 바로가기!");
                    JObject message_button = new JObject();
                    message_button.Add("label", "네이버 스포츠 야구");
                    message_button.Add("url", "https://sports.news.naver.com/kbaseball/index.nhn");
                    message.Add("message_button", message_button);
                    break;


                default:
                    message.Add("text", "KBO 정보 알려주는 친구 입니다.");
                    JArray bt = new JArray();
                    bt.Add("오늘의 경기");
                    bt.Add("오늘의 팀 순위");
                    bt.Add("네이버 스포츠 야구 바로가기");
                    return_button = new JObject();
                    return_button.Add(new JProperty("type", "buttons"));
                    return_button.Add(new JProperty("buttons", bt));

                    break;
            }
            jo.Add("message", message);
            
            jo.Add("keyboard", return_button);
            return jo;
        }
    }
}
