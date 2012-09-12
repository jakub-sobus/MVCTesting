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

namespace construction_journal.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoRepository repository = new PhotoRepository();
        private UsersRepository userRepository = new UsersRepository();
        private BlogRepository blogRepository = new BlogRepository();

        //
        // GET: /Photo/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPhoto()
        {
            Models.Photo newPhoto = new Models.Photo();

            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(Models.Photo photo, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                photo.AddDate = DateTime.Now;
                photo.UserID = userRepository.GetUserInfo(User.Identity.Name).Id;
                repository.InsertPhoto(photo);

                if (!Directory.Exists(Server.MapPath("~/uploads")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/uploads"));
                } 

                if (file != null && file.ContentLength > 0)
                {

                    var path = Path.Combine(Server.MapPath("~/uploads"),
                        photo.PhotoID.ToString() + ".jpg");
                    file.SaveAs(path);
                    
                }

                AddPhotoPost(photo);

                return RedirectToAction("ShowPhotos");
            }

            return View();
        }

        public ActionResult ShowPhotos()
        {
            GalleryViewData photos = new GalleryViewData();
            photos.Photos = repository.GetUserPhotos(userRepository.GetUserInfo(User.Identity.Name).Id);
            photos.Photos.Reverse();

            return View(photos);
        }

        public void AddPhotoPost(Models.Photo photo)
        {
            Post newPost = new Post();
            newPost.CreationDate = DateTime.Now;
            newPost.UserId = userRepository.GetUserInfo(User.Identity.Name).Id;
            newPost.PhotoId = photo.PhotoID;
            newPost.Text = photo.Description;
            blogRepository.InsertPost(newPost);
        }
    }
}
