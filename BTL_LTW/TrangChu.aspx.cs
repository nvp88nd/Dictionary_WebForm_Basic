using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_LTW
{
    public partial class TrangChu : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        string selectLanguage = "anhviet";
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
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "showContentScript", "showContent();", true);
            ddlLanguage_SelectedIndexChanged(sender, e);
            LoadData();
        }
        protected void LoadData()
        {
            string searchTerm = Request.Form["txtSearch"]; // Hoặc Request["txtSearch"]
            if (!string.IsNullOrEmpty(searchTerm))
            {
                StringBuilder sb = new StringBuilder();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT top 1 id ,word, content FROM dbo.{selectLanguage} where word like '{searchTerm}%'";
                    SqlCommand command = new SqlCommand(query, connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            string langId = "";
                            while (reader.Read())
                            {
                                langId = reader["id"].ToString();
                                showData(sb, (string)reader["word"], (string)reader["content"]);
                            }
                            connection.Close();
                            if (Session["UserName"] != null)
                            {
                                string id = getIdUser(Session["UserName"].ToString());
                                string saveHistory = $"insert into History ({selectLanguage}_id, acc_id) values ({langId}, {id})";
                                SqlCommand saveH = new SqlCommand(saveHistory, connection);
                                connection.Open();
                                saveH.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        else
                            sb.Append("Không tìm thấy kết quả phù hợp.");
                        reader.Close();
                        litSearchResults.Text = sb.ToString();

                    }
                    catch (Exception ex)
                    {
                        Response.Write("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                litSearchResults.Text = "<p>Vui lòng nhập từ khóa tìm kiếm.</p>";
            }
        }

        public static List<string> GetSuggestions(string query)
        {
            List<string> suggestions = new List<string> { "Apple", "Banana", "Cherry", "Date" };
            return suggestions.Where(s => s.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        private string getIdUser(string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                string query = $"SELECT id FROM dbo.Accounts where username = '{v}'";
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
                                return dt.Rows[0]["id"].ToString();
                            }
                        }
                        cnn.Close();
                    }
                }
            }
            return null;
        }
        protected void showData(StringBuilder sb, string word, string content)
        {
            string[] parts = content.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            sb.AppendFormat("<h2>{0}</h2>", word);
            foreach (string part in parts)
            {
                if(part[0] == '*')
                {
                    string newpart = part.Substring(1);
                    sb.AppendFormat("<h3>{0}</h3>", newpart);
                }
                else if(part[0] == '-')
                {
                    string newpart = part.Substring(1);
                    sb.AppendFormat("<h4>{0}</h4>", newpart);
                }
                else if(part[0] == '=')
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
        protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlLanguage.SelectedValue;
            if (selectedValue == "en")
            {
                selectLanguage = "anhviet";
            }
            else if (selectedValue == "vi")
            {
                selectLanguage = "vietanh";
            }
        }
    }
}