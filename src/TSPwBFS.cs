using System;
using System.Collections.Generic;
using System.Linq;

namespace TSPwBFS
{
    public class cariTSP
    {
        public string checkAdjacent(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 + 1 && y1 == y2)
                return "U";
            else if (x1 == x2 - 1 && y1 == y2)
                return "D";
            else if (x1 == x2 && y1 == y2 + 1)
                return "L";
            else if (x1 == x2 && y1 == y2 - 1)
                return "R";
            else
                return "";
        }

        public string printStep(int[,] array)
        {
            string step = "";
            for (int i = 0; i < (array.Length / 2) - 1; i++)
            {
                step += checkAdjacent(array[i, 0], array[i, 1], array[i + 1, 0], array[i + 1, 1]);
            }
            return step;
        }

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
                        // Console.WriteLine("FOUND");
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

        public int[,] lokasiTreasure(char[,] map, int baris, int kolom, int jumlah)
        {
            int[,] lokasi = new int[jumlah, 2];
            int count = 0;
            for (int m = 0; m < baris; m++)
            {
                for (int n = 0; n < kolom; n++)
                    if (map[m, n] == 'T')
                    {
                        lokasi[count, 0] = m;
                        lokasi[count, 1] = n;
                        count++;
                    }
            }
            return lokasi;
        }

        public int[,] tambahTitik(int[,] array, int x, int y)
        {
            int panjang = (array.Length / 2);
            int[,] arrayBaru = new int[panjang + 1, 2];
            arrayBaru[0, 0] = x;
            arrayBaru[0, 1] = y;

            for (int i = 0; i < panjang; i++)
            {
                arrayBaru[i + 1, 0] = array[i, 0];
                arrayBaru[i + 1, 1] = array[i, 1];
            }
            return arrayBaru;
        }

        public bool pernahDikunjungi(int[,] array, int x, int y)
        {
            bool sudahAda = false;
            for (int i = 0; i < (array.Length / 2); i++)
            {
                if (array[i, 0] == x && array[i, 1] == y)
                {
                    sudahAda = true;
                    break;
                }
            }
            return sudahAda;
        }

