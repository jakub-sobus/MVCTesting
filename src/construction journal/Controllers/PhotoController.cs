using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Repository;
using Photo.Repository;
using Blog.Repository;
using Models;
using Models.Partial;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace construction_journal.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoRepository repository = new PhotoRepository();
        private UsersRepository userRepository = new UsersRepository();
        private BlogRepository blogRepository = new BlogRepository();

        //
        // GET: /Photo/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPhoto()
        {
            Models.Photo newPhoto = new Models.Photo();

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddPhoto(Guid? file_guid, Models.Photo photo, HttpPostedFileBase file)
        {
            if (file_guid == null && file == null)
            {
                ModelState.AddModelError("Brak pliku", "Proszę wybrać plik do wysłania");
            }

            if (ModelState.IsValid)
            {
                photo.AddDate = DateTime.Now;
                photo.UserID = userRepository.GetUserInfo(User.Identity.Name).Id;
                repository.InsertPhoto(photo);

                //upload file when no swf
                if (file != null && file.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads"),
                        photo.PhotoID.ToString() + ".jpg");

                    file.SaveAs(path);
                }

                // rename asyn uploaded file
                else
                {
                    System.IO.File.Move(Path.Combine(Server.MapPath("~/uploads"),
                        file_guid + ".jpgaaa"), Path.Combine(Server.MapPath("~/uploads"),
                        photo.PhotoID.ToString() + ".jpg"));

                    //for (int i = 0; i < 10000; i++) ;

                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/uploads"), file_guid + ".jpg"));
                }

                AddPhotoPost(photo);

                return RedirectToAction("ShowPhotos");
            }

            return View();
        }

        [Authorize]
        public ActionResult ShowPhotos()
        {
            GalleryViewData photos = new GalleryViewData();
            photos.Photos = repository.GetUserPhotos(userRepository.GetUserInfo(User.Identity.Name).Id);
            photos.Photos.Reverse();

            return View(photos);
        }

        [Authorize]
        public void AddPhotoPost(Models.Photo photo)
        {
            Post newPost = new Post();
            newPost.CreationDate = DateTime.Now;
            newPost.UserId = userRepository.GetUserInfo(User.Identity.Name).Id;
            newPost.PhotoId = photo.PhotoID;
            newPost.Text = photo.Description;
            blogRepository.InsertPost(newPost);
        }


        public string AsyncUpload()
        {
            string fileName = Guid.NewGuid().ToString();

            if (!Directory.Exists(Server.MapPath("~/uploads")))
            {
                Directory.CreateDirectory(Server.MapPath("~/uploads"));
            } 

            var path = Path.Combine(Server.MapPath("~/uploads"),
                        fileName + ".jpg");

            Request.Files[0].SaveAs(path);

            Image img = Image.FromFile(path);
            Image resizedImg = resizeImage(img, new Size(2000,1500));

            if (img.Width >= 2000 || img.Height >= 1500)
            {
                saveJpeg(path, (Bitmap)resizedImg, 45L);
            }
            else
                saveJpeg(path, (Bitmap)resizedImg, 85L);

            img.Dispose();
            resizedImg.Dispose();

            return fileName;
        }

        private Image resizeImage(Image imgToResize, Size size)
        {
            if (imgToResize.Height < 1500 || imgToResize.Width < 2000)
                return imgToResize;

            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter(Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path+"aaa", jpegCodec, encoderParams);

            return;
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
