using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace Heming_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static BitArray messageArray1;
        static BitArray messageArray2;
        static BitArray messageDeCoded;
        static int dovj;
        static int pochatok;
        bool flag = false;
        bool flag1 = false;
        string abc;
        string abcd;
        static BitArray ConvertFileToBitArray(string path)
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                BitArray messageArray = new BitArray(fileBytes);

                return messageArray;
            }
        static BitArray Convert1FileToBitArray(string path1)
        {
            byte[] fileBytes = File.ReadAllBytes(path1);
            BitArray messageArray1 = new BitArray(fileBytes);


            return messageArray1;
        }

        public static byte[] BitArrayToBytes(System.Collections.BitArray messageCoded1) // переводимо масив бітів в масив байтів
        {
            if (messageCoded1.Length == 0)
            {
                throw new System.ArgumentException("must have at least length 1", "bitarray");
            }

            int num_bytes = messageCoded1.Length / 8;

            if (messageCoded1.Length % 8 != 0)
            {
                num_bytes += 1;
            }

            var bytes = new byte[num_bytes];
            messageCoded1.CopyTo(bytes, 0);
            return bytes;
        }

        static BitArray MyCoding(BitArray messageArray)
        {
            if (1==2)
            {
                int countBits = messageArray.Count; // кількість біт в масиві
                pochatok = messageArray.Count;
                int newBits = (int)Math.Ceiling(countBits / 5.0) * 4;

                int lastBits = countBits + newBits;
                for (int d = 0; d < 100; d++)
                {
                    if (lastBits % 9 == 0)
                    {
                        break;
                    }
                    else
                    {
                        lastBits++;
                    }
                }
                BitArray resultat = new BitArray(lastBits);
                BitArray messageCoded = new BitArray(lastBits); // новий пустий масив біт
                for (int i = 0; i < countBits; i += 5)
                {


                    BitArray pol = new BitArray(5);

                    for (int j = 0; j < 5; j++)
                    {
                        if (j + i >= countBits)
                        {
                            pol[j] = false;
                        }
                        else
                        {
                            pol[j] = messageArray[j + i];
                        }
                    }

                    for (int a = 0; a < 1; a++)
                    {
                        resultat[2] = pol[4];
                        resultat[4] = pol[3];
                        resultat[5] = pol[2];
                        resultat[6] = pol[1];
                        resultat[8] = pol[0];
                    }
                    BitArray pol1 = new BitArray(9);
                    for (int a = 0; a < 1; a++)
                    {
                        pol1[0] = false;
                        pol1[1] = false;
                        pol1[2] = pol[4];
                        pol1[3] = false;
                        pol1[4] = pol[3];
                        pol1[5] = pol[2];
                        pol1[6] = pol[1];
                        pol1[7] = false;
                        pol1[8] = pol[0];
                    }
                    BitArray m1 = new BitArray(9);     //(1, 0, 1, 0, 1, 0, 1, 0, 1);
                    m1[0] = true; m1[1] = false; m1[2] = true; m1[3] = false; m1[4] = true; m1[5] = false; m1[6] = true; m1[7] = false; m1[8] = true;
                    BitArray m2 = new BitArray(9);       //(0, 1, 1, 0, 0, 1, 1, 0, 0);
                    m2[0] = false; m2[1] = true; m2[2] = true; m2[3] = false; m2[4] = false; m2[5] = true; m2[6] = true; m2[7] = false; m2[8] = false;
                    BitArray m3 = new BitArray(9);       //(0, 0, 0, 1, 1, 1, 1, 0, 0);
                    m3[0] = false; m3[1] = false; m3[2] = false; m3[3] = true; m3[4] = true; m3[5] = true; m3[6] = true; m3[7] = false; m3[8] = false;
                    BitArray m4 = new BitArray(9);      //(0, 0, 0, 0, 0, 0, 0, 1, 1);
                    m4[0] = false; m4[1] = false; m4[2] = false; m4[3] = false; m4[4] = false; m4[5] = false; m4[6] = false; m4[7] = true; m4[8] = true;
                    BitArray sohran = new BitArray(4);
                    int provirka = 0;
                    int provirka1 = 0;
                    int provirka2 = 0;
                    int provirka3 = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        if (k == 0)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                if (pol1[l] & m1[l] == true)
                                {
                                    provirka++;
                                }
                                else { }
                            }
                        }
                        if (k == 1)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                if (pol1[l] & m2[l] == true)
                                {
                                    provirka1++;
                                }
                                else { }
                            }
                        }
                        if (k == 2)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                if (pol1[l] & m3[l] == true)
                                {
                                    provirka2++;
                                }
                                else { }
                            }
                        }
                        if (k == 3)
                        {
                            for (int l = 0; l < 9; l++)
                            {
                                if (pol1[l] & m4[l] == true)
                                {
                                    provirka3++;
                                }
                                else { }
                            }
                        }
                    }
                    if (provirka % 2 == 0)
                    {
                        resultat[0] = false;
                    }
                    else { resultat[0] = true; }
                    if (provirka1 % 2 == 0)
                    {
                        resultat[1] = false;
                    }
                    else { resultat[1] = true; }
                    if (provirka2 % 2 == 0)
                    {
                        resultat[3] = false;
                    }
                    else { resultat[3] = true; }
                    if (provirka3 % 2 == 0)
                    {
                        resultat[7] = false;
                    }
                    else { resultat[7] = true; }




                    for (int k = 0; k < 9; k++)
                    {
                        messageCoded[k + (9 * (i / 5))] = resultat[k];
                    }

                }

                return messageCoded;
            }
            if (2 == 3)
            {
                int countBits = messageArray.Count; // кількість біт в масиві
                pochatok = messageArray.Count;
                int newBits = (int)Math.Ceiling(countBits / 4.0) * 3;
                int lastBits = countBits + newBits;
                for (int d = 0; d < 100; d++)
                {
                    if (lastBits % 7 == 0)
                    {
                        break;
                    }
                    else
                    {
                        lastBits++;
                    }
                }
                BitArray resultat = new BitArray(lastBits);
                BitArray messageCoded = new BitArray(lastBits); // новий пустий масив біт
                for (int i = 0; i < countBits; i += 4)
                {
                    BitArray pol = new BitArray(4);

                    for (int j = 0; j < 4; j++)
                    {
                        if (j + i >= countBits)
                        {
                            pol[j] = false;
                        }
                        else
                        {
                            pol[j] = messageArray[j + i];
                        }
                    }

                    for (int a = 0; a < 1; a++)
                    {
                        resultat[2] = pol[3];
                        resultat[4] = pol[2];
                        resultat[5] = pol[1];
                        resultat[6] = pol[0];
                    }
                    BitArray pol1 = new BitArray(7);
                    for (int a = 0; a < 1; a++)
                    {
                        pol1[0] = false;
                        pol1[1] = false;
                        pol1[2] = pol[3];
                        pol1[3] = false;
                        pol1[4] = pol[2];
                        pol1[5] = pol[1];
                        pol1[6] = pol[0];
                    }
                    BitArray m1 = new BitArray(7);     //(1, 0, 1, 0, 1, 0, 1, 0, 1);
                    m1[0] = true; m1[1] = false; m1[2] = true; m1[3] = false; m1[4] = true; m1[5] = false; m1[6] = true; 
                    BitArray m2 = new BitArray(7);       //(0, 1, 1, 0, 0, 1, 1, 0, 0);
                    m2[0] = false; m2[1] = true; m2[2] = true; m2[3] = false; m2[4] = false; m2[5] = true; m2[6] = true;
                    BitArray m3 = new BitArray(7);       //(0, 0, 0, 1, 1, 1, 1, 0, 0);
                    m3[0] = false; m3[1] = false; m3[2] = false; m3[3] = true; m3[4] = true; m3[5] = true; m3[6] = true;
                    BitArray sohran = new BitArray(3);
                    int provirka = 0;
                    int provirka1 = 0;
                    int provirka2 = 0;
                    for (int k = 0; k <3; k++)
                    {
                        if (k == 0)
                        {
                            for (int l = 0; l < 7; l++)
                            {
                                if (pol1[l] & m1[l] == true)
                                {
                                    provirka++;
                                }
                                else { }
                            }
                        }
                        if (k == 1)
                        {
                            for (int l = 0; l < 7; l++)
                            {
                                if (pol1[l] & m2[l] == true)
                                {
                                    provirka1++;
                                }
                                else { }
                            }
                        }
                        if (k == 2)
                        {
                            for (int l = 0; l < 7; l++)
                            {
                                if (pol1[l] & m3[l] == true)
                                {
                                    provirka2++;
                                }
                                else { }
                            }
                        }
                        
                    }
                    if (provirka % 2 == 0)
                    {
                        resultat[0] = false;
                    }
                    else { resultat[0] = true; }
                    if (provirka1 % 2 == 0)
                    {
                        resultat[1] = false;
                    }
                    else { resultat[1] = true; }
                    if (provirka2 % 2 == 0)
                    {
                        resultat[3] = false;
                    }
                    else { resultat[3] = true; }

                    for (int k = 0; k < 7; k++)
                    {
                        messageCoded[k + (7 * (i / 4))] = resultat[k];
                    }

                }

                return messageCoded;
            }
            if (2 == 2)
            {
                int countBits = messageArray.Count; // кількість біт в масиві
                pochatok = messageArray.Count;
                int newBits = (int)Math.Ceiling(countBits / 1.0) * 2;
                int lastBits = countBits + newBits;
                for (int d = 0; d < 100; d++)
                {
                    if (lastBits % 3 == 0)
                    {
                        break;
                    }
                    else
                    {
                        lastBits++;
                    }
                }
                BitArray resultat = new BitArray(lastBits);
                BitArray messageCoded = new BitArray(lastBits); // новий пустий масив біт
                for (int i = 0; i < countBits; i += 1)
                {
                    BitArray pol = new BitArray(1);

                    for (int j = 0; j < 1; j++)
                    {
                        if (j + i >= countBits)
                        {
                            pol[j] = false;
                        }
                        else
                        {
                            pol[j] = messageArray[j + i];
                        }
                    }

                    for (int a = 0; a < 1; a++)
                    {
                        resultat[2] = pol[0];
                    }
                    BitArray pol1 = new BitArray(3);
                    for (int a = 0; a < 1; a++)
                    {
                        pol1[0] = false;
                        pol1[1] = false;
                        pol1[2] = pol[0];
                    }
                    BitArray m1 = new BitArray(7);     //(1, 0, 1, );
                    m1[0] = true; m1[1] = false; m1[2] = true; 
                    BitArray m2 = new BitArray(7);       //(0, 1, 1,);
                    m2[0] = false; m2[1] = true; m2[2] = true; ;
                    BitArray sohran = new BitArray(2);
                    int provirka = 0;
                    int provirka1 = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        if (k == 0)
                        {
                            for (int l = 0; l < 3; l++)
                            {
                                if (pol1[l] & m1[l] == true)
                                {
                                    provirka++;
                                }
                                else { }
                            }
                        }
                        if (k == 1)
                        {
                            for (int l = 0; l <3; l++)
                            {
                                if (pol1[l] & m2[l] == true)
                                {
                                    provirka1++;
                                }
                                else { }
                            }
                        }

                    }
                    if (provirka % 2 == 0)
                    {
                        resultat[0] = false;
                    }
                    else { resultat[0] = true; }
                    if (provirka1 % 2 == 0)
                    {
                        resultat[1] = false;
                    }
                    else { resultat[1] = true; }
                    for (int k = 0; k < 3; k++)
                    {
                        messageCoded[k + (3 * (i / 1))] = resultat[k];
                    }

                }

                return messageCoded;
            }

        }


        static BitArray MyDeCoding(BitArray messageArray2)
        {
            if (1 == 2)
            {
                int countBits = messageArray2.Count; // кількість біт в масиві
                BitArray messageDeCoded = new BitArray(dovj); // новий пустий масив біт
                int schet = countBits;
                int a = 8;
                int b = 6;
                int r = 5;
                int c = 4;
                int m = 2;
                for (int i = 0; i < schet; i += 5)
                {
                    if (i + 3 >= dovj || i + 4 >= dovj || i + 2 >= dovj || i + 1 >= dovj || i >= dovj) { break; }
                    if (a >= countBits || b >= countBits || r >= countBits || c >= countBits || m >= countBits) { break; }
                    messageDeCoded[i] = messageArray2[a];
                    messageDeCoded[i + 1] = messageArray2[b];
                    messageDeCoded[i + 2] = messageArray2[r];
                    messageDeCoded[i + 3] = messageArray2[c];
                    messageDeCoded[i + 4] = messageArray2[m];
                    a += 9;
                    b += 9;
                    r += 9;
                    c += 9;
                    m += 9;
                }
                return messageDeCoded;
            }
            if (2 == 3)
            {
                int countBits = messageArray2.Count; // кількість біт в масиві
                BitArray messageDeCoded = new BitArray(dovj); // новий пустий масив біт
                int schet = countBits;
                int a = 6;
                int b = 5;
                int r = 4;
                int c = 2;
                for (int i = 0; i < schet; i += 4)
                {
                    if (i + 3 >= dovj || i + 4 >= dovj || i + 2 >= dovj || i + 1 >= dovj || i >= dovj) { break; }
                    if (a >= countBits || b >= countBits || r >= countBits || c >= countBits) { break; }
                    messageDeCoded[i] = messageArray2[a];
                    messageDeCoded[i + 1] = messageArray2[b];
                    messageDeCoded[i + 2] = messageArray2[r];
                    messageDeCoded[i + 3] = messageArray2[c];
                    a += 7;
                    b += 7;
                    r += 7;
                    c += 7;
                }
                return messageDeCoded;
            }

            if (2 == 2)
            {
                int countBits = messageArray2.Count; // кількість біт в масиві
                BitArray messageDeCoded = new BitArray(dovj); // новий пустий масив біт
                int schet = countBits;
                int a = 2;
                for (int i = 0; i < schet; i += 1)
                {
                    if (i >= dovj) { break; }
                    if (a >= countBits ) { break; }
                    messageDeCoded[i] = messageArray2[a];
                    a += 3;
                }
                return messageDeCoded;
            }
        }



        private void button1_Click(object sender, EventArgs e)
            {
                if (flag == true)
                {
                    //File.Create(Directory.GetCurrentDirectory() + "\\test1.txt");
                    //string path1 = Directory.GetCurrentDirectory() + "\\test1.txt";
                    if (File.Exists(abc) == false) return; // перевірка на наявність файла
                    BitArray messageArray = ConvertFileToBitArray(abc); // читаємо файл і записуємо у BitArray
                    BitArray messageCoded = MyCoding(messageArray); // кодуємо bitArray

                //BitArrayToBytes(abcd, messageCoded); // записуємо bitArray у файл
                System.IO.File.WriteAllBytes(abcd, BitArrayToBytes(messageCoded));
                MessageBox.Show("Файл Сохранен");
            }
                if (flag1 == true)
                {
                    //string path1 = Directory.GetCurrentDirectory() + "\\test2.txt";
                    if (File.Exists(abc) == false) return;
                    BitArray messageArray = Convert1FileToBitArray(abc);
                    BitArray messageDeCoded = MyDeCoding(messageArray2);

                //WriteBitArrayToFile(abcd, messageCoded);
                System.IO.File.WriteAllBytes(abcd, BitArrayToBytes(messageDeCoded));
                MessageBox.Show("Файл Сохранен");
            }

                flag = false;
                flag1 = false;


            }


            private void button2_Click(object sender, EventArgs e)
            {

            }

            private void button4_Click(object sender, EventArgs e)
            {
            if (flag == true)
                {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                messageArray1 = ConvertFileToBitArray(filename);
                dovj = messageArray1.Count;
                textBox1.Text = filename;
                abc = filename;
                 }
            if (flag1 == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                messageArray2 = ConvertFileToBitArray(filename);
                textBox1.Text = filename;
            }
            }

            private void button3_Click(object sender, EventArgs e)
            {
            if (flag == true || flag1 == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog1.FileName;
                textBox2.Text = filename;
                abcd = filename;
            }
        }

            private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
            {
                flag = true;
                flag1 = false;
            }

            private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
            {
                flag1 = true;
                flag = false;
            }

            private void button2_Click_1(object sender, EventArgs e)
            {
            textBox1.Clear();
            textBox2.Clear();
            }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            comboBox1.SelectedItem = "4";
        }
    }
    }

