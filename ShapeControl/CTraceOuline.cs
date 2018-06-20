using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace TraceOutline
{
    class CTraceOuline
    {


        public int color_threshold = 200;
        public bool use_red = true;
        public bool use_green = true;
        public bool use_blue = true;


        //for argb color pixel format
        private int CoordsToIndex(int x, int y, int stride)
        {
            return (stride * y) + (x * 4);
        }

        public int GetGrayScaleColor(Color c)
        {

            int numcolorplane = 3;

            numcolorplane = (!use_blue) ? numcolorplane - 1 : numcolorplane;
            numcolorplane = (!use_green) ? numcolorplane - 1 : numcolorplane;
            numcolorplane = (!use_red) ? numcolorplane - 1 : numcolorplane;

            if (numcolorplane == 0)
                return (((int)c.B + (int)c.G + (int)c.R) / 3);

            int accvalue = 0;
            accvalue = use_blue ? accvalue + (int)c.B : accvalue;
            accvalue = use_green ? accvalue + (int)c.G : accvalue;
            accvalue = use_red ? accvalue + (int)c.R : accvalue;


            return (int)(accvalue / numcolorplane);

        }

        private int GetMonoColor(Color c)
        {
            int i = GetGrayScaleColor(c);
            if (i < color_threshold)
                return 0;
            else
                return 1;
        }



        public Point[] StringOutline2Polygon(string outline)
        {
            string[] s = outline.Split(';');

            if (s.Length < 5)
                return null;

            Point[] p = new Point[s.Length - 1];

            string[] s1 = s[0].Split(',');
            for (int i = 0; i < s.Length - 1; i++)
            {
                s1 = s[i].Split(',');
                p[i].X = int.Parse(s1[0]);
                p[i].Y = int.Parse(s1[1]);

            }


            return p;
        }


        public string TraceOutlineN(Bitmap bm, int x0, int y0, int probe_width, Color fg, Color bg, bool bauto_threshold, int n)
        {

            string s = "";
            int x = 0, y = y0;
            int x1 = 0, y1 = 0;
            Color c1, c2;
            int start_direction = 0, current_direction = 0;

            int gc1 = 0;
            int gc2 = 0;
            bool hitborder = false;
            bool hitstart = false;
            int max_width = bm.Width, max_height = bm.Height;

            //direct bit manipulation
            Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
            BitmapData bmpData =
                     bm.LockBits(rect, ImageLockMode.ReadOnly,
                     bm.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = bm.Width * bm.Height * 4;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            bm.UnlockBits(bmpData);



            if (bauto_threshold)
            {
                //get max pix value diff
                int maxpixdiff = 0;
                int maxpixvalue = 0;

                for (int i = 0; i < (probe_width * 2); i++)
                {
                    try
                    {
                        if (i % 2 == 1)
                            x = x0 + i / 2;
                        else
                            x = x0 - i / 2;


                        if (x < 0) continue;
                        if (x >= max_width) continue;

                        int index = CoordsToIndex(x, y0, bmpData.Stride);
                        if (index < 0 || index > (bytes - 1)) break;
                        c1 = Color.FromArgb(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);

                        gc1 = GetGrayScaleColor(c1);



                        index = CoordsToIndex(x + 1, y0, bmpData.Stride);
                        if (index < 0 || index > (bytes - 1)) break;
                        c2 = Color.FromArgb(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);

                        gc2 = GetGrayScaleColor(c2);

                        if (maxpixdiff < Math.Abs(gc1 - gc2))
                            maxpixdiff = Math.Abs(gc1 - gc2);

                        if (gc1 > maxpixvalue)
                            maxpixvalue = gc1;
                        if (gc2 > maxpixvalue)
                            maxpixvalue = gc2;



                    }
                    catch (Exception)
                    {
                        break;
                    }

                }

                if (maxpixdiff > 0)
                    color_threshold = maxpixvalue - (int)(0.3 * maxpixdiff);

                if (color_threshold < 0) color_threshold = 0;

            }

            int gfg = GetMonoColor(fg);
            int gbg = GetMonoColor(bg);

            for (int i = 0; i < (probe_width * 2); i++)
            {
                try
                {
                    if (i % 2 == 1)
                        x = x0 + i / 2;
                    else
                        x = x0 - i / 2;

                    if (x < 0) continue;
                    if (x >= max_width) continue;

                    int index = CoordsToIndex(x, y0, bmpData.Stride);
                    if (index < 0 || index > (bytes - 1)) break;
                    c1 = Color.FromArgb(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);


                    gc1 = GetMonoColor(c1);

                    index = CoordsToIndex(x + 1, y0, bmpData.Stride);
                    if (index < 0 || index > (bytes - 1)) break;
                    c2 = Color.FromArgb(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);

                    gc2 = GetMonoColor(c2);

                    if ((gc1 == gfg && gc2 == gbg) ||
                        (gc1 == gbg && gc2 == gfg))
                    {
                        if (gc1 == gfg && gc2 == gbg) start_direction = 4;
                        if (gc1 == gbg && gc2 == gfg) start_direction = 0;
                        hitborder = true;
                        x1 = x; y1 = y;
                        break;
                    }
                }
                catch (Exception)
                {

                    break;
                }

            }
            if (!hitborder)
            {
                return "";
            }

            Color[] cn = new Color[8 * n];
            int count = 0; int countlimit = 10000;
            x = x1; y = y1;


            int diffx = 0, diffy = 0;
            int index1 = 0;
            while (!hitstart)
            {
                count++;

                //fallback to prevent infinite loop
                if (count > countlimit)
                {

                    return "";
                }


                //getting all the neighbours' pixel color
                try
                {
                    //processing top neighbours left to right
                    for (int i = 0; i <= 2 * n; i++)
                    {
                        diffx = i - n;
                        index1 = CoordsToIndex(x + diffx, y - n, bmpData.Stride);
                        cn[i] = ((x + diffx) >= 0 && (x + diffx) < max_width && (y - n) >= 0 && (y - n) < max_height) ?
                            Color.FromArgb(rgbValues[index1 + 2], rgbValues[index1 + 1], rgbValues[index1])
                            : Color.Empty;

                    }

                    //processing right neighbours top to bottom
                    for (int i = 2 * n + 1; i < 4 * n; i++)
                    {
                        diffy = i - 3 * n;
                        index1 = CoordsToIndex(x + n, y + diffy, bmpData.Stride);

                        cn[i] = ((x + n) >= 0 && (x + n) < max_width && (y + diffy) >= 0 && (y + diffy) < max_height) ?
                         Color.FromArgb(rgbValues[index1 + 2], rgbValues[index1 + 1], rgbValues[index1])
                             : Color.Empty;

                    }

                    //processing bottom neighbours right to left					
                    for (int i = 4 * n; i <= 6 * n; i++)
                    {
                        diffx = i - 5 * n;
                        index1 = CoordsToIndex(x - diffx, y + n, bmpData.Stride);

                        cn[i] = ((x - diffx) >= 0 && (x - diffx) < max_width && (y + n) >= 0 && (y + n) < max_height) ?
                          Color.FromArgb(rgbValues[index1 + 2], rgbValues[index1 + 1], rgbValues[index1])
                            : Color.Empty;

                    }

                    //processing left neighbours bottom to top	
                    for (int i = 6 * n + 1; i < 8 * n; i++)
                    {
                        diffy = i - 7 * n;
                        index1 = CoordsToIndex(x - n, y - diffy, bmpData.Stride);
                        cn[i] = ((x - n) >= 0 && (x - n) < max_width && (y - diffy) >= 0 && (y - diffy) < max_height) ?
                         Color.FromArgb(rgbValues[index1 + 2], rgbValues[index1 + 1], rgbValues[index1])
                             : Color.Empty;

                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());

                    return "";

                }

                int index = 0;
                string tests = "";
                bool dir_found = false;

                //find the first valid foreground pixel				
                for (int i = start_direction; i < start_direction + (8 * n); i++)
                {
                    index = i % (8 * n);


                    if (!cn[index].Equals(Color.Empty))
                        if (GetMonoColor(cn[index]) == gfg)
                        {
                            current_direction = index;
                            dir_found = true;
                            break;
                        }

                }


                //if no foreground pixel found, just find the next valid pixel 

                if (!dir_found)
                    for (int i = start_direction; i < start_direction + (8 * n); i++)
                    {
                        index = i % (8 * n);


                        if (!cn[index].Equals(Color.Empty))
                        {
                            current_direction = index;
                            dir_found = true;
                            break;

                        }

                    }


                // find the next direction to look for foreground pixels
                if ((index >= 0) && (index <= 2 * n))
                {
                    diffx = index - n;
                    x += diffx;
                    y -= n;

                }
                if ((index > 2 * n) && (index < 4 * n))
                {
                    diffy = index - 3 * n;
                    x += n;
                    y += diffy;
                }

                if ((index >= 4 * n) && (index <= 6 * n))
                {
                    diffx = index - 5 * n;
                    x -= diffx;
                    y += n;
                }

                if ((index > 6 * n) && (index < 8 * n))
                {
                    diffy = index - 7 * n;
                    x -= n;
                    y -= diffy;
                }



                //store the found outline
                tests = x + "," + y + ";";

                s = s + tests;


                start_direction = (current_direction + (4 * n + 1)) % (8 * n);



                //adaptive stop condition

                bool bMinCountOK = (n > 1) ? (count > (max_height / 5)) : (count > 10);

                if (bMinCountOK && (Math.Abs(x - x1) < (n + 1) && (Math.Abs(y - y1) < (n + 1))))
                    hitstart = true;


            }

            return s;

        }



    }
}
