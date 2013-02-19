using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.ComponentModel;

namespace FogBugzNet
{

    public class EServerError : Exception
    {
        public EServerError(string reason)
            : base(reason)
        {
        }
    }

    public class EURLError : Exception
    {
        public EURLError(string reason)
            : base(reason)
        {
        }
    }

    public class HttpUtils
    {
        public static string httpGet(string url)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse res = req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                return sr.ReadToEnd();
            }
            catch (System.Net.WebException x)
            {
                Utils.Log.Error(x.ToString() + ". Connection status: " + x.Status.ToString());
                throw new EServerError("Unable to find FogBugz server");
            }
            catch (System.UriFormatException x)
            {
                Utils.Log.Error(x.ToString());
                throw new EURLError("The server URL you provided appears to be malformed");
            }
        }

        public static void ReadStreamToFile(Stream src, string dst)
        {

            FileStream fs = new FileStream(dst, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryWriter br = new BinaryWriter(fs);

            byte[] buffer = new byte[4096];
            int count = 0;
            do
            {
                count = src.Read(buffer, 0, buffer.Length);
                br.Write(buffer, 0, count);
            } while (count != 0);

            fs.Close();

        }

        public static void httpGetBinary(string url, string targetFile)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse res = req.GetResponse();
                Utils.Log.DebugFormat("Writing response to binary file {0}", targetFile);
                ReadStreamToFile(res.GetResponseStream(), targetFile);
            }
            catch (System.Net.WebException x)
            {
                Utils.Log.Error(x.ToString() + ". Connection status: " + x.Status.ToString());
                throw new EServerError("Unable to find FogBugz server");
            }
            catch (System.UriFormatException x)
            {
                Utils.Log.Error(x.ToString());
                throw new EURLError("The server URL you provided appears to be malformed");
            }
        }



    }
}
