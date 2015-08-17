namespace Blog.Models
{
    using MyFirstApp.Models;
    using System;
    using System.Collections.Generic;

    public class Comment //This is my comment class
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public System.DateTimeOffset Created { get; set; }
        public Nullable<System.DateTimeOffset> Updated { get; set; } //set to nullable because this will be set to a time in the future, which we are unaware of it
        public string UpdateReason { get; set; } //Identify a reason why the admin updated the comment

        public virtual ApplicationUser Author { get; set; } //pulling from authorization database
        public virtual BlogPost Post { get; set; } //connecting to the BlogPost database

    }

}

//public class ApplicationUser :  IdentityUser
//{
   // public string FirstName { get; set;  }
    //public string LastName { get; set;  }
    //public string DisplayName { get; set; }

    //public ApplicationUser()
    //{
       // this.BlogComments = new HashSet<Blog.Models>();
    //}
    //public virtual ICollection<Comment> BlogComments { get; set;  }
//}
