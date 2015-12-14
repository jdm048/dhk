using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RIAT2
{
    class myconnectionclass
    {
       public  HttpWebResponse my_result_connect;
       public  HttpWebRequest my_connect;
        public async Task<byte[]> go(string utl,string s)
        {
            HttpClient client = new HttpClient() ;
            client.BaseAddress = new Uri(utl);
            ServicePoint sp = my_connect.ServicePoint;
            sp.Expect100Continue = false; 
            var response = await client.GetByteArrayAsync(s);
            return response;
        }
        public async void psl(string lrk ,byte[] kzl,string s )
        {
            HttpClient usr = new HttpClient() ;
            usr.BaseAddress = new Uri(lrk);
            ServicePoint sp = my_connect.ServicePoint;
              sp.Expect100Continue = false;
            HttpContent content = new ByteArrayContent(kzl);
            var response = usr.PostAsync(s, content);
            HttpContent ontent = response.Result.Content;
            string responseFromServer = ontent.ReadAsStringAsync().Result;
        }
    }
}
