using bacaFile;
using System.Data;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;




public class Program
{
    static void Main(string[] args)
    {
        string filename = "C:\\Users\\Asus\\Downloads\\New folder\\read txt\\read txt\\K R R R.txt";
        TXT abc = new TXT();
        abc.toMatrix(filename);
        Console.WriteLine("done!"); Console.ReadLine();
        Console.WriteLine();
        
    }
}