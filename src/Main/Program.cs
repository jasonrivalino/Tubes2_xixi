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
        char[,] hasil;
        TXT abc = new TXT();
        hasil = abc.toMatrix(filename);
        int baris = hasil.GetLength(0);
        int kolom = hasil.GetLength(1);

        for (int w = 0; w < baris; w++)
        {
            for (int h = 0; h < kolom; h++)
            {
                Console.Write(hasil[w, h] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("done!"); Console.ReadLine();
        Console.WriteLine();
        
    }
}