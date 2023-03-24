# TUGAS BESAR 2 STRATEGI ALGORITMA - IF2211
> Tugas Besar 2: Pengaplikasian Algoritma BFS dan DFS dalam Menyelesaikan Persoalan Maze Treasure Hunt

## Anggota Kelompok
<table>
    <tr>
        <td colspan="3", align = "center"><center>Nama Kelompok: xixi</center></td>
    </tr>
    <tr>
        <td>No.</td>
        <td>Nama</td>
        <td>NIM</td>
    </tr>
    <tr>
        <td>1.</td>
        <td>Jason Rivalino</td>
        <td>13521008</td>
    </tr>
    <tr>
        <td>2.</td>
        <td>Syarifa Dwi Purnamasari</td>
        <td>13521018</td>
    </tr>
    <tr>
        <td>3.</td>
        <td>Agsha Athalla Nurkareem</td>
        <td>13521027</td>
    </tr>
</table>

## Table of Contents
* [Deskripsi Singkat](#deskripsi-singkat)
* [Keterangan Penting](#keterangan-penting)
* [Struktur File](#struktur-file)
* [Requirements](#requirements)
* [Cara Menjalankan Program](#cara-menjalankan-program)
* [Cara Mengoperasikan Program](#cara-mengoperasikan-program)
* [Tampilan Interface Program](#tampilan-interface-program)
* [Video Demo Penjelasan](#video-demo-penjelasan)
* [Acknowledgements](#acknowledgements)
* [Foto Bersama](#foto-bersama)

## Deskripsi Singkat 
Repository ini berisi aplikasi dengan GUI sederhana untuk mengimplementasikan algoritma BFS dan DFS untuk memperoleh seluruh harta karun dari titik awal. Program ini dibuat dengan menggunakan bahasa C# dan memanfaatkan Windows Forms untuk membuat GUInya. Program dapat membaca sebuah file txt yang berisi maze yang akan dicari solusi rute untuk mendapatkan treasurenya. Untuk penelusuran harta, pengguna bisa memilih secara bebas antara penelusuran secara melebar menggunakan algoritma BFS ataupun penelusuran secara mendalam menggunakan algoritma DFS. Pergerakan hanya bisa dilakukan keatas, bawah, kiri, dan, kanan (tidak ada gerakan diagonal).

## Keterangan Penting
1. Keterangan Informasi Simbol Maze
- K: Krusty Krab (Titik awal) -> dilambangkan dengan warna merah pada peta
- T: Treasure -> dilambangkan dengan warna hijau pada peta
- R: grid yang mungkin diakses (sebuah lintasan) -> dilambangkan dengan warna emas pada peta
- X: grid halangan yang tidak dapat diakses -> dilambangkan dengan warna hitam pada peta
- Grid yang dilewati hanya 1x -> dilambangkan dengan warna orange pada peta
- Grid yang dilewati lebih dari 1x -> dilambangkan dengan warna coklat pada peta

2. Keterangan Prioritas Pembangkit Arah Simpul: (Right Down Left Up / RDLU)

## Struktur File
```bash
📦Tubes2_xixi
 ┣ 📂bin
 ┣ 📂doc
 ┃ ┗ 📜xixi_018.pdf
 ┣ 📂src
 ┃ ┣ 📂.vs
 ┃ ┣ 📂properties
 ┃ ┣ 📂bin
 ┃ ┣ 📂obj
 ┃ ┣ 📜App.config
 ┃ ┣ 📜BFS.cs
 ┃ ┣ 📜DFS.cs
 ┃ ┣ 📜Form1.Designer.cs
 ┃ ┣ 📜Form1.cs
 ┃ ┣ 📜Form1.resx
 ┃ ┣ 📜MazeSolver.csproj
 ┃ ┣ 📜MazeSolver.csproj.user
 ┃ ┣ 📜MazeSolver.sln
 ┃ ┣ 📜Program.cs
 ┃ ┣ 📜TSPwBFS.cs
 ┃ ┣ 📜bacaFile.cs
 ┃ ┗ 📜background.jpg
 ┣ 📂test
 ┃ ┣ 📜sample-1.txt
 ┃ ┣ 📜sample-2.txt
 ┃ ┣ 📜sample-3.txt
 ┃ ┣ 📜sample-4.txt
 ┃ ┗ 📜sample-5.txt 
 ┗ 📜README.md
 ```
 
## Requirements
1. Visual Studio 2022
2. .NET 7.0
3. Windows Operating System

## Cara Menjalankan Program
Langkah-langkah proses setup program adalah sebagai berikut:
1. Clone repository ini
2. Buka file `MazeSolver.sln` dari repository ini yang terdapat pada folder src
3. Jalankan program dengan mengklik tombol Run atau tekan F5 pada keyboard di Visual Studio 2022

## Cara Mengoperasikan Program
1. Menyiapkan file.txt yang berisi map
2. Menekan tombol find pada program untuk mencari program txt yang ingin dipetakan
3. Setelah didapatkan filenya, melakukan pemilihan algoritma untuk mencari rute (bisa BFS, DFS, atau TSP)
4. Jika ingin melihat visualisasi peta, dapat menekan tombol visualize dan peta akan ditampilkan pada grid
5. Untuk mencari jarak dengan algoritma yang dipilih, setelah memilih file, dapat menekan tombol search dan peta pencarian akan langsung ditampilkan
6. Untuk reset program kembali ke default, dapat menekan tombol reset

## Tampilan Interface Program
![Screenshot 2023-03-24 215441](https://user-images.githubusercontent.com/91790457/227560277-d30ea6dd-c64d-403f-bcfc-7e0d3cf470ff.png)

## Video Demo Penjelasan
Link: https://youtu.be/22WJvKkyisw

## Acknowledgements
- Tuhan Yang Maha Esa
- Dosen Mata Kuliah yaitu Pak Rinaldi (K1), Bu Ulfa (K2), dan Pak Rila (K3)
- Kakak-Kakak Asisten Mata Kuliah Strategi Algoritma IF2211

## Foto Bersama
![foto kelompok xixi](https://user-images.githubusercontent.com/91790457/226720011-70a46730-6c94-4d1e-aea5-12ac61148595.jpg)
