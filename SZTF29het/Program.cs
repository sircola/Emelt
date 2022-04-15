using System;
using System.Collections.Generic;
using System.IO;

namespace SZTF29het
{
    class Graph<T>
    {
        public class El
        {
            public T hova;
            public double súly;
        }

        List<T> tartalmak;
        List<List<El>> szomszédok;

        public delegate void ExternalProcessor(string item);
        public ExternalProcessor externalprocessor;

        public class GraphEventArgs
        {
            public T A;
            public T B;
        }

        public delegate void GraphEventHandler(object source, GraphEventArgs geargs);
        public event GraphEventHandler grapheventhandler;

        public Graph()
        {
            tartalmak = new List<T>();
            szomszédok = new List<List<El>>();
        }

        public void AddNode(T Node)
        {
            tartalmak.Add(Node);
            szomszédok.Add(new List<El>());
        }

        public void AddEdge(T from, T to, double súly = 1, bool irányított = false )
        {
            int i = tartalmak.IndexOf(from);
            szomszédok[i].Add(new El()
            {
                hova = to,
                súly = súly
            });

            if( !irányított )
            {
                i = tartalmak.IndexOf(to);
                szomszédok[i].Add(new El()
                {
                    hova = from,
                    súly = súly
                });
            }

            grapheventhandler?.Invoke(from,new GraphEventArgs()
            {
                A = from,
                B = to
            }
            );
        }

        public bool HasEdge( T from, T to)
        {
            List<T> l = Neighbors(from);

            foreach (T i in l)
            {
                if ( i.Equals(to) )
                {
                    return true;
                }
            }
            return false;
        }

        public List<T> Neighbors( T node )
        {
            int i = tartalmak.IndexOf(node);
            List<T> l = new List<T>();
            
            foreach (El j in szomszédok[i])
            {
                l.Add(j.hova);
            }           
            return l;
        }

        public int BFS( T honnan, T hova )
        {
            Queue<T> S = new Queue<T>();
            List<T> F = new List<T>();
            List<KeyValuePair<T, int>> d = new List<KeyValuePair<T, int>>();

            S.Enqueue(honnan);
            F.Add(honnan);
            d.Add(new KeyValuePair<T, int>(honnan, 0));

            while( S.Count != 0 )
            {
                T k = S.Dequeue();
                externalprocessor?.Invoke(k.ToString());

                foreach (T x in Neighbors(k) )
                {
                    if( !F.Contains(x) )
                    {
                        S.Enqueue(x);
                        F.Add(x);

                        int v=0;
                        foreach (KeyValuePair<T, int> i in d)
                        {
                            if (i.Key.Equals(k) )
                                v = i.Value + 1;
                        }
                        
                        if (x.Equals(hova))
                            return v;

                        d.Add(new KeyValuePair<T, int>(x, v));
                    }
                }
            }
            return 0;
        }

        void DFSRek( T k, List<T> F )
        {
            F.Add(k);
            externalprocessor?.Invoke(k.ToString());

            foreach (T x in Neighbors(k) )
            {
                if( !F.Contains(x) )
                {
                    DFSRek(x, F);
                }
            }
        }

        public void DFS( T honnan )
        {
            List<T> F = new List<T>();

            DFSRek(honnan, F);
        }
    }

    class Person : IComparable, IEquatable<Person>
    {
        public string Name { get; set; }

        public int CompareTo(object o)
        {
            Person másik = o as Person;
            return Name.CompareTo(másik.Name);
        }

        public bool Equals(Person other)
        {
            if (other == null)
                return false;

            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Graph<Person> gráf = new Graph<Person>();
            gráf.externalprocessor += delegate (string s)
            {
                Console.WriteLine(s);
                File.AppendAllText("log.txt", s);
            };
            gráf.grapheventhandler += delegate (object o, Graph<Person>.GraphEventArgs geargs)
            {
                Console.WriteLine($"{geargs.A.ToString()} -> {geargs.B.ToString()}");
            };

            gráf.AddNode(new Person() { Name = "Stew" });
            gráf.AddNode(new Person() { Name = "Joseph" });
            gráf.AddNode(new Person() { Name = "Marge" });
            gráf.AddNode(new Person() { Name = "Gerald" });
            gráf.AddNode(new Person() { Name = "Zack" });
            gráf.AddNode(new Person() { Name = "Peter" });
            gráf.AddNode(new Person() { Name = "Janet" });

            gráf.AddEdge(new Person() { Name = "Stew" }, new Person() { Name = "Joseph" });
            gráf.AddEdge(new Person() { Name = "Stew" }, new Person() { Name = "Marge" });
            gráf.AddEdge(new Person() { Name = "Marge" }, new Person() { Name = "Joseph" });
            gráf.AddEdge(new Person() { Name = "Joseph" }, new Person() { Name = "Gerald" });
            gráf.AddEdge(new Person() { Name = "Joseph" }, new Person() { Name = "Zack" });
            gráf.AddEdge(new Person() { Name = "Zack" }, new Person() { Name = "Gerald" });
            gráf.AddEdge(new Person() { Name = "Zack" }, new Person() { Name = "Peter" });
            gráf.AddEdge(new Person() { Name = "Peter" }, new Person() { Name = "Janet" });

            Console.WriteLine("\nszélességi:");
            Console.WriteLine("Janet -> Gerald: " + gráf.BFS(new Person() { Name = "Janet" }, new Person() { Name = "Gerald" }) + " fokú." );

            Console.WriteLine("\nmélységi:");
            gráf.DFS(new Person() { Name = "Stew" });
        }
    }
}
