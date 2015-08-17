namespace Blog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class BlogPost
    {
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();

        }

        public int Id { get; set; }
        public System.DateTimeOffset Created { get; set; }
        public Nullable<System.DateTimeOffset> Updated { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        [Required]
        public string Blog { get; set; }
        public string MediaURL { get; set; }
        //public bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }


    }
}

