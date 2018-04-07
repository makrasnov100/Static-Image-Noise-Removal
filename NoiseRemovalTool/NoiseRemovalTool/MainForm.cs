using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoiseRemovalTool
{
    public partial class MainForm : Form
    {
        //Effect Ideas:
        //--|Noise Removal
        //--|Show Dominant color

        //Validations:
        //--|All images have to be thesame resolution
        //--|Images have to be similar at the pixel level

        //Bugs
        //--|Using too much memory when loading images


        List <string> loadedImageNamesFull = new List<string>();
        List <string> loadedImageNames = new List<string>();
        Bitmap viewedImage;

        NoiseRemoveEffect removeNoise = new NoiseRemoveEffect();
        FileOperations getImages = new FileOperations();

        int thresholdLevel = 8;
        long curMemoryStrain = 1;
        bool isZoomRequested = false;

        //Boolean state controls
        bool isStarted = false;


        public MainForm()
        {
            //Create the form UI components
            InitializeComponent();

            //Subscibe to the image finished event
            removeNoise.ImageLineFinished += OnImageFinished;
            thresholdBar.Value = 8;
            progressLabel.Text = "Max RGB Deviation: " + thresholdLevel;
        }

        //[DisplayImage - Shows the passed bmp image inside the picture box]
        public void DisplayImage(Bitmap shownImage, string title = "")
        {
            pictureBox.Image = shownImage;
            if (title != "")
                statusLabel.Text = "Viewing: " + title;
            if (curMemoryStrain < shownImage.Width * shownImage.Height * 4)
            {
                GC.RemoveMemoryPressure(curMemoryStrain);
                long viewedImageSize = viewedImage.Width * viewedImage.Height * 4;
                curMemoryStrain = viewedImageSize;
                GC.AddMemoryPressure(viewedImageSize);
            }
        }

        //[OnImageFinished - resets picture box with modified image]
        public void OnImageFinished(object sender, ImageEventArgs e)
        {
            DisplayImage(e.bmap);
            //if (e.isFinished)
            //{
            //    loadedImages.Add(e.bmap);
            //    //ListViewItem lvi = new ListViewItem("Result_Image_" + imagesCreated);
            //    imagesCreated++;
            //    //listView.Items.Add(lvi);
            //    isStarted = false;
            //}
        }

        //BUTTON CONTROL
        private void btnStop_Click(object sender, EventArgs e)
        {
        }

        //[btnLoad_Click - Allows users to begin the image conversion proccess]
        private void btnBegin_Click(object sender, EventArgs e)
        {
            if (loadedImageNamesFull.Count < 2)
                return;

            isStarted = true;

            //Debug Setup for 8 images
            int[][] setup = new int[1][];
            setup[0] = new int[4];
            setup[0][0] = 8;
            setup[0][1] = 1;
            setup[0][2] = 2;
            setup[0][3] = thresholdLevel;


            //Start analyzation on a seperate thread
            Thread t1 = new Thread(() => removeNoise.ManipulateImage(loadedImageNamesFull, setup));
            t1.Start();


            //removeNoise.ManipulateImage(loadedImages);
        }

        //[btnRemove_Click - Allows users to remove certain imported images]
        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Storage of the removal images
            ListView.SelectedIndexCollection selectIndexes = listView.SelectedIndices;
            List<int> removeIndexes = new List<int>();

            //Reverse index order flow so there is no out of bound exeption
            foreach (int index in selectIndexes)
                removeIndexes.Add(index);
            removeIndexes.Reverse();
            foreach (int index in removeIndexes)
                loadedImageNamesFull.RemoveAt(index);

            //Remove selected photos from the GUI list view
            foreach (ListViewItem eachItem in listView.SelectedItems)
                listView.Items.Remove(eachItem);

            //Load the next avaliable picture into the picture box (if any)
            if(listView.Items.Count > 0)
            {
                viewedImage = null;
                viewedImage = new Bitmap(Image.FromFile(loadedImageNamesFull[0]));
                DisplayImage(viewedImage, loadedImageNamesFull[0]);
                listView.Items[0].Selected = true;
                listView.Select();
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        //[btnLoad_Click - Allows users to select image(s) for import upon click of "Load"]
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (isStarted)
                return;

            //Add Images Data to Lists
            FileOperations.ImportPackage newPackage = getImages.OpenFile();
            int importCount = 0;
            foreach (String itemName in newPackage.importedImageNames)
            {
                ListViewItem lvi = new ListViewItem(itemName);
                listView.Items.Add(lvi);
                loadedImageNamesFull.Add(newPackage.importedImageNamesFull[importCount]);
                loadedImageNames.Add(newPackage.importedImageNames[importCount]);
                importCount++;
            }

            //Displays the first loaded image
            if(loadedImageNamesFull.Count >= 1)
            {
                viewedImage = null;
                viewedImage = new Bitmap(Image.FromFile(loadedImageNamesFull[0]));
                DisplayImage(viewedImage, loadedImageNamesFull[0]);
            }
        }

        //[btnSave_Click - Allows users to save image(s) upon click of "Save"]
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Displays a SaveFileDialog so the user can save the Image  
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            sfd.Title = "Save an Image File";
            sfd.ShowDialog();

            //If the file name is not an empty string open it for saving.  
            if (sfd.FileName != "")
            {
                //Saves the Image via a FileStream created by the OpenFile method.  
                System.IO.FileStream fs =
                   (System.IO.FileStream)sfd.OpenFile();
                //Saves the Image in the appropriate ImageFormat based upon the  
                //File type selected in the dialog box.  
                //NOTE that the FilterIndex property is one-based.  
                switch (sfd.FilterIndex)
                {
                    case 1:
                        pictureBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        //OTHER EVENTS
        //[listView_SelectedIndexChanged - Shows new image when listview selection changed]
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Find indexes of new selection
            ListView.SelectedIndexCollection selectIndexes = listView.SelectedIndices;

            //Return if the action was to deselect
            if (selectIndexes.Count == 0)
                return;

            //Display the first index of the selected indexes
            viewedImage = null;
            viewedImage = new Bitmap(Image.FromFile(loadedImageNamesFull[selectIndexes[0]]));
            long viewedImageSize = viewedImage.Width * viewedImage.Height * 4;
            GC.AddMemoryPressure(viewedImageSize);
            DisplayImage(viewedImage, loadedImageNamesFull[selectIndexes[0]]);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listView.Select();
        }

        private void thresholdBar_Scroll(object sender, EventArgs e)
        {
            thresholdLevel = thresholdBar.Value;
            progressLabel.Text = "Max RGB Deviation: " + thresholdLevel;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isZoomRequested = true;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            isZoomRequested = false;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isZoomRequested)
            {
                MouseEventArgs mouse = (MouseEventArgs)e;
                Point mPos = mouse.Location;
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isZoomRequested = false;
        }
    }
}
