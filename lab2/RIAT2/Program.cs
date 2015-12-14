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
    
    class Program
    {
        
        static void Main(string[] args)
        {

            var s =Console.ReadLine();
            var n = Connect.Connection("http://127.0.0.1:" + s + "/Ping");
            var request = "http://127.0.0.1:" + s ;
            string str = null;            
            Input nn = new Input();
           var sos = n.go(request, "/GetInputData");
            str = Encoding.UTF8.GetString(sos.Result);
            n.my_result_connect.Close();
            JsonSR<Input> sr = new JsonSR<Input>();
            nn = sr.Deserialize(str);
            JsonSR<Output> my_json_out = new JsonSR<Output>();
            Output outp = JsonSR<Output>.MakeOutputFromInput(nn);
            string it_is_not_true = my_json_out.Serialize(outp);
            byte[] byteArray = Encoding.UTF8.GetBytes(it_is_not_true);
            n.psl("http://127.0.0.1:" + s , byteArray,"/WriteAnswer");
        }
    }
}
