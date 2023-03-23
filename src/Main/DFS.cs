using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //char[,] arr = {{'K','R','R','R'}, {'X','R','X','X'}, {'X','T','X','X'}, {'X','R','R','T'}};
            //char[,] arr = { { 'K', 'R', 'R', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'R', 'T' } };
            char[,] arr = { { 'T', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'R', 'X', 'X', 'X' }, { 'X', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'K', 'R', 'R', 'R', 'R', 'R', 'R' } };
            //char[,] arr = { { 'K', 'R', 'R', 'R', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'T', 'X', 'R', 'R' }, { 'X', 'T', 'X', 'R', 'X', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'T', 'X' } };
            //char[,] arr = { { 'K', 'R', 'R', 'X', 'X', 'X' }, { 'X', 'X', 'R', 'R', 'R', 'T' }, { 'T', 'R', 'R', 'X', 'X', 'R' }, { 'R', 'X', 'R', 'R', 'R', 'R' } };
            //char[,] arr = { { 'K', 'X', 'X', 'X', 'X', 'X' }, { 'R', 'R', 'X', 'X', 'X', 'X' }, { 'R', 'R', 'R', 'X', 'X', 'X' }, { 'R', 'R', 'R', 'R', 'X', 'X' }, { 'R', 'R', 'R', 'R', 'R', 'X' }, { 'R', 'R', 'R', 'R', 'R', 'T' } };
            //char[,] arr = { { 'X', 'X', 'X', 'X', 'X', 'X' }, { 'X', 'T', 'K', 'R', 'T', 'X' }, { 'X', 'X', 'X', 'X', 'X', 'X' } };
            Algorithm.printMatrix(arr);
            Stack<Point> treasure = new Stack<Point>();
            treasure = Algorithm.stackTreasure(arr);
            Algorithm.printStack(treasure);
            //Point p = new Point(1, 3);
            //treasureArrived(treasure, p);
            //printStack(treasure);
            Point K =  Algorithm.findK(arr);
            Stack<Point> visited = new Stack<Point>();
            visited.Push(K);
            Algorithm.printStack(visited);
            Stack<Point> unvisited = new Stack<Point>();
            Stack<Point> backtracking = new Stack<Point>();
            Stack<int> countNode = new Stack<int>();
            int countBacktracking = 0;
            while (treasure.Count != 0)
            {
                Console.WriteLine("Visited sekarang: " + visited.Peek().x + "," + visited.Peek().y);
                Algorithm.treasureArrived(treasure, visited.Peek());
                Console.WriteLine("Treasure skrg bgt");
                Algorithm.printStack(treasure);
                countBacktracking = Algorithm.adjacentPoint(arr, visited, unvisited, backtracking, countBacktracking, countNode);
                //Console.WriteLine("Backtracking skrg");
                //Algorithm.printStack(backtracking);
                //Console.WriteLine("Unvisited skrg");
                //Algorithm.printStack(unvisited);
                //Console.WriteLine("Visited skrg");
                //Algorithm.printStack(visited);
                if (treasure.Count != 0)
                {
                    Algorithm.move(visited, unvisited);
                }
            }
            Console.WriteLine("Jalannya lewat mana sihhh");
            Algorithm.printStack(visited);
            int count = visited.Count;
            string direction = Algorithm.printStep(visited, count);
            Console.WriteLine(direction);
            Console.WriteLine();
            Console.WriteLine("Jumlah node yang dilewati: " + Algorithm.countNode(visited));
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
        public static int adjacentPoint(char[,] arr, Stack<Point> visited, Stack<Point> unvisited, Stack<Point> backtracking, int countBacktracking, Stack<int> countNode)
        {
            //Console.WriteLine("count" + countBacktracking);
            Stack<Point> treasure = new Stack<Point>();
            treasure = Algorithm.stackTreasure(arr);
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
            if (backtracking.Count != 0 && count != 0)
            {
                countBacktracking++;
                backtracking.Push(p);
                //Console.WriteLine("Masuk sini");
            }
            if (count > 1)
            {
                if (countBacktracking != 0)
                {
                    countNode.Push(countBacktracking);
                }
                Console.WriteLine("Simpangan");
                countBacktracking = 1;
                backtracking.Push(p);
                
            }
            Algorithm.treasureArrived(treasure, visited.Peek());
            if (count == 0 && treasure.Count != 0)
            {
                if (countBacktracking != 0)
                {
                    countNode.Push(countBacktracking);
                    int tempCount = countNode.Pop();
                    while (tempCount != 0)
                    {
                        Point temp = backtracking.Pop();
                        visited.Push(temp);
                        tempCount--;
                    }
                    if (backtracking.Count != 0)
                    {
                        Point temp = visited.Peek();
                        Point temp1 = backtracking.Peek();
                        if (checkAdjacentBool(temp,temp1))
                        {
                            int tempCount1 = countNode.Pop();
                            while (tempCount1 != 0)
                            {
                                Point temp2 = backtracking.Pop();
                                visited.Push(temp2);
                                tempCount1--;
                            }
                        }
                    }
                }
                Console.WriteLine("Backtraking nih");
                countBacktracking = 0;
            }
            return countBacktracking;
        }

        public static void move(Stack<Point> visited, Stack<Point> unvisited)
        {
            Point temp = unvisited.Pop();
            visited.Push(temp);
        }
        public static string checkAdjacent(Point p1, Point p2)
        {
            string direction;
            if (p1.x == p2.x + 1 && p1.y == p2.y)
                //Console.Write("U");
                direction = "U";
            else if (p1.x == p2.x - 1 && p1.y == p2.y)
                //Console.Write("D");
                direction = "D";
            else if (p1.x == p2.x && p1.y == p2.y + 1)
                //Console.Write("L");
                direction = "L";
            else if (p1.x == p2.x && p1.y == p2.y - 1)
                //Console.Write("R");
                direction = "R";
            else
                //Console.Write("Backtracking sampai di (" + p2.x + "," + p2.y + ")");
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
        public static int countNode (Stack<Point> s)
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
    }
}

