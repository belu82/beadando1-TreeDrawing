using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeDrawing
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
        }

        private void Drawing(Graphics gr, Pen pen, int depth, int max_depth, float x, float y, float length,
            float angle, float length_scale, float dtheta)
        {
            float x1 = (float)(x + length * Math.Cos(angle));
            float y1 = (float)(y + length * Math.Sin(angle));

            if (depth == 1){ 
                pen.Color = Color.Green;
            }
            else
            {
                int g = 255 * (max_depth - depth) / max_depth;
                int r = 139 * (depth - 3) / max_depth;
                if (r < 0) r = 0;
                int b = 0;
                pen.Color = Color.FromArgb(r, g, b);
            }

            int thickness = 10 * depth / max_depth;
            if (thickness < 0) thickness = 0;
            pen.Width = thickness;

            gr.DrawLine(pen, x, y, x1, y1);

            if (depth > 1)
            {
                Drawing(gr, pen, depth - 1, max_depth, x1, y1,length * length_scale, angle + dtheta, length_scale,dtheta);
                Drawing(gr, pen, depth - 1, max_depth, x1, y1,length * length_scale, angle - dtheta, length_scale,dtheta);
                Drawing(gr, pen, depth - 1, max_depth, x1, y1, length * length_scale, angle, length_scale, dtheta);
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 4;
            int depth = 10;
            float X = pictureBox1.Width / 2;
            float Y = pictureBox1.Height ;
            float angle = (float)(Math.PI / 180.0 * (double)35);
            float length =(float) 0.75;
            int length2 = 50;

            Drawing(e.Graphics, pen,
                        depth, depth, X, Y,
                        length2, (float)(-Math.PI / 2), length,
                        angle);
        }
    }
}
