using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace empreview
{
    public partial class login : System.Web.UI.Page
    {
        empreviewtrackEntities1 en = new empreviewtrackEntities1();
        emptable tb = new emptable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Labelmsg.Text = "";
        }
        protected void Btnlogin_Click(object sender, EventArgs e)
        {
            var plainpassword = "";
            var password = "";
            plainpassword = Textpassword.Text;
            password = Encrypt(plainpassword);
            var row = (from x in en.emptables where x.employeeid == Textusername.Text && x.password == password select x).FirstOrDefault();
            if (row != null)
            {
                Session["name"] = row.name;
                Session["empid"] = row.employeeid;
                Session["role"] = row.roleid;
                Response.Redirect("homepage.aspx");
                clear();
            }
            else
            {
                Labelmsg.Text = "Wrong username or password!";
                Textpassword.Text = "";
            }
        }

        public void clear()
        {
            Textusername.Text = "";
            Textpassword.Text = "";
        }

        protected void Btnreset_Click(object sender, EventArgs e)
        {
            clear();
            Labelmsg.Text = "";
        }
        protected void LinkButtonforgotpassword_Click(object sender, EventArgs e)
        {
            if (Textusername.Text == "")
            {
                Labelmsg.Text = "Please fill your username to recieve email";
            }
            else
            {
                var row = (from x in en.emptables where x.employeeid == Textusername.Text select x).FirstOrDefault();
                if (row != null)
                {
                    Labelmsg.Text = "";
                    string plainPassword = "";
                    string cipherPassword = row.password;
                    plainPassword = Decrypt(cipherPassword);
                    string to = row.email;
                    sendmail(to, plainPassword);
                }
                else
                {
                    Labelmsg.Text = "Employee id not found, Email not sent!";
                }
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

        //Method to decrypt.
        public string Decrypt(string cipherText)
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

        //Method to send mail
        public void sendmail(string tomail, string pwd)
        {
            try
            {
                string from = "ramkarthick18@gmail.com"; //standard mail id
                MailMessage mail = new MailMessage();
                mail.To.Add(tomail);
                mail.From = new MailAddress(from);
                mail.Subject = "Employee review track";
                string Body = "Your password is \n" + pwd;
                mail.Body = Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                string pass = "";  //give your password here.
                smtp.Credentials = new System.Net.NetworkCredential(from, pass);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Labelmsg.ForeColor = Color.Green;
                Labelmsg.Text = "Mail sent successfully to " + tomail;
            }
            catch (Exception)
            {
                Labelmsg.Text = "Error at mail code, give password";
            }
        }
    }
}