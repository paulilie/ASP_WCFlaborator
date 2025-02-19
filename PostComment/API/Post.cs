﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
namespace PostComment
{
    public partial class Post
    {
        public bool AddPost()
        {
            using (Model1Container ctx = new Model1Container())
            {
                bool bResult = false;
                if (this.PostId == 0)
                {
                    var it = ctx.Entry<Post>(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }
        public Post UpdatePost(Post newPost)
        {
            using (Model1Container ctx = new Model1Container())
            {
                Post oldPost = ctx.Posts.Find(newPost.PostId);
                if (oldPost == null) // nu exista in bd
                {
                    return null;
                }
                oldPost.Description = newPost.Description;
                oldPost.Domian = newPost.Domian;
                oldPost.Date = newPost.Date;
                ctx.SaveChanges();
                return oldPost;
            }
        }
        public int DeletePost(int id)
        {
            using (Model1Container ctx = new Model1Container())
            {
                return ctx.Database.ExecuteSqlCommand($"Delete From Post where postid ={id} ");
            }
        }
        public Post GetPostById(int id)
        {
            using (Model1Container ctx = new Model1Container())
            {
                var items = from p in ctx.Posts where (p.PostId == id) select p;
                if (items != null)
                    return items.Include(c => c.Comments).SingleOrDefault();
                return null; // trebuie verificat in apelant
            }
        }
        public List<Post> GetAllPosts()
        {
            using (Model1Container ctx = new Model1Container())
            {
                return ctx.Posts.Include("Comments").ToList<Post>();
                // Obligatoriu de verificat in apelant rezultatul primit.
            }
        }
    }
}