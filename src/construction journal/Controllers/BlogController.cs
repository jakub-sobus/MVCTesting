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

        public ActionResult Index()
        {
            User user = userRepository.GetUserInfo(User.Identity.Name);
            BlogViewData viewData = new BlogViewData();
            viewData.Posts = repository.GetUserPosts(user.Id);
            viewData.Posts.Reverse();
            return View(viewData);
        }

        [HttpGet]
        public ActionResult AddPost()
        {
            Post newPost = new Post();
            return View(newPost);
        }

        [HttpPost]
        public ActionResult AddPost(Post post)
        {
            post.CreationDate = DateTime.Now;
            post.UserId = userRepository.GetUserInfo(User.Identity.Name).Id;
            repository.InsertPost(post);
            return RedirectToAction("Index", "Blog");
        }

    }
}
