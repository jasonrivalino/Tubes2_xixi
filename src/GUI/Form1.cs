using bacaFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace GUI_Demo
{
    public partial class Form1 : Form
    {
        private int timemSec, timeSec;
        TXT test = new TXT();
        private string fileName;

        public Form1()
        {
            InitializeComponent();
        }

        // Button yang find atas untuk searching file
        protected void button2_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();


            openFileDialog.InitialDirectory = @"..\..\Tubes2_xixi\test";
            openFileDialog.Title = "Search File";

            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = Path.GetFileName(openFileDialog.FileName);
            }
            fileName = openFileDialog.FileName;
            timer1.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Jalanin yang BFS
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Jalanin yang DFS
        }

        public static DataTable GetDataTableFrom2DArray<T>(T[,] array)
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

        // Buat visualisasi
        private void button1_Click(object sender, EventArgs e)
        {
            char[,] hasil = test.toMatrix(fileName);
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
                    dataGridView1[j,i].ReadOnly = true;
                    dataGridView1[j,i].Style.ForeColor = Color.Black;
                }
            }

            for (int row = 0; row < hasil.GetLength(0); row++)
            {
                for (int col = 0; col <  hasil.GetLength(1); col++)
                {
                    char element = hasil[row, col];
                    if (element == 'X')
                    {
                        dataGridView1[col, row].Style.BackColor = Color.Black;
                    }
                    if (element == 'K')
                    {
                        dataGridView1[col, row].Style.BackColor = Color.Red;
                    }
                    if (element == 'T')
                    {
                        dataGridView1[col, row].Style.BackColor = Color.LimeGreen;
                    }
                }
            }
        }

        // Buat yang search
        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        // Fungsi untuk menjalankan waktu
        private void timer1_Tick(object sender, EventArgs e)
        {
            IncreaseTime();
            DrawTime();
        }

        // Fungsi untuk menambah waktu
        private void IncreaseTime() 
        { 
            if (timemSec == 9)
            {
                timemSec = 0;
                timeSec++;
            }
            else
            {
                timemSec++;
            }
        }

        // Fungsi untuk mengatur label
        private void DrawTime()
        {
            label8.Text = String.Format("{00,00}", timeSec);
            label9.Text = String.Format("{00,00}", timemSec);
        }

        // Button untuk reset
        private void button4_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            timer1.Enabled = false;
            timeSec = 0;
            timemSec = 0;
            DrawTime();
        }
    }
}
