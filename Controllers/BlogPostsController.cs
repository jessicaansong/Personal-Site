using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using MyFirstApp.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

//Controllers are the managers for the site. BlogPosts controller manages the Blog Portion of my site. 
namespace MyFirstApp.Controllers
{
     [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        ///////// GET: BlogPosts ////////////////////////////
        public ActionResult Index(int ? page, string Query)           
            //if int ? page, if there is no page number, then null. if not then 2.
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //search feature
            var p = db.Posts.AsQueryable(); //new variable is going to equal the queryable db.Posts.
            if (!String.IsNullOrEmpty(Query)) //query the string, null or empty
            {
                p = db.Posts.Where(s => s.Title.Contains(Query) || s.Blog.Contains(Query));

            }   
               return View(p.OrderByDescending(x => x.Created).ToPagedList(pageNumber, pageSize)); //This command tells it to extract the information from the database.
        }

         //===============================================================//


        //////////// Admin Page ////////////////////////

         [Authorize(Roles = ("Admin"))]
        public ActionResult Admin()
        {
            return View(db.Posts.ToList()); //This command tells it to extract the information from the database.
        }
         //===============================================================//


        ///////// GET: BlogPosts/Details/5 ////////////////

        public ActionResult Details(string slug)
        {
            if (slug == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Include("Comments").FirstOrDefault(p=>p.Slug == slug);
            ViewBag.PostList = db.Posts.ToList();//include the comments on the blog post. First or default = the first comment listed or the default, which may be null, in which case nothing is displayed
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            
            return View(blogPost);
        }
        //===============================================================//


        ////////// GET: BlogPosts/Create ////////////////////

     [Authorize (Roles =("Admin"))]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Slug,Blog,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        { 
            if (ModelState.IsValid)
                {
               string slug = StringUtilities.UrlFriendly(blogPost.Title); //slug to automatically populate
                   if (String.IsNullOrWhiteSpace(slug))
                   {
                       ModelState.AddModelError("Title", "Invalid title.");
                       return View(blogPost);
                   }
                   if (db.Posts.Any(p => p.Slug == slug))
                   {
                       ModelState.AddModelError("Title", "The title must be unique.");
                       return View(blogPost);
                   } 
                   else
                   {
                       blogPost.Created = System.DateTimeOffset.Now;
                       blogPost.Slug = slug;

                       db.Posts.Add(blogPost);
                       db.SaveChanges();
                       return RedirectToAction("Index");
                   }

                if(image !=null && image.ContentLength > 0)
                {
                    //check the file name to make sure its an image
                    var ext = System.IO.Path.GetExtension(image.FileName).ToLower();
                    if (ext != ".png" || ext != ".jpg" || ext != ".jpeg" || ext != "gif " || ext != ".bmp" || ext != ".pdf" )
                        ModelState.AddModelError("image", "Invalid Format");
                }
                if (ModelState.IsValid)
                {
                if(image != null)
                    {
                        //relative server path
                        var filePath = "/Uploads/";
                        var absPath = Server.MapPath("~" +filePath);
                        // media url for relative path
                        blogPost.MediaURL = filePath + image.FileName;
                        // save image
                        image.SaveAs(System.IO.Path.Combine(absPath, image.FileName));
                    }
                    db.Posts.Add(blogPost);
                        db.SaveChanges();
                        return RedirectToAction ("Index");
                }
            }
            return View(blogPost);
        }

    ////////////////////////////////////////////////EDIT///////////////////////////////////////////////////////////////////

        // GET: BlogPosts/Edit/5
         [Authorize(Roles = ("Admin, Moderator"))]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Updated,Title,Slug,Blog,MediaURL,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
               // Blog.Models.blogPost=System.DateTimeOffset.Now;
                blogPost.Updated = System.DateTimeOffset.Now;
                 db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         
         //POST: Comments/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()] /*only allows people who are logged in to leave a comment*/
         public ActionResult CreateComment([Bind(Include = "PostId,Body")]Comment comment)
        {
            var post = db.Posts.Find(comment.PostId);
            if(post == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                comment.Created = DateTimeOffset.Now;
                comment.AuthorId = User.Identity.GetUserId();

                db.Comments.Add(comment);
                db.SaveChanges();

            }
            return RedirectToAction("Details", new { slug = post.Slug }); //refer to the properties of the post
        }

        // GET: Edit Comment
        public ActionResult EditComment(int id)
        {
            return View(db.Comments.Find(id));
        }

         // POST: Edit Comment
         [HttpPost]
        public ActionResult EditComment([Bind(Include = "PostId,Body")]Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Created = DateTimeOffset.Now;
                comment.AuthorId = User.Identity.GetUserId();

                db.Comments.Add(comment);
                db.SaveChanges();
            }
            var post = db.Posts.Find(comment.PostId);

            return RedirectToAction("Details", new { slug = post.Slug }); //refer to the properties of the post         
        }



         // GET: Delete Comments
         public ActionResult DeleteComment(int ? id) 
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             var comment = db.Comments.Find(id);
             
             if (comment == null)
             {
                 return HttpNotFound();
             }
             return View("DeleteComment", comment);
         }

         // POST: Delete Comments
         [HttpPost, ActionName("DeleteComment")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteCommentConfirmed(int id)
         {
             var comment = db.Comments.Find(id);
             var slug = comment.Post.Slug; //WHY am i using slug and not id here?
             db.Comments.Remove(comment);
             db.SaveChanges();
             return RedirectToAction("Details", new { slug });
         }

         protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
 }