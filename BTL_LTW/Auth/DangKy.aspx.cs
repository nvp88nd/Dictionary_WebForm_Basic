using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW
{
    public partial class DangKy : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            wrongAgainPass.Visible = false;
            existAcc.Visible = false;
            ckb_t_p.Visible = false;

            string tk = username.Value;
            string mail = email.Value;
            string pass = password.Value;
            string pass2 = againpassword.Value;
            if(!GetUserByUsername(tk))
            {
                if (pass == pass2)
                {
                    if (agree_t_p.Checked)
                        insertTK(tk, mail, pass);
                    else
                    {
                        ckb_t_p.Text = "Bạn cần đồng ý điều khoản sử dụng";
                        ckb_t_p.Visible = true;
                    }
                }
                else
                {
                    wrongAgainPass.Text = "Mật khẩu không đúng!";
                    wrongAgainPass.Visible = true;
                }
            }
            else
            {
                existAcc.Text = "Tài khoản đã tồn tại!";
                existAcc.Visible = true;
            }    
        }

        private void insertTK(string tk, string mail, string pass)
        {
            if (!string.IsNullOrEmpty(tk) && !string.IsNullOrEmpty(mail) && !string.IsNullOrEmpty(pass))
            {
                string query = $"INSERT INTO Accounts (username, pass, email)" +
                                $"values ('{tk}', '{pass}', '{mail}')";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, cnn))
                    {
                        cnn.Open();
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đăng ký thành công!')", true);
                        }
                        cnn.Close();
                    }
                }
            }
        }

        private bool GetUserByUsername(string v)
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
                                return true;
                                //return cmd.ExecuteScalar() as string;
                            }
                        }
                        cnn.Close();
                    }
                }
            }
            return false;
        }
    }
}