﻿using bacaFile;
using dfsSearching;
using BFS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Security.Policy;
using TSPwBFS;

namespace GUI_Demo
{
    public partial class Form1 : Form
    {
        // Inisiasi variabel
        TXT test = new TXT();
        private string fileName;
        cariBFS bfs = new cariBFS();
        cariTSP tsp = new cariTSP();
        Stopwatch stopwatch = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        // Default parameter
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "Belum ada arah";
        }

        // Button yang find atas untuk searching file
        protected void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            openFileDialog.InitialDirectory = @"..\..\Tubes2_xixi\test";
            openFileDialog.Title = "Search File";

            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = Path.GetFileName(openFileDialog.FileName);
            }
            fileName = openFileDialog.FileName;
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "Belum ada arah";
        }

        // Fungsi untuk memasukkan input huruf ke dataGridView
        public static DataTable ConvertArrayToTable(char[,] array)
        {
            DataTable data = new DataTable();
            for (int i = 0; i < array.GetLength(1); i++)
            {
                data.Columns.Add();
            }
            for (int i = 0; i < array.GetLength(0) - 1; i++)
            {
                DataRow row = data.NewRow();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row[j] = array[i,j];
                }
                data.Rows.Add();
            }
            return data;
        }

        // Buat visualisasi awal
        private void button1_Click(object sender, EventArgs e)
        {
            visualize();
        }

        private void visualize()
        {
            bool checkElement = true;

            if (fileName == null)
            {
                // Do Nothing
            }
            else
            {
                char[,] hasil = test.toMatrix(fileName);
                char[] valid = new char[] { 'K', 'T', 'R', 'X' };

                for (int row1 = 0; row1 < hasil.GetLength(0); row1++)
                {
                    for (int col1 = 0; col1 < hasil.GetLength(1); col1++)
                    {
                        if (!(valid.Contains(hasil[row1, col1])))
                        {
                            checkElement = false;
                            dataGridView1.BackgroundColor = Color.Red;
                        }
                    }
                }

                if (checkElement)
                {
                    dataGridView1.DataSource = ConvertArrayToTable(hasil);
                    dataGridView1.BackgroundColor = Color.Gold;
                    dataGridView1.DefaultCellStyle.BackColor = Color.Gold;
                    dataGridView1.GridColor = Color.Black;
                    dataGridView1.ScrollBars = ScrollBars.None;

                    foreach (DataGridViewColumn cols in dataGridView1.Columns)
                        cols.Width = dataGridView1.Width / dataGridView1.Columns.Count;

                    foreach (DataGridViewRow rows in dataGridView1.Rows)
                        rows.Height = dataGridView1.Height / dataGridView1.Rows.Count;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            dataGridView1[j, i].ReadOnly = true;
                            dataGridView1[j, i].Style.ForeColor = Color.Black;
                        }
                    }

                    for (int row2 = 0; row2 < hasil.GetLength(0); row2++)
                    {
                        for (int col2 = 0; col2 < hasil.GetLength(1); col2++)
                        {
                            char element = hasil[row2, col2];
                            if (element == 'X')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Black;
                            }
                            if (element == 'K')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Red;
                                dataGridView1[col2, row2].Style.ForeColor = Color.White;
                                dataGridView1[col2, row2].Value = "K";
                            }
                            if (element == 'T')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.LimeGreen;
                                                                    dataGridView1[col2, row2].Style.ForeColor = Color.Black;
                                    dataGridView1[col2, row2].Value = "T";
                            }
                        }
                    }
                }
            }
        }

        // Buat yang search
        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) // Jalanin radioButton1 (BFS)
            {
                stopwatch.Start();
                visualizeBFS();
                stopwatch.Stop();
            }

            if (radioButton2.Checked) // Jalanin radioButton2 (DFS)
            {
                stopwatch.Start();
                visualizeDFS();
                stopwatch.Stop();
            }

            if (radioButton3.Checked) // Jalanin radioButton3 (TSP)
            {
                stopwatch.Start();
                visualizeTSP();
                stopwatch.Stop();
            }
            long runningTime = stopwatch.ElapsedMilliseconds;
            textBox2.Text = runningTime.ToString();
        }

        // Fungsi untuk visualisasi pencarian dengan BFS
        public void visualizeBFS()
        {
            bool checkElement = true;

            if (fileName == null)
            {
                // Do Nothing
            }
            else
            {
                char[,] arr = test.toMatrix(fileName);
                char[] valid = new char[] { 'K', 'T', 'R', 'X' };

                for (int row1 = 0; row1 < arr.GetLength(0); row1++)
                {
                    for (int col1 = 0; col1 < arr.GetLength(1); col1++)
                    {
                        if (!(valid.Contains(arr[row1, col1])))
                        {
                            dataGridView1.BackgroundColor = Color.Red;
                            checkElement = false;
                        }
                    }
                }

                if (checkElement)
                {
                    int baris = arr.GetLength(0);
                    int kolom = arr.GetLength(1);
                    int[] lokasiPlayer = new int[2];

                    dataGridView1.DataSource = ConvertArrayToTable(arr);
                    dataGridView1.BackgroundColor = Color.Gold;
                    dataGridView1.DefaultCellStyle.BackColor = Color.Gold;
                    dataGridView1.GridColor = Color.Black;
                    dataGridView1.ScrollBars = ScrollBars.None;

                    foreach (DataGridViewColumn cols in dataGridView1.Columns)
                        cols.Width = dataGridView1.Width / dataGridView1.Columns.Count;

                    foreach (DataGridViewRow rows in dataGridView1.Rows)
                        rows.Height = dataGridView1.Height / dataGridView1.Rows.Count;

                    lokasiPlayer = bfs.cariPlayer(arr, baris, kolom);
                    int jumlahTreasure = bfs.jumlahTreasure(arr, baris, kolom);
                    int[,] lokasiTreasure = bfs.lokasiTreasure(arr, baris, kolom, jumlahTreasure);
                    int[,] hasilnyaBFS = bfs.cariJalan(arr, baris, kolom, lokasiPlayer);

                    for (int i = 0; i < jumlahTreasure; i++)
                    {
                        int xTreasure = lokasiTreasure[i, 0];
                        int yTreasure = lokasiTreasure[i, 1];
                        dataGridView1.Rows[xTreasure].Cells[yTreasure].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[xTreasure].Cells[yTreasure].Value = "T";
                    }

                    for (int i = 0; i < hasilnyaBFS.GetLength(0)-1; i++)
                    {
                        for (int j = 0; j < hasilnyaBFS.GetLength(1)-1; j++)
                        {
                            int xKunjung = hasilnyaBFS[i, 0];
                            int yKunjung = hasilnyaBFS[i, 1];
                            dataGridView1.Rows[xKunjung].Cells[yKunjung].Style.BackColor = Color.Orange;
                        }
                    }

                    for (int i = 0; i < hasilnyaBFS.GetLength(0); i++)
                    {
                        for (int j = i + 1; j < hasilnyaBFS.GetLength(0); j++)
                        {
                            if (hasilnyaBFS[i, 0] == hasilnyaBFS[j, 0] && hasilnyaBFS[i, 1] == hasilnyaBFS[j, 1])
                            {
                                dataGridView1.Rows[hasilnyaBFS[i, 0]].Cells[hasilnyaBFS[i, 1]].Style.BackColor = Color.Brown;
                            }
                        }
                    }

                    for (int row2 = 0; row2 < arr.GetLength(0); row2++)
                    {
                        for (int col2 = 0; col2 < arr.GetLength(1); col2++)
                        {
                            char element2 = arr[row2, col2];
                            if (element2 == 'X')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Black;
                            }
                            if (element2 == 'K')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Red;
                                dataGridView1[col2, row2].Style.ForeColor = Color.White;
                                dataGridView1[col2, row2].Value = "K";
                            }
                            if (element2 == 'T')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.LimeGreen;
                                dataGridView1[col2, row2].Style.ForeColor = Color.Black;
                                dataGridView1[col2, row2].Value = "T";
                            }
                        }
                    }
                    string hasilJalan = bfs.printStep(hasilnyaBFS);
                    textBox3.Text = (hasilnyaBFS.GetLength(0) - 2).ToString();
                    textBox4.Text = (hasilnyaBFS[(hasilnyaBFS.Length / 2) - 1,0]).ToString();
                    textBox5.Text = hasilJalan;
                }
            }
        }

        // Fungsi untuk visualisasi pencarian dengan DFS
        public void visualizeDFS()
        {
            bool checkElement = true;

            if (fileName == null)
            {
                // Do Nothing
            }
            else
            {
                char[,] arr = test.toMatrix(fileName);
                char[] valid = new char[] { 'K', 'T', 'R', 'X' };

                for (int row1 = 0; row1 < arr.GetLength(0); row1++)
                {
                    for (int col1 = 0; col1 < arr.GetLength(1); col1++)
                    {
                        if (!(valid.Contains(arr[row1, col1])))
                        {
                            dataGridView1.BackgroundColor = Color.Red;
                            checkElement = false;
                        }
                    }
                }

                if (checkElement)
                {
                    Stack<Points> treasure = new Stack<Points>();
                    treasure = Algorithm.stackTreasure(arr);
                    //Algorithm.printStack(treasure);
                    Points K = Algorithm.findK(arr);
                    Stack<Points> visited = new Stack<Points>();
                    Stack<Points> path = new Stack<Points>();
                    visited.Push(K);
                    path.Push(K);
                    //Algorithm.printStack(visited);
                    Stack<Points> unvisited = new Stack<Points>();

                    dataGridView1.DataSource = ConvertArrayToTable(arr);
                    dataGridView1.BackgroundColor = Color.Gold;
                    dataGridView1.DefaultCellStyle.BackColor = Color.Gold;
                    dataGridView1.GridColor = Color.Black;
                    dataGridView1.ScrollBars = ScrollBars.None;

                    foreach (DataGridViewColumn cols in dataGridView1.Columns)
                        cols.Width = dataGridView1.Width / dataGridView1.Columns.Count;

                    foreach (DataGridViewRow rows in dataGridView1.Rows)
                        rows.Height = dataGridView1.Height / dataGridView1.Rows.Count;

                    while (treasure.Count != 0)
                    {
                        //Console.WriteLine("Visited sekarang: " + visited.Peek().x + "," + visited.Peek().y);
                        int xKunjung = (int)visited.Peek().x;
                        int yKunjung = (int)visited.Peek().y;
                        dataGridView1.Rows[xKunjung].Cells[yKunjung].Style.BackColor = Color.Orange;
                        Algorithm.treasureArrived(treasure, visited.Peek());
                        Algorithm.adjacentPoints(arr, visited, unvisited);
                        if (treasure.Count != 0)
                        {
                            Algorithm.move(visited, unvisited, path);
                        }
                    }
                    //Console.WriteLine("Jalannya lewat mana sihhh");
                    //Algorithm.printStack(path);
                    Stack<Points> pathCopy = path;
                    Points[] arrayStack = pathCopy.ToArray();
                    //Algorithm.printArray(arrayStack);

                    for (int i = 0; i < arrayStack.GetLength(0); i++)
                    {
                        for (int j = i + 1; j < arrayStack.GetLength(0); j++)
                        {
                            if (arrayStack[i].x == arrayStack[j].x && arrayStack[i].y == arrayStack[j].y)
                            {
                                dataGridView1.Rows[arrayStack[i].x].Cells[arrayStack[i].y].Style.BackColor = Color.Brown;
                            }
                        }
                    }

                    for (int row2 = 0; row2 < arr.GetLength(0); row2++)
                    {
                        for (int col2 = 0; col2 < arr.GetLength(1); col2++)
                        {
                            char element2 = arr[row2, col2];
                            if (element2 == 'X')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Black;
                            }
                            if (element2 == 'K')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Red;
                                dataGridView1[col2, row2].Style.ForeColor = Color.White;
                                dataGridView1[col2, row2].Value = "K";
                            }
                            if (element2 == 'T')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.LimeGreen;
                                dataGridView1[col2, row2].Style.ForeColor = Color.Black;
                                dataGridView1[col2, row2].Value = "T";
                            }
                        }
                    }
                    int count = path.Count;
                    string direction = Algorithm.printStep(path, count);
                    //Console.WriteLine(direction);
                    //Console.WriteLine("Jumlah node yang dilewati: " + Algorithm.countNode(path));
                    textBox3.Text = (count-1).ToString();
                    textBox4.Text = count.ToString();
                    textBox5.Text = direction;
                }
            }
        }

        // Fungsi untuk visualisasi pencarian dengan TSP
        public void visualizeTSP()
        {
            bool checkElement = true;

            if (fileName == null)
            {
                // Do Nothing
            }
            else
            {
                char[,] arr = test.toMatrix(fileName);
                char[] valid = new char[] { 'K', 'T', 'R', 'X' };

                for (int row1 = 0; row1 < arr.GetLength(0); row1++)
                {
                    for (int col1 = 0; col1 < arr.GetLength(1); col1++)
                    {
                        if (!(valid.Contains(arr[row1, col1])))
                        {
                            dataGridView1.BackgroundColor = Color.Red;
                            checkElement = false;
                        }
                    }
                }

                if (checkElement)
                {
                    int baris = arr.GetLength(0);
                    int kolom = arr.GetLength(1);
                    int[] lokasiPlayer = new int[2];

                    dataGridView1.DataSource = ConvertArrayToTable(arr);
                    dataGridView1.BackgroundColor = Color.Gold;
                    dataGridView1.DefaultCellStyle.BackColor = Color.Gold;
                    dataGridView1.GridColor = Color.Black;
                    dataGridView1.ScrollBars = ScrollBars.None;

                    foreach (DataGridViewColumn cols in dataGridView1.Columns)
                        cols.Width = dataGridView1.Width / dataGridView1.Columns.Count;

                    foreach (DataGridViewRow rows in dataGridView1.Rows)
                        rows.Height = dataGridView1.Height / dataGridView1.Rows.Count;

                    lokasiPlayer = tsp.cariPlayer(arr, baris, kolom);
                    int jumlahTreasure = tsp.jumlahTreasure(arr, baris, kolom);
                    int[,] lokasiTreasure = tsp.lokasiTreasure(arr, baris, kolom, jumlahTreasure);
                    int[,] hasilnyaTSP = tsp.cariJalan(arr, baris, kolom, lokasiPlayer);

                    for (int i = 0; i < jumlahTreasure; i++)
                    {
                        int xTreasure = lokasiTreasure[i, 0];
                        int yTreasure = lokasiTreasure[i, 1];
                        dataGridView1.Rows[xTreasure].Cells[yTreasure].Style.ForeColor = Color.Black;
                        dataGridView1.Rows[xTreasure].Cells[yTreasure].Value = "T";
                    }

                    for (int i = 0; i < hasilnyaTSP.GetLength(0)-1; i++)
                    {
                        for (int j = 0; j < hasilnyaTSP.GetLength(1)-1; j++)
                        {
                            int xKunjung = hasilnyaTSP[i, 0];
                            int yKunjung = hasilnyaTSP[i, 1];
                            dataGridView1.Rows[xKunjung].Cells[yKunjung].Style.BackColor = Color.Orange;
                        }
                    }

                    for (int i = 0; i < hasilnyaTSP.GetLength(0); i++)
                    {
                        for (int j = i + 1; j < hasilnyaTSP.GetLength(0); j++)
                        {
                            if (hasilnyaTSP[i, 0] == hasilnyaTSP[j, 0] && hasilnyaTSP[i, 1] == hasilnyaTSP[j, 1])
                            {
                                dataGridView1.Rows[hasilnyaTSP[i, 0]].Cells[hasilnyaTSP[i, 1]].Style.BackColor = Color.Brown;
                            }
                            dataGridView1.Rows[hasilnyaTSP[0, 0]].Cells[hasilnyaTSP[0, 1]].Style.BackColor = Color.Red;
                            dataGridView1.Rows[hasilnyaTSP[0, 0]].Cells[hasilnyaTSP[0, 1]].Style.ForeColor = Color.White;
                            dataGridView1.Rows[hasilnyaTSP[0, 0]].Cells[hasilnyaTSP[0, 1]].Value = "K";
                        }
                    }

                    for (int row2 = 0; row2 < arr.GetLength(0); row2++)
                    {
                        for (int col2 = 0; col2 < arr.GetLength(1); col2++)
                        {
                            char element2 = arr[row2, col2];
                            if (element2 == 'X')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Black;
                            }
                            if (element2 == 'K')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.Red;
                                dataGridView1[col2, row2].Style.ForeColor = Color.White;
                                dataGridView1[col2, row2].Value = "K";
                            }
                            if (element2 == 'T')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.LimeGreen;
                                dataGridView1[col2, row2].Style.ForeColor = Color.Black;
                                dataGridView1[col2, row2].Value = "T";
                            }
                        }
                    }
                    string hasilJalan = tsp.printStep(hasilnyaTSP);
                    textBox3.Text = (hasilnyaTSP.GetLength(0) - 2).ToString();
                    textBox4.Text = (hasilnyaTSP[(hasilnyaTSP.Length / 2) - 1, 0]).ToString();
                    textBox5.Text = hasilJalan;
                }
            }
        }

        // Button untuk reset
        private void button4_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "Belum ada arah";
        }
    }
}