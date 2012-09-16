using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Partial;
using Blog.Repository;
using Users.Repository;

namespace construction_journal.Controllers
{
    public class BlogController : Controller
    {
        private BlogRepository repository = new BlogRepository();
        private UsersRepository userRepository = new UsersRepository();

        //
        // GET: /Blog/

        [Authorize]
        public ActionResult Index()
        {
            User user = userRepository.GetUserInfo(User.Identity.Name);
            List<Post> viewData = new List<Post>();
            viewData = repository.GetUserPosts(user.Id);
            return View(viewData);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPost()
        {
            Post newPost = new Post();
            return View(newPost);
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreationDate = DateTime.Now;
                post.UserId = userRepository.GetUserInfo(User.Identity.Name).Id;
                repository.InsertPost(post);
                return RedirectToAction("Index", "Blog");
            }
            return View();
        }

        

    }
}
