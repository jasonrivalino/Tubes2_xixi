using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dfsSearching
{
    public class Points
    {
        public int x { get; set; }
        public int y { get; set; }

        public Points()
        {
            x = 0;
            y = 0;
        }
        public Points(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Points temp = (Points)obj;
            return (x == temp.x) && (y == temp.y);
        }
        public override int GetHashCode()
        {
            return (x + y).GetHashCode();
        }

    }
    internal class Programs
    {
        static void Main(string[] args)
        {
            //char[,] arr = {{'K','R','R','R'}, {'X','R','X','X'}, {'X','T','X','X'}, {'X','R','R','T'}};
            //char[,] arr = { { 'K', 'R', 'R', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'R', 'T' } };
            char[,] arr = { { 'T', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'R', 'X', 'X', 'X' }, { 'X', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'K', 'R', 'R', 'R', 'R', 'R', 'R' } };
            //char[,] arr = { { 'K', 'R', 'R', 'R', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'T', 'X', 'R', 'R' }, { 'X', 'T', 'X', 'R', 'X', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'T', 'X' } };
            //char[,] arr = { { 'K', 'R', 'R', 'X', 'X', 'X' }, { 'X', 'X', 'R', 'R', 'R', 'T' }, { 'T', 'R', 'R', 'X', 'X', 'R' }, { 'R', 'X', 'R', 'R', 'R', 'R' } };
            //char[,] arr = { { 'K', 'X', 'X', 'X', 'X', 'X' }, { 'R', 'R', 'X', 'X', 'X', 'X' }, { 'R', 'R', 'R', 'X', 'X', 'X' }, { 'R', 'R', 'R', 'R', 'X', 'X' }, { 'R', 'R', 'R', 'R', 'R', 'X' }, { 'R', 'R', 'R', 'R', 'R', 'T' } };
            printMatrix(arr);
            Stack<Points> treasure = new Stack<Points>();
            treasure = stackTreasure(arr);
            printStack(treasure);
            //Points p = new Points(1, 3);
            //treasureArrived(treasure, p);
            //printStack(treasure);
            Points K = findK(arr);
            Stack<Points> visited = new Stack<Points>();
            visited.Push(K);
            printStack(visited);
            Stack<Points> unvisited = new Stack<Points>();
            while (treasure.Count != 0)
            {
                Console.WriteLine("Visited sekarang: " + visited.Peek().x + "," + visited.Peek().y);
                treasureArrived(treasure, visited.Peek());
                Console.WriteLine("Treasure skrg bgt");
                printStack(treasure);
                adjacentPoints(arr, visited, unvisited);
                Console.WriteLine("Unvisited skrg");
                printStack(unvisited);
                if (treasure.Count != 0)
                {
                    move(visited, unvisited);
                }
            }
            Console.WriteLine("Jalannya lewat mana sihhh");
            printStack(visited);
            int count = visited.Count;
            printStep(visited, count);
            Console.ReadLine();
        }
        public static Stack<Points> stackTreasure(char[,] arr)
        {
            Stack<Points> treasure = new Stack<Points>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 'T')
                    {
                        treasure.Push(new Points(i, j));
                    }
                }
            }
            return treasure;
        }
        static void printStep(Stack<Points> s, int count)
        {
            if (s.Count == 1)
                return;
            Points temp = s.Peek();
            s.Pop();
            Points temp1 = s.Peek();
            printStep(s, count);
            checkAdjacent(temp1, temp);
            if (s.Count != count-1)
                Console.Write(" - ");
            s.Push(temp);
        }
        public static void printStack(Stack<Points> s)
        {
            if (s.Count == 0)
                return;
            Points temp = s.Peek();
            s.Pop();
            printStack(s);
            Console.WriteLine(temp.x + "," + temp.y);
            s.Push(temp);
        }
        public static void treasureArrived(Stack<Points> s, Points p)
        {
            if (s.Count == 0)
                return;
            Points temp = s.Peek();
            s.Pop();
            treasureArrived(s, p);
            if ((p.x != temp.x) | (p.y != temp.y))
            {
                s.Push(temp);
            }       
        }
        static void printMatrix(char[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static Points findK(char[,] arr)
        {
            Points p = new Points();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 'K')
                    {
                        p = new Points(i, j);
                    }
                }
            }
            return p;
        }
        public static void adjacentPoints(char[,] arr, Stack<Points> visited, Stack<Points> unvisited)
        {
            Points p = visited.Peek();
            if ((p.x != 0) && (arr[p.x - 1, p.y] != 'X') && (!visited.Contains(new Points(p.x - 1, p.y))))
            {
                Points p1 = new Points(p.x - 1, p.y);
                unvisited.Push(p1);
            }
            if ((p.y != 0) && (arr[p.x, p.y - 1] != 'X') && (!visited.Contains(new Points(p.x, p.y - 1))))
            {
                Points p1 = new Points(p.x, p.y - 1);
                unvisited.Push(p1);
            }
            if ((p.x != arr.GetLength(0) - 1) && (arr[p.x + 1, p.y] != 'X') && (!visited.Contains(new Points(p.x + 1, p.y))))
            {
                Points p1 = new Points(p.x + 1, p.y);
                unvisited.Push(p1);
            }
            if ((p.y != arr.GetLength(1) - 1) && (arr[p.x, p.y + 1] != 'X') && (!visited.Contains(new Points(p.x, p.y + 1))))
            {
                Points p1 = new Points(p.x, p.y + 1);
                unvisited.Push(p1);
            } 
            else
            {
                Console.WriteLine("Bactraking nih");
            }
        }
        public static void move(Stack<Points> visited, Stack<Points> unvisited)
        {
            Points temp = unvisited.Pop();
            visited.Push(temp);
        }
        static void checkAdjacent(Points p1, Points p2)
        {
            if (p1.x == p2.x + 1 && p1.y == p2.y)
                Console.Write("U");
            else if (p1.x == p2.x - 1 && p1.y == p2.y)
                Console.Write("D");
            else if (p1.x == p2.x && p1.y == p2.y + 1)
                Console.Write("L");
            else if (p1.x == p2.x && p1.y == p2.y - 1)
                Console.Write("R");
            else
                Console.Write("Backtracking sampai di (" + p2.x + "," + p2.y + ")");
        }
        /*
        static bool checkAdjacentBool(Points p1, Points p2)
        {
            bool flag = false;
            if (p1.x == p2.x + 1 && p1.y == p2.y)
                flag = true;
            else if (p1.x == p2.x - 1 && p1.y == p2.y)
                flag = true;
            else if (p1.x == p2.x && p1.y == p2.y + 1)
                flag = true;
            else if (p1.x == p2.x && p1.y == p2.y - 1)
                flag = true;
            return flag;
        }
        */
    }
}
