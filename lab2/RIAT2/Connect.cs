using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace RIAT2
{
    class Connect
    {
        public static myconnectionclass Connection(string Url)
        {    
           HttpWebResponse my_result_connect;
            HttpWebRequest my_connect;
           do
           {
               my_connect = (HttpWebRequest)WebRequest.Create(Url);
               my_connect.Method = "GET";
               my_result_connect = (HttpWebResponse)my_connect.GetResponse();
           }
           while (my_result_connect.StatusCode != HttpStatusCode.OK);
            my_result_connect.Close();
            myconnectionclass n = new myconnectionclass();
            n.my_connect = my_connect;
            n.my_result_connect = my_result_connect;
            return n;
        }
    }
}
