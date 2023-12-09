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
            //clear pictures box
            pictureBoxCleaner();
            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            Bitmap resultX = new Bitmap(pictureBox1.Image);
            Bitmap resultY = new Bitmap(pictureBox1.Image);
            Bitmap resultXY = new Bitmap(pictureBox1.Image);
            Color currentPix,nextPixelX,nextPixelY,sumX,sumY,sumXY;
            int resX,resY,res;
            for (int x = 0; x <= originalImage.Width-2; x++)
            {
                for (int y = 0; y <= originalImage.Height-2; y++)
                {
                    currentPix = originalImage.GetPixel(x, y);
                    nextPixelX = originalImage.GetPixel(x+1, y);
                    nextPixelY = originalImage.GetPixel(x , y+1);
                    resX = Math.Abs(currentPix.R - nextPixelX.R);
                    resY = Math.Abs(currentPix.R - nextPixelY.R);
                    //check radio button and check res between 0 to 255
                    res = checkhandler(resX, resY);

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

        private void button2_Click(object sender, EventArgs e)
        {
            //clear pictures box
            pictureBoxCleaner();
            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            Bitmap resultX = new Bitmap(pictureBox1.Image);
            Bitmap resultY = new Bitmap(pictureBox1.Image);
            Bitmap resultXY = new Bitmap(pictureBox1.Image);
            Color currentPix, nextPixelZ, nextPixelX, nextPixelY, sumX, sumY, sumXY;
            int resX, resY, res;
            for (int x = 0; x <= originalImage.Width - 2; x++)
            {
                for (int y = 0; y <= originalImage.Height - 2; y++)
                {
                    //edge detect X
                    currentPix = originalImage.GetPixel(x, y);
                    nextPixelZ = originalImage.GetPixel(x+1,y+1);
                    resX = Math.Abs(currentPix.R - nextPixelZ.R);
                    //edge detect Y
                    nextPixelX = originalImage.GetPixel(x+1,y);
                    nextPixelY = originalImage.GetPixel(x,y+1);
                    resY = Math.Abs(nextPixelX.R - nextPixelY.R);
                    //check radio button and check res between 0 to 255
                    res = checkhandler(resX,resY);

                    sumX = Color.FromArgb(resX, resX, resX);
                    sumY = Color.FromArgb(resY, resY, resY);
                    sumXY = Color.FromArgb(res, res, res);
                    resultX.SetPixel(x, y, sumX);
                    resultY.SetPixel(x, y, sumY);
                    resultXY.SetPixel(x, y, sumXY);
                }
            }
            pictureBox2.Image = resultX;
            pictureBox3.Image = resultY;
            pictureBox4.Image = resultXY;
        }

        private void pictureBoxCleaner()
        {
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
        }

        private int checkhandler(int resX, int resY)
        {
            int res;
            //radiobutoon check
            if (radioButton1.Checked)
            {
                res = Convert.ToInt32(Math.Sqrt(Math.Ceiling(Math.Pow(resX, 2) + Math.Pow(resY, 2))));
            }
            else
            {
                res = resX + resY;
            }
            // check res betwen 0 to 255
            res = checkTrueVal(res);
            
            return res;
        }

        private int checkTrueVal(int res)
        {
            if (res > 255)
            {
                res = 255;
            }
            return res;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clear pictures box
            pictureBoxCleaner();
            Bitmap originalImage = new Bitmap(pictureBox1.Image);
            Bitmap resultX = new Bitmap(pictureBox1.Image);
            Bitmap resultY = new Bitmap(pictureBox1.Image);
            Bitmap resultXY = new Bitmap(pictureBox1.Image);
            Color currentPix,previousPixelX1, previousPixelX2, previousPixelX3, nextPixelX1, nextPixelX2, nextPixelX3, sumX, sumY, sumXY;
            Color previousPixelY1, previousPixelY2, previousPixelY3, nextPixelY1, nextPixelY2, nextPixelY3;
            int resX, resY, res,temp;
            for (int x = 1; x <= originalImage.Width - 2; x++)
            {
                for (int y = 1; y <= originalImage.Height - 2; y++)
                {
                    //edge detect X
                    currentPix = originalImage.GetPixel(x, y);

                    previousPixelX1 = originalImage.GetPixel(x - 1, y - 1);
                    previousPixelX2 = originalImage.GetPixel(x - 1, y );
                    previousPixelX3 = originalImage.GetPixel(x - 1, y + 1);
                    nextPixelX1 = originalImage.GetPixel(x + 1,y - 1);
                    nextPixelX2 = originalImage.GetPixel(x + 1, y);
                    nextPixelX3 = originalImage.GetPixel(x + 1, y + 1);
                    temp = -previousPixelX1.R - previousPixelX2.R - previousPixelX3.R + nextPixelX1.R + nextPixelX2.R + nextPixelX3.R;
                    resX = Math.Abs(temp);
                    resX = checkTrueVal(resX);
                    //edge detect Y
                    previousPixelY1 = originalImage.GetPixel(x - 1, y - 1);
                    previousPixelY2 = originalImage.GetPixel(x , y - 1);
                    previousPixelY3 = originalImage.GetPixel(x + 1, y - 1);
                    nextPixelY1 = originalImage.GetPixel(x - 1, y + 1);
                    nextPixelY2 = originalImage.GetPixel(x , y + 1);
                    nextPixelY3 = originalImage.GetPixel(x + 1, y + 1);
                    temp = -previousPixelY1.R - previousPixelY2.R - previousPixelY3.R + nextPixelY1.R + nextPixelY2.R + nextPixelY3.R;
                    resY = Math.Abs(temp);
                    resY = checkTrueVal(resY);
                    //check radio button and check res between 0 to 255
                    res = checkhandler(resX, resY);

                    sumX = Color.FromArgb(resX, resX, resX);
                    sumY = Color.FromArgb(resY, resY, resY);
                    sumXY = Color.FromArgb(res, res, res);
                    resultX.SetPixel(x, y, sumX);
                    resultY.SetPixel(x, y, sumY);
                    resultXY.SetPixel(x, y, sumXY);
                }
            }
            pictureBox2.Image = resultX;
            pictureBox3.Image = resultY;
            pictureBox4.Image = resultXY;
        }
    
    }
}
