using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

namespace empreview
{
    public partial class viewreport : System.Web.UI.Page
    {
        empreviewtrackEntities1 en = new empreviewtrackEntities1();
        emptable tb1 = new emptable();
        reviewtable tb2 = new reviewtable();
        DateTime today = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null || Session["role"] == null || Session["empid"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                string name = Session["name"].ToString();
                int role = Convert.ToInt32(Session["role"]);
                nameLabel.Text = name;
                var row = (from x in en.roletables where x.roleid == role select x).FirstOrDefault();
                roleLabel.Text = row.rolename;
                string empid = Session["empid"].ToString();
                if (role == 1)
                {
                    DropDownList1.Visible = true;
                    DropDownList1.DataSource = from x in en.emptables where x.roleid != 1 select new { x.employeeid };
                    DropDownList1.DataTextField = "employeeid";
                    DropDownList1.DataBind();
                    Labelselectemp.Text = "Select Employee ID";
                }
                else if (role == 2)
                {
                    register.Visible = false;
                    DropDownList1.Visible = true;
                    DropDownList1.DataSource = (from x in en.emptables where x.roleid != 1 && x.teamleader == empid select new { x.employeeid });
                    DropDownList1.DataTextField = "employeeid";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Add(empid);
                    Labelselectemp.Text = "Select Employee ID";
                }
                else if (role == 3)
                {
                    register.Visible = false;
                    review.Visible = false;
                    DropDownList1.Items.Add(empid);
                    DropDownList1.SelectedIndex = 0;
                }
                showReview();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showReview();
        }

        public void showReview()
        {
            Labelerror.Text = "";
            var getempid = DropDownList1.SelectedItem.ToString();
            var name = "";
            int role = 0;
            var rolename = "";
            var info = (from x in en.emptables where x.employeeid == getempid select x).FirstOrDefault();
            name = info.name;
            role = Convert.ToInt32(info.roleid);
            var getrole = (from x in en.roletables where x.roleid == role select x).FirstOrDefault();
            rolename = getrole.rolename;
            Labelempid.Text = getempid;
            Labelname.Text = name;
            Labelrole.Text = rolename;
            var getreview = (from y in en.reviewtables where y.employeeid == getempid && y.date.Month == today.Month && y.date.Year == today.Year select y).FirstOrDefault();
            if (getreview != null)
            {
                Labeldate.Text = getreview.date.ToShortDateString();
                var getgivenby = getreview.givenby;
                var givenbyname = (from c in en.emptables where c.employeeid == getgivenby select c).FirstOrDefault();
                Labelgivenby.Text = givenbyname.name + " (" + getgivenby + ")";
                Labelerror.ForeColor = Color.Green;
                Labelerror.Text = "Review found this month for " + getempid;
                set_review();

            }
            else
            {
                Labelerror.ForeColor = Color.Red;
                Labelerror.Text = "No Review found for " + getempid;
                clear();
                Labelempid.Text = getempid;
                Labelname.Text = name;
                Labelrole.Text = rolename;
            }
        }

        public void set_review()
        {
            var item = DropDownList1.SelectedItem.ToString();
            var setdata = (from x in en.reviewtables where x.employeeid == item && x.date.Month == today.Month && x.date.Year == today.Year select x).ToList();
            string[] qarr = (from x in en.questiontables select x.question).ToArray();
            int j = 1;
            foreach (var data in setdata)
            {
                try
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    cell1.Text = j.ToString();
                    cell2.Text = qarr[j - 1];
                    cell3.Text = data.mark.ToString();
                    cell4.Text = data.comment;
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    Table1.Rows.Add(row);
                    ta.InnerText = data.overallcomment;
                    j++;
                }
                catch (Exception)
                {

                }
            }
        }

        public void clear()
        {
            Labelempid.Text = "";
            Labelname.Text = "";
            Labelrole.Text = "";
            Labelgivenby.Text = "";
            Labeldate.Text = "";
            ta.InnerText = "";
        }

        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}