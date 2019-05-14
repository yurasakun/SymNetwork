using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymNetwork
{
    class Graph
    {
        public List<Node> Vertexts;
        List<Edge> Edges;
        List<Arc> Arcs;
        public Graph(string Data)
        {
            Vertexts = new List<Node>();
            Edges = new List<Edge>();
            Arcs = new List<Arc>();
            ParseData(Data);
            AddNewRealtation();
        }
        /// <summary>
        /// Поиск вершины по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Node FindNode(string name)
        {
            return Vertexts.Find(new Predicate<Node>(x => { return name == x.Name; }));
        }
        /// <summary>
        /// Поиск вершины по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Node FindNode(int id)
        {
            return Vertexts.Find(new Predicate<Node>(x => { return id == x.Id; }));
        }
        /// <summary>
        /// Поиск дуги по айди
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Arc FindArc(int id)
        {
            return Arcs.Find(new Predicate<Arc>(x => { return id == x.ID; }));
        }
        /// <summary>
        /// Поиск ребра по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Edge FindEdge(string name)
        {
            return Edges.Find(new Predicate<Edge>(x => { return name == x.Name; }));
        }
        private void ParseData(string fileName)
        {

            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            StreamReader streamReader = new StreamReader(path);
            int block = 0;
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine()?.Trim();
                if (String.Compare(line, "#1", StringComparison.Ordinal) == 0)
                {
                    block = 1;
                    continue;
                }
                if (String.Compare(line, "#2", StringComparison.Ordinal) == 0)
                {
                    block = 2;
                    continue;
                }
                if (String.Compare(line, "#3", StringComparison.Ordinal) == 0)
                {
                    block = 3;
                    continue;
                }
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }
                var resourceLine =
                        line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(el => el.Trim())
                        .ToArray();
                if (block == 1 && resourceLine.Length == 2)
                {
                    Vertexts.Add(new Node(Convert.ToInt32(resourceLine[0]), resourceLine[1]));
                    continue;
                }
                if (block == 2 && resourceLine.Length == 3)
                {
                    var code = Convert.ToInt32(resourceLine[0]);
                    Arcs.Add(new Arc(code, resourceLine[1]));
                    continue;
                }
                if (block == 3 && resourceLine.Length == 3)
                {

                    Edges.Add(new Edge(FindArc(Convert.ToInt32(resourceLine[1])).Name, FindNode(Convert.ToInt32(resourceLine[0])), FindNode(Convert.ToInt32(resourceLine[2]))));
                    continue;
                }
                throw new FormatException($"Incorrect format. line:\n{line}");
            }
            streamReader.Close();
        }
        /// <summary>
        /// Нахождения всех связей
        /// </summary>
        void AddNewRealtation() {
            foreach (var Noode in Vertexts)
            {
                foreach (var NoodeObject in Noode.OutEdges)
                {
                    var Object = NoodeObject.Target;
                        foreach (var NoodeIst in Noode.InEdges)
                        {

                        //    Console.WriteLine(NoodeIst.Object.Name + " " + NoodeObject.Object.DataType +" "+ NoodeObject.Name + " " +Object );
                              Edges.Add(new Edge(NoodeObject.Name,NoodeIst.Object, Object));

                    }
                    
                }
            }

        }
        /// <summary>
        /// нахождения связей если id:?:? or ?:?:?
        /// </summary>
        /// <param name="id">Айди вершины</param>
        /// <returns></returns>
        public string GetDefinition(string id)
        {
            Node n = FindNode(Convert.ToInt32(id));
            try
            {
                if (n.OutEdges.Count == 0)
                    return " ";
                else
                {
                    StringBuilder def = new StringBuilder();
                    StringBuilder is_str = new StringBuilder();
                    StringBuilder has_str = new StringBuilder();
                    StringBuilder act_str = new StringBuilder();
                    bool has_is = false, has_has = false, has_act = false;
                    foreach (Edge edge in n.OutEdges)
                    {
                        switch (edge.DataType)
                        {
                            case EdgeType.Is:
                                {
                                    is_str.AppendFormat("{0} является {1}",edge.Object.Name, edge.Target.Name);
                                    has_is = true;
                                    break;
                                }
                            case EdgeType.Has:
                                {
                                    if (has_has)
                                        has_str.AppendFormat("\n{0} {1} {2}", edge.Object.Name, edge.Name, edge.Target.Name);
                                    else has_str.AppendFormat("\n{0} {1} {2}", edge.Object.Name, edge.Name, edge.Target.Name);
                                    has_has = true;
                                    break;
                                }
                            default:
                                {
                                    if (edge.Target == null)
                                        break;
                                    if (has_act)
                                        act_str.Append(", ");
                                    act_str.AppendFormat("{0} {1} {2}",edge.Object.Name, edge.Name, edge.Target.Name);
                                    has_act = true;
                                    break;
                                }
                        }
                    }
                    if (has_is && (has_has || has_act))
                        def.Append(is_str.ToString());
                    else def.Append(is_str.ToString());
                    if (has_has)
                        def.Append(has_str.ToString());
                    if (has_has && has_act)
                        def.Append("\n");
                    if (has_act)
                        def.Append(act_str.ToString());
                    return def.ToString().Replace("  ", " ");
                }
            }
            catch (NullReferenceException)
            {
                return "Object Not Found";
            }
        }
        /// <summary>
        /// нахождения связей если ? : id : ?
        /// </summary>
        /// <param name="id">Айди Дуги</param>
        /// <returns></returns>
        public string GetDefinition(int id, string idNodeOb=null, int idNodeIst=0)
        {
            string Answer = "";
            foreach (var Noode in Vertexts)
            {
                foreach (var realtation in Noode.OutEdges)
                {
                    if (realtation.Name == FindArc(id).Name)
                    {
                        Answer += realtation.Object.Name + " " + realtation.Name + " " + realtation.Target.Name + "\n";
                    }
                }
            }


            if (Answer.Length != 0)
            {
                return Answer;
            }

            return "Таких связей нет";
        }
        /// <summary>
        /// нахождения связей если ? : ? : idNodeIst
        /// </summary>
        /// <param name="idNodeIst"></param>
        /// <param name="id"></param>
        /// <param name="idNodeOb"></param>
        /// <returns></returns>
        public string GetDefinition(string idNodeIst, int id = 0, string idNodeOb = null)
        {
            string Answer = "";
            foreach (var EdgeIn in FindNode(Convert.ToInt32(idNodeIst)).InEdges)
            {
                if (EdgeIn.Target.Id == Convert.ToInt32(idNodeIst))
                {
                    Answer += EdgeIn.Object.Name + " " + EdgeIn.Name + " " + EdgeIn.Target.Name + "\n";
                }
            }


            if (Answer.Length != 0)
            {
                return Answer;
            }

            return "Таких связей нет";
        }
        /// <summary>
        /// нахождения связей если idNode:idArc:idNodeInst
        /// </summary>
        /// <param name="idNode"></param>
        /// <param name="idArc"></param>
        /// <param name="idNodeInst"></param>
        /// <returns></returns>
        public string GetDefinition(string idNode, string idArc, string idNodeInst)
        {
            if (FindNode(Convert.ToInt32(idNode)) != null
                && FindEdge(FindArc(Convert.ToInt32(idArc)).Name) != null &&
                FindEdge(FindArc(Convert.ToInt32(idArc)).Name).Target.Id == Convert.ToInt32(idNodeInst))
            {
                return "Yes";
            }
            return "no";
        }
        /// <summary>
        /// нахождения связей если idNode:idArc:?
        /// </summary>
        /// <param name="idNode"></param>
        /// <param name="idArc"></param>
        /// <returns></returns>
        public string GetDefinition(string idNode, string idArc)
        {
            var Answer = "";
            foreach (var edge in FindNode(Convert.ToInt32(idNode)).OutEdges)
            {
                if (edge.Name == FindArc(Convert.ToInt32(idArc)).Name)
                {
                    Answer+= edge.Object.Name + " " + edge.Name +" "+edge.Target.Name +"\n";
                }
            }
            if (Answer.Length != 0)
            {
                return Answer;
            }

            return "Такого отношения нет";
        }
        /// <summary>
        /// нахождения связей если ?:idArc:idNodeIst
        /// </summary>
        /// <param name="idArc">Айди дуг</param>
        /// <param name="idNode">Айди наслдника вершины</param>
        /// <returns></returns>
        public string GetDefinition( int idArc, int idNode)
        {
            string Answer = "";
            foreach (var Eges in FindNode(idNode).InEdges)
            {
                if (Eges.Name == FindArc(idArc).Name && Eges.Target.Id == idNode)
                {
                    Answer += Eges.Object.Name + " " + Eges.Name + " " + Eges.Target.Name + "\n";
                }
            }
            if (Answer.Length != 0)
            {
                return Answer;
            }

            return "Таких связей нет";
        }
        /// <summary>
        /// нахождения связей если idNodeOb : ? : idNodeIst
        /// </summary>
        /// <param name="idNodeOb"></param>
        /// <param name="idNodeIst"></param>
        /// <returns></returns>
        public string GetDefinition( string idNodeOb, int idNodeIst )
        {
            string Answer = "";
            foreach (var NoodeObj in FindNode(Convert.ToInt32(idNodeOb)).OutEdges)
            {
               if(NoodeObj.Object.Id==Convert.ToInt32(idNodeOb) && NoodeObj.Target.Id == idNodeIst)
                    Answer += NoodeObj.Object.Name + " " + NoodeObj.Name + " " + NoodeObj.Target.Name + "\n";
               
            }


            if (Answer.Length != 0)
            {
                return Answer;
            }

            return "Таких связей нет";
        }

    }
   
}
    

