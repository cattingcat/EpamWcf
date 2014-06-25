using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MvcClient
{
    /// <summary>
    /// Сводное описание для CaptchaHandler
    /// </summary>
    public class CaptchaHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";
            
            Stream os = context.Response.OutputStream;
            Random rnd = new Random(DateTime.Now.Millisecond);

            int captcha = rnd.Next(100, 999);

            context.Session.Add("captcha", captcha);

            InstalledFontCollection fonts = new InstalledFontCollection();
            
            Font font = null;

            while (font == null)
            {
                try
                {
                    FontFamily randomFamily = fonts.Families[rnd.Next(fonts.Families.Length)];
                    font = new Font(randomFamily, 20);
                }
                catch (ArgumentException)
                {

                }
            }

            Bitmap bmp = new Bitmap(150, 75);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(100, rnd.Next(100, 180), 100));
            g.DrawString(captcha.ToString(), font, new SolidBrush(Color.Black), new PointF(0.0F, 0.0F));

            bmp.Save(os, ImageFormat.Jpeg);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}