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

            //return �� json ���� ����
            JObject jo = new JObject();
            JObject message = new JObject();

            //�� ȭ�鸶�� ���ư��� ��ư ����
            JObject return_button = new JObject();
            return_button.Add("type", "buttons");

            JArray return_button_arr = new JArray();
            return_button_arr.Add("ó������");
            return_button.Add("buttons", return_button_arr);


            GetInfo getinfo = new GetInfo();


            switch (content)
            {
                case "������ ���":

                    message.Add("text", "������ ��⸦ �˷��帮�ڽ��ϴ�. \n" + string.Join("\n", getinfo.getInfoList("match").ToArray()));

                    break;

                case "������ �� ����":

                    message.Add("text", "�������� �˷��帮�ڽ��ϴ�. \n" + string.Join("\n", getinfo.getInfoList("rate").ToArray()));

                    break;

                case "���̹� ������ �߱� �ٷΰ���":
                    message.Add("text", "���̹� ������ �߱� �ٷΰ���!");
                    JObject message_button = new JObject();
                    message_button.Add("label", "���̹� ������ �߱�");
                    message_button.Add("url", "https://sports.news.naver.com/kbaseball/index.nhn");
                    message.Add("message_button", message_button);
                    break;


                default:
                    message.Add("text", "KBO ���� �˷��ִ� ģ�� �Դϴ�.");
                    JArray bt = new JArray();
                    bt.Add("������ ���");
                    bt.Add("������ �� ����");
                    bt.Add("���̹� ������ �߱� �ٷΰ���");
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
