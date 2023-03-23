﻿using System;

namespace BFS
{
    public class cariBFS
    {
        // public static void checkAdjacent(int x1, int y1, int x2, int y2)
        // {
        //     if (x1 == x2 + 1 && p1.y == p2.y)
        //         Console.Write("U");
        //     else if (p1.x == p2.x - 1 && p1.y == p2.y)
        //         Console.Write("D");
        //     else if (p1.x == p2.x && p1.y == p2.y + 1)
        //         Console.Write("L");
        //     else if (p1.x == p2.x && p1.y == p2.y - 1)
        //         Console.Write("R");
        //     else
        //         Console.Write("Backtracking sampai di (" + p2.x + "," + p2.y + ")");
        // }

        public int[] cariPlayer(char[,] map, int baris, int kolom)
        {
            int[] lokasiPlayer = new int[2];
            for (int k = 0; k < baris; k++)
            {
                for (int l = 0; l < kolom; l++)
                {
                    if (map[k, l] == 'K')
                    {
                        lokasiPlayer[0] = k;
                        lokasiPlayer[1] = l;
                        //break;
                        Console.WriteLine("FOUND");
                    }
                }

            }
            return lokasiPlayer;
        }

        public int jumlahTreasure(char[,] map, int baris, int kolom)
        {
            int count = 0;
            for (int m = 0; m < baris; m++)
            {
                for (int n = 0; n < kolom; n++)
                    if (map[m, n] == 'T')
                        count++;
            }
            return count;
        }

        public int[,] tambahTitik(int[,] array, int x, int y)
        {
            //Console.WriteLine("Bagian tambah titik");
            //Console.WriteLine("x: " + x + " y: " + y);
            int panjang = (array.Length / 2);
            int[,] arrayBaru = new int[panjang + 1, 2];
            arrayBaru[0, 0] = x; 
            arrayBaru[0, 1] = y;

            for (int i = 0; i < panjang; i++)
            {
                arrayBaru[i + 1, 0] = array[i, 0];
                arrayBaru[i + 1, 1] = array[i, 1];
            }
            // Console.Write("Array baru: ");
            // for(int i = 0; i<panjang + 1; i++)
            // {
            //     Console.Write("[" + arrayBaru[i, 0] + "," + arrayBaru[i, 1] + "], ");
            // }
            // Console.WriteLine();
            // Console.WriteLine("-----------------");

            return arrayBaru;
        }

        public void cariJalan(char[,] map, int baris, int kolom, int[] player)
        {
            Queue<int[,]> queue = new Queue<int[,]>();
            cariBFS tiwal = new cariBFS();
            int[] titikAwal = tiwal.cariPlayer(map, baris, kolom);
            Console.WriteLine("tiwal "+titikAwal[0] + "," + titikAwal[1]);
            int jumlahT = tiwal.jumlahTreasure(map, baris, kolom);

            queue.Enqueue(new int[,] { { titikAwal[0], titikAwal[1] } });
            bool ketemu = false;
            bool pencarian = true;
            while (pencarian == true)
            //for(int h = 0; h<5; h++)
            {
                bool pojok = false;
                int[,] simpul = queue.Dequeue();
                Console.Write("Queue diambil: ");
                for (int c = 0; c < (simpul.Length / 2); c++)
                {
                    Console.Write("[" + simpul[c, 0] + "," + simpul[c, 1] + "], ");
                }
                Console.WriteLine();
                int xSaatIni = simpul[0, 0];
                int ySaatIni = simpul[0, 1];
                Console.WriteLine("Posisi: " + xSaatIni + "," + ySaatIni);

                if (xSaatIni == 0 && ySaatIni == 0) //pojok kiri atas
                {
                    Console.WriteLine("ada di pojok kiri atas");
                    pojok = true;
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }
                }
                else if (xSaatIni == 0 && ySaatIni == kolom - 1) //pojok kanan atas
                {
                    Console.WriteLine("ada di pojok kiri bawah");
                    pojok = true;
                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("turun " + (xSaatIni + 1) + "," + ySaatIni);
                    }
                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                }

