using System;
using System.Collections;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ImageCompare
{
    public partial class Form1 : Form
    {
        List<string> images = new List<string>();
        bool stopRunning = false;

        List<string> errorList = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                MessageBox.Show("No image found in source folder!");
                return;
            }

            button1.Enabled = false;
            btnCancel.Enabled = true;
            textBox1.Text = "";
            progressBar1.Value = 0;
            progressBar1.Maximum = images.Count;
            errorList.Clear();


            Thread t1 = new Thread(() =>
            {
                RunCommand();
            });

            t1.Start();
        }

        void RunCommand()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i <= images.Count - 1; i++)
            {
                int threadNum = i; // To avoid the captured variable trap
                threads.Add(new Thread(() => Analyze(images[threadNum].ToString())));
            }
            // Run 10 threads at a time until all threads are complete
            int batchSize = 20;
            for (int i = 0; i < threads.Count; i += batchSize)
            {
                if (!stopRunning)
                {
                    for (int j = i; j < Math.Min(i + batchSize, threads.Count); j++)
                    {
                        threads[j].Start();
                    }

                    for (int j = i; j < Math.Min(i + batchSize, threads.Count); j++)
                    {
                        threads[j].Join();
                        if (progressBar1.InvokeRequired)
                        {
                            progressBar1.Invoke(new MethodInvoker(delegate
                            {
                                progressBar1.Maximum = threads.Count;
                                progressBar1.Value++;
                            }));
                        }
                        //progressBar1.Value++;
                    }
                }
            }

            Thread.SpinWait(5000);
            Console.WriteLine("Finished");
            MessageBox.Show("Proccess Complete ");
            if (button1.InvokeRequired)
            {
                button1.Invoke(new MethodInvoker(delegate
                {
                    button1.Enabled = true;
                }));
            }
            if (btnCancel.InvokeRequired)
            {
                btnCancel.Invoke(new MethodInvoker(delegate
                {
                    btnCancel.Enabled = false;
                }));
            }

        }

        void Analyze(string image)
        {

            var imgBytes = Helper.DownloadImageAsBytes(image);
            ImageFile imgFile = new ImageFile(imgBytes);
            if (!imgFile.Complete)
            {
                Console.WriteLine("Thread complete for image: ", image);
                errorList.Add(image);
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new MethodInvoker(delegate
                    {
                        textBox1.Text += image + Environment.NewLine;
                    }));
                }
            }
        }

        private void SelectInputFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select Input Folder";
                fbd.UseDescriptionForTitle = true;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    images.Clear();
                    var items = Directory.GetFiles(fbd.SelectedPath);

                    foreach (var file in items)
                    {
                        string extension = Path.GetExtension(file).ToLower();

                        if (extension == ".tga" || extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                        {
                            images.Add(file);
                        }
                    }

                    label1.Text = images.Count.ToString() + " Images found!";
                    lblAddress.Text = fbd.SelectedPath;
                }
            }

        }

        private void BTNBrowse_Click(object sender, EventArgs e)
        {
            SelectInputFolder();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                Clipboard.SetText(textBox1.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stopRunning = true;
        }

        bool SortAsync = false;
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            SortAsync = !SortAsync;
            if (SortAsync)
            {
                errorList.Sort();
            }
            else
            {
                errorList.Reverse();
            }
            foreach (var item in errorList)
            {
                textBox1.Text += item.ToString() + Environment.NewLine;
            }
        }

        public void ImageViewer(string path)
        {
            Process.Start("explorer.exe", path);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = @"http://www.instagram.com/Bigjavani";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = true;
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            int selectedline = textBox1.GetLineFromCharIndex(textBox1.SelectionStart);
            string path = errorList[selectedline].ToString();
            ImageViewer(path);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (errorList.Count <= 0)
                return;

            try
            {
                List<string> frameNumbers = new List<string>();
                frameNumbers.Clear();

                foreach (var item in errorList)
                {
                    string fileName = Path.GetFileName(item);
                    int dotIndex = fileName.IndexOf('.');
                    fileName = fileName.Substring(0, dotIndex);
                    int underlineIndex = fileName.IndexOf('_', fileName.Length - 7);
                    string frameNumber = int.Parse(fileName.Substring(underlineIndex + 1)).ToString();

                    frameNumbers.Add(frameNumber);
                }
                textBox1.Text = "";
                string copyText = "";
                frameNumbers.Sort();

                for (int i = 0; i < frameNumbers.Count - 1; i++)
                {
                    textBox1.Text += frameNumbers[i] + Environment.NewLine;
                }

                int start = int.Parse(frameNumbers[0]);
                int end = int.Parse(frameNumbers[0]);

                for (int i = 1; i < frameNumbers.Count; i++)
                {
                    if (int.Parse(frameNumbers[i]) == end + 1)
                    {
                        end = int.Parse(frameNumbers[i]);
                    }
                    else
                    {
                        copyText += start == end ? $"{start}, " : $"{start}-{end}, ";
                        start = end = int.Parse(frameNumbers[i]);
                    }
                }
                copyText += start == end ? $"{start}" : $"{start}-{end}";

                Clipboard.SetText(copyText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

public class Helper
{
    public static byte[] DownloadImageAsBytes(String url)
    {
        byte[] fileByteArray = File.ReadAllBytes(url);
        return fileByteArray;

        //using (var webClient = new WebClient())
        //{
        //    return webClient.DownloadData(url);
        //}
    }
}

public class ImageFile
{

    private Types _eFileType = Types.FileNotFound;
    private bool _blComplete = false;
    public bool Complete
    {
        get { return _blComplete; }
    }
    private int _iEndingNull = 0;

    private readonly byte[] _abTagPNG = { 137, 80, 78, 71, 13, 10, 26, 10 };
    private readonly byte[] _abTagJPG = { 255, 216, 255 };
    private readonly byte[] _abTagGIFa = { 71, 73, 70, 56, 55, 97 };
    private readonly byte[] _abTagGIFb = { 71, 73, 70, 56, 57, 97 };
    private readonly byte[] _abEndPNG = { 73, 69, 78, 68, 174, 66, 96, 130 };
    private readonly byte[] _abEndJPGa = { 255, 217, 255, 255 };
    private readonly byte[] _abEndJPGb = { 255, 217 };
    private readonly byte[] _abEndGIF = { 0, 59 };

    public enum Types { FileNotFound, FileEmpty, FileNull, FileTooLarge, FileUnrecognized, PNG, JPG, GIFa, GIFb }

    public ImageFile(byte[] abtTmp)
    {

        _eFileType = Types.FileUnrecognized; // default if found

        //byte[] abtTmp = File.ReadAllBytes(_sFilename);
        // check the length of actual data
        int iLength = abtTmp.Length;
        if (abtTmp[abtTmp.Length - 1] == 0)
        {
            for (int i = (abtTmp.Length - 1); i > -1; i--)
            {
                if (abtTmp[i] != 0)
                {
                    iLength = i;
                    break;
                }
            }
        }
        // check that there is actual data
        if (iLength == 0)
        {
            _eFileType = Types.FileNull;
        }
        else
        {
            _iEndingNull = (abtTmp.Length - iLength);
            // resize the data so we can work with it
            Array.Resize<byte>(ref abtTmp, iLength);
            // get the file type
            if (_StartsWith(abtTmp, _abTagPNG))
            {
                _eFileType = Types.PNG;
            }
            else if (_StartsWith(abtTmp, _abTagJPG))
            {
                _eFileType = Types.JPG;
            }
            else if (_StartsWith(abtTmp, _abTagGIFa))
            {
                _eFileType = Types.GIFa;
            }
            else if (_StartsWith(abtTmp, _abTagGIFb))
            {
                _eFileType = Types.GIFb;
            }
            // check the file is complete
            switch (_eFileType)
            {
                case Types.PNG:
                    _blComplete = _EndsWidth(abtTmp, _abEndPNG);
                    break;
                case Types.JPG:
                    _blComplete = (_EndsWidth(abtTmp, _abEndJPGa) || _EndsWidth(abtTmp, _abEndJPGb));
                    break;
                case Types.GIFa:
                case Types.GIFb:
                    _blComplete = _EndsWidth(abtTmp, _abEndGIF);
                    break;
            }
            // get rid of ending null bytes at caller's option
            //if(_blComplete && cullEndingNullBytes) File.WriteAllBytes(_sFilename, abtTmp);
        }

    }

    public Types FileType { get { return _eFileType; } }
    public bool IsComplete { get { return _blComplete; } }
    public int EndingNullBytes { get { return _iEndingNull; } }

    private bool _StartsWith(byte[] data, byte[] search)
    {
        bool blRet = false;
        if (search.Length <= data.Length)
        {
            blRet = true;
            for (int i = 0; i < search.Length; i++)
            {
                if (data[i] != search[i])
                {
                    blRet = false;
                    break;
                }
            }
        }
        return blRet; // RETURN
    }

    private bool _EndsWidth(byte[] data, byte[] search)
    {
        bool blRet = false;
        if (search.Length <= data.Length)
        {
            int iStart = (data.Length - search.Length);
            blRet = true;
            for (int i = 0; i < search.Length; i++)
            {
                if (data[iStart + i] != search[i])
                {
                    blRet = false;
                    break;
                }
            }
        }
        return blRet; // RETURN
    }
}