using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace empreview
{
    public partial class reviewpage : System.Web.UI.Page
    {
        empreviewtrackEntities1 en = new empreviewtrackEntities1();
        string empid;
        int i;
        DateTime today = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null || Session["role"] == null || Session["empid"] == null)
            {
                Response.Redirect("login.aspx");
            }
            setTable();
            if (!IsPostBack)
            {
                Btnupdate.Visible = false;
                Btnsubmit.Visible = true;
                load();
                checkemployeedropdown();
            }
        }

        public void load()
        {
            empid = Session["empid"].ToString();
            int role = Convert.ToInt32(Session["role"]);
            string name = Session["name"].ToString();
            Labelname.Text = name;
            int getrole = Convert.ToInt32(Session["role"]);
            var row = (from x in en.roletables where x.roleid == role select x).FirstOrDefault();
            Labelrole.Text = row.rolename;
            if (role == 1)
            {
                DropDownempid.DataSource = from x in en.emptables where x.roleid != 1 select new { x.employeeid };
                DropDownempid.DataTextField = "employeeid";
                DropDownempid.DataBind();
            }
            else if (role == 2)
            {
                register.Visible = false;
                DropDownempid.DataSource = from x in en.emptables where x.roleid != 1 && x.teamleader == empid select new { x.employeeid };
                DropDownempid.DataTextField = "employeeid";
                DropDownempid.DataBind();
            }
        }

        public void setTable()
        {
            int[] marks = new int[] { 1, 2, 3, 4, 5 };
            string[] questions = (from x in en.questiontables select x.question).ToArray();
            i = 1;
            foreach (string q1 in questions)
            {
                DropDownList ddr = new DropDownList();
                ddr.ID = "ddl" + i;
                foreach (var mark in marks)
                {
                    ddr.Items.Add(mark.ToString());
                }
                TextBox tb = new TextBox();
                tb.ID = "tid" + i;
                tb.TextMode = TextBoxMode.MultiLine;
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                cell1.Text = i.ToString();
                cell2.Text = q1;
                cell3.Controls.Add(ddr);
                cell4.Controls.Add(tb);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                Table1.Rows.Add(row);
                i++;
            }
        }

        public void checkemployeedropdown()
        {
            empid = Session["empid"].ToString();
            Labelmsg.Text = "";
            enable_controls();
            var item = DropDownempid.SelectedItem.ToString();
            var getreview = (from x in en.reviewtables where x.employeeid == item && x.date.Month == today.Month && x.date.Year == today.Year select x).FirstOrDefault();
            if (getreview != null)
            {
                Btnsubmit.Visible = false;
                Btnupdate.Visible = true;
                clear();
                setreview();
                if (getreview.givenby != empid)
                {
                    disable_controls();
                }
                else
                {
                    enable_controls();
                }
            }
            else
            {
                clear();
                Btnsubmit.Visible = true;
                Btnupdate.Visible = false;
            }
        }

        public void setreview()
        {
            int z = 1;
            var item = DropDownempid.SelectedItem.ToString();
            var reviews = (from x in en.reviewtables where x.employeeid == item && x.date.Month == today.Month && x.date.Year == today.Year select x).ToList();
            foreach (var review in reviews)
            {
                try
                {
                    TableRow row;
                    row = Table1.Rows[z];
                    ((DropDownList)row.FindControl("ddl" + z)).SelectedValue = review.mark.ToString();
                    ((TextBox)row.FindControl("tid" + z)).Text = review.comment;
                    ta.InnerText = review.overallcomment;
                    z++;
                }
                catch (Exception)
                {

                }
            }
        }

        protected void DropDownempid_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkemployeedropdown();
        }

        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            store_review();
            Labelmsg.ForeColor = Color.Green;
            Labelmsg.Text = "Review submitted successfully for " + DropDownempid.SelectedValue;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Review submitted successfully')", true);
            clear();
            checkemployeedropdown();
        }

        public void store_review()
        {
            try
            {
                empid = Session["empid"].ToString();
                var item = DropDownempid.SelectedItem.ToString();
                var qidarray = (from x in en.questiontables select x.qid).ToArray();
                TableRow row;
                row = Table1.Rows[1];
                for (int j = 1; j < i; j++) // i tells the number of questions in table
                {
                    reviewtable tb = new reviewtable();
                    tb.employeeid = item;
                    tb.date = today;
                    tb.comment = ((TextBox)row.FindControl("tid" + j)).Text;
                    tb.qid = qidarray[j - 1];
                    tb.givenby = empid;
                    tb.mark = Convert.ToInt32(((DropDownList)row.FindControl("ddl" + j)).SelectedItem.Text);
                    tb.overallcomment = ta.InnerText;                    
                    en.reviewtables.AddObject(tb);
                    en.SaveChanges();
                }

            }
            catch (Exception)
            {

            }
        }

        public void delete_review()
        {
            var item = DropDownempid.SelectedItem.ToString();
            var getreview = (from x in en.reviewtables where x.employeeid == item && x.date.Month == today.Month && x.date.Year == today.Year select x).FirstOrDefault();
            if (getreview != null)
            {
                var deletereview = (from x in en.reviewtables where x.employeeid == item && x.date.Month == today.Month && x.date.Year == today.Year select x).ToList();
                foreach (var element in deletereview)
                {
                    en.reviewtables.DeleteObject(element);
                    en.SaveChanges();
                }
            }
            else
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "Cannot update review";
            }
        }

        protected void Btnupdate_Click(object sender, EventArgs e)
        {
            //reviews will be deleted first and then added newly... because questions might be added newly
            delete_review();
            store_review();
            Labelmsg.ForeColor = Color.Green;
            Labelmsg.Text = "Review submitted successfully for " + DropDownempid.SelectedValue;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Review updated successfully')", true);
            clear();
            checkemployeedropdown();
        }

        protected void Btnreset_Click(object sender, EventArgs e)
        {
            DropDownempid.SelectedIndex = 0;
            checkemployeedropdown();
        }

        public void enable_controls()
        {
            ta.Disabled = false;
            int z = 1;
            foreach (TableRow myrow in Table1.Rows)
            {
                try
                {
                    ((DropDownList)myrow.FindControl("ddl" + z)).Enabled = true;
                    ((TextBox)myrow.FindControl("tid" + z)).Enabled = true;
                    z++;
                }
                catch (Exception)
                {

                }
            }
        }

        public void disable_controls()
        {
            Labelmsg.ForeColor = Color.Red;
            Labelmsg.Text = "You cannot edit this review, because its not given by you this month.";
            Btnsubmit.Visible = false;
            Btnupdate.Visible = false;
            ta.Disabled = true;
            int z = 1;
            foreach (TableRow myrow in Table1.Rows)
            {
                try
                {
                    ((DropDownList)myrow.FindControl("ddl" + z)).Enabled = false;
                    ((TextBox)myrow.FindControl("tid" + z)).Enabled = false;
                    z++;
                }
                catch (Exception)
                {

                }
            }
        }

        public void clear()
        {
            ta.InnerText = "";
            for (int z = 1; z < i; z++)
            {
                try
                {
                    TableRow row;
                    row = Table1.Rows[z];
                    ((DropDownList)row.FindControl("ddl" + z)).SelectedIndex = 0;
                    ((TextBox)row.FindControl("tid" + z)).Text = "";
                }
                catch (Exception)
                {

                }
            }
        }
    }
}