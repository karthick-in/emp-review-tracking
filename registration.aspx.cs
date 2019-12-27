using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Drawing;

namespace empreview
{
    public partial class registration : System.Web.UI.Page
    {
        empreviewtrackEntities1 en = new empreviewtrackEntities1();
        emptable tb = new emptable();
        string admin_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null || Session["role"] == null || Session["empid"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Labelmsg.Text = "";
                string name = Session["name"].ToString();
                Labelname.Text = name;
                int role = Convert.ToInt32(Session["role"]);
                var row = (from x in en.roletables where x.roleid == role select x).FirstOrDefault();
                Labelrole.Text = row.rolename;
                admin_id = Session["empid"].ToString();
                if (role == 2)
                {
                    register.Visible = false;
                }
                else if (role == 3)
                {
                    register.Visible = false;
                    review.Visible = false;
                }

                Btnupdate.Visible = false;
                Btndelete.Visible = false;

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Labelmsg.Text = "";
            showListbox();
        }

        public void showListbox()
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            if (Textempid.Text != null)
            {
                var checkempid = Textempid.Text;
                var checkemp_in_table = (from x in en.emptables where x.employeeid == checkempid select x).FirstOrDefault();
                if (checkemp_in_table != null)
                {
                    Btndelete.Visible = true;
                    Btnupdate.Visible = true;
                }
                else
                {
                    Btndelete.Visible = false;
                    Btnupdate.Visible = false;
                    Btnsubmit.Visible = true;
                }
            }
            if (DropDownList1.SelectedIndex == 1)
            {
                Panel1.Visible = true;
                ListBox1.DataSource = from p in en.emptables where p.roleid == 3 && p.teamleader == "" select new { p.employeeid };
                ListBox1.DataTextField = "employeeid";
                ListBox1.DataBind();
            }
            else
            {
                Panel1.Visible = false;
            }
        }

