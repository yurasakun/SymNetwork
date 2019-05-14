using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph("data.txt");
            while (true)
            {
                Console.WriteLine("==================ВВедите запрос по примеру, без Пробела==================");
                Console.WriteLine("==================Пример запроса: ?:?:?  ==================");
                string Query = Console.ReadLine();
                String[] QueryString = Query.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (QueryString[0] == "?" && QueryString[1] == "?" && QueryString[2] == "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    foreach (var Nodes in graph.Vertexts)
                    {
                        Console.WriteLine(graph.GetDefinition(Convert.ToString(Nodes.Id)));
                    }
                }
                else if (QueryString[0] != "?" && QueryString[1] == "?" && QueryString[2] == "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(QueryString[0]));
                }
                else if (QueryString[0] != "?" && QueryString[1] != "?" && QueryString[2] != "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(QueryString[0], QueryString[1], QueryString[2]));
                }
                else if (QueryString[0] != "?" && QueryString[1] != "?" && QueryString[2] == "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(QueryString[0], QueryString[1]));
                }
                else if (QueryString[0] == "?" && QueryString[1] != "?" && QueryString[2] != "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(Convert.ToInt32(QueryString[1]), Convert.ToInt32(QueryString[2])));
                }
                else if (QueryString[0] != "?" && QueryString[1] == "?" && QueryString[2] != "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(QueryString[0], Convert.ToInt32(QueryString[2])));
                }
                else if (QueryString[0] == "?" && QueryString[1] != "?" && QueryString[2] == "?")
                {
                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(Convert.ToInt32(QueryString[1])));
                }
                else if (QueryString[0] == "?" && QueryString[1] == "?" && QueryString[2] != "?")
                {

                    Console.WriteLine("===================Ответ:==================");
                    Console.WriteLine(graph.GetDefinition(QueryString[2], 0, null));
                }
                //string S = Console.ReadLine();
            }





            
            Console.ReadKey();
        }
    }
}
