using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW.Page
{
    public partial class LichSu : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                options.InnerHtml = @"<a href='/Page/CaNhan.aspx'><i class='fa-solid fa-user'></i> Hồ sơ</a>
                                      <a href='/Page/LichSu.aspx'><i class='fa-sharp fa-solid fa-clock-rotate-left'></i> Lịch sử tra từ</a>
                                      <a href='/Auth/DangNhap.aspx'><i class='fa-solid fa-arrow-right-from-bracket'></i> Đăng xuất</a>";
            }
            else
            {
                options.InnerHtml = @"<a href='/Auth/DangNhap.aspx'><i class='fa-solid fa-user'></i> Đăng nhập</a>
                                      <a href='/Auth/DangKy.aspx'><i class='fa-solid fa-pen-to-square'></i> Đăng ký</a>";
            }
            LoadData();
        }
        protected void LoadData()
        {
            string user = Session["UserName"].ToString();
            if (!string.IsNullOrEmpty(user))
            {
                StringBuilder sb = new StringBuilder();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $@"SELECT 
                                        h.id AS HistoryId,
                                        a.username,
                                        ah.word AS AnhVietWord, 
                                        ah.content AS AnhVietContent, 
                                        vh.word AS VietAnhWord, 
                                        vh.content AS VietAnhContent,
                                        h.created_at
                                    FROM
                                        History h
                                    INNER JOIN
                                        Accounts a ON h.acc_id = a.id
                                    LEFT JOIN
                                        anhviet ah ON h.anhviet_id = ah.id
                                    LEFT JOIN
                                        vietanh vh ON h.vietanh_id = vh.id
                                    WHERE
                                        a.username = '{user}'
                                    ORDER BY
                                        h.created_at DESC; ";
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!DBNull.Value.Equals(reader["AnhVietWord"]) && !DBNull.Value.Equals(reader["AnhVietContent"]))
                                showData(sb, (string)reader["AnhVietWord"], (string)reader["AnhVietContent"]);
                            if (!DBNull.Value.Equals(reader["VietAnhWord"]) && !DBNull.Value.Equals(reader["VietAnhContent"]))
                                showData(sb, (string)reader["VietAnhWord"], (string)reader["VietAnhContent"]);
                        }
                        reader.Close();
                        litHistory.Text = sb.ToString();

                    }
                    catch (Exception ex)
                    {
                        Response.Write("Lỗi: " + ex.Message);
                    }
                }
            }
        }
        protected void showData(StringBuilder sb, string word, string content)
        {
            string[] parts = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            sb.AppendFormat("<h2>{0}</h2>", word);
            foreach (string part in parts)
            {
                if (part[0] == '*')
                {
                    string newpart = part.Substring(1);
                    sb.AppendFormat("<h3>{0}</h3>", newpart);
                }
                else if (part[0] == '-')
                {
                    string newpart = part.Substring(1);
                    sb.AppendFormat("<h4>{0}</h4>", newpart);
                }
                else if (part[0] == '=')
                {
                    string newpart = part.Substring(1);
                    newpart = newpart.Replace("+", ": ");
                    sb.AppendFormat("<h4>{0}</h4>", newpart);
                }
                else
                {
                    string newpart = part.Substring(1);
                    sb.AppendFormat("<h4>Khác: {0}</h4>", newpart);
                }
            }
        }
    }
}