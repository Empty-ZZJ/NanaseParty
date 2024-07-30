using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Model.Weather
{
    public static class WeatherAPI
    {
        private const string SecretID = "AKIDF5qXub2F2owUpJ4a2nEtXpJc4xo72INc6or5";
        private const string SecretKey = "5emc3vkg34Hdmqds9Vx9i5vRmbbssciki43snYNr";

        /// <summary>
        /// 腾讯云API重构
        /// </summary>
        public static async Task<WeatherInfo> GetWeather (string cityID)
        {
            var APIUrl = "https://service-6drgk6su-1258850945.gz.apigw.tencentcs.com/release/lundear/weather1d";
            var method = "GET";
            var querys = $"areaCn=&areaCode={cityID}&ip=&lat=&lng=&need1hour=&need3hour=&needIndex=&needObserve=&needalarm=";
            var source = "market";
            var dt = DateTime.UtcNow.GetDateTimeFormats('r')[0];
            APIUrl = APIUrl + "?" + querys;
            var signStr = "x-date: " + dt + "\n" + "x-source: " + source;
            var sign = HMACSHA1Text(signStr, SecretKey);
            var auth = "hmac id=\"" + SecretID + "\", algorithm=\"hmac-sha1\", headers=\"x-date x-source\", signature=\"";
            auth = auth + sign + "\"";
            Console.WriteLine(auth + "\n");
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            if (APIUrl.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(APIUrl));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(APIUrl);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", auth);
            httpRequest.Headers.Add("X-Source", source);
            httpRequest.Headers.Add("X-Date", dt);
            try
            {
                httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
                throw;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));

            var _json = await reader.ReadToEndAsync();
            Debug.Log(_json);
            return JsonConvert.DeserializeObject<WeatherInfo>(_json);

        }
        public static String HMACSHA1Text (String EncryptText, String EncryptKey)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = System.Text.Encoding.UTF8.GetBytes(EncryptKey);

            byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(EncryptText);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
        public static bool CheckValidationResult (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

    }
    public class Day
    {
        /// <summary>
        /// 28日白天
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 日出 05:08
        /// </summary>
        public string sunUp { get; set; }
        /// <summary>
        /// 南风
        /// </summary>
        public string wind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weather_pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// <3级
        /// </summary>
        public string wind_pow { get; set; }
        /// <summary>
        /// 小雨
        /// </summary>
        public string weather { get; set; }
    }

    public class Night
    {
        /// <summary>
        /// 27日夜间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 日落 19:33
        /// </summary>
        public string sunDown { get; set; }
        /// <summary>
        /// 东南风
        /// </summary>
        public string wind { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weather_pic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// <3级
        /// </summary>
        public string wind_pow { get; set; }
        /// <summary>
        /// 多云
        /// </summary>
        public string weather { get; set; }
    }

    public class CityInfo
    {
        /// <summary>
        /// 北京
        /// </summary>
        public string areaCn { get; set; }
        /// <summary>
        /// 中国
        /// </summary>
        public string nationCn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string areaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nationEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cityEn { get; set; }
        /// <summary>
        /// 北京
        /// </summary>
        public string provCn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string provEn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string areaEn { get; set; }
        /// <summary>
        /// 北京
        /// </summary>
        public string cityCn { get; set; }
    }

    public class Now
    {
        /// <summary>
        /// 东南风
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rain24h { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string njd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wde { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wse { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tempf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string aqi { get; set; }
        /// <summary>
        /// 07月27日(星期六)
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 北京
        /// </summary>
        public string cityname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string aqi_pm25 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weathercode { get; set; }
        /// <summary>
        /// 3级
        /// </summary>
        public string WS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string temp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weathere { get; set; }
        /// <summary>
        /// 晴
        /// </summary>
        public string weather { get; set; }
        /// <summary>
        /// 不限行
        /// </summary>
        public string limitnumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nameen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public Day day { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Night night { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CityInfo cityInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Now now { get; set; }
    }

    public class WeatherInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
    }
}