<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="DatMonOnline.NguoiDung.DangKy" %>
<%@ Import Namespace="DatMonOnline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section class="book-section layout_padding">
        <div class="container">
            <div class="heading-container">
                <div class="align-self-end">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <asp:Label ID="lblHeaderMessage" runat="server" Text="<h2>Đăng ký</h2>"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-container">

                        <div>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Nhập vào tên" 
                                ToolTip="Tên đầy đủ"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Vui lòng nhập vào tên!" ControlToValidate="txtName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revName" runat="server" ErrorMessage="Tên không được là số! Vui lòng nhập lại!" ControlToValidate="txtName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$"></asp:RegularExpressionValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Nhập vào tên người dùng" 
                                ToolTip="Tên người dùng"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="Vui lòng nhập vào tên người dùng!" ControlToValidate="txtUserName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Nhập vào email" 
                                ToolTip="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Vui lòng nhập vào email!" ControlToValidate="txtEmail"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txtSoDT" runat="server" CssClass="form-control" placeholder="Nhập vào số điện thoại" 
                                ToolTip="Số điện thoại"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSoDT" runat="server" ErrorMessage="Vui lòng nhập vào số điện thoại!" ControlToValidate="txtSoDT"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revSoDT" runat="server" ErrorMessage="Không đúng định dạng!" ControlToValidate="txtSoDT"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                        </div>

                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form-container">

                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
