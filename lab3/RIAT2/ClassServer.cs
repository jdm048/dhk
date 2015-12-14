using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace RIAT2
{
    class Server : ServerBase
        {
            static JsonSR<Input> serializer = new JsonSR<Input>();
            Input inputData;
            Output answer;

            static Output Convert(Input data)
            {
                var answer = new Output();
                answer.SumResult = data.Sums.Sum() * data.K;
                answer.MulResult = data.Muls.Aggregate((a, b) => a * b);
                answer.SortedInputs = Array.ConvertAll(data.Muls, x => (decimal)x)
                                        .Concat(data.Sums)
                                        .OrderBy(x => x)
                                        .ToArray();
                return answer;
            }

            public Server(string host, string port)
                : base(host, port)
            { }

            public void Start()
            {
                while (listener.IsListening)
                {
                    context = listener.GetContext();
                    request = context.Request;
                    response = context.Response;

                    var rawUrl = request.RawUrl;
                    var trimmedUrl = rawUrl.Trim(new char[] { '/' });
                    var indexOf = trimmedUrl.IndexOf('?');
                    var methodName = indexOf >= 0 ? trimmedUrl.Substring(0, indexOf) : trimmedUrl;
                    if (methodName == "Stop")
                    {
                        WriteResponse(string.Empty);
                        Stop();
                        break;
                    }

                    MethodInfo info = typeof(Server).GetMethod(methodName);
                    try
                    {
                        info.Invoke(this, null);
                    }
                    catch
                    {
                        WriteResponse(string.Empty);
                    }
                }
            }

            public void Ping()
            {
                WriteResponse(string.Empty);
            }

            public void PostInputData()
            {
                inputData = serializer.Deserialize(GetRequestBody());
                WriteResponse(string.Empty);
            }

            public void GetAnswer()
            {
                answer = Convert(inputData);
                WriteResponse(serializer.Serialize(answer));
            }

            public void Stop()
            {
                listener.Stop();
            }
        }
}
