using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoiseRemovalTool
{
    class FileOperations
    {
        /// <summary>
        /// Allows for any set of images to be imported into the system for the analysis
        /// </summary>

        //Variables that stores the image(s) currently being imported
        public class ImportPackage
        {
            public List<string> importedImageNamesFull = new List<string>();
            public List<string> importedImageNames = new List<string>();
        }


        //Open file dialog and converted selected image to a bmp object
        public ImportPackage OpenFile()
        {
            //Declare and instatiate a new file selection dialog
            OpenFileDialog ofd = new OpenFileDialog();

            //Declare and Instatiate the Package of Information
            ImportPackage newPackage = new ImportPackage();

            //Set Import Dialog initial location and supported file types
            ofd.InitialDirectory = "C:\\Images";
            ofd.Filter = "Image Files| *.jpg; *.jpeg; *.png; *.bmp";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = true;
            ofd.Title = "Please Select Source File(s) for Analysis";

            //Converted the image if one was selected
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach(var file in ofd.FileNames)
                {
                    newPackage.importedImageNamesFull.Add(file);
                    newPackage.importedImageNames.Add(file.Substring(file.LastIndexOf("\\")+1, file.Length - (file.LastIndexOf("\\")+1)));
                }
            }

            //return the resulting image(s0
            return newPackage;
        }
    }
}
