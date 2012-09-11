using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Photo.Repository
{
    public class PhotoRepository
    {
        DataBaseEntities context = new DataBaseEntities();

        public void InsertPhoto(Models.Photo photo)
        {
            context.Photos.Add(photo);
            context.SaveChanges();
        }

        public Models.Photo GetPhoto(int PhotoID)
        {
            return context.Photos.Where(p => p.PhotoID.Equals(PhotoID)).SingleOrDefault();
        }

        public List<Models.Photo> GetUserPhotos(int userId)
        {
            return context.Photos.Where(p => p.UserID.Equals(userId)).ToList();
        }
    }
}