        public int[,] cariJalan(char[,] map, int baris, int kolom, int[] player)
        {
            Queue<int[,]> queue = new Queue<int[,]>();
            cariTSP tiwal = new cariTSP();
            int[] titikAwal = tiwal.cariPlayer(map, baris, kolom);
            int jumlahT = tiwal.jumlahTreasure(map, baris, kolom);

            int[,] kujungan = new int[,] { { titikAwal[0], titikAwal[1] } };


            queue.Enqueue(new int[,] { { titikAwal[0], titikAwal[1] } });
            bool ketemu = false;
            bool pencarian = true;
            int nodes = 0;
            while (pencarian == true)
            {
                bool pojok = false;
                int[,] simpul = queue.Dequeue();
                // Console.Write("Queue diambil: ");
                // for (int c = 0; c < (simpul.Length / 2); c++)
                // {
                //     Console.Write("[" + simpul[c, 0] + "," + simpul[c, 1] + "], ");
                // }
                // Console.WriteLine();
                int xSaatIni = simpul[0, 0];
                int ySaatIni = simpul[0, 1];
                // Console.WriteLine("Posisi: " + xSaatIni + "," + ySaatIni);

                if (xSaatIni == 0 && ySaatIni == 0) //pojok kiri atas
                {
                    //Console.WriteLine("ada di pojok kiri atas");
                    pojok = true;
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T' || map[xSaatIni, ySaatIni + 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni + 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T' || map[xSaatIni + 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni + 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }
                }
                else if (xSaatIni == 0 && ySaatIni == kolom - 1) //pojok kanan atas
                {
                    //Console.WriteLine("ada di pojok kiri bawah");
                    pojok = true;
                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T' || map[xSaatIni + 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni + 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }
                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T' || map[xSaatIni, ySaatIni - 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni - 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                }

                else if (xSaatIni == baris - 1 && ySaatIni == 0) //pojok kiri bawah
                {
                    //Console.WriteLine("ada di pojok kiri bawah");
                    pojok = true;
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T' || map[xSaatIni, ySaatIni + 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni + 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T' || map[xSaatIni - 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni - 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else if (xSaatIni == baris - 1 && ySaatIni == kolom - 1)//pojok kanan bawah
                {
                    //Console.WriteLine("ada di pojok kanan bawah");
                    pojok = true;
                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, (ySaatIni - 1)] == 'T' || map[xSaatIni, ySaatIni - 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni - 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T' || map[xSaatIni - 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni - 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }
                else if (xSaatIni == 0 && pojok == false) // ada di paling atas
                {
                    //Console.WriteLine("ada di paling kiri");
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T' || map[xSaatIni, ySaatIni + 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni + 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T' || map[xSaatIni + 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni + 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T' || map[xSaatIni, ySaatIni - 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni - 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }
                }

                else if (xSaatIni == baris - 1 && pojok == false) // ada di paling bawah
                {
                    //Console.WriteLine("ada di paling kiri");
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T' || map[xSaatIni, ySaatIni + 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni + 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T' || map[xSaatIni, ySaatIni - 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni - 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T' || map[xSaatIni - 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni - 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else if (ySaatIni == kolom - 1 && pojok == false) // ada di paling kanan
                {
                    //Console.WriteLine("ada di paling kanan");
                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T' || map[xSaatIni + 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni + 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T' || map[xSaatIni, ySaatIni - 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni - 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T' || map[xSaatIni - 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni - 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else if (ySaatIni == 0 && pojok == false) // ada di paling kiri
                {
                    //Console.WriteLine("ada di paling atas");
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T' || map[xSaatIni, ySaatIni + 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni + 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T' || map[xSaatIni + 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni + 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T' || map[xSaatIni - 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni - 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                else // diluar kondisi di atas
                {
                    if ((map[xSaatIni, ySaatIni + 1] == 'R' || map[xSaatIni, ySaatIni + 1] == 'T' || map[xSaatIni, ySaatIni + 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni + 1)) == false)
                    {
                        //Console.WriteLine("ada di tengah");
                        if (map[xSaatIni, ySaatIni + 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni + 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni + 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kanan " + xSaatIni + "," + (ySaatIni + 1));
                    }

                    if ((map[xSaatIni + 1, ySaatIni] == 'R' || map[xSaatIni + 1, ySaatIni] == 'T' || map[xSaatIni + 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni + 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni + 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni + 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni + 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("bawah " + (xSaatIni + 1) + "," + ySaatIni);
                    }

                    if ((map[xSaatIni, ySaatIni - 1] == 'R' || map[xSaatIni, ySaatIni - 1] == 'T' || map[xSaatIni, ySaatIni - 1] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, xSaatIni, (ySaatIni - 1)) == false)
                    {
                        if (map[xSaatIni, ySaatIni - 1] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, xSaatIni, (ySaatIni - 1));
                        kujungan = cariTSP.tambahTitik(kujungan, xSaatIni, (ySaatIni - 1));
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("kiri " + xSaatIni + "," + (ySaatIni - 1));
                    }

                    if ((map[xSaatIni - 1, ySaatIni] == 'R' || map[xSaatIni - 1, ySaatIni] == 'T' || map[xSaatIni - 1, ySaatIni] == 'K') && ketemu == false && tiwal.pernahDikunjungi(kujungan, (xSaatIni - 1), ySaatIni) == false)
                    {
                        if (map[xSaatIni - 1, ySaatIni] == 'T')
                        {
                            ketemu = true;
                            jumlahT--;
                            // Console.WriteLine("Ketemu Treasure");
                        }
                        cariTSP cariTSP = new cariTSP();
                        int[,] titikBaru = cariTSP.tambahTitik(simpul, (xSaatIni - 1), ySaatIni);
                        kujungan = cariTSP.tambahTitik(kujungan, (xSaatIni - 1), ySaatIni);
                        queue.Enqueue(titikBaru);
                        nodes++;
                        // Console.WriteLine("atas " + (xSaatIni - 1) + "," + ySaatIni);
                    }
                }

                // Console.Write("Queue Saat ini: ");
                // for (int i = 0; i < queue.Count; i++)
                // {
                //     Console.Write("{");
                //     for (int j = 0; j < (queue.ElementAt(i).Length / 2); j++)
                //     {
                //         Console.Write("[" + queue.ElementAt(i)[j, 0] + "," + queue.ElementAt(i)[j, 1] + "], ");
                //     }
                //     Console.Write("}, ");
                // }
                // Console.WriteLine();

                if (ketemu == true)
                {
                    // Console.WriteLine("---queue---");
                    int harusDequeue = queue.Count;
                    // Console.WriteLine("jumlah queue = " + harusDequeue);
                    for (int a = 0; a < harusDequeue - 1; a++)
                    {
                        int[,] buang = queue.Dequeue();
                        // for (int k = 0; k < buang.Length / 2; k++)
                        // {
                        // Console.Write("[" + buang[k, 0] + "," + buang[k, 1] + "], ");
                        // }
                        // Console.WriteLine();
                    }
                    // Console.WriteLine("---dibuang---");
                    map[queue.ElementAt(0)[0, 0], queue.ElementAt(0)[0, 1]] = 'R';

                    // Console.Write("jalur saat ini = ");
                    // for (int k = 0; k < (queue.ElementAt(0).Length / 2); k++)
                    // {
                    //     Console.Write("[" + queue.ElementAt(0)[k, 0] + "," + queue.ElementAt(0)[k, 1] + "], ");
                    // }
                    // Console.WriteLine();

                    if (jumlahT > 0)
                    {
                        ketemu = false;
                        kujungan = new int[1, 2];
                        kujungan[0, 0] = queue.ElementAt(0)[0, 0];
                        kujungan[0, 1] = queue.ElementAt(0)[0, 1];
                    }
                    else if (jumlahT == 0)
                    {
                        ketemu = false;
                        map[titikAwal[0], titikAwal[1]] = 'T';
                        kujungan = new int[1, 2];
                        kujungan[0, 0] = queue.ElementAt(0)[0, 0];
                        kujungan[0, 1] = queue.ElementAt(0)[0, 1];
                    }
                    else
                    {
                        pencarian = false;
                    }
                }
            }
            int[,] hasil = queue.Dequeue();
            cariTSP tambah = new cariTSP();
            hasil = tambah.tambahTitik(hasil, nodes, 0);
            for (int i = 0; i < hasil.Length / 2 / 2; i++)
            {
                int temp = hasil[i, 0];
                hasil[i, 0] = hasil[hasil.Length / 2 - i - 1, 0];
                hasil[hasil.Length / 2 - i - 1, 0] = temp;

                int temp1 = hasil[i, 1];
                hasil[i, 1] = hasil[hasil.Length / 2 - i - 1, 1];
                hasil[hasil.Length / 2 - i - 1, 1] = temp1;
            }
            // Console.WriteLine("Hasil BFS: ");
            // for (int i = 0; i < hasil.Length / 2; i++)
            // {
            //     Console.Write("[" + hasil[i, 0] + "," + hasil[i, 1] + "], ");
            // }
            // Console.WriteLine();

            Console.WriteLine("nodes = " + nodes);

            return hasil;
        }
    }
}