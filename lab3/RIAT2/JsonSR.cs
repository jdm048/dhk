using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Globalization;
namespace RIAT2
{
    class JsonSR<T>
    {

        public T Deserialize(string someSerialize)
        {

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            StreamWriter sw = new StreamWriter(stream);
            sw.Write(someSerialize);
            sw.Flush();
            stream.Position = 0;
            return (T)serializer.ReadObject(stream);
        }


        public string Serialize(Output output)
        {
            MemoryStream stream = new MemoryStream();
            output.SortedInputs = output.SortedInputs.Select(i => i ).ToArray();
            

      
           
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Output));

            serializer.WriteObject(stream, output);
            stream.Position = 0;
            string res;
            using (StreamReader sr = new StreamReader(stream))
            {
                res = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            return res;
        }

        
        public static Output MakeOutputFromInput(Input someInput)
        {
            Output Res = new Output();
            Res.SumResult = someInput.Sums.Sum() * someInput.K;
            Res.MulResult = someInput.Muls.Aggregate((acc, i) => acc * i);
            someInput.Sums = someInput.Sums.Select(i => (decimal)i + 0.0M).ToArray();
            Res.SortedInputs = someInput.Sums.Concat(someInput.Muls.Select(i => (decimal)i + 0.0M)).OrderBy(x => x).ToArray();
            return Res;
        }

    }
}
