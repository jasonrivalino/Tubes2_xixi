using bacaFile;
using BFS;
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

        cariBFS aaa = new cariBFS();
        int[] lokasiPlayer = new int[2];
        lokasiPlayer = aaa.cariPlayer(hasil, baris, kolom);
        Console.Write(lokasiPlayer[0] + ",");
        Console.Write(lokasiPlayer[1] + "\n");

        int jumlahTreasure = aaa.jumlahTreasure(hasil, baris, kolom);
        Console.WriteLine(jumlahTreasure);

        int[,] jalan = aaa.cariJalan(hasil, baris, kolom, lokasiPlayer);

        string hasilJalan = aaa.printStep(jalan);
        Console.WriteLine("arah: "+ hasilJalan);

        Console.WriteLine("done!"); Console.ReadLine();
        Console.WriteLine();
        
    }
}