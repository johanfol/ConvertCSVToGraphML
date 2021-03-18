using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var trans = File.ReadAllLines("C:\\source\\github\\gremlin\\src\\sample-files\\ml_transactions_list_payer_to_payee.csv");
            List<string> nodesAdded = new List<string>();
            List<string> transAdded = new List<string>();
            List<string> colNames;
            StringBuilder xmlString = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
"<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"\r\n" +
"    xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" +
"    xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\">\r\n" +
"  <key id=\"labelE\" for=\"edge\" attr.name=\"labelE\" attr.type=\"string\"></key>\r\n" +
"  <graph id=\"G\">\r\n");

            colNames = trans[0].Split(",").ToList();
            for (int i = 1; i < trans.Length; i++)
            {
                var cols = trans[i].Split("\",\"");
                var payerIDNo = colNames.IndexOf("PayerIDNumber3");
                var payeeIDNo = colNames.IndexOf("PayeeIDNumber3");
                var transID = colNames.IndexOf("TransactionID");

                if (!nodesAdded.Contains(cols[payerIDNo])) {
                    xmlString.Append($"    <node id=\"{cols[payerIDNo]}\"/>\r\n");
                    nodesAdded.Add(cols[payerIDNo]);
                }
                if (!nodesAdded.Contains(cols[payeeIDNo]))
                {
                    xmlString.Append($"    <node id=\"{cols[payeeIDNo]}\"/>\r\n");
                    nodesAdded.Add(cols[payeeIDNo]);
                }
                if (!transAdded.Contains(cols[transID]))
                {
                    xmlString.Append($"    <edge id=\"{cols[transID]}\" source=\"{cols[payerIDNo]}\" target=\"{cols[payeeIDNo]}\">\r\n      <data key=\"labelE\">pays</data>\r\n    </edge>\r\n");
                    transAdded.Add(cols[transID]);
                }
            }

            xmlString.Append("  </graph>\r\n</graphml>");
            File.WriteAllText("C:\\source\\github\\csv-to-graphson\\sample-files\\graphmc.xml", xmlString.ToString());
            Console.WriteLine("DONE.");
            Console.ReadLine();
        }
    }
}