                else if (xSaatIni == baris - 1 && ySaatIni == 0) //pojok kiri bawah
                {
                    Console.WriteLine("ada di pojok kiri bawah");
                    pojok = true;
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else if (xSaatIni == baris - 1 && ySaatIni == kolom - 1)//pojok kanan bawah
                {
                    Console.WriteLine("ada di pojok kanan bawah");
                    pojok = true;
                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, (ySaatIni - 1)] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, ySaatIni - 1);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }
                else if (xSaatIni == 0 && pojok == false) // ada di paling atas
                {
                    Console.WriteLine("ada di paling kiri");
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }
                }

                else if (xSaatIni == baris - 1 && pojok == false) // ada di paling bawah
                {
                    Console.WriteLine("ada di paling kiri");
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni+1));
                    }
                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }
                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else if (ySaatIni == kolom - 1 && pojok == false) // ada di paling kanan
                {
                    Console.WriteLine("ada di paling kanan");
                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni +1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + (xSaatIni+1) + "," + ySaatIni);
                    }
                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("bawah " + xSaatIni + "," + (ySaatIni - 1));
                    }
                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else if (ySaatIni == 0 && pojok == false) // ada di paling kiri
                {
                    Console.WriteLine("ada di paling atas");
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else // diluar kondisi di atas
                {
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T') && ketemu == false)
                    {
                        Console.WriteLine("ada di tengah");
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T') && ketemu == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            Console.WriteLine("Ketemu Treasure");
                        }
                        cariBFS cariBFS = new cariBFS();
                        int[,] titikBaru = cariBFS.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                Console.Write("Queue Saat ini: ");
                for (int i = 0; i < queue.Count; i++)
                {
                    Console.Write("{");
                    for(int j = 0; j < (queue.ElementAt(i).Length/2); j++)
                    {
                        Console.Write("[" + queue.ElementAt(i)[j, 0] + "," + queue.ElementAt(i)[j, 1] + "], ");
                    }
                    Console.Write("}, ");
                }
                Console.WriteLine();

                if (ketemu == true){
                    if (jumlahT > 0){
                        ketemu = false;
                    }
                    else{
                        pencarian = false;
                    }
                    Console.WriteLine("---queue---");
                    int harusDequeue = queue.Count;
                    Console.WriteLine("jumlah queue = " + harusDequeue);
                    for (int a = 0; a < harusDequeue - 1; a++){
                        int[,] buang = queue.Dequeue();
                        for (int k = 0; k < buang.Length/2; k++){
                            Console.Write("[" + buang[k, 0] + "," + buang[k, 1] + "], ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("---dibuang---");
                    map[queue.ElementAt(0)[0,0],queue.ElementAt(0)[0,1]] = 'R';
                    
                    Console.Write("jalur saat ini = ");
                    for(int k = 0; k < (queue.ElementAt(0).Length/2); k++){
                        Console.Write("[" + queue.ElementAt(0)[k, 0] + "," + queue.ElementAt(0)[k, 1] + "], ");
                    }
                    Console.WriteLine();
                }
            }
            int[,] hasil = queue.Dequeue();
            //reverse array
            for (int i = 0; i < hasil.Length/2 / 2; i++)
            {
                int temp = hasil[i, 0];
                hasil[i, 0] = hasil[hasil.Length/2 - i - 1, 0];
                hasil[hasil.Length/2 - i - 1, 0] = temp;

                int temp1 = hasil[i, 1];
                hasil[i, 1] = hasil[hasil.Length/2 - i - 1, 1];
                hasil[hasil.Length/2 - i - 1, 1] = temp1;
            }
            Console.WriteLine("Hasil BFS: ");
            for (int i = 0; i < hasil.Length/2; i++)
            {
                Console.Write("[" + hasil[i, 0] + "," + hasil[i, 1] + "], ");
            }
        }
    }
}