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
            var localhost = "http://127.0.0.1";
            var port = Console.ReadLine();
            var server = new Server(localhost, port);
            server.Start();

         }
    }
}
