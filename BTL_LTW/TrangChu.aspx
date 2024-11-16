<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="BTL_LTW.TrangChu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dictionary Web</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="~/Assets/css/normalize.min.css" />
    <link rel="stylesheet" href="~/Assets/css/base.css" />
    <link rel="stylesheet" href="~/Assets/font/font-awesome-6-5-2-pro-full-main/css/all.css" />
    <link rel="stylesheet" href="~/Assets/css/trangchu.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function dropDownMenu() {
            var dropdown = document.getElementById("options");
            if (dropdown.style.display === "block") {
                dropdown.style.display = "none";
            } else {
                dropdown.style.display = "block";
            }
        }

        function showContent() {
            const views = document.querySelectorAll('.content-card');
            views.forEach(v => v.style.display = 'none');
            document.getElementById('content-search').style.display = 'block';
        }

        function checkEnter(event) {
            if (event.key === 'Enter') {
                event.preventDefault(); // Ngăn chặn hành động mặc định (để không gửi form)
                document.getElementById('btnSearch').click();
            }
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="app">
        <header class="header">
            <div class="grid">
                <div class="header__nav">
                    <a href="TrangChu.aspx">
                        <div class="logo">
                            <img src="/images/logo.png" alt="logo" class="logo-img">
                            <h1>Từ điển trực tuyến</h1>
                        </div>
                    </a>

                    <div class="profile" onclick="dropDownMenu()">
                        <img src="/images/profile-avt.jpg" alt="Hồ sơ" class="profile-avt">
                        <span class="profile-username">Tài khoản</span>
                        <div class="dropdown-menu" runat="server" id="options" style="display: none;"></div>
                    </div>
                </div>

                <div class="header__search">
                    <div class="header__search-all">
                        <input type="text" class="header__search-input" id="txtSearch" placeholder="Nhập từ cần tra cứu..." runat="server" onkeydown="checkEnter(event)"/>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList CssClass="header__search-selection" ID="ddlLanguage" runat="server" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Anh - Việt" Value="en"></asp:ListItem>
                                    <asp:ListItem Text="Việt - Anh" Value="vi"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="header__search-btn" id="btnSearch" runat="server" onserverclick="btnSearch_Click">
                            <i class="fas fa-search"></i>
                        </button>
                        <div id="searchSuggestions" class="header__search-suggestions" runat="server"></div>
                    </div>
                </div>
            </div>
        </header>

        <div class="container">
            <div class="grid">
                <div class="content">
                    <div class="content-card col-5" id="recently">
                        <h2>Từ đã tra cứu gần đây</h2>
                        <ul class="content-list">
                            <li class="content-item"><a href="/word/hello">Hello</a></li>
                            <li class="content-item"><a href="/word/computer">Computer</a></li>
                            <li class="content-item"><a href="/word/technology">Technology</a></li>
                        </ul>
                    </div>

                    <div class="content-card col-5" id="outstanding">
                        <h2>Từ điển nổi bật</h2>
                        <ul class="content-list">
                            <li class="content-item"><a href="/word/">Artificial Intelligence</a></li>
                            <li class="content-item"><a href="/word/cloud-computing">Cloud Computing</a></li>
                            <li class="content-item"><a href="/word/data-science">Data Science</a></li>
                        </ul>
                    </div>

                    <div class="content-search" id="content-search">
                        <asp:Literal ID="litSearchResults" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>

        <footer class="footer">
            <div class="grid">
                <p>&copy; 2024 Từ điển trực tuyến</p>
                <a href="#">Điều khoản sử dụng</a>
                <a href="#">Chính sách bảo mật</a>
            </div>
        </footer>
    </div>
    
    </form>
</body>
</html>
