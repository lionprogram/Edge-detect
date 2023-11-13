using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edge_detect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            Bitmap resultX = new Bitmap(pictureBox1.Image);
            Bitmap resultY = new Bitmap(pictureBox1.Image);
            Bitmap resultXY = new Bitmap(pictureBox1.Image);
            Color currentPix,nextPixelX,nextPixelY,sumX,sumY,sumXY;
            int resX,resY,res;
            for (int x = 0; x < originalImage.Width-1; x++)
            {
                for (int y = 0; y < originalImage.Height-1; y++)
                {
                    currentPix = originalImage.GetPixel(x, y);
                    nextPixelX = originalImage.GetPixel(x+1, y);
                    nextPixelY = originalImage.GetPixel(x , y+1);
                    resX = currentPix.R - nextPixelX.R;
                    resY = currentPix.R - nextPixelY.R;
                    if (radioButton1.Checked) {
                        res = Convert.ToInt32(Math.Sqrt(Math.Ceiling(Math.Pow(resX, 2) + Math.Pow(resY, 2))));
                    }
                    else
                    {
                        res = resX + resY;
                    }
                    // check resX betwen 0 to 255
                    resX = CheckRange(resX);
                    // check resY betwen 0 to 255
                    resY = CheckRange(resY);
                    // check res betwen 0 to 255
                    res = CheckRange(res);

                    sumX = Color.FromArgb(resX, resX, resX);
                    sumY = Color.FromArgb(resY, resY, resY);
                    sumXY = Color.FromArgb(res, res, res);
                    resultX.SetPixel(x,y,sumX);
                    resultY.SetPixel(x, y, sumY);
                    resultXY.SetPixel(x, y, sumXY);
                }
            }
            pictureBox2.Image = resultX;
            pictureBox3.Image = resultY;
            pictureBox4.Image = resultXY;
        }

        private int CheckRange(int pixel)
        {
            int resualt;
            if (pixel > 255)
            {
                resualt = 255;
            }
            else if (pixel < 0)
            {
                resualt = 0;
            }
            else {
                return pixel;
            }
            return resualt;
            
        }
    }
}
