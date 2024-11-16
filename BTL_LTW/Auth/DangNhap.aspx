<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="BTL_LTW.DangNhap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dictionary Web</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="~/Assets/css/normalize.min.css" />
    <link rel="stylesheet" href="~/Assets/css/base.css" />
    <link rel="stylesheet" href="~/Assets/font/font-awesome-6-5-2-pro-full-main/css/all.css" />
    <link rel="stylesheet" href="~/Assets/css/trangchu.css" />
    <link rel="stylesheet" href="~/Assets/css/auth.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <a href="../TrangChu.aspx">
                <div class="logo">
                    <img src="/images/logo.png" alt="logo" class="logo-img">
                    <h1>Từ điển trực tuyến</h1>
                </div>
            </a>
            <div class="form" id="login">
                <h2>Đăng nhập</h2>
                <div class="input-box">
                    <input type="text" required id="username" runat="server">
                    <label>Tài khoản</label>
                    <asp:Label ID="existAcc" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="input-box">
                    <input type="password" required id="password" runat="server">
                    <label>Mật khẩu</label>
                    <asp:Label ID="wrongPass" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="remember-forgot">
                    <label for="savepass"><input type="checkbox" id="savepass" runat="server" checked> Lưu mật khẩu</label>
                    <a href="#">Quên mật khẩu?</a>
                </div>
                <input type="submit" value="Đăng nhập" class="btn" id="btnLogin" runat="server" onserverclick="btnLogin_Click"/>
                <div class="login-register">
                    <p>Chưa có tài khoản <a href="DangKy.aspx" class="register-link">Đăng ký</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
