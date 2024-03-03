<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="DatMonOnline.NguoiDung.DangKy" %>

<%@ Import Namespace="DatMonOnline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var giay = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID%>").style.display = "none";
            }, giay * 1000);
        };
    </script>

    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgUser.ClientID%>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <asp:Label ID="lblHeaderMessage" runat="server" Text="<h2>Đăng ký</h2>"></asp:Label>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        <div>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Vui lòng nhập vào tên!" ControlToValidate="txtName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revName" runat="server" ErrorMessage="Tên không được là số! Vui lòng nhập lại!" ControlToValidate="txtName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Nhập vào tên"
                                ToolTip="Tên đầy đủ"></asp:TextBox>
                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Vui lòng nhập vào tên người dùng!" ControlToValidate="txtUserName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Nhập vào tên người dùng"
                                ToolTip="Tên người dùng"></asp:TextBox>

                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Vui lòng nhập vào email!" ControlToValidate="txtEmail"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Nhập vào email"
                                ToolTip="Email"></asp:TextBox>

                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfvSoDT" runat="server" ErrorMessage="Vui lòng nhập vào số điện thoại!" ControlToValidate="txtSoDT"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revSoDT" runat="server" ErrorMessage="Số điện thoại phải có 10 chữ số!" ControlToValidate="txtSoDT"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtSoDT" runat="server" CssClass="form-control" placeholder="Nhập vào số điện thoại"
                                ToolTip="Số điện thoại"></asp:TextBox>

                        </div>

                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form_container">

                        <div>
                            <asp:RequiredFieldValidator ID="rfvDiaChi" runat="server" ErrorMessage="Vui lòng nhập vào địa chỉ!" ControlToValidate="txtDiaChi"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control" placeholder="Nhập vào địa chỉ"
                                ToolTip="Địa chỉ" TextMode="MultiLine"></asp:TextBox>

                        </div>

                        <%--<div>
                            <asp:RequiredFieldValidator ID="rfvMaXacNhan" runat="server" ErrorMessage="Vui lòng nhập vào mã xác nhận!" ControlToValidate="txtMaXacNhan"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMaXacNhan" runat="server" ErrorMessage="Mã phải có 6 số!" ControlToValidate="txtMaXacNhan"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{6}$"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtMaXacNhan" runat="server" CssClass="form-control" placeholder="Nhập vào mã xác nhận"
                                ToolTip="Mã xác nhận" TextMode="Number"></asp:TextBox>

                        </div>--%>

                        <div>
                            <asp:FileUpload ID="fuUserImage" runat="server" CssClass="form-control" ToolTip="Ảnh người dùng" onchange="ImagePreview(this);" />

                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập vào mật khẩu!" ControlToValidate="txtMatKhau"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtMatKhau" runat="server" CssClass="form-control" placeholder="Nhập vào mật khẩu"
                                ToolTip="Mật khẩu" TextMode="Password"></asp:TextBox>

                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfvNhapLaiMatKhau" runat="server" ErrorMessage="Vui lòng nhập lại mật khẩu!" ControlToValidate="txtNhapLaiMatKhau"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtNhapLaiMatKhau" runat="server" CssClass="form-control" placeholder="Nhập vào mật khẩu"
                                ToolTip="Nhập lại mật khẩu" TextMode="Password"></asp:TextBox>

                        </div>

                    </div>
                </div>

                <div class="row pl-4">
                    <div class="btn_box">
                        <asp:Button ID="btnDangKy" runat="server" Text="Đăng ký" CssClass="btn btn-success rounded-pill pl-4 pr-4 text-white" 
                            OnClick="btnDangKy_Click"/>
                        <asp:Label ID="lblDaDK" runat="server" CssClass="pl-3 text-black-100" Text="Đã có tài khoản? <a href='DangNhap.aspx' class='badge badge-info'>Đăng nhập ngay!</a>"></asp:Label>
                    </div>
                </div>

                <div class="row pt-5">
                    <div style="align-items: center">
                        <asp:Image ID="imgUser" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
