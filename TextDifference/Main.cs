﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDifference
{
    public partial class MainForm : Form
    {
        string firstText, secondText;
        int selectFirst, selectLast;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text1RichTextBox.SelectionBackColor = Color.Yellow;
            Text1RichTextBox.AutoWordSelection = false;
            Text1RichTextBox.Text = null;
            Text2RichTextBox.Text = null;
        }

        private void LoadText1Button_Click(object sender, EventArgs e)
        {
            SelectFileFromDialog("1");

            //GetMissingWord();
            //HighlightText();
        }

        private void LoadText2Button_Click(object sender, EventArgs e)
        {
            SelectFileFromDialog("2");
            HighlightText();
        }

        void SelectFileFromDialog(string richTextBox)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {

                        using (TextReader reader = new StreamReader(myStream, Encoding.GetEncoding(1252), true)) // Using special encoding to get Swedish åäö letters
                        {
                            String text = reader.ReadToEnd();
                            if(richTextBox == "1")
                            {
                                Text1RichTextBox.Text = text;
                                firstText = Text1RichTextBox.Text;
                            }
                            else
                            {
                                Text2RichTextBox.Text = text;
                                secondText = Text2RichTextBox.Text;
                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        void CompareStrings(string text1, string text2)
        {
            int result = 0;

            result = string.Compare(text1, text2);


        }

        void GetMissingWord()
        {
            if (string.IsNullOrEmpty(firstText))
            {
                return;
            }
            if (string.IsNullOrEmpty(secondText))
            {
                return;
            }
            List<string> listFirstText = new List<string>();
            List<string> listSecondText = new List<string>();
            List<int> listRemovedWords = new List<int>();

            // Add words to First Text list
            string[] wordsText1 = firstText.Split(' ');
            foreach (string word in wordsText1)
            {
                listFirstText.Add(word);
            }

            // Add words to Second Text list
            string[] wordsText2 = secondText.Split(' ');
            foreach (string word in wordsText2)
            {
                listSecondText.Add(word);
            }

            int num = 0;

            while(num < 1)
            {
                num++;
                int index = 0;
                foreach(string word in listFirstText)
                {
                    if(word == listSecondText[index])
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        listRemovedWords.Add(index);
                        listSecondText.RemoveAt(index);
                        num = 0;
                        break;
                    }
                }
            }
            //foreach(string wurd in listFirstText)
            //{
            //    Text1RichTextBox.Text += Environment.NewLine + wurd;
            //}
        }

        void MatchString(string text1, string text2)
        {

        }

        int CountSpaceChars(string value)
        {
            int result = 0;
            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    result++;
                }
            }
            return result;
        }

        void HighlightText()
        {
            //if (String.IsNullOrEmpty(Text1RichTextBox.Text))
            //{
            //    return;
            //}
            //int start = 0;
            //int end = Text1RichTextBox.Text.LastIndexOf("hoppade");

            Text1RichTextBox.SelectAll();
            Text1RichTextBox.SelectionBackColor = Color.Transparent;


            //Text1RichTextBox.Find("hoppade", 0, Text1RichTextBox.TextLength, RichTextBoxFinds.MatchCase);

            //Text1RichTextBox.Select(5, 10);
            
            //Text1RichTextBox.SelectionBackColor = Color.Yellow;

            Text1RichTextBox.Select(20, 27 - 20);

            Text1RichTextBox.SelectionBackColor = Color.Yellow;

            //start = Text1RichTextBox.Text.IndexOf("hoppade", start) + 1;

        }

        
    }
}
