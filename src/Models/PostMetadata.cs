using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Models;
using System.ComponentModel;

namespace Models
{
    [MetadataType(typeof(PostMetadata))]
    public partial class Post
    {
        
    }

    public class PostMetadata
    {
        [Required]
        [DisplayName("Wiadomość")]
        public string Text { get; set; }
    }
}
