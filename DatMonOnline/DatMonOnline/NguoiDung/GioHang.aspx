<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="GioHang.aspx.cs" Inherits="DatMonOnline.NguoiDung.GioHang" %>

<%@ Import Namespace="DatMonOnline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                <h2>Giỏ hàng</h2>
            </div>
        </div>

        <div class="container">
            <asp:Repeater ID="repeatSanPhamGioHang" runat="server" OnItemCommand="repeatSanPhamGioHang_ItemCommand" OnItemDataBound="repeatSanPhamGioHang_ItemDataBound">
                <HeaderTemplate>
                    <table class="table">
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
                            <img width="60" src="<%# Utils.LayURLHinhAnh(Eval("imageURL")) %>" />
                        </td>
                        <td>
                            <asp:Label ID="lblGia" runat="server" Text='<%# Eval("gia") %>'></asp:Label>
                            <asp:HiddenField ID="hdProductID" runat="server" Value='<%# Eval("productID") %>' />
                            <%-- note, lưu ý lưu ý --%>
                            <asp:HiddenField ID="hdSoLuong" runat="server" Value='<%# Eval("SoLuong") %>' />
                            <asp:HiddenField ID="hdSLSP" runat="server" Value='<%# Eval("slsp") %>' />
                        </td>

                        <%--<td>
                            <div class="product__details__option">
                                <div class="quantity">
                                    <div class="pro-qty">
                                        <asp:TextBox ID="txtSoLuong" runat="server" TextMode="Number" Text='<%# Eval("soluong") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revSoLuong" runat="server" ErrorMessage="*" Font-Size="Small" ForeColor="Red"
                                            ValidationExpression="[1-9]*" ControlToValidate="txtSoLuong"
                                            Display="Dynamic" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>

                                    </div>
                                </div>
                            </div>
                        </td>--%>

                        <%-- số lượng --%>
                        <td>
                            <asp:TextBox ID="txtSoLuong" runat="server" TextMode="Number" Text='<%# Eval("soluong") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revSoLuong" runat="server" ErrorMessage="*" Font-Size="Small" ForeColor="Red"
                                ValidationExpression="[1-9]*" ControlToValidate="txtSoLuong"
                                Display="Dynamic" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>
                        </td>

                        <td>
                            <asp:Label ID="lblTongTien" runat="server"></asp:Label>
                        </td>

                        <td>
                            <asp:LinkButton ID="lbXoa" runat="server" Text="Xóa" CommandName="xoa" CommandArgument='<%# Eval("productID") %>'
                                OnClientClick="return confirm('Bạn có chắc chắn muốn xóa món này ra khỏi giỏ hàng?');"><i class="fa fa-close"></i></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>

                    <tr>
                        <td colspan="3"></td>
                        <td class="pl-lg-5">
                            <b>Tổng thành tiền: </b>
                        </td>
                        <td><%Response.Write(Session["tongThanhTien"]); %></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td colspan="2" class="continue__btn">
                            <a href="Menu.aspx" class="btn btn-info"><i class="fa fa-arrow-circle-left mr-2">Tiếp tục mua sắm!</i></a>
                        </td>

                        <td>
                            <asp:LinkButton ID="lbCapNhat" runat="server" CommandName="capnhat" CssClass="btn btn-warning">
                                <i class="fa fa-refresh mr-2"></i> Cập nhật
                            </asp:LinkButton>
                        </td>

                        <td>
                            <asp:LinkButton ID="lbKiemTra" runat="server" CommandName="kiemtra" CssClass="btn btn-success">
                                <i class="fa fa-arrow-circle-right mr-2"></i> Kiểm tra
                            </asp:LinkButton>
                        </td>
                    </tr>


                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>


    </section>
</asp:Content>
