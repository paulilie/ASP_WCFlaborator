using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace PostComment.APIStatic
{
    public static class API
    {
        public static bool AddPost(Post post)
        {
            using (Model1Container ctx = new Model1Container())
            {
                bool bResult = false;
                if (post.PostId == 0)
                {
                    var it = ctx.Entry<Post>(post).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }
        public static Post UpdatePost(Post newPost)
        {
            using (Model1Container ctx = new Model1Container())
            {
                
                Post oldPost = ctx.Posts.Find(newPost.PostId);
                if (oldPost == null) 
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
        public static int DeletePost(int id)
        {
            using (Model1Container ctx = new Model1Container())
            {
                return ctx.Database.ExecuteSqlCommand($"Delete From Post where postid ={id}");
            }
        }
    
        public static Post GetPostById(int id)
        {
            using (Model1Container ctx = new Model1Container())
            {
                var items = from p in ctx.Posts where (p.PostId == id) select p;
                if (items != null)
                    return items.Include(c => c.Comments).SingleOrDefault();
                return null;
            }
        }
       
        public static List<Post> GetAllPosts()
        {
            using (Model1Container ctx = new Model1Container())
            {
                return ctx.Posts.Include("Comments").ToList<Post>();
            }
        }
        // Comment table
        public static bool AddComment(Comment comment)
        {
            using (Model1Container ctx = new Model1Container())
            {
                bool bResult = false;
                if (comment == null || comment.PostPostId == 0)
                    return bResult;
                if (comment.CommentId == 0)
                {
                    ctx.Entry<Comment>(comment).State = EntityState.Added;
                    Post p = ctx.Posts.Find(comment.PostPostId);
                    ctx.Entry<Post>(p).State = EntityState.Unchanged;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }
        public static Comment UpdateComment(Comment newComment)
        {
            using (Model1Container ctx = new Model1Container())
            {
                Comment oldComment = ctx.Comments.Find(newComment.CommentId);
                if (newComment.Text != null)
                    oldComment.Text = newComment.Text;
                if ((oldComment.PostPostId != newComment.PostPostId)
               && (newComment.PostPostId != 0))
                {
                    oldComment.PostPostId = newComment.PostPostId;
                }
                ctx.SaveChanges();
                return oldComment;
            }
        }
        public static Comment GetCommentById(int id)
        {
            using (Model1Container ctx = new Model1Container())
            {
                var items = from c in ctx.Comments where (c.CommentId == id) select c;
                return items.Include(p => p.Post).SingleOrDefault();
            }
        }
    }
}