using bacaFile;
using dfsSearching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace GUI_Demo
{
    public partial class Form1 : Form
    {
        // Inisiasi variabel
        TXT test = new TXT();
        private string fileName;
        Programs dfs = new Programs();
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
        public static DataTable GetDataTableFrom2DArray(char[,] array)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < array.GetLength(1); i++)
            {
                dt.Columns.Add();
            }

            for (int i = 0; i < array.GetLength(0) - 1; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    row[j] = array[i, j];
                }
                dt.Rows.Add();
            }
            return dt;
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
                    dataGridView1.DataSource = GetDataTableFrom2DArray(hasil);
                    dataGridView1.BackgroundColor = Color.Gold;
                    dataGridView1.DefaultCellStyle.BackColor = Color.Gold;
                    dataGridView1.GridColor = Color.Black;
                    dataGridView1.ScrollBars = ScrollBars.None;

                    dataGridView1.AllowUserToResizeRows = false;
                    dataGridView1.AllowUserToResizeColumns = false;
                    dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
                            }
                            if (element == 'T')
                            {
                                dataGridView1[col2, row2].Style.BackColor = Color.LimeGreen;
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
                
            }

            if(radioButton2.Checked) // Jalanin radioButton2 (DFS)
            {
                stopwatch.Start();
                visualizeDFS();
                stopwatch.Stop();
            }
            long runningTime = stopwatch.ElapsedMilliseconds;
            textBox2.Text = runningTime.ToString();
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
                char[,] hasil = test.toMatrix(fileName);
                char[] valid = new char[] { 'K', 'T', 'R', 'X' };

                for (int row1 = 0; row1 < hasil.GetLength(0); row1++)
                {
                    for (int col1 = 0; col1 < hasil.GetLength(1); col1++)
                    {
                        if (!(valid.Contains(hasil[row1, col1])))
                        {
                            dataGridView1.BackgroundColor = Color.Red;
                            checkElement = false;
                        }
                    }
                }
                if (checkElement)
                {
                    char[,] arr = test.toMatrix(fileName);
                    Stack<Points> treasure = new Stack<Points>();
                    Stack<Points> visited = new Stack<Points>();
                    Stack<Points> unvisited = new Stack<Points>();
                    treasure = Programs.stackTreasure(arr);
                    Points K = Programs.findK(arr);
                    visited.Push(K);
                    int step = 1;

                    dataGridView1.DataSource = GetDataTableFrom2DArray(arr);
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
                        Console.WriteLine("Visited sekarang: " + visited.Peek().x + "," + visited.Peek().y);
                        int xKunjung = (int)visited.Peek().x;
                        int yKunjung = (int)visited.Peek().y;
                        dataGridView1.Rows[xKunjung].Cells[yKunjung].Style.BackColor = Color.Orange;
                        dataGridView1.Rows[xKunjung].Cells[yKunjung].Value = step;
                        Programs.treasureArrived(treasure, visited.Peek());
                        Programs.adjacentPoints(arr, visited, unvisited);
                        if (treasure.Count != 0)
                        {
                            Programs.move(visited, unvisited);
                            step++;
                        }

                        for (int row2 = 0; row2 < arr.GetLength(0); row2++)
                        {
                            for (int col2 = 0; col2 < arr.GetLength(1); col2++)
                            {
                                char element = arr[row2, col2];
                                if (element == 'X')
                                {
                                    dataGridView1[col2, row2].Style.BackColor = Color.Black;
                                }
                                if (element == 'K')
                                {
                                    dataGridView1[col2, row2].Style.BackColor = Color.Red;
                                }
                                if (element == 'T')
                                {
                                    dataGridView1[col2, row2].Style.BackColor = Color.LimeGreen;
                                }
                            }
                        }
                    }
                    Console.WriteLine("Jalannya lewat mana aja sihhh:");
                    Programs.printStack(visited);
                    textBox3.Text = visited.Count.ToString();
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
