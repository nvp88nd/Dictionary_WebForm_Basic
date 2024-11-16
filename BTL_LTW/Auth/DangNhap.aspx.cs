using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BTL_LTW
{
    public partial class DangNhap : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null && Session["Password"] != null)
            {
                string savedUsername = Session["UserName"].ToString();
                string savedPassword = Session["Password"].ToString();

                Session.Remove("UserName");
                Session.Remove("Password");

                ClientScript.RegisterStartupScript(this.GetType(), "autoFill",
                    $"document.getElementById('username').value = '{savedUsername}';" + 
                    $"document.getElementById('password').value = '{savedPassword}';", true);
                savepass.Checked = true;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            existAcc.Visible = false;
            wrongPass.Visible = false;
            string storedPassword = GetUserByUsername(Request["username"]);
            if (storedPassword == null)
            {
                existAcc.Text = "Tài khoản không tồn tại.";
                existAcc.Visible = true;
            }
            if (VerifyPassword(Request["password"], storedPassword))
            {
                Session["UserName"] = Request["username"];
                if (savepass.Checked)
                {
                    Session["Password"] = Request["password"];
                }
                Response.Redirect("../TrangChu.aspx");
            }
            else
            {
                wrongPass.Text = "Mật khẩu sai.";
                wrongPass.Visible = true;
            }
        }

        private string GetUserByUsername(string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                string query = $"SELECT pass FROM dbo.Accounts where username = '{v}'";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, cnn))
                    {
                        cnn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return cmd.ExecuteScalar() as string;
                            }
                        }
                        cnn.Close();
                    }
                }
            }
            return null;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }

    }
}