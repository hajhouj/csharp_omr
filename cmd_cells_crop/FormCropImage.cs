using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
namespace My_EMGU_Program
{
    public partial class FormCropImage : Form
    {
        Image<Bgr, byte> imgInput;
        Rectangle rect;
        Point StartLocation;
        Point EndLocation;

        bool IsMouseDown = false;
        public FormCropImage()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                imgInput = new Image<Bgr, byte>(ofd.FileName);
                pictureBox1.Image = imgInput.Bitmap;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            StartLocation = e.Location;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true) {

                EndLocation = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(rect !=null){
                e.Graphics.DrawRectangle(Pens.Red, GetRectangle());
            }
        }

        private Rectangle GetRectangle() {
            rect = new Rectangle();
        rect.X = Math.Min(StartLocation.X,EndLocation.X );
        rect.Y = Math.Min(StartLocation.Y , EndLocation.Y);
        rect.Width = Math.Abs(StartLocation.X-EndLocation.X);
        rect.Height = Math.Abs(StartLocation.Y - EndLocation.Y);
            
        return rect;
           }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (rect != null) {
                IsMouseDown = false;
                imgInput.ROI = rect;
                Image<Bgr, byte> temp = imgInput.CopyBlank();
                imgInput.CopyTo(temp);
                imgInput.ROI = Rectangle.Empty;
                pictureBox2.Image = temp.Bitmap;
            }
        }


    }
}
