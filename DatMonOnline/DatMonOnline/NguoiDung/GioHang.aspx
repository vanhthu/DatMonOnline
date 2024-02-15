<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="GioHang.aspx.cs" Inherits="DatMonOnline.NguoiDung.GioHang" %>
<%@ Import Namespace="DatMonOnline" %>
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

        <div class="container">
            <asp:Repeater ID="repeatSanPhamGioHang" runat="server" OnItemCommand="repeatSanPhamGioHang_ItemCommand" OnItemDataBound="repeatSanPhamGioHang_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <thead>
                            <tr>
                                <th>Tên</th>
                                <th>Hình ảnh</th>
                                <th>Giá</th>
                                <th>Số lượng</th>
                                <th>Tổng tiền</th>
                                <th></th>
                            </tr>
                        </thead>

                        <tbody>

                        
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                        </td>
                        <td>
                            <img width="60" src="<%# Utils.LayURLHinhAnh("imageURL") %>" />
                        </td>
                        <td>
                            <asp:Label ID="lblGia" runat="server" Text='<%# Eval("gia") %>'></asp:Label>
                            <asp:HiddenField ID="hdProductID" runat="server" Value='<%# Eval("productID") %>' />
                            <asp:HiddenField ID="hdSoLuong" runat="server" Value='<%# Eval("SoLuong") %>' />
                            <asp:HiddenField ID="hdSLSP" runat="server" Value='<%# Eval("slsp") %>' />                            
                        </td>

                        <td>

                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>


    </section>
</asp:Content>
