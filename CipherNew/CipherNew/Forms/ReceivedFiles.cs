using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CipherNew.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CipherNew.Forms
{
    public partial class ReceivedFiles : Form
    {
        private Dictionary<string, string> _cachedFiles;
        private string _folderPath;

        public ReceivedFiles(List<CachedFile> files)
        {
            _cachedFiles = new Dictionary<string, string>();
            _folderPath = String.Empty;

            InitializeComponent();

            foreach (CachedFile file in files)
            {
                chkListBoxFiles.Items.Add(file.Name);
                _cachedFiles[file.Name] = file.Text;
            }   //caching files in dictionary

            chkListBoxFiles.CheckOnClick = true;
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (chkListBoxFiles.CheckedItems.Count == 0)
            {
                MessageBox.Show("SELECT FILES");
                return;
            }
            if (_folderPath == String.Empty)
            {
                MessageBox.Show("SELECT A FOLDER");
                return;
            }

            //string path = ConfigurationManager.AppSettings["received_files_folder"];
            foreach (var checkedItem in chkListBoxFiles.CheckedItems)
            {
                string selectedFilename = checkedItem.ToString();
                string pathFull = $"{_folderPath}\\{selectedFilename}";
                WriteToFile(pathFull, _cachedFiles[selectedFilename]);
            }

            MessageBox.Show("Files are downloaded succesfully");
        }

        private string SelectFolderPath()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = ".\\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
            {
                return String.Empty;
            }
            lblFolderName.Text = dialog.FileName;

            return dialog.FileName;
        }

        private void WriteToFile(string filePath, string data)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                fileStream.Write(dataBytes, 0, dataBytes.Length);
            }
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            _folderPath = SelectFolderPath();
        }

        /*        private void CreateFolderByInput()
                {
                    string path = ConfigurationManager.AppSettings["received_files_folder"];
                    string pathFull = $"{path}{txtInputFolder.Text}";
                    Directory.CreateDirectory(pathFull);
                }*/


    }
}
