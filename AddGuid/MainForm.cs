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

namespace AddGuid
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (FilesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryTextBox.Text = FilesFolderBrowserDialog.SelectedPath;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DirectoryTextBox.Text))
            {
                MessageBox.Show("You should select the folder for the files", "Empty Folder", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(DirectoryTextBox.Text))
            {
                MessageBox.Show("Folder does not exist", "Nonexistence Folder", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            string[] files = Directory.GetFiles(DirectoryTextBox.Text);
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    string filename = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);
                    string path = Path.GetDirectoryName(file);
                    if (!string.IsNullOrEmpty(HyphenTextBox.Text))
                        filename += HyphenTextBox.Text;
                    filename += Guid.NewGuid().ToString("N");
                    string newFile = Path.ChangeExtension(filename, extension);
                    string newpath = Path.Combine(path, newFile);
                    File.Move(file, newpath);
                }
            }
            MessageBox.Show("File names changed successfully", "Success", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        }
    }
}
