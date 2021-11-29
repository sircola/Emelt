using System.IO;

namespace Matt
{
    public class TreeCalculator
    {
        private Verem verem;
        private Node root;
        private Node iterator;
        private Node.EdgeType edgeType;

        public long GetNumberOfPossibleCombinations(string[] input)
        {
            verem = new Verem();
            root = new Node(null);
            iterator = root;
            edgeType = Node.EdgeType.IfEdge;

            for (int i = 2; i < input.Length - 1; i++)
            {
                ProcessInstruction(input[i]);
            }

            return root.GetNodeValue() - 1; //root node has only one direction, the other must be subtracted.
        }

        private void ProcessInstruction(string instruction)
        {
            if (instruction == "if")
            {
                ExecuteInstructionIf();
            }
            else if (instruction == "else")
            {
                ExecuteInstructionElse();
            }
            else if (instruction == "endif")
            {
                ExecuteInstructionEndif();
            }
            else
            {
                // This shouldn't happen
            }
        }

        private void ExecuteInstructionIf()
        {
            verem.Push(edgeType);
            verem.Push(Node.EdgeType.IfEdge);

            iterator = iterator.AddNode(edgeType);
            edgeType = Node.EdgeType.IfEdge;
        }

        private void ExecuteInstructionElse()
        {
            edgeType = Node.EdgeType.ElseEdge;
        }

        private void ExecuteInstructionEndif()
        {
            verem.Pop();
            edgeType = verem.Pop();

            iterator = iterator.GetParent();
        }
    }

    class Verem
    {
        Node.EdgeType[] typeStack = new Node.EdgeType[0];
        int count = 0;

        public Node.EdgeType Pop()
        {
            count--;
            return typeStack[count];
        }

        public void Push(Node.EdgeType edgeType)
        {
            if (count == typeStack.Length)
            {
                ExtendVeremByOne();
            }

            typeStack[count] = edgeType;
            count++;
        }

        private void ExtendVeremByOne()
        {
            Node.EdgeType[] newTypeStack = new Node.EdgeType[typeStack.Length + 1];

            for (int i = 0; i < typeStack.Length; i++)
            {
                newTypeStack[i] = typeStack[i];
            }

            typeStack = newTypeStack;
        }
    }

    class Node
    {
        public enum EdgeType
        {
            IfEdge = 0,
            ElseEdge,
            EdgeTypeLength,
        }

        private readonly Node parent;
        private readonly Node[][] nodes;

        public Node(Node parent)
        {
            this.parent = parent;
            nodes = new Node[(int)EdgeType.EdgeTypeLength][];
        }

        public Node AddNode(EdgeType edgeType)
        {
            int type = (int)edgeType;

            ExtendNodesByOne(type);

            nodes[type][nodes[type].Length - 1] = new Node(this);

            return nodes[type][nodes[type].Length - 1];
        }

        public Node GetParent()
        {
            return this.parent;
        }

        public long GetNodeValue()
        {
            long sum = 0;

            for (EdgeType i = 0; i < EdgeType.EdgeTypeLength; i++)
            {
                sum += GetNodeValueByType(i);
            }

            return sum;
        }

        private long GetNodeValueByType(EdgeType edgeType)
        {
            long sum = 1;
            int type = (int)edgeType;

            if (nodes[type] != null)
            {
                for (int i = 0; i < nodes[type].Length; i++)
                {
                    sum *= nodes[type][i].GetNodeValue();
                }
            }

            return sum;
        }

        private void ExtendNodesByOne(int type)
        {
            if (nodes[type] == null)
            {
                nodes[type] = new Node[0];
            }

            Node[] newNode = new Node[nodes[type].Length + 1];

            for (int i = 0; i < nodes[type].Length; i++)
            {
                newNode[i] = nodes[type][i];
            }

            nodes[type] = newNode;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string inputFileName = "input.txt";
            string outputFileName = "output.txt";

            long result = new TreeCalculator().GetNumberOfPossibleCombinations(File.ReadAllLines(inputFileName));

            File.WriteAllText(outputFileName, result.ToString());
        }
    }
}
