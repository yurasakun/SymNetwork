using System.Text;

namespace SymNetwork
{
    public enum EdgeType { Is, Has, Other }
    /// <summary>
    /// Ребро семантической сети
    /// </summary>
    public class Edge
    {
            /// <summary>
            /// Имя ребра
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Тип данных, который содержит ребро
            /// </summary>
            public EdgeType DataType { get; private set; }

            /// <summary>
            /// Вершина-объект
            /// </summary>
            public Node Object { get; set; }

            /// <summary>
            /// Вершина-цель
            /// </summary>
            public Node Target { get; set; }


            /// <summary>
            /// Существует ли зависимость
            /// </summary>
            public bool IsDepended = false;

            /// <summary>
            /// Значение ребра
            /// </summary>
            public string Value { get; set; }
             
            /// <summary>
            /// Построить ребро семантической сети
            /// </summary>
            /// <param name="name">Имя ребра</param>
            /// <param name="from">Откуда</param>
            /// <param name="to">Куда</param>
            /// <param name="chain">Зависимое ребро</param>
            public Edge(string name, Node from, Node to)
            {
                Name = name;
                Object = from;
                Object.OutEdges.Add(this);
                Target = to;
                if (Target != null)
                    Target.InEdges.Add(this);
                switch (name)
                {
                    case "является": DataType = EdgeType.Is; break;
                    case "имеет часть": DataType = EdgeType.Has; break;
                    default:
                        DataType = EdgeType.Other; break;
                }
                Value = "";
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(string.Format("{0} {1}", Object.Name, Name));
                if (Target != null)
                    sb.Append(" " + Target.Name);
                return sb.ToString();
            }

        }
    }