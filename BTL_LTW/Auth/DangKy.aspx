<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="BTL_LTW.DangKy" %>

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
                    <img src="/images/logo.png" alt="logo" class="logo-img"/>
                    <h1>Từ điển trực tuyến</h1>
                </div>
            </a>
            <div class="form" id="register">
                <h2>Đăng ký</h2>
                <div class="input-box">
                    <input type="text" id="username" runat="server" required/>
                    <label>Tài khoản</label>
                    <asp:Label ID="existAcc" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="input-box">
                    <input type="text" id="email" runat="server" required/>
                    <label>Email</label>
                    <asp:Label ID="emailError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="input-box">
                    <input type="password" id="password" runat="server" required/>
                    <label>Mật khẩu</label>
                </div>
                <div class="input-box">
                    <input type="password" id="againpassword" runat="server" required/>
                    <label>Nhập lại mật khẩu</label>
                    <asp:Label ID="wrongAgainPass" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                </div>
                <div class="remember-forgot">
                    <label for="agree-t-p"><input type="checkbox" id="agree_t_p" runat="server"/> Đồng ý với chính sách và điều
                        khoản</label>
                </div>
                    <asp:Label ID="ckb_t_p" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                <input type="submit" value="Đăng ký" class="btn" id="btnRegister" runat="server" onserverclick="btnRegister_Click"/>
                <div class="login-register">
                    <p>Đã có tài khoản <a href="DangNhap.aspx" class="login-link">Đăng nhập</a></p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
