using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace RIAT_LABS
{
    class Program
    {  
        static void Main(string[] args)
        {
            string serializeType = Console.ReadLine();
            string someText = Console.ReadLine();
            if (!String.IsNullOrEmpty(someText))
            {
                Lab1.Input li=new Lab1.Input();
                Lab1.Output output = new Lab1.Output();
                if (serializeType=="Json")
                {
                    Lab1.JSONSerialize json = new Lab1.JSONSerialize();
                    li = json.Deserialize <Lab1.Input>(someText);
                    output = Lab1.InputToOutput.MakeOutputFromInput(li);
                    Console.WriteLine(json.Serialize(output));
                }
                else
                {
                    Lab1.XMLSerialize xml = new Lab1.XMLSerialize();
                    li = xml.Deserialize<Lab1.Input>(someText);
                    output = Lab1.InputToOutput.MakeOutputFromInput(li);
                    Console.WriteLine(xml.Serialize(output));
                }

            }
        }

        
    }
}
