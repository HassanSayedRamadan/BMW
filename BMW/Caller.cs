using BMW.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BMW
{
    public class Caller
    {
        public Response<string> PostAuth(string DestinationUrl, string RequestJsonBody)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebResponse response;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DestinationUrl);
                byte[] bytes;
                bytes = Encoding.ASCII.GetBytes(RequestJsonBody);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Authorization", "Basic " + ConfigurationManager.AppSettings["Authorization"].ToString());
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Response<string>.Valid(responseStr);
                }
                else
                {
                    return Response<string>.Failed(responseStr);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }
        }

        public Response<string> GetRequest(string DestinationUrl)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DestinationUrl);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + ConfigurationManager.AppSettings["access_token"].ToString());
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Response<string>.Valid(responseStr);
                }
                else
                {
                    return Response<string>.Failed(responseStr);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }
        }

        public Response<string> PostRequest(string DestinationUrl, string RequestJsonBody)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpWebResponse response;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DestinationUrl);
                byte[] bytes;
                bytes = Encoding.ASCII.GetBytes(RequestJsonBody);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Authorization", "Bearer " + ConfigurationManager.AppSettings["access_token"].ToString());
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Response<string>.Valid(responseStr);
                }
                else
                {
                    return Response<string>.Failed(responseStr);
                }
            }
            catch (Exception e)
            {
                return Response<string>.Failed(e);
            }
        }

    }
}