﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaNhan.aspx.cs" Inherits="BTL_LTW.Page.CaNhan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dictionary - Profile</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="~/Assets/css/normalize.min.css" />
    <link rel="stylesheet" href="~/Assets/css/base.css" />
    <link rel="stylesheet" href="~/Assets/font/font-awesome-6-5-2-pro-full-main/css/all.css" />
    <link rel="stylesheet" href="~/Assets/css/trangchu.css" />
    <link rel="stylesheet" href="~/Assets/css/profile.css" />
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
            </div>
        </header>

        <div class="container">
            <div class="grid">
                <div class="content">
                    <div class="profile-page">
                        <div class="profile-left">
                            <img src="/images/profile-avt.jpg" alt="Avatar" class="profile-avt-large">
                        </div>

                        <div class="profile-right">
                            <h2 class="profile-username-large">Thông tin tài khoản</h2>
                            <div class="profile-info">
                                <ul>
                                    <li id="profile_item_1" runat="server"></li>
                                    <li id="profile_item_2" runat="server"></li>
                                    <li id="profile_item_3" runat="server"></li>
                                </ul>
                                <button class="btn-change-password" runat="server" id="btnChangePass" onserverclick="btnChangePass_Click">Đổi mật khẩu</button>
                            </div>
                        </div>
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
