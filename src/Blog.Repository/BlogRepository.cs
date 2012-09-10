using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Blog.Repository
{
    public class BlogRepository
    {
        DataBaseEntities context = new DataBaseEntities();

        public List<Post> GetUserPosts(int userId)
        {
            return context.Posts.Where(p => p.UserId.Equals(userId)).ToList();
        }

        public void InsertPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
