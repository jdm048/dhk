using System.IO;
using System.Net;
using System.Text;

namespace RIAT2
{
    abstract class ServerBase 
    {
        protected HttpListener listener;
        protected HttpListenerContext context;
        protected HttpListenerRequest request;
        protected HttpListenerResponse response;

        public ServerBase(string host, string port)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format("{0}:{1}/", host, port));
            listener.Start();
        }

        public string GetRequestBody()
        {
            byte[] bytes = new byte[request.ContentLength64];
            using (Stream stream = request.InputStream)
            {
                stream.Read(bytes, 0, (int)request.ContentLength64);
            }
            return Encoding.UTF8.GetString(bytes);
        }

        public void WriteResponse(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            response.ContentLength64 = bytes.Length;
            using (Stream stream = response.OutputStream)
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
