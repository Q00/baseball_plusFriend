using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;


namespace message_api
{
    class GetInfo
    {
        //팀들 경기 정보 담을 리스트
        List<string> teamList = new List<string>();

        public List<string> getInfoList(string type)
        {
            try
            {
                string res = send_url("https://sports.news.naver.com/kbaseball/index.nhn", Method.GET);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(res);


                //오늘의 경기 가져오는 로직
                if (type.Equals("match"))
                {
                    get_match(doc);
                }
                else if(type.Equals("rate"))
                {
                    get_rate(doc);
                }

            }
            catch(Exception e)
            {
                teamList.Clear();
                teamList.Add(type + " 야구 정보를 가져오는 데 실패하였습니다. 개발자에게 연락 부탁드립니다." + e.Message.ToString());
                
            }


            return teamList;
        }

        private void get_rate(HtmlDocument doc)
        {
            //순위정보 가져오기
            HtmlNode nodelist = doc.DocumentNode.SelectSingleNode("//div[@id='rank_template1']");
            HtmlNodeCollection tbody = nodelist.SelectNodes(".//tr");

            
            foreach(HtmlNode tr in tbody)
            {
                HtmlNodeCollection teams = tr.SelectNodes(".//span");
                //팀 정보 합친 리스트
                List<string> team_info_list = new List<string>();

                foreach (HtmlNode info in teams)
                {
                    team_info_list.Add(info.InnerText);
                }

                teamList.Add(string.Join("\t", team_info_list.ToArray()));
            }
        }

        private void get_match(HtmlDocument doc)
        {
            HtmlNodeCollection nodelist = doc.DocumentNode.SelectNodes("//li[@class='hmb_list_items']");

            foreach (HtmlNode node in nodelist)
            {
                HtmlNodeCollection teams = node.SelectNodes(".//span");

                //팀정보
                string team_name = "";
                string pitcher = "";

                //합칠변수
                string concat_team = "";

                //두팀 경기 매치 리스트
                List<string> match_list = new List<string>();

                foreach (HtmlNode team in teams)
                {
                    //투수캡쳐했는지 확인하는 플래그
                    bool AddFlag = false;


                    if (team.Attributes.Count > 0)
                    {
                        team_name = team.InnerText;
                    }
                    else
                    {
                        AddFlag = true;

                        if (teams.Count < 4)
                        {
                            pitcher = "미정";
                        }
                        else
                        {
                            pitcher = team.InnerText;
                        }
                    }

                    if (AddFlag)
                    {
                        concat_team = team_name + "팀 , 투수 : " + pitcher;
                        match_list.Add(concat_team);
                    }
                }

                teamList.Add(string.Join("/", match_list.ToArray()));
            }
        }

        private string send_url(string url, Method method)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);
      
            //request.AddHeader();
            //request.AddParameter();

            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
