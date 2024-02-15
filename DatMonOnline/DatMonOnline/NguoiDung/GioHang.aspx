<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="GioHang.aspx.cs" Inherits="DatMonOnline.NguoiDung.GioHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" ></asp:Label>
                </div>
                <h2>Giỏ hàng</h2>
            </div>
        </div>
    </section>
</asp:Content>
