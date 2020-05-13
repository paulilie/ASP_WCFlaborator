using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientPostComment;
using PostComment;

namespace ClientPostComment
{
    public partial class Form1 : Form
    {
        private DataGridView dgp;
        private DataGridView dgc;
        List<Post> posts = new List<Post>();
        public Form1()
        {
            InitializeComponent();
        }
        // Handler pentru evenimentul Load al ferestrei principale
        private void Form1_Load(object sender, EventArgs e)
        {
            posts = LoadPosts().ToList<Post>();
            dgp.DataSource = posts;
            dgp.Columns[0].Width = 0;
            if (dgp.Rows.Count > 0)
                dgc.DataSource = posts[0].Comments;
        }
        private static PostComment.Post[] LoadPosts()
        {
            PostCommentClient pc = new PostCommentClient();
            PostComment.Post[] p = pc.GetPosts();
            return p;
        }
        // Handler pentru evenimentul CellMouseClick din DatagridView numit dgp
        private void dgp_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            // Se afiseaza Comment-urile pentru Post-ul selectat
            dgc.DataSource = null;
            dgc.DataSource = posts[e.RowIndex].Comments;
        }

        private void InitializeComponent()
        {
            this.dgp = new System.Windows.Forms.DataGridView();
            this.dgc = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgp
            // 
            this.dgp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgp.Location = new System.Drawing.Point(12, 57);
            this.dgp.Name = "dgp";
            this.dgp.Size = new System.Drawing.Size(563, 167);
            this.dgp.TabIndex = 0;
            // 
            // dgc
            // 
            this.dgc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgc.Location = new System.Drawing.Point(12, 230);
            this.dgc.Name = "dgc";
            this.dgc.Size = new System.Drawing.Size(563, 165);
            this.dgc.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(587, 436);
            this.Controls.Add(this.dgc);
            this.Controls.Add(this.dgp);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgc)).EndInit();
            this.ResumeLayout(false);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}