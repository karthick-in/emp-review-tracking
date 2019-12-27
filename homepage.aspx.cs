using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Drawing;

namespace empreview
{
    public partial class homepage : System.Web.UI.Page
    {
        empreviewtrackEntities1 en = new empreviewtrackEntities1();
        roletable tbrole = new roletable();
        emptable tbemp = new emptable();
        string empid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null || Session["role"] == null || Session["empid"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                string name = Session["name"].ToString();
                Labelname.Text = name;
                int role = Convert.ToInt32(Session["role"]);
                var row = (from x in en.roletables where x.roleid == role select x).FirstOrDefault();
                Labelrole.Text = row.rolename;
                empid = Session["empid"].ToString();
                if (role == 2)  //teamleader
                {
                    register.Visible = false;
                }
                else if (role == 3) //employee
                {
                    register.Visible = false;
                    review.Visible = false;
                }
            }
        }

        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void Btnnewpassword_Click(object sender, EventArgs e)
        {
            if (Textnewpassword.Text == Textconfirmpassword.Text)
            {
                string cipherPassword;
                var plainPassword = Textconfirmpassword.Text;
                cipherPassword = Encrypt(plainPassword);
                var row = (from x in en.emptables where x.employeeid == empid select x).FirstOrDefault();
                if (row != null)
                {
                    row.password = cipherPassword;
                    en.SaveChanges();
                    Textnewpassword.Text = "";
                    Textconfirmpassword.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password updated successfully.');window.location ='homepage.aspx';", true);
                    Session.Abandon();
                    Response.Redirect("login.aspx");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('password mismatched!')", true);
            }
        }
        //Method to encrypt.
        public string Encrypt(string clearText)
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
    }
}