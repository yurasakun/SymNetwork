﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymNetwork
{
  public enum NodeType { Object, Instance };//объекст и наследник

  public  class Node
    {
        /// <summary>
		/// Имя вершины
		/// </summary>
        public string Name { get; set; }
        /// <summary>
		/// ID вершины
		/// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Рёбра, которые входят в вершину
        /// </summary>
        public List<Edge> InEdges { get; set; }

        /// <summary>
        /// Рёбра, которые выходят из вершины
        /// </summary>
        public List<Edge> OutEdges { get; set; }

        /// <summary>
        /// Тип данных, который содержит вершина
        /// </summary>
        public NodeType DataType
        {
            get
            {
                //if (InEdges.Any(edge => edge.DataType == EdgeType.Is))
                if (OutEdges.Any(edge => edge.DataType == EdgeType.Is) )

                    return NodeType.Object;
                else 
                    return NodeType.Instance;

            }
        }

        /// <summary>
        /// Количество рёбер, связанных с вершиной
        /// </summary>
        public int EdgeCount
        {
            get { return InEdges.Count + OutEdges.Count; }
        }

        /// <summary>
        /// Создать вершину графа семантической сети
        /// </summary>
        /// <param name="name">Имя вершины</param>
        /// <param name="id">Id вершины</param>
        public Node(int id,string name)
        {
            Id = id;
            Name = name;
            InEdges = new List<Edge>();
            OutEdges = new List<Edge>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
