using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RIAT_LABS
{
    public class Lab1
    {
        [DataContract]
        public class Input
        {
            [DataMember(Order = 1)]
            public int K { get; set; }
            [DataMember(Order = 2)]
            public decimal[] Sums { get; set; }
            [DataMember(Order = 3)]
            public int[] Muls { get; set; }
        }
        [DataContract]
        public class Output
        {
            [DataMember(Order = 1)]
            public decimal SumResult { get; set; }
            [DataMember(Order = 2)]
            public int MulResult { get; set; }
            [DataMember(Order = 3)]
            public decimal[] SortedInputs { get; set; }
            public Output()
            {
                this.SumResult = 0;
                this.MulResult = 1;
            }
        }

        public class InputToOutput
        {
            public static Output MakeOutputFromInput(Input someInput)
            {
                Output result = new Output();
                result.SumResult = someInput.Sums.Sum() * someInput.K;
                result.MulResult = someInput.Muls.Aggregate((acc, i) => acc * i);
                result.SortedInputs = someInput.Sums.Concat(someInput.Muls.Select(i => (decimal)i)).ToArray();
                Array.Sort(result.SortedInputs);
                return result;
            }
        }

        public interface ISerialize
        {
            string Serialize <T>(T output);
            T Deserialize<T>(string someSerialize);
        }

        public class XMLSerialize : ISerialize
        {
            public T Deserialize<T>(string someSerialize)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                MemoryStream str = new MemoryStream();
                StreamWriter stw = new StreamWriter(str);
                str.Position = 0;
                stw.WriteLine(someSerialize);
                stw.Flush();
                str.Position = 0;
                XmlReader reader = XmlReader.Create(str);
                T res = (T)serializer.Deserialize(reader);
                return res;
            }

            public string Serialize<T>(T output)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                MemoryStream stream = new MemoryStream();
                XmlWriterSettings xmlsettings = new XmlWriterSettings();
                xmlsettings.OmitXmlDeclaration = true;
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlWriter writer = XmlWriter.Create(stream, xmlsettings);
                string res;
                serializer.Serialize(writer, output, ns);
                using (StreamReader sr = new StreamReader(stream))
                {
                    stream.Position = 0;
                    res = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                return res;
            }
        }

        public class JSONSerialize : ISerialize
        {

            public T Deserialize<T>(string someSerialize)
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
                DataContractJsonSerializerSettings jss = new DataContractJsonSerializerSettings();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Output));
                output.SortedInputs = output.SortedInputs.Select(i => i + 0.0M).ToArray();
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

            public string Serialize<T>(T output)
            {
                throw new NotImplementedException();
            }
        }
        
    }
}
