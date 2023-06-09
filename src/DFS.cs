using System;
using System.Collections.Generic;
using System.Drawing;

namespace dfsSearching
{
    internal class Programs
    {
        static void Main(string[] args)
        {
            //char[,] arr = {{'K','R','R','R'}, {'X','R','X','X'}, {'X','T','X','X'}, {'X','R','R','T'}};
            //char[,] arr = { { 'K', 'R', 'R', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'R', 'T' } };
            //char[,] arr = { { 'T', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'R', 'X', 'X', 'X' }, { 'X', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'K', 'R', 'R', 'R', 'R', 'R', 'R' } };
            //char[,] arr = { { 'K', 'R', 'R', 'R', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'T', 'X', 'R', 'R' }, { 'X', 'T', 'X', 'R', 'X', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'T', 'X' } };
            //char[,] arr = { { 'K', 'R', 'R', 'X', 'X', 'X' }, { 'X', 'X', 'R', 'R', 'R', 'T' }, { 'T', 'R', 'R', 'X', 'X', 'R' }, { 'R', 'X', 'R', 'R', 'R', 'R' } };
            //char[,] arr = { { 'K', 'X', 'X', 'X', 'X', 'X' }, { 'R', 'R', 'X', 'X', 'X', 'X' }, { 'R', 'R', 'R', 'X', 'X', 'X' }, { 'R', 'R', 'R', 'R', 'X', 'X' }, { 'R', 'R', 'R', 'R', 'R', 'X' }, { 'R', 'R', 'R', 'R', 'R', 'T' } };
            //char[,] arr = { { 'X', 'X', 'X', 'X', 'X', 'X' }, { 'X', 'T', 'K', 'R', 'T', 'X' }, { 'X', 'X', 'X', 'X', 'X', 'X' } };
            //char[,] arr = TXT.toMatrix("TextFile4.txt");
            //Algorithm.printMatrix(arr);
            Stack<Points> treasure = new Stack<Points>();
            //treasure = Algorithm.stackTreasure(arr);
            Algorithm.printStack(treasure);
            //Points K = Algorithm.findK(arr);
            Stack<Points> visited = new Stack<Points>();
            Stack<Points> path = new Stack<Points>();
            //visited.Push(K);
            //path.Push(K);
            Algorithm.printStack(visited);
            Stack<Points> unvisited = new Stack<Points>();
            while (treasure.Count != 0)
            {
                Console.WriteLine("Visited sekarang: " + visited.Peek().x + "," + visited.Peek().y);
                Algorithm.treasureArrived(treasure, visited.Peek());
                //Algorithm.adjacentPoints(arr, visited, unvisited);
                if (treasure.Count != 0)
                {
                    Algorithm.move(visited, unvisited, path);
                }
            }
            Console.WriteLine("Jalannya lewat mana sihhh");
            Algorithm.printStack(path);
            Stack<Points> pathCopy = path;
            Points[] array = pathCopy.ToArray();
            Algorithm.printArray(array);
            int count = path.Count;
            string direction = Algorithm.printStep(path, count);
            Console.WriteLine(direction);
            Console.WriteLine();
            Console.WriteLine("Jumlah node yang dilewati: " + Algorithm.countNode(path));
            Console.ReadLine();
        }
    }

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

    public class Algorithm
    {
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
        public static string printStep(Stack<Points> s, int count)
        {
            string direction;
            if (s.Count == 1)
                return "";
            Points temp = s.Peek();
            s.Pop();
            Points temp1 = s.Peek();
            direction = printStep(s, count) + "" + checkAdjacent(temp1, temp);
            s.Push(temp);
            return direction;
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
        public static void printArray(Points[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine("(" + a[i].x + "," + a[i].y + ")");
            }
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
        public static void printMatrix(char[,] arr)
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
            int count = 0;
            Points p = visited.Peek();
            if ((p.x != 0) && (arr[p.x - 1, p.y] != 'X') && (!visited.Contains(new Points(p.x - 1, p.y))))
            {
                Points p1 = new Points(p.x - 1, p.y);
                unvisited.Push(p1);
                count++;
            }
            if ((p.y != 0) && (arr[p.x, p.y - 1] != 'X') && (!visited.Contains(new Points(p.x, p.y - 1))))
            {
                Points p1 = new Points(p.x, p.y - 1);
                unvisited.Push(p1);
                count++;
            }
            if ((p.x != arr.GetLength(0) - 1) && (arr[p.x + 1, p.y] != 'X') && (!visited.Contains(new Points(p.x + 1, p.y))))
            {
                Points p1 = new Points(p.x + 1, p.y);
                unvisited.Push(p1);
                count++;
            }
            if ((p.y != arr.GetLength(1) - 1) && (arr[p.x, p.y + 1] != 'X') && (!visited.Contains(new Points(p.x, p.y + 1))))
            {
                Points p1 = new Points(p.x, p.y + 1);
                unvisited.Push(p1);
                count++;
            }
            if (count == 0)
            {
                Console.WriteLine("BackTracking");
            }
        }
        public static void move(Stack<Points> visited, Stack<Points> unvisited, Stack<Points> path)
        {
            Points temp = unvisited.Pop();
            Stack<Points> visitedCopy = new Stack<Points> (visited);
            Reverse(visitedCopy);
            if (!checkAdjacentBool(temp, visited.Peek()))
            {
                Console.WriteLine("masuk sini");
                Points temp1 = visitedCopy.Pop();
                Points temp2 = visitedCopy.Pop();
                Points temp4 = visited.Pop();
                path.Push(temp2);
                Console.WriteLine("Temp: " + temp.x + "," + temp.y);
                while (!checkAdjacentBool(temp, temp2))
                {
                    temp2 = visitedCopy.Pop();
                    temp4 = visited.Pop();
                    path.Push(temp2);
                }
            }
            visited.Push(temp);
            path.Push(temp);
        }
        public static string checkAdjacent(Points p1, Points p2)
        {
            string direction;
            if (p1.x == p2.x + 1 && p1.y == p2.y)
                direction = "U";
            else if (p1.x == p2.x - 1 && p1.y == p2.y)
                direction = "D";
            else if (p1.x == p2.x && p1.y == p2.y + 1)
                direction = "L";
            else if (p1.x == p2.x && p1.y == p2.y - 1)
                direction = "R";
            else
                direction = "Backtracking sampai di (" + p2.x + "," + p2.y + ")";
            return direction;
        }
        public static bool checkAdjacentBool(Points p1, Points p2)
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
        public static int countNode(Stack<Points> s)
        {
            if (s.Count == 0)
                return 0;
            Points temp = s.Peek();
            s.Pop();
            countNode(s);
            if (!s.Contains(temp))
                s.Push(temp);
            return s.Count;
        }
        static void Reverse(Stack<Points> s)
        {
            if (s.Count == 0)
                return;
            Points element = s.Pop();
            Reverse(s);
            InsertAndRearrange(s, element);
        }
        static void InsertAndRearrange(Stack<Points> s, Points element)
        {
            if (s.Count == 0)
                s.Push(element);
            else
            {
                Points temp = s.Pop();
                InsertAndRearrange(s, element);
                s.Push(temp);
            }
        }
    }
}
