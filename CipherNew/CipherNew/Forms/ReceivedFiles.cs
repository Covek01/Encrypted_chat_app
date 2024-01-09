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

namespace CipherNew.Forms
{
    public partial class ReceivedFiles : Form
    {
        private Dictionary<string, string> _cachedFiles;

        public ReceivedFiles(List<CachedFile> files)
        {
            _cachedFiles = new Dictionary<string, string>();

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
            CreateFolderByInput();
            string path = ConfigurationManager.AppSettings["received_files_folder"];
            foreach (var checkedItem in chkListBoxFiles.CheckedItems)
            {
                string selectedFilename = checkedItem.ToString();
                string pathFull = $"{path}{txtInputFolder.Text}\\{selectedFilename}";
                WriteToFile(pathFull, _cachedFiles[selectedFilename]);
            }

            MessageBox.Show("Files are downloaded succesfully");
        }

        private void WriteToFile(string filePath, string data)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                fileStream.Write(dataBytes, 0, dataBytes.Length);
            }
        }

        private void CreateFolderByInput()
        {
            string path = ConfigurationManager.AppSettings["received_files_folder"];
            string pathFull = $"{path}{txtInputFolder.Text}";
            Directory.CreateDirectory(pathFull);
        }

        private void txtInputFolder_TextChanged(object sender, EventArgs e)
        {
            if (txtInputFolder.Text != "" && txtInputFolder.Text != "Received")
            {
                btnDownload.Enabled = true;
            }
            else
            {
                btnDownload.Enabled = false;
            }
        }
    }
}
