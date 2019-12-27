using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace empreview
{
    public partial class chartview : System.Web.UI.Page
    {
        empreviewtrackEntities1 en = new empreviewtrackEntities1();
        reviewtable tb1 = new reviewtable();
        string empid;
        DateTime today = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null || Session["role"] == null || Session["empid"] == null)
            {
                Response.Redirect("login.aspx");
            }
            setQuestions();
            if (!IsPostBack)
            {
                Table1.Visible = true;
                Labelmsg.Text = "";
                empid = Session["empid"].ToString();
                string name = Session["name"].ToString();
                Labelname.Text = name;
                int role = Convert.ToInt32(Session["role"]);
                var row = (from x in en.roletables where x.roleid == role select x).FirstOrDefault();
                Labelrole.Text = row.rolename;
                var getemp = (from x in en.emptables where x.employeeid == empid select x).FirstOrDefault();
                if (getemp != null)
                {
                    if (getemp.roleid == 1)
                    {
                        DropDownListemp.DataSource = from p in en.emptables where p.roleid != 1 select new { p.employeeid };
                        DropDownListemp.DataTextField = "employeeid";
                        DropDownListemp.DataBind();
                        getchartdata();
                    }
                    else if (getemp.roleid == 2)
                    {
                        register.Visible = false;
                        DropDownListemp.DataSource = from p in en.emptables where p.roleid != 1 && p.teamleader == empid select new { p.employeeid };
                        DropDownListemp.DataTextField = "employeeid";
                        DropDownListemp.DataBind();
                        DropDownListemp.Items.Add(empid);
                        getchartdata();
                    }
                    else if (getemp.roleid == 3)
                    {
                        register.Visible = false;
                        review.Visible = false;
                        DropDownListemp.Items.Add(empid);
                        DropDownListemp.Visible = false;
                        getchartdata();
                    }
                }
            }
        }

        public void getchartdata()
        {
            Labelmsg.Text = "";
            var emp = DropDownListemp.SelectedItem.ToString();
            var getreview = (from x in en.reviewtables where x.employeeid == emp && x.date.Month == today.Month && x.date.Year == today.Year select x).FirstOrDefault();
            if (getreview != null)
            {
                Table1.Visible = true;
                Labelmsg.ForeColor = Color.Green;
                Labelmsg.Text = "Review for " + emp;
                Chart1.ChartAreas[0].AxisY.Maximum = 5;
                Series series = Chart1.Series["Series1"];
                var getmarklist = (from x in en.reviewtables where x.employeeid == emp && x.date.Month == today.Month && x.date.Year == today.Year select x.mark).ToArray();
                int i = 0;
                int j = 1;
                foreach (int mark in getmarklist)
                {
                    series.Points.AddXY(j, mark);
                    i++;
                    j++;
                }
            }
            else
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "No review found for " + emp;
                Table1.Visible = false;
            }
        }

        protected void DropDownListemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            getchartdata();
        }

        public void setQuestions()
        {
            string[] questions = (from x in en.questiontables select x.question).ToArray();
            int i = 1;
            foreach (var question in questions)
            {
                TableRow trow = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                cell1.Text = i.ToString();
                i++;
                cell2.Text = question;
                trow.Cells.Add(cell1);
                trow.Cells.Add(cell2);
                Table1.Rows.Add(trow);
            } 
        }
        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}