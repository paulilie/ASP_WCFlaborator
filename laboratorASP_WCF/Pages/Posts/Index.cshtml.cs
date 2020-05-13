using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostComment;

namespace laboratorASP_WCF.Pages.Posts
{
   
 public class IndexModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        public List<Post> Posts { get; set; }

        public IndexModel()
        {
            Posts = new List<Post>();
        }
        public async Task OnGetAsync()
        {
            var posts = await pcc.GetPostsAsync();
            foreach (var item in posts)
            {
                // Trebuia folosit AutoMapper. Transform Post in PostDTO
                Post pd = new Post();
                pd.Description = item.Description;
                pd.PostId = item.PostId;
              
                pd.Date = item.Date;
                foreach (var cc in item.Comments)
                {
                    Comment cdto = new Comment();
                    cdto.PostPostId = cc.PostPostId;
                    cdto.Text = cc.Text;
                    pd.Comments.Add(cdto);
                }
                Posts.Add(pd);
            }
        }
    }
}
