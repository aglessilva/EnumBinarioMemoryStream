using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using IronOcr;
using IronOcr.Languages;


namespace RatinhoMouse
{
    public partial class FormOcr : Form
    {
        public FormOcr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var Ocr = new AdvancedOcr()
            {
                Language = IronOcr.Languages.Portuguese.OcrLanguagePack,
                ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                EnhanceResolution = true,
                EnhanceContrast = true,
                CleanBackgroundNoise = true,
                ColorDepth = 4,
                RotateAndStraighten = false,
                DetectWhiteTextOnDarkBackgrounds = false,
                ReadBarCodes = false,
                Strategy = AdvancedOcr.OcrStrategy.Fast,
                InputImageType = AdvancedOcr.InputTypes.Document
            };

            OcrResult Result = Ocr.Read(@"C:\Temp\alexandre.png");

            textBox1.Text = Result.Text;
        }


      





    }



}
