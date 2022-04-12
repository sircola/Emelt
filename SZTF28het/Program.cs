using System;
using System.Collections.Generic;

namespace SZTF28het
{
    class BinaryExpressionTree
    {
        enum Operator
        {
            Add = (int)'+',
            Sub = (int)'-',
            Mul = (int)'*',
            Div = (int)'/',
            Pow = (int)'^'
        }

        abstract class Node
        {
            public char Data { get; }
            public Node Left { get; }
            public Node Right { get; }
            
            public Node(char data, Node left, Node right)
            {
                Data = data;
                Left = left;
                Right = right;
            }

            public Node( char data ) 
                : this(data, null, null)
            {
            }
        }

        class OperandNode : Node
        {
            public OperandNode(char data) 
                : base(data)
            {
            }
            public OperandNode(char data, Node left, Node right )
                : base(data,left,right)
            {
            }
        }

        class OperatorNode : Node
        {
            public Operator Operator { get; }

            public OperatorNode(char data, Node left, Node right)
                : base(data,left,right)
            {
                Operator = (Operator)data;
            }
        }

        Node Root { get; }
        private BinaryExpressionTree(Node root)
        {
            Root = root;
        }

        public static BinaryExpressionTree Build(string expression)
        {
            return Build( expression.ToCharArray() );
        }

        public static BinaryExpressionTree Build(char[] expression)
        {
            Stack<Node> verem = new Stack<Node>();

            for( int i=0; i<expression.Length; i++ )
            {
                char c = expression[i];

                if( c >= '0' && c <= '9') 
                    verem.Push( new OperandNode(c) );
                else
                if( Enum.IsDefined(typeof(Operator),(int)c) )
                {
                    Node jobb = verem.Pop();
                    Node bal = verem.Pop();
                    verem.Push(new OperatorNode(c, bal, jobb));
                }
                else
                    throw new InvalidExpressionException(new string(expression), i);
            }

            BinaryExpressionTree fa = new BinaryExpressionTree(verem.Pop() as Node);

            return fa;
        }

        public override string ToString()
        {
            if (Root == null)
                return "";
            
            return ToString(Root);
        }

        string ToString(Node node)
        {
            if (node != null)
                return ToString(node.Left) + ToString(node.Right) + node.Data.ToString();

            return "";
        }


        public string Convert()
        {
            if (Root == null)
                return "";

            return Convert(Root);
        }

        string Convert( Node node )
        {
            if (node != null)
            {
                string s = "";

                if (node is OperatorNode)
                    s += "(";

                s += Convert(node.Left) + node.Data.ToString() + Convert(node.Right);

                if (node is OperatorNode)
                    s += ")";

                return s;
            }
            return "";
        }


        public double Evaluate()
        {
            if (Root == null)
                return 0;

            return Evaluate(Root);
        }

        double Evaluate( Node node )
        {
            if (node != null)
            {
                if (node is OperandNode)
                    return (double)node.Data - '0';

                double l = Evaluate(node.Left);
                double r = Evaluate(node.Right);
                double res = double.NaN;

                switch ((node as OperatorNode).Operator)
                {
                    case Operator.Add:
                        res = l + r;
                        break;
                    case Operator.Sub:
                        res = l - r;
                        break;
                    case Operator.Mul:
                        res = l * r;
                        break;
                    case Operator.Div:
                        res = l / r;
                        break;
                    case Operator.Pow:
                        res = Math.Pow(l, r);
                        break;
                }
                return res;
            }
            return 0;
        }
    }

    class InvalidExpressionException : Exception
    {
        public InvalidExpressionException( string expression, int position)
            : base($"Invalid character found at position: {position}, in the following expression: '{expression}'!")
        {
        }

        public override string ToString()
        {
            return $"InvalidExpressionException: {base.ToString()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinaryExpressionTree fa;
            string f = "{0, -30} = {1, -30} = {2, -30}";

            Console.WriteLine("Test 'number' is only node..");
            fa = BinaryExpressionTree.Build("7");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));

            Console.WriteLine("\nTest 'simple' operators..");
            fa = BinaryExpressionTree.Build("28+");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("28-");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("28*");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("28/");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("28^");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));

            Console.WriteLine("\nTest 'example' expressions..");
            fa = BinaryExpressionTree.Build("234*+");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(),fa.Evaluate()));
            fa = BinaryExpressionTree.Build("23*4+");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("23*45*+");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("23+45-/");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));
            fa = BinaryExpressionTree.Build("23+4*5+67^8+/");
            Console.WriteLine(String.Format(f, fa.ToString(), fa.Convert(), fa.Evaluate()));

            try
            {
                fa = BinaryExpressionTree.Build("12-3-A-45");
            }
            catch( InvalidExpressionException ex )
            {
                Console.WriteLine("\nInvalidExpressionException: " + ex.Message);
            }
        }
    }
}
