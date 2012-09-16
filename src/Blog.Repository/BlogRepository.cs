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
        {   List<Post> ret = context.Posts.Where(p => p.UserId.Equals(userId)).ToList();
            ret.Reverse();
            return ret.Take(10).ToList();
        }

        public void InsertPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
