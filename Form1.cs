
using ExifLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;

namespace metadata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                foreach (string dirFile in Directory.GetFiles(folderDlg.SelectedPath))
                {
                    
                    FileInfo fi = new FileInfo(dirFile);
                    string extn = fi.Extension;
                    if (String.Equals(extn, ".jpg"))
                    {
                        try
                        {
                            using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                BitmapSource img = BitmapFrame.Create(fs);
                                BitmapMetadata md = (BitmapMetadata)img.Metadata;
                                string date = md.DateTaken;
                                string cam = md.CameraModel;
                                var file = ImageFile.FromFile(dirFile);
                                var latTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLatitude);
                                string loca = latTag + "";
                                var longTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLongitude);
                                string longt = longTag + "";
                                string make = md.CameraManufacturer;
                                string copy = md.Copyright;
                                string name = fi.Name;
                                string size = fi.Length.ToString();
                                string[] row = { name, dirFile, size + " bytes", date, make, cam, loca, longt, copy };
                                var listViewItem = new ListViewItem(row);
                                listView1.Items.Add(listViewItem);
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (String.Equals(extn, ".png"))
                    {
                       try
                       {

                            using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                BitmapSource img = BitmapFrame.Create(fs);
                                BitmapMetadata md = (BitmapMetadata)img.Metadata;
                                string date = md.DateTaken;
                                string cam = "Not supported";
                                string loca = "Not supported";
                                string longt = "Not supported";
                                string make = "Not supported";
                                string copy = "Not supported";
                                string name = fi.Name;
                                string size = fi.Length.ToString();
                                string[] row = { name, dirFile, size + " bytes", date, make, cam, loca, longt, copy };
                                var listViewItem = new ListViewItem(row);
                                listView1.Items.Add(listViewItem);
                            }
                        }
                       catch
                       {

                       }
                       


                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A File";
            openDialog.Filter = "Image Files (*.png;*.jpg)|*.png;*.jpg" + "|" +
                                "All Files (*.*)|*.*";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(openDialog.FileName);
                    string extn = fi.Extension;
                    if (String.Equals(extn, ".jpg"))
                    {
                        try
                        {
                            using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                BitmapSource img = BitmapFrame.Create(fs);
                                BitmapMetadata md = (BitmapMetadata)img.Metadata;
                                string date = md.DateTaken;
                                string cam = md.CameraModel;
                                 var file = ImageFile.FromFile(openDialog.FileName);
                                 var latTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLatitude);
                                  string loca = latTag + "";
                                  var longTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLongitude);
                                   string longt = longTag + "";
                                  string make = md.CameraManufacturer;
                                string copy = md.Copyright;
                                string name = fi.Name;
                                string size = fi.Length.ToString();
                                string[] row = { name, openDialog.FileName, size + " bytes", date, make, cam, loca, longt, copy };
                                var listViewItem = new ListViewItem(row);
                                listView1.Items.Add(listViewItem);
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (String.Equals(extn, ".png"))
                    {
                       try
                       {

                            using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            BitmapSource img = BitmapFrame.Create(fs);
                            BitmapMetadata md = (BitmapMetadata)img.Metadata;
                            string date = md.DateTaken;
                            string cam = "Not supported";
                            string loca = "Not supported";
                            string longt = "Not supported";
                            string make = "Not supported";
                            string copy = "Not supported";
                            string name = fi.Name;
                            string size = fi.Length.ToString();
                            string[] row = { name, openDialog.FileName, size + " bytes", date, make, cam, loca, longt, copy };
                            var listViewItem = new ListViewItem(row);
                            listView1.Items.Add(listViewItem);
                        }
                        }
                       catch
                       {

                       }
                       


                    }


            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string a = textBox1.Text;
                foreach (string dirFile in Directory.GetFiles(a))
                {

                    FileInfo fi = new FileInfo(dirFile);
                    string extn = fi.Extension;
                    string Name = fi.Name;
                    if (String.Equals(extn, ".jpg"))
                    {
                        var file = ImageFile.FromFile(dirFile);
                        file.Properties.Clear();
                        Directory.CreateDirectory("output");
                        file.Save("output/" + Name);
                    }
                    if (String.Equals(extn, ".png"))
                    {

                        var file = ImageFile.FromFile(dirFile);
                        file.Properties.Clear();
                        Directory.CreateDirectory("output");
                        file.Save("output/" + Name);
                        

                    }
                    
                }
                Process.Start("explorer.exe", "output");
            }
            catch
            {

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            textBox1.Text = "";
        }

        
    }
}
