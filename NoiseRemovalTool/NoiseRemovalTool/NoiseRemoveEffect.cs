using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoiseRemovalTool
{


    //Manipulated Image Class
    public class ImageEventArgs : EventArgs
    {
        public Bitmap bmap { get; set; }
        public bool isFinished { get; set; }
    }


    class NoiseRemoveEffect
    {

        //Temp Storage
        string tempPath = Path.GetTempPath();
        //Analysis Parameters
        int imgHeight;
        int imgWidth;
        int imageAmount;
        int imagesComplete;
        float thresholdValue;
        //Stack Level Storage
        List<string> lvl0ImageNames = new List<string>();
        List<string> lvl1ImageNames = new List<string>();
        List<string> lvl2ImageNames = new List<string>();
        List<string> lvl3ImageNames = new List<string>();
        List<string> lvl4ImageNames = new List<string>();
        List<string> lvl5ImageNames = new List<string>();
        int[][] analisisSetup;
        int tempCounter;
        //Lockbits storage variables
        float[][][] originPixAvg;
        float[][][] finalPixAvg;
        List<int>[][] removalPix;


        //[ManipulateImage - Returns a noiseless image created from the list of passed imported images]
        public void ManipulateImage(object analysImages, object analysSetup)
        {
            CreateTempFolder();

            List<string> analisisImagesNames = (List<string>)analysImages;

            analisisSetup = (int[][])analysSetup;
            //analisisSetup codes and variable locations
            //[0][] - img level
            //[][0-3] - img level settings
            //--|[][0] - images per group
            //--|[][1] - groups in level
            //--|[][2] - type of staking to use
            //----|[][2] = 0 (median)
            //----|[][2] = 1 (mean)
            //----|[][2] = 2 (advanced)
            //--|[][3] - threshold (for advanced stack)

            //Iterate over first level execution
            for (int a = 0; a < analisisSetup[0][1]; a++)
            {

                CheckForCompletedLevels();

                lvl0ImageNames = new List<string>();
                for (int b = 0; b < analisisSetup[0][0]; b++)
                    lvl0ImageNames.Add(analisisImagesNames[(a * analisisSetup[0][1]) + b]);

                BeginStacking(lvl0ImageNames, 1);
            }

            int lastLvl = analisisSetup.GetLength(0);
            string finalFileName = null;
            switch (lastLvl)
            {
                case 1:
                    finalFileName = lvl1ImageNames[0];
                    break;
                case 2:
                    finalFileName = lvl2ImageNames[0];
                    break;
                case 3:
                    finalFileName = lvl3ImageNames[0];
                    break;
                case 4:
                    finalFileName = lvl4ImageNames[0];
                    break;
                case 5:
                    finalFileName = lvl5ImageNames[0];
                    break;
                default:
                    break;
            }
            if (finalFileName != null)
                OnImageFinished(new Bitmap(Image.FromFile(finalFileName)), true);
        }

        private void BeginStacking(List<string> imageNames, int resultLvl)
        {
            if (analisisSetup[resultLvl - 1][3] == 0)
                StackMedian(imageNames, resultLvl);
            else if (analisisSetup[resultLvl - 1][3] == 1)
                StackAverage(imageNames, resultLvl);
            else
                StackAdvanced(imageNames, resultLvl);
        }

        private void CheckForCompletedLevels()
        {
            //Level 1 Check
            if (analisisSetup[0][1] == 1)
                return;
            if(lvl1ImageNames.Count >= analisisSetup[1][0])
            {
                Console.WriteLine("Level 1 Stack Begun!");
                BeginStacking(lvl1ImageNames, 2);
                foreach (var filename in lvl1ImageNames)
                    File.Delete(filename);
                lvl1ImageNames = new List<string>();
            }

            //Level 2 Check
            if (analisisSetup[1][1] == 1)
                return;
            if (lvl2ImageNames.Count >= analisisSetup[2][0])
            {
                Console.WriteLine("Level 1 Stack Begun!");
                BeginStacking(lvl2ImageNames, 3);
                foreach (var filename in lvl2ImageNames)
                    File.Delete(filename);
                lvl1ImageNames = new List<string>();
            }

            //Level 3 Check
            if (analisisSetup[2][1] == 1)
                return;
            if (lvl3ImageNames.Count >= analisisSetup[3][0])
            {
                Console.WriteLine("Level 1 Stack Begun!");
                BeginStacking(lvl3ImageNames, 4);
                foreach (var filename in lvl3ImageNames)
                    File.Delete(filename);
                lvl1ImageNames = new List<string>();
            }

            //Level 4 Check
            if (analisisSetup[3][1] == 1)
                return;
            if (lvl4ImageNames.Count >= analisisSetup[4][0])
            {
                Console.WriteLine("Level 1 Stack Begun!");
                BeginStacking(lvl4ImageNames, 5);
                foreach (var filename in lvl4ImageNames)
                    File.Delete(filename);
                lvl1ImageNames = new List<string>();
            }
        }

        //[StackMedian - gets the median pixel out of group and uses it in result pixel]
        private void StackMedian(List<string> analisisImagesNames, int resultLvl)
        {

        }

        //[StackAverage - gets the average of all pixels in the stack and uses in result image]
        private void StackAverage(List<string> analisisImagesNames, int resultLvl)
        {

        }

        //[StackAdvanced - improved method that uses threshold and multiple means to remove noise]
        private void StackAdvanced(List<string> analisisImagesNames, int resultLvl)
        {
            //Load in bitmaps of files
            List<Bitmap> analisisImages = new List<Bitmap>();
            foreach (var file in analisisImagesNames)
                analisisImages.Add(new Bitmap(Image.FromFile(file)));

            //Declare supporting variables and the final image
            imgHeight = analisisImages[0].Height;
            imgWidth = analisisImages[0].Width;
            imageAmount = analisisImagesNames.Count;
            this.thresholdValue = (float) analisisSetup[resultLvl-1][3];
            Bitmap resultImage = new Bitmap(analisisImages[0]);

            //Declare and instantiate the color holding array for current pixel
            List<Color> curPixelColor;
            //Go through each pixel seperately
            for (int i = 0; i < analisisImages[0].Height; i++)
            {
                for (int j = 0; j < analisisImages[0].Width; j++)
                {
                    //Get colors from all images on a specific pixel
                    curPixelColor = new List<Color>();
                    for (int u = 0; u < imageAmount; u++)
                        curPixelColor.Add(analisisImages[u].GetPixel(j, i));


                    //Get integer values for all (R,G,B)
                    int[] redValues = new int[curPixelColor.Count];
                    int[] greenValues = new int[curPixelColor.Count];
                    int[] blueValues = new int[curPixelColor.Count];
                    for (int k = 0; k < curPixelColor.Count; k++)
                    {
                        redValues[k] = curPixelColor[k].R;
                        greenValues[k] = curPixelColor[k].G;
                        blueValues[k] = curPixelColor[k].B;
                    }

                    //Get RGB Totals
                    int totalRed = redValues.Sum();
                    int totalGreen = greenValues.Sum();
                    int totalBlue = blueValues.Sum();

                    //Get RGB Averages
                    int avgColorValueR = totalRed / redValues.Length;
                    int avgColorValueG = totalGreen / greenValues.Length;
                    int avgColorValueB = totalBlue / blueValues.Length;

                    //Throw out invalid pixels
                    for (int f = 0; f < curPixelColor.Count(); f++)
                    {
                        int rVal = Math.Abs(avgColorValueR - redValues[f]);
                        int gVal = Math.Abs(avgColorValueG - greenValues[f]);
                        int bVal = Math.Abs(avgColorValueB - blueValues[f]);
                        int totalDiff = (rVal + gVal + bVal) / 3;
                        if (totalDiff > thresholdValue)
                            curPixelColor.RemoveAt(f);
                    }

                    //If no more pixels present repopulate the pixel array
                    if (curPixelColor.Count == 0)
                        for (int u = 0; u < imageAmount; u++)
                            curPixelColor.Add(analisisImages[u].GetPixel(j, i));

                    //Recalculate correct mean
                    redValues = new int[curPixelColor.Count];
                    greenValues = new int[curPixelColor.Count];
                    blueValues = new int[curPixelColor.Count];
                    for (int k = 0; k < curPixelColor.Count; k++)
                    {
                        redValues[k] = curPixelColor[k].R;
                        greenValues[k] = curPixelColor[k].G;
                        blueValues[k] = curPixelColor[k].B;
                    }
                    totalRed = redValues.Sum();
                    totalGreen = greenValues.Sum();
                    totalBlue = blueValues.Sum();
                    avgColorValueR = totalRed / redValues.Length;
                    avgColorValueG = totalGreen / greenValues.Length;
                    avgColorValueB = totalBlue / blueValues.Length;

                    //Create and Set Final Color
                    Color newColor = Color.FromArgb(avgColorValueR, avgColorValueG, avgColorValueB);
                    resultImage.SetPixel(j, i, newColor);
                }
                if (i % 10 == 0)
                {
                    Bitmap tempImage = new Bitmap(resultImage);
                    Console.WriteLine("Now on: " + i);
                    using (var graphics = Graphics.FromImage(tempImage))
                    {
                        using (var blackPen = new Pen(Color.Red, 8))
                            graphics.DrawLine(blackPen, 0, i, imgWidth, i);
                    }
                    OnImageFinished(tempImage, false);
                }
            }

            //Publish the completion event
            Console.WriteLine("Advanced Stack Complete!");
            WriteImageToLvl(resultImage, resultLvl);
        }

        //[WriteImageToLvl - Writes sub image to apropriate level]
        private void WriteImageToLvl(Bitmap writeImage, int targetLvl)
        {
            tempCounter++;
            string fullSavePath = tempPath + "\\" + tempCounter;
            switch (targetLvl)
            {
                case 1:
                    lvl1ImageNames.Add(fullSavePath);
                    writeImage.Save(fullSavePath);
                    break;
                case 2:
                    lvl2ImageNames.Add(fullSavePath);
                    writeImage.Save(fullSavePath);
                    break;
                case 3:
                    lvl3ImageNames.Add(fullSavePath);
                    writeImage.Save(fullSavePath);
                    break;
                case 4:
                    lvl4ImageNames.Add(fullSavePath);
                    writeImage.Save(fullSavePath);
                    break;
                case 5:
                    lvl5ImageNames.Add(fullSavePath);
                    writeImage.Save(fullSavePath);
                    break;
                default:
                    Console.WriteLine("Image Level Not Found: Temp File NOT Saved in LVL - " + targetLvl);
                    break;
            }
        }



        //[Clamp - returns integer constrained between the min and max given any integer]
        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        



        //LOCKBITS METHOD (NOT WORKING - FIX IN FUTURE)
        //[ManipulateImageLockBits - Returns a noiseless image created from the list of passed imported images w/ faster LockBits method]
        public void ManipulateImageLockBits(object analysisImages, int thresholdInt)
        {

            //STEP 1 - CREATE SUPPORT VARIABLES FOR LATER USE
            //Initial image parameter setup
            List<string> analysisImageNames = (List<string>) analysisImages;
            Bitmap resultImage = new Bitmap(Image.FromFile(analysisImageNames[0]));
            imgHeight = resultImage.Height;
            imgWidth = resultImage.Width;
            imageAmount = analysisImageNames.Count;
            this.thresholdValue = (float) thresholdInt;
            //Storage arrays for all pixel operations
            //---|Original Pixel Averages
            originPixAvg = new float[imgHeight][][];
            for (int i = 0; i < imgHeight; i++)
            {
                originPixAvg[i] = new float[imgWidth][];
                for (int j = 0; j < imgWidth; j++)
                {
                    originPixAvg[i][j] = new float[3];
                }
            }
            //---|Final Image Average Values
            finalPixAvg = new float[imgHeight][][];
            for (int i = 0; i < imgHeight; i++)
            {
                finalPixAvg[i] = new float[imgWidth][];
                for (int j = 0; j < imgWidth; j++)
                {
                    finalPixAvg[i][j] = new float[3];
                }
            }
            //---|Pixels to Skip During Final Calculation
            removalPix = new List<int>[imgHeight][];
            for (int i = 0; i < imgHeight; i++)
            {
                removalPix[i] = new List<int>[imgWidth];
                for (int j = 0; j < imgWidth; j++)
                    removalPix[i][j] = new List<int>();
            }


            //STEP 2 - CALCULATE original pixel averages
            for (int i = 0; i < imageAmount; i++)
            {
                AddImgToOriginAvg(analysisImageNames[i], i);
            }

            //STEP 3 - FIND Pixels to throw out
            for (int i = 0; i < imageAmount; i++)
            {
                FillIgnorePix(analysisImageNames[i], i);
            }

            //STEP 4 - CALCULATE final image pixel averages
            for (int i = 0; i < imageAmount; i++)
            {
                AddImgToFinalAvg(analysisImageNames[i], i);
            }

            //STEP 5 - CREATE & SEND final image
            OnImageFinished(CreateFinalImage(analysisImageNames[0]), true);

            //Publish the event completiom event
            Console.WriteLine("Effect Complete!");
        }

        //[AddImgToOriginAvg - Averages values for all pixels]
        private void AddImgToOriginAvg(string imgName, int i)
        {
            unsafe
            {
                Bitmap curImgLoad = new Bitmap(Image.FromFile(imgName));
                GC.AddMemoryPressure(curImgLoad.Height * curImgLoad.Width * 4);
                BitmapData bitmapData =
                    curImgLoad.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
                    ImageLockMode.ReadWrite, curImgLoad.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(curImgLoad.PixelFormat) / 8;
                int widthInBytes = imgWidth * bytesPerPixel;

                byte* firstPixPointer = (byte*)bitmapData.Scan0;

                Parallel.For(0, imgHeight, y =>
                {
                    byte* currentLine = firstPixPointer + (y + bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int indxX = x / bytesPerPixel;
                        originPixAvg[y][indxX][2] = ((originPixAvg[y][indxX][2] * i) + currentLine[x]) / (i + 1);
                        originPixAvg[y][indxX][1] = ((originPixAvg[y][indxX][1] * i) + currentLine[x + 1]) / (i + 1);
                        originPixAvg[y][indxX][0] = ((originPixAvg[y][indxX][0] * i) + currentLine[x + 2]) / (i + 1);
                    }
                });

                Console.WriteLine("Finished Origin Average Calculculation on Image: " + (i+1));
                curImgLoad.UnlockBits(bitmapData);
            }
        }

        //[AddImgToOriginAvg - Averages values for all pixels]
        private void AddImgToFinalAvg(string imgName, int i)
        {
            unsafe
            {
                Bitmap curImgLoad = new Bitmap(Image.FromFile(imgName));
                GC.AddMemoryPressure(curImgLoad.Height * curImgLoad.Width * 4);
                BitmapData bitmapData =
                    curImgLoad.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
                    ImageLockMode.ReadWrite, curImgLoad.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(curImgLoad.PixelFormat) / 8;
                int widthInBytes = imgWidth * bytesPerPixel;

                byte* firstPixPointer = (byte*)bitmapData.Scan0;

                Parallel.For(0, imgHeight, y =>
                {
                    byte* currentLine = firstPixPointer + (y + bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int indxX = x / bytesPerPixel;
                        if (!removalPix[y][indxX].Contains(i))
                        {
                            finalPixAvg[y][indxX][2] = ((finalPixAvg[y][indxX][2] * i) + currentLine[x]) / (i + 1);
                            finalPixAvg[y][indxX][1] = ((finalPixAvg[y][indxX][1] * i) + currentLine[x + 1]) / (i + 1);
                            finalPixAvg[y][indxX][0] = ((finalPixAvg[y][indxX][0] * i) + currentLine[x + 2]) / (i + 1);
                        }         
                    }
                });

                Console.WriteLine("Finished Final Average Calculculation on Image: " + (i + 1));
                curImgLoad.UnlockBits(bitmapData);
            }
        }

        //[FillIgnorePix - Add extraordinary pixels to ignore list]
        private void FillIgnorePix(string imgName, int i)
        {
            unsafe
            {
                Bitmap curImgLoad = new Bitmap(Image.FromFile(imgName));
                GC.AddMemoryPressure(curImgLoad.Height * curImgLoad.Width * 4);
                BitmapData bitmapData =
                    curImgLoad.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
                    ImageLockMode.ReadWrite, curImgLoad.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(curImgLoad.PixelFormat) / 8;
                int widthInBytes = imgWidth * bytesPerPixel;

                byte* firstPixPointer = (byte*)bitmapData.Scan0;

                Parallel.For(0, imgHeight, y =>
                {
                    byte* currentLine = firstPixPointer + (y + bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int indxX = x / bytesPerPixel;
                        float rVal = Math.Abs(originPixAvg[y][indxX][2] - currentLine[x]);
                        float gVal = Math.Abs(originPixAvg[y][indxX][1] - currentLine[x + 1]);
                        float bVal = Math.Abs(originPixAvg[y][indxX][0] - currentLine[x + 2]);
                        float totalDiff = (float) (rVal + gVal + bVal) / 3f;
                        if (totalDiff > thresholdValue)
                            removalPix[y][indxX].Add(i);
                    }
                });

                Console.WriteLine("Finished Skip Pixels Calculation on Image: " + (i+1));
                curImgLoad.UnlockBits(bitmapData);
            }
        }

        //[AddImgToFinalAvg - Averages values for all pixels]
        private Bitmap CreateFinalImage(string imgName)
        {
            unsafe
            {
                Bitmap curImgLoad = new Bitmap(Image.FromFile(imgName));
                GC.AddMemoryPressure(curImgLoad.Height * curImgLoad.Width * 4);
                BitmapData bitmapData =
                    curImgLoad.LockBits(new Rectangle(0, 0, imgWidth, imgHeight),
                    ImageLockMode.ReadWrite, curImgLoad.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(curImgLoad.PixelFormat) / 8;
                int widthInBytes = imgWidth * bytesPerPixel;

                byte* firstPixPointer = (byte*)bitmapData.Scan0;

                Parallel.For(0, imgHeight, y =>
                {
                    byte* currentLine = firstPixPointer + (y + bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int indxX = x / bytesPerPixel;
                        currentLine[x] = (byte) finalPixAvg[y][indxX][2];
                        currentLine[x + 1] = (byte) finalPixAvg[y][indxX][1];
                        currentLine[x + 2] = (byte) finalPixAvg[y][indxX][0];
                    }
                });

                Console.WriteLine("Finished Creating Final Image");
                curImgLoad.UnlockBits(bitmapData);
                return curImgLoad;
            }
        }





        //EventHandler Delagates Definition
        public event EventHandler<ImageEventArgs> ImageLineFinished;

        //Add Event Method
        protected virtual void OnImageFinished(Bitmap bmap, bool isFinished)
        {
            //If the event has subscribers pass the finished image to the subscribers
            ImageLineFinished?.Invoke(this, new ImageEventArgs() { bmap = bmap , isFinished = isFinished});
        }

        /// <summary>
        /// Creates a temporary folder for sub-result-image storage
        /// </summary>
        private void CreateTempFolder()
        {
            if (!Directory.Exists(tempPath + "\\MasterStaker"))
                Directory.CreateDirectory(tempPath + "\\MasterStaker");
            tempPath = tempPath + "\\MasterStaker";
        }

    }
}