        protected void Btnadd_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem != null)
            {
                Labelmsg.Text = "";
                ListBox2.Items.Add(ListBox1.SelectedItem);
                int i = 0;
                i = ListBox1.SelectedIndex;
                ListBox1.Items.RemoveAt(i);
            }
            else
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "No employee selected!";
            }
            Btnupdate.Visible = true;
            Btndelete.Visible = true;
        }

        protected void Btnremove_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedItem != null)
            {
                Labelmsg.Text = "";
                ListBox1.Items.Add(ListBox2.SelectedItem);
                int i = 0;
                i = ListBox2.SelectedIndex;
                ListBox2.Items.RemoveAt(i);
            }
            else
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "No employee selected!";
            }
            Btnupdate.Visible = true;
            Btndelete.Visible = true;
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            Labelmsg.Text = "";
            int role;
            if (DropDownList1.SelectedIndex == 0)
            {
                role = 1;
                tb.teamleader = "";
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                role = 2;
                tb.teamleader = "";
            }
            else
            {
                role = 3;
                tb.teamleader = "";
            }
            tb.roleid = role;
            var name = Textname.Text;
            tb.name = name;
            var empid = Textempid.Text;
            tb.employeeid = empid;
            var password = Textpassword.Text;
            password = Encrypt(password);
            tb.password = password;
            tb.email = Textemail.Text;
            tb.designation = DropDownList2.Text;
            DateTime dateofjoin = Convert.ToDateTime(Textdatepick.Text);
            tb.date_of_joining = dateofjoin;
            checkListbox(empid);
            try
            {
                admin_id = Session["empid"].ToString();
                tb.inserted_by = admin_id;
                en.emptables.AddObject(tb);
                en.SaveChanges();
                Labelmsg.ForeColor = Color.Green;
                Labelmsg.Text = "Submitted succesfully.";
                clear();
            }
            catch (Exception)
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "Employee id/Email you entered is already exists!";
            }
        }

        protected void Btnupdate_Click(object sender, EventArgs e)
        {
            Labelmsg.Text = "";
            var empid = Textempid.Text;
            var password = Textpassword.Text;
            int role;
            var row = (from x in en.emptables where x.employeeid == empid select x).FirstOrDefault();
            if (DropDownList1.SelectedIndex == 0)
            {
                role = 1;
                check_tl_column();
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                role = 2;
            }
            else
            {
                role = 3;
                check_tl_column();
            }
            DateTime dateofjoin = Convert.ToDateTime(Textdatepick.Text);
            row.date_of_joining = dateofjoin;
            checkListbox(empid);
            try
            {
                row.designation = DropDownList2.Text;
                var name = Textname.Text;
                row.name = name;
                password = Encrypt(password);
                row.password = password;
                row.email = Textemail.Text;
                row.roleid = role;
                en.SaveChanges();
                Labelmsg.ForeColor = Color.Green;
                Labelmsg.Text = "Data updated successfully for " + empid;
                clear();
            }
            catch (Exception)
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "Updation failed!";
            }
        }

        //function for emptying teamleader column if a TL gets demoted to employee
        public void check_tl_column()
        {
            var check_empid = Textempid.Text;
            var getresult = (from x in en.emptables where x.teamleader == check_empid select x).ToList();
            if (getresult != null)
            {
                foreach (var delete_col in getresult)
                {
                    delete_col.teamleader = "";
                }
                en.SaveChanges();
            }
        }

        protected void Btnreset_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            Textname.Text = "";
            Textemail.Text = "";
            Textempid.Text = "";
            Textpassword.Text = "";
            Textdatepick.Text = "";
            DropDownList1.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            showListbox();
            Btnsubmit.Visible = true;
            Btnupdate.Visible = false;
            Btndelete.Visible = false;
        }

        //Method to encrypt.
        private string Encrypt(string clearText)
        {
            string cipher;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    cipher = Convert.ToBase64String(ms.ToArray());
                }
            }
            return cipher;
        }

        //Method to decrypt.
        private string Decrypt(string cipherText)
        {
            string cleartext;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cleartext = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cleartext;
        }

        public void checkListbox(string empdemo)
        {
            try
            {
                if (ListBox2.Items != null)
                {
                    var emp = "";
                    foreach (var item in ListBox2.Items)
                    {
                        emp = item.ToString();
                        var checkrecord = (from i in en.emptables where i.employeeid == emp select i).FirstOrDefault();
                        //empdemo is the teamleader id
                        checkrecord.teamleader = empdemo;
                    }
                }
                if (ListBox1.Items != null)
                {
                    var emp = "";
                    foreach (var item in ListBox1.Items)
                    {
                        emp = item.ToString();
                        var checkrecord = (from i in en.emptables where i.employeeid == emp select i).FirstOrDefault();
                        //empdemo is the teamleader id
                        checkrecord.teamleader = "";
                    }
                }
            }
            catch (Exception)
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "some error occured!";
            }
        }

        protected void Btndelete_Click(object sender, EventArgs e)
        {
            if (Textempid.Text != "")
            {
                var deleteid = Textempid.Text;
                var row = (from x in en.emptables where x.employeeid == deleteid select x).FirstOrDefault();
                if (row != null)
                {
                    //if teamleader gets deleted, all the employee under him will have no value on their teamleader column...
                    if (row.roleid == 2)
                    {
                        var deletetl = (from o in en.emptables where o.teamleader == deleteid select o).ToList();
                        foreach (var del in deletetl)
                        {
                            del.teamleader = "";
                        }
                    }
                    en.emptables.DeleteObject(row);
                    en.SaveChanges();
                    clear();
                    Labelmsg.ForeColor = Color.Green;
                    Labelmsg.Text = "Employee " + deleteid + " deleted successfully";
                }
                else
                {
                    clear();
                    Labelmsg.ForeColor = Color.Red;
                    Labelmsg.Text = "This Employee ID is not found!";
                }
            }
            else
            {
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "Please fill employee ID textbox to delete";
            }
        }

        protected void Textempid_TextChanged(object sender, EventArgs e)
        {
            Labelmsg.Text = "";
            var row = (from x in en.emptables where x.employeeid == Textempid.Text select x).FirstOrDefault();
            if (row != null)
            {
                Btnsubmit.Visible = false;
                Btnupdate.Visible = true;
                Btndelete.Visible = true;
                int role = Convert.ToInt32(row.roleid);
                if (role == 1)
                {
                    DropDownList1.SelectedIndex = 0;
                    DropDownList2.SelectedIndex = 0;
                }
                else if (role == 2)
                {
                    DropDownList1.SelectedIndex = 1;
                    DropDownList2.SelectedIndex = 1;
                }
                else
                {
                    DropDownList1.SelectedIndex = 2;
                    DropDownList2.SelectedIndex = 2;
                }
                showListbox();
                ListBox2.DataSource = from p in en.emptables where p.teamleader == Textempid.Text && p.roleid == 3 select new { p.employeeid };
                ListBox2.DataTextField = "employeeid";
                ListBox2.DataBind();
                var designation = row.designation;
                if (designation == null || designation == "")
                {
                    DropDownList2.SelectedIndex = 0;
                }
                else
                {
                    DropDownList2.Text = designation;
                }
                Textname.Text = row.name;
                Textemail.Text = row.email;
                DateTime date = row.date_of_joining;
                Textdatepick.Text = date.ToString("yyyy-MM-dd");
                var password = Decrypt(row.password);
                Textpassword.Text = password;
            }
            else
            {
                var newempid = Textempid.Text;
                Btnsubmit.Visible = true;
                clear();
                Textempid.Text = newempid;
                Btnupdate.Visible = false;
                Btndelete.Visible = false;
            }
        }

        protected void Textdatepick_TextChanged(object sender, EventArgs e)
        {
            DateTime getdate = Convert.ToDateTime(Textdatepick.Text);
            DateTime currentdate = DateTime.Now;
            if (currentdate < getdate)
            {
                Btnsubmit.Visible = false;
                Labelmsg.ForeColor = Color.Red;
                Labelmsg.Text = "Invalid date";
            }
            else
            {
                Labelmsg.Text = "";
                Btnsubmit.Visible = true;
            }
        }

        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

    }
}