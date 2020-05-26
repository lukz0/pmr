using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DummyimageController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(ImageRequest content)
        {
            Image img = await ImageUtils.GetImage(content.ImageText);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);
            var imgBytes = ms.ToArray();
            img.Dispose();
            ms.Dispose();
            return File(imgBytes, "image/png");
        }
        
        [HttpGet("{text}")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string text) {
            Image img = await ImageUtils.GetImage(text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);
            var imgBytes = ms.ToArray();
            img.Dispose();
            ms.Dispose();
            return File(imgBytes, "image/png");
        }
    }

    public class ImageRequest
    {
        public string ImageText { get; set; }
    }
    public static class ImageUtils
    {
        static Font _font = new Font(
            FontFamily.GenericMonospace,
            300,
            FontStyle.Bold,
            GraphicsUnit.Pixel);
        public static async Task<Image> GetImage(string text)
        {
            Image txtImage = await DrawText(text, _font, Color.DarkGreen, Color.Black);
            Image img = new Bitmap(418, 150);
            Graphics drawing = Graphics.FromImage(img);
            Brush fillBrush = new SolidBrush(Color.Black);
            
            drawing.FillRegion(fillBrush, new Region(new Rectangle(0, 0, 418, 150)));
            drawing.DrawImage(txtImage, new[] {new Point(10, 10), new Point(408, 10), new Point(10, 140)});
            drawing.Save();
            
            drawing.Dispose();
            txtImage.Dispose();
            fillBrush.Dispose();

            return img;
        }

        private static async Task<Image> DrawText(String text, Font font, Color textColor, Color backColor)
        {
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            
            SizeF textSize = drawing.MeasureString(text, font);
            
            img.Dispose();
            drawing.Dispose();
        
            img = new Bitmap((int) textSize.Width, (int)textSize.Height);
        
            drawing = Graphics.FromImage(img);
        
            drawing.Clear(backColor);
        
            Brush textBrush = new SolidBrush(textColor);
        
            drawing.DrawString(text, font, textBrush, 0, 0);
        
            drawing.Save();
        
            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}