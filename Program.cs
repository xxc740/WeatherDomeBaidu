using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherDomeBaidu
{
    class Program
    {
        static string url = "http://apis.baidu.com/heweather/weather/free";
        static string cityName = "city=shanghai";

        static void Main(string[] args)
        {
            var re = request(url, cityName);

            JObject jo = JsonConvert.DeserializeObject(re) as JObject;
            readJson(jo);
            Console.ReadLine();
        }

        public static string request(string url, string city) 
        {
            string strURL = url + '?' + city;       
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
            request.Method = "GET";
            request.Headers.Add("apikey", "ea3514ced8bc7946a2bd7d05f508cfda");
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s = response.GetResponseStream();
            string strDate = "";
            string strValue = "";
            System.IO.StreamReader reader = new System.IO.StreamReader(s, Encoding.UTF8);
            while ((strDate = reader.ReadLine()) != null)
            {
                strValue += strDate + "\r\n";
            }

            return strValue;
        }

        public static void readJson(JObject jObj) 
        {
            foreach (var ele in jObj)
            {
                Console.Write(ele.Key + ":");

                if (ele.Value is JObject)
                {
                    Console.WriteLine();
                    readJson(JsonConvert.DeserializeObject(ele.Value.ToString()) as JObject);
                }
                else
                {
                    Console.WriteLine(ele.Value);
                }
            }
        }
    }
}
