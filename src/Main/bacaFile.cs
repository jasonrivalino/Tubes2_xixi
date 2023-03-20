using System;

namespace bacaFile;
public class TXT{
	public void toMatrix(string filename)
	{
        String input = File.ReadAllText(filename);
        int i = 0, j = 0;
        string[] data = File.ReadAllLines(filename);
        char[] chars = new char[data.Length];
        int baris = data.Length;
        int kolom = data[0].Split(' ').Length;

        Console.WriteLine("baris: " + baris);
        Console.WriteLine("kolom: " + kolom);

        char[,] result = new char[baris, kolom];
        foreach (var row in input.Split('\n'))
        {
            j = 0;
            foreach (var col in row.Trim().Split(' '))
            {
                result[i, j] = char.Parse(col.Trim());
                j++;
            }
            i++;
        }

        for (int w = 0; w < baris; w++)
        {
            for (int h = 0; h < kolom; h++)
            {
                Console.Write(result[w, h] + " ");
            }
            Console.WriteLine();
        }
    }
}
