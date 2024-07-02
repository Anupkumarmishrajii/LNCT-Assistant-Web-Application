using BAL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using WebLNCTAssistant.Areas.AdminUsers.Models;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using CaptchaMvc.Interface;
using CaptchaMvc.Models;




namespace WebLNCTAssistant.Areas.AdminUsers.Controllers
{
    [Area("AdminUsers")]
    [Route("AdminUsers")]
    public class AdminUsersController : Controller
    {
        private const string CaptchaSessionKey = "CaptchaValue";

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                // Generate a captcha code
                var captchaCode = GenerateCaptcha();

                // Generate the captcha image URL
                // Generate the captcha image URL
                var captchaImageUrl = Url.Action("CaptchaImage", "AdminUsers", new { code = captchaCode });

                // Pass the captcha image URL to the view
                ViewBag.CaptchaImage = captchaImageUrl;


                HttpContext.Session.SetString(CaptchaSessionKey, captchaCode);

                AdminUsersModel objAdminUsersModel = new AdminUsersModel();
                return View(objAdminUsersModel);
            }
            catch (Exception ex) 
            {
                return RedirectToAction("FailureMessage");
            }
        }



       

        // Action to generate captcha image
        [HttpGet]
        public IActionResult CaptchaImage(string code)
        {
            // Generate captcha image based on the provided code
            byte[] captchaImageBytes = GenerateComplexCaptchaImage(code);

            // Return the captcha image bytes with appropriate content type
            return File(captchaImageBytes, "image/jpeg");
        }




        private byte[] GenerateComplexCaptchaImage(string code)
        {
            using (Bitmap bitmap = new Bitmap(300, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Draw a curvy background
                    DrawCurvyBackground(graphics, bitmap.Width, bitmap.Height);

                    // Define the font and brush for drawing characters
                    Font font = new Font("Arial", 20); // Adjust the font size here
                    Brush brush = new SolidBrush(Color.Black);

                    // Define the initial position for drawing characters
                    float startX = 10;

                    foreach (char character in code)
                    {
                        // Measure the width of the character
                        SizeF charSize = graphics.MeasureString(character.ToString(), font);

                        // Calculate a slight curve in the character's alignment
                        float yOffset = (float)Math.Sin(startX / 20) * 5; // Adjust the factor for curvature

                        // Draw the character at the slightly curved position
                        PointF location = new PointF(startX, 30 + yOffset); // Adjust the Y-coordinate for vertical alignment
                        graphics.DrawString(character.ToString(), font, brush, location);

                        // Update the position for the next character
                        startX += 50; // Adjust spacing between characters
                    }
                }


                // Convert the bitmap to byte array
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
        }






        // Method to generate captcha code
        private string GenerateCaptcha()
        {
            // Generate a random captcha code
            // You can implement your own logic to generate captcha codes
            // For simplicity, let's generate a random alphanumeric code here
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private void DrawComplexBackground(Graphics graphics, int width, int height)
        {
            // Define various colors for the background
            Color[] backgroundColors = { Color.LightBlue, Color.LightGreen, Color.LightPink, Color.LightYellow, Color.LightCyan };

            Random random = new Random();

            // Select a random background color
            Color backgroundColor = backgroundColors[random.Next(backgroundColors.Length)];

            // Fill the background with the selected color
            using (Brush brush = new SolidBrush(backgroundColor))
            {
                graphics.FillRectangle(brush, 0, 0, width, height);
            }

            // Define various colors for the lines
            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple, Color.Yellow };

            // Define various line styles
            Pen[] pens = { new Pen(Color.OrangeRed, 1), new Pen(Color.Pink, 2), new Pen(Color.RebeccaPurple) { DashStyle = DashStyle.Dot }, new Pen(Color.Red) { DashStyle = DashStyle.DashDot } };

            // Draw multiple lines with random colors and styles
            for (int i = 0; i < 500; i++)
            {
                int x1 = random.Next(width);
                int y1 = random.Next(height);
                int x2 = random.Next(width);
                int y2 = random.Next(height);

                Color randomColor = colors[random.Next(colors.Length)];
                Pen randomPen = pens[random.Next(pens.Length)];

                graphics.DrawLine(randomPen, x1, y1, x2, y2);
            }
        }




        // Method to draw a curvy background
        private void DrawCurvyBackground(Graphics graphics, int width, int height)
        {
            // Define various colors for the background
            Color[] backgroundColors = { Color.LightBlue, Color.LightGreen, Color.LightPink, Color.LightYellow, Color.LightCyan };

            Random random = new Random();

            // Select a random background color
            Color backgroundColor = backgroundColors[random.Next(backgroundColors.Length)];

            // Fill the background with the selected color
            using (Brush brush = new SolidBrush(backgroundColor))
            {
                graphics.FillRectangle(brush, 0, 0, width, height);
            }

            // Draw multiple curves in random colors
            for (int i = 0; i < 15; i++)
            {
                int x1 = random.Next(width);
                int y1 = random.Next(height);
                int x2 = random.Next(width);
                int y2 = random.Next(height);
                int x3 = random.Next(width);
                int y3 = random.Next(height);

                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                Pen pen = new Pen(randomColor, 2);

                graphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x3, y3);
            }
        }


        [HttpPost]
        [Route("CodeRefresher")]
        public async Task<IActionResult> CodeRefresher()
        {
            try
            {
                //return  RedirectToAction("Login");
                var captchaCode = GenerateCaptcha();
                HttpContext.Session.SetString(CaptchaSessionKey, captchaCode);

                // Generate the captcha image URL
                // Generate the captcha image URL
                var captchaImageUrl = Url.Action("CaptchaImage", "AdminUsers", new { code = captchaCode });

                //return Json(captchaImageUrl);
                return Json(new { imageUrl = captchaImageUrl });
            }
            catch (Exception ex)
            {
                return RedirectToAction("FailureMessage");
            }
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(AdminUsersModel objAdminUsersModel)
        {
            try
            {
                //var captchaCode = GenerateCaptcha();
                //HttpContext.Session.SetString(CaptchaSessionKey, captchaCode);
                //var captchaImageUrl = Url.Action("CaptchaImage", "AdminUsers", new { code = captchaCode });
                //ViewBag.CaptchaImage = captchaImageUrl;

                if (!ModelState.IsValid)
                {
                    objAdminUsersModel.IsShowValidation = false;
                    objAdminUsersModel.ErrorMessage = "validate the model.";
                    return View(objAdminUsersModel);
                }
                var OriginalCaptchaCode = HttpContext.Session.GetString(CaptchaSessionKey);
                HttpContext.Session.Remove(CaptchaSessionKey);


                string? InputEmail = objAdminUsersModel.Email;
                AdminUsersDTO objAdminUsersDTO1 = await BALFactory.Instance.AdminUsersBAL.GetPasswordByEmail(InputEmail);
                if (objAdminUsersDTO1 != null && objAdminUsersDTO1.Password == objAdminUsersModel.Password)
                {
                    if (objAdminUsersModel.Captcha == OriginalCaptchaCode)
                    {
                        return RedirectToAction("SuccessMessage");
                    }
                    else
                    {
                        return RedirectToAction("CaptchDismatch");
                    }
                }
                else
                {
                    return RedirectToAction("PasswordDismatch");

                }
            }
            catch(Exception ex){
                return RedirectToAction("FailureMessage");
            }
        }

        [HttpGet]
        [Route("DashBoard")]
        public async Task<IActionResult> DashBoard()
        {

            return View();
        }

        [HttpGet]
        [Route("SuccessMessage")]
        public async Task<IActionResult> SuccessMessage()
        {
            return View("_SuccessMessage");
        }

        [HttpGet]
        [Route("_PasswordDismatch")]
        public async Task<IActionResult> PasswordDismatch()
        {
            return View("_PasswordDismatch");
        }

        [HttpGet]
        [Route("CaptchDismatch")]
        public async Task<IActionResult> CaptchDismatch()
        {
            return View("_CaptchDismatch");
        }

        [HttpGet]
        [Route("FailureMessage")]
        public async Task<IActionResult> FailureMessage()
        {
            return View("_FailureMessage");
        }

      

        [HttpGet]
        [Route("GetAllAdminUsers")]
        public async Task<IActionResult> GetAllAdminUsers()
        {
            try
            {
                List<AdminUsersDTO> lstAdminUsersDTO = await BALFactory.Instance.AdminUsersBAL.GetAllAdminUsers();
                if(lstAdminUsersDTO != null)
                {
                    return View(lstAdminUsersDTO);

                }
                else
                {
                    return RedirectToAction("FailureMessage");

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("FailureMessage");

            }

        }
    }
}
