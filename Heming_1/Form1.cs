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
        
            static BitArray ConvertFileToBitArray(string path)
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                BitArray messageArray = new BitArray(fileBytes);

                return messageArray;
            }

        //static void WriteBitArrayToFile(string fileName, BitArray bitArray)
        //{
        //    byte[] bytes = new byte[bitArray.Length / 8 + (bitArray.Length % 8 == 0 ? 0 : 1)];
        //    bitArray.CopyTo(bytes, 0);

        //    string path = Directory.GetCurrentDirectory() + "\\" + fileName;

        //    //File.Create(path);
        //    using (FileStream fs = File.Create(path))
        //    {
        //        fs.Write(bytes, 0, bytes.Length);
        //    }
        //}
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
                int countBits = messageArray.Count; // кількість біт в масиві
                BitArray messageCoded = new BitArray(countBits, false); // новий пустий масив біт

                //for (int i = 0; i < countBits; i+=2) // якесь кодування (це заміни на те що потрібно)
                //{
                //    messageCoded[i] = messageArray[i] ^ true;
                //    messageCoded[i+1] = messageArray[i+1] ^ false;
                //}
                for (int i = 0; i < countBits; i++)
                {
                    if (messageArray[i] == true)
                        messageCoded[i] = true;
                    else
                        messageCoded[i] = false;
                }
                int messageInd = 0;
                int retInd = 0;
                int controlIndex = 1;
                var retArray = new BitArray(messageCoded.Length + 1 + (int)Math.Ceiling(Math.Log(messageCoded.Length, 2)));
                while (messageInd < messageCoded.Length)
                {
                    if (retInd + 1 == controlIndex)
                    {
                        retInd++;
                        controlIndex = controlIndex * 2;
                        continue;
                    }
                    retArray.Set(retInd, messageCoded.Get(messageInd));
                    messageInd++;
                    retInd++;
                }
                retInd = 0;
                controlIndex = 1 << (int)Math.Log(retArray.Length, 2);
                while (controlIndex > 0)
                {
                    int c = controlIndex - 1;
                    int counter = 0;

                    while (c < retArray.Length)
                    {
                        for (int i = 0; i < controlIndex && c < retArray.Length; i++)
                        {
                            if (retArray.Get(c))
                                counter++;
                            c++;
                        }
                        c += controlIndex;
                    }

                    if (counter % 2 != 0) retArray.Set(controlIndex - 1, true);
                    controlIndex = controlIndex / 2;
                }
                return retArray;
                //return messageCoded;
            }

            static BitArray MyDeCoding(BitArray messageArray)
            {
                int countBits = messageArray.Count; // кількість біт в масиві
                BitArray messageCoded = new BitArray(countBits, false); // новий пустий масив біт

                for (int i = 0; i < countBits; i++)
                {
                    if (messageArray[i] == true)
                        messageCoded[i] = true;
                    else
                        messageCoded[i] = false;
                }
                var decodedArray = new BitArray((int)(messageCoded.Count - Math.Ceiling(Math.Log(messageCoded.Count, 2))), false);
                int count = 0;
                for (int i = 0; i < messageCoded.Length; i++)
                {
                    for (int j = 0; j < Math.Ceiling(Math.Log(messageCoded.Count, 2)); j++)
                    {
                        if (i == Math.Pow(2, j) - 1)
                            i++;
                    }
                    decodedArray[count] = messageCoded[i];
                    count++;
                }
                BitArray strDecodedArray = new BitArray(countBits);
                for (int i = 0; i < decodedArray.Length; i++)
                {
                    if (decodedArray[i])
                        strDecodedArray[i] = true;
                    else
                        strDecodedArray[i] = false;
                }
                var checkArray = MyCoding(strDecodedArray);
                byte[] failBits = new byte[checkArray.Length - decodedArray.Length];
                count = 0;
                bool isMistake = false;
                for (int i = 0; i < checkArray.Length - decodedArray.Length; i++)
                {
                    if (messageCoded[(int)Math.Pow(2, i) - 1] != checkArray[(int)Math.Pow(2, i) - 1])
                    {
                        failBits[count] = (byte)(Math.Pow(2, i));
                        count++;
                        isMistake = true;
                    }
                }
                if (isMistake)
                {
                    int mistakeIndex = 0;
                    for (int i = 0; i < failBits.Length; i++)
                        mistakeIndex += failBits[i];
                    mistakeIndex--;
                    messageCoded.Set(mistakeIndex, !messageCoded[mistakeIndex]);
                    Console.WriteLine($"Ошибка в бите №{mistakeIndex}");
                    count = 0;
                    for (int i = 0; i < messageCoded.Length; i++)
                    {
                        for (int j = 0; j < Math.Ceiling(Math.Log(messageCoded.Count, 2)); j++)
                        {
                            if (i == Math.Pow(2, j) - 1)
                                i++;
                        }
                        decodedArray[count] = messageCoded[i];
                        count++;
                    }
                }
                return decodedArray;
            }

            bool flag = false;
            bool flag1 = false;
        string abc;
        string abcd;

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
                    BitArray messageArray = ConvertFileToBitArray(abc);
                    BitArray messageCoded = MyDeCoding(messageArray);

                //WriteBitArrayToFile(abcd, messageCoded);
                System.IO.File.WriteAllBytes(abcd, BitArrayToBytes(messageCoded));
            }

                flag = false;
                flag1 = false;


            }


            private void button2_Click(object sender, EventArgs e)
            {

            }

            private void button4_Click(object sender, EventArgs e)
            {

                if (flag == true || flag1 == true)
                {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                var fileText = System.IO.File.ReadAllBytes(filename);
                textBox1.Text = filename;
                abc = filename;
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
            }

            private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
            {
                flag1 = true;
            }

            private void button2_Click_1(object sender, EventArgs e)
            {
            
            flag = false;
            flag1 = false;
            }
        }
    }

