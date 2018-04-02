using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEdit
{
    public partial class Form1 : Form
    {
        string filePath;

        public Form1()
        {
            InitializeComponent();
            filePath = null;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.Clear();
            filePath = null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (.txt)|*.txt";
            openFileDialog.Title = "Open File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader stream = new StreamReader(openFileDialog.FileName);
                richText.Text = stream.ReadToEnd();
                filePath = openFileDialog.FileName;
                stream.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath == null)
            {
                saveAs();
            }
            else
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.WriteAllText(filePath, richText.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The operation failed. You may need to run the program as an administrator.\n\n" + ex.ToString());
                    }
                }
                else
                {
                    saveAs();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richText.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = richText.SelectionFont;
            if (font.ShowDialog() == DialogResult.OK)
            {
                richText.SelectionFont = font.Font;
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked)
            {
                richText.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
            }
            else
            {
                richText.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
            }
        }

        private void saveAs()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text Files (.txt)|*.txt";
            save.Title = "Save File";

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter stream = new StreamWriter(save.FileName);
                stream.Write(richText.Text);
                filePath = save.FileName;
                stream.Close();
            }
        }
    }
}
