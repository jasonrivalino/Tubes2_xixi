using System;
using System.Collections.Generic;
using System.Drawing;

namespace dfsSearching
{
    internal class Program
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
            char[,] arr = TXT.toMatrix("TextFile4.txt");
            Algorithm.printMatrix(arr);
            Stack<Point> treasure = new Stack<Point>();
            treasure = Algorithm.stackTreasure(arr);
            Algorithm.printStack(treasure);
            Point K = Algorithm.findK(arr);
            Stack<Point> visited = new Stack<Point>();
            Stack<Point> path = new Stack<Point>();
            visited.Push(K);
            path.Push(K);
            Algorithm.printStack(visited);
            Stack<Point> unvisited = new Stack<Point>();
            while (treasure.Count != 0)
            {
                Console.WriteLine("Visited sekarang: " + visited.Peek().x + "," + visited.Peek().y);
                Algorithm.treasureArrived(treasure, visited.Peek());
                Algorithm.adjacentPoint(arr, visited, unvisited);
                if (treasure.Count != 0)
                {
                    Algorithm.move(visited, unvisited, path);
                }
            }
            Console.WriteLine("Jalannya lewat mana sihhh");
            Algorithm.printStack(path);
            Stack<Point> pathCopy = path;
            Point[] array = pathCopy.ToArray();
            Algorithm.printArray(array);
            int count = path.Count;
            string direction = Algorithm.printStep(path, count);
            Console.WriteLine(direction);
            Console.WriteLine();
            Console.WriteLine("Jumlah node yang dilewati: " + Algorithm.countNode(path));
            Console.ReadLine();
        }
    }

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point()
        {
            x = 0;
            y = 0;
        }
        public Point(int x, int y)
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

            Point temp = (Point)obj;
            return (x == temp.x) && (y == temp.y);
        }
        public override int GetHashCode()
        {
            return (x + y).GetHashCode();
        }
    }

    public class Algorithm
    {
        public static Stack<Point> stackTreasure(char[,] arr)
        {
            Stack<Point> treasure = new Stack<Point>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 'T')
                    {
                        treasure.Push(new Point(i, j));
                    }
                }
            }
            return treasure;
        }
        public static string printStep(Stack<Point> s, int count)
        {
            string direction;
            if (s.Count == 1)
                return "";
            Point temp = s.Peek();
            s.Pop();
            Point temp1 = s.Peek();
            direction = printStep(s, count) + "" + checkAdjacent(temp1, temp);
            s.Push(temp);
            return direction;
        }
        public static void printStack(Stack<Point> s)
        {
            if (s.Count == 0)
                return;
            Point temp = s.Peek();
            s.Pop();
            printStack(s);
            Console.WriteLine(temp.x + "," + temp.y);
            s.Push(temp);
        }
        public static void printArray(Point[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine("(" + a[i].x + "," + a[i].y + ")");
            }
        }
        public static void treasureArrived(Stack<Point> s, Point p)
        {
            if (s.Count == 0)
                return;
            Point temp = s.Peek();
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
        public static Point findK(char[,] arr)
        {
            Point p = new Point();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == 'K')
                    {
                        p = new Point(i, j);
                    }
                }
            }
            return p;
        }
        public static void adjacentPoint(char[,] arr, Stack<Point> visited, Stack<Point> unvisited)
        {
            int count = 0;
            Point p = visited.Peek();
            if ((p.x != 0) && (arr[p.x - 1, p.y] != 'X') && (!visited.Contains(new Point(p.x - 1, p.y))))
            {
                Point p1 = new Point(p.x - 1, p.y);
                unvisited.Push(p1);
                count++;
            }
            if ((p.y != 0) && (arr[p.x, p.y - 1] != 'X') && (!visited.Contains(new Point(p.x, p.y - 1))))
            {
                Point p1 = new Point(p.x, p.y - 1);
                unvisited.Push(p1);
                count++;
            }
            if ((p.x != arr.GetLength(0) - 1) && (arr[p.x + 1, p.y] != 'X') && (!visited.Contains(new Point(p.x + 1, p.y))))
            {
                Point p1 = new Point(p.x + 1, p.y);
                unvisited.Push(p1);
                count++;
            }
            if ((p.y != arr.GetLength(1) - 1) && (arr[p.x, p.y + 1] != 'X') && (!visited.Contains(new Point(p.x, p.y + 1))))
            {
                Point p1 = new Point(p.x, p.y + 1);
                unvisited.Push(p1);
                count++;
            }
            if (count == 0)
            {
                Console.WriteLine("BackTracking");
            }
        }
        public static void move(Stack<Point> visited, Stack<Point> unvisited, Stack<Point> path)
        {
            Point temp = unvisited.Pop();
            Stack<Point> visitedCopy = new Stack<Point> (visited);
            Reverse(visitedCopy);
            if (!checkAdjacentBool(temp, visited.Peek()))
            {
                Console.WriteLine("masuk sini");
                Point temp1 = visitedCopy.Pop();
                Point temp2 = visitedCopy.Pop();
                Point temp4 = visited.Pop();
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
        public static string checkAdjacent(Point p1, Point p2)
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
        public static bool checkAdjacentBool(Point p1, Point p2)
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
        public static int countNode(Stack<Point> s)
        {
            if (s.Count == 0)
                return 0;
            Point temp = s.Peek();
            s.Pop();
            countNode(s);
            if (!s.Contains(temp))
                s.Push(temp);
            return s.Count;
        }
        static void Reverse(Stack<Point> s)
        {
            if (s.Count == 0)
                return;
            Point element = s.Pop();
            Reverse(s);
            InsertAndRearrange(s, element);
        }
        static void InsertAndRearrange(Stack<Point> s, Point element)
        {
            if (s.Count == 0)
                s.Push(element);
            else
            {
                Point temp = s.Pop();
                InsertAndRearrange(s, element);
                s.Push(temp);
            }
        }
    }
}

