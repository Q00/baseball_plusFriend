
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace plusFriend_API
{
    public class Function
    {

        /// <summary>
        /// ùȭ��
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>

        public JObject FunctionHandler()
        {

            JObject JAns = new JObject();
            JArray type = new JArray();

            type.Add("������ ���");
            type.Add("������ �� ����");
            type.Add("���̹� ������ �߱� �ٷΰ���");

            JAns.Add(new JProperty("type", "buttons"));
            JAns.Add(new JProperty("buttons", type));

            return JAns;

        }
    }
}
