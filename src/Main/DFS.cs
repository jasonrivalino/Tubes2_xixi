using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //char[,] arr = {{'K','R','R','R'}, {'X','R','X','X'}, {'X','T','X','X'}, {'X','R','R','T'}};
            //char[,] arr = { { 'K', 'R', 'R', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'X', 'R' }, { 'X', 'R', 'R', 'T' } };
            char[,] arr = { { 'T', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'R', 'X', 'X', 'X' }, { 'X', 'R', 'R', 'T', 'R', 'R', 'X' }, { 'X', 'R', 'X', 'X', 'X', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'X', 'R', 'X', 'X', 'R', 'X', 'X' }, { 'K', 'R', 'R', 'R', 'R', 'R', 'R' } };
            Stack<char> myStack = new Stack<char>();
            printMatrix(arr);
            int[] point = findK(arr);
            Console.WriteLine(point[0] + "," + point[1]);
            int count = countT(arr);
            Console.WriteLine("Jumlah treasure ada " + count);
            int step = 0; 
            while (count != 0)
            {
                move(arr, point, myStack);
                printMatrix(arr);
                Console.WriteLine(point[0] + "," + point[1]);
                Console.WriteLine("Jumlah treasure ada  " + count);
                if (arr[point[0], point[1]] == 'T') {
                    count--;
                }
                step++;
            }
            //Console.WriteLine("Hello World");
            printStep(myStack);
            Console.WriteLine("Jumlah step ada " + step);
            Console.ReadLine();
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
        static void printStep(Stack<char> myStack)
        {
            while (myStack.Count > 0)
                Console.Write(myStack.Pop());
            Console.WriteLine();
        }
        static int[] findK(char[,] arr)
        {
            int[] point = new int[2];
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    if (arr[i, j] == 'K')
                    {
                        point[0] = i;
                        point[1] = j;
                    }
                }
            }
            return point;
        }
        static int countT(char[,] arr)
        {
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++) {
                for (int j = 0; j < arr.GetLength(1); j++) {
                    if (arr[i, j] == 'T')
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        static void move(char[,] arr, int[] point, Stack<char> myStack)
        {
            arr[point[0], point[1]] = 'A';
            if ((point[1] != arr.GetLength(1) - 1) && (arr[point[0], point[1] + 1] != 'X') && (arr[point[0], point[1] + 1] != 'A'))
            {
                point[1] += 1;
                Console.WriteLine("Geser Kanan");
                myStack.Push('R');
            }
            else if ((point[0] != arr.GetLength(0) - 1) && (arr[point[0] + 1, point[1]] != 'X') && (arr[point[0] + 1, point[1]] != 'A'))
            {
                point[0] += 1;
                Console.WriteLine("Geser Bawah");
                myStack.Push('D');
            }
            else if ((point[1] != 0) && (arr[point[0], point[1] - 1] != 'X') && (arr[point[0], point[1] - 1] != 'A'))
            {
                point[1] -= 1;
                Console.WriteLine("Geser Kiri");
                myStack.Push('L');
            }
            else if ((point[0] != 0) && (arr[point[0] - 1, point[1]] != 'X') && (arr[point[0] - 1, point[1]] != 'A'))
            {
                point[0] -= 1;
                Console.WriteLine("Geser Atas");
                myStack.Push('U');
            }
            else
            {
                char backtracking = myStack.Pop();
                if (backtracking == 'L')
                {
                    point[1] += 1;
                    Console.WriteLine("Backtracking Ke Kanan");
                }
                else if (backtracking == 'U')
                {
                    point[0] += 1;
                    Console.WriteLine("Backtracking Ke Bawah");
                }
                else if (backtracking == 'R')
                {
                    point[1] -= 1;
                    Console.WriteLine("Backtracking Ke Kiri");
                }
                else if (backtracking == 'D')
                {
                    point[0] -= 1;
                    Console.WriteLine("Backtracking Ke Atas");
                }
            }
        }
    }
}
