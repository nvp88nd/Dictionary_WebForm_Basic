using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW.Page
{
    public partial class CaNhan : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                options.InnerHtml = @"<a href='/Page/CaNhan.aspx'><i class='fa-solid fa-user'></i> Hồ sơ</a>
                                      <a href='/Page/LichSu.aspx'><i class='fa-sharp fa-solid fa-clock-rotate-left'></i> Lịch sử tra từ</a>
                                      <a href='/Auth/DangNhap.aspx'><i class='fa-solid fa-arrow-right-from-bracket'></i> Đăng xuất</a>";
                loadInfo();
            }
            else
            {
                options.InnerHtml = @"<a href='/Auth/DangNhap.aspx'><i class='fa-solid fa-user'></i> Đăng nhập</a>
                                      <a href='/Auth/DangKy.aspx'><i class='fa-solid fa-pen-to-square'></i> Đăng ký</a>";
            }
        }
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            
        }
        protected void loadInfo()
        {
            string username = Session["UserName"].ToString();
            string query = $"select username, email, account_type from Accounts where username = '{username}'";
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
                            profile_item_1.InnerHtml = $@"<strong>Tên tài khoản:</strong> {dt.Rows[0]["username"]}";
                            profile_item_2.InnerHtml = $@"<strong>Email:</strong> {dt.Rows[0]["email"]}";
                            string type_acc = "Khách";
                            if (dt.Rows[0]["account_type"].ToString() != "user") type_acc = "ADMIN";
                            profile_item_3.InnerHtml = $@"<strong>Loại tài khoản:</strong> {type_acc}";
                        }
                    }
                    cnn.Close();
                }
            }
        }
    }
}