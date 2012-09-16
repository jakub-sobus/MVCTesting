using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Models;
using System.ComponentModel;

namespace Models
{
    [MetadataType(typeof(PhotoMetadata))]
    public partial class Photo
    {

    }

    public class PhotoMetadata
    {
        [Required]
        [DisplayName("Opis zdjęcia")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Nazwa zdjęcia")]
        public string PhotoName { get; set; }
    }
}
