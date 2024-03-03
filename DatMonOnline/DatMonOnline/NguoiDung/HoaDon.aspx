<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="HoaDon.aspx.cs" Inherits="DatMonOnline.NguoiDung.HoaDon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var giay = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID%>").style.display = "none";
            }, giay * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>                
            </div>
        </div>
        <div class="container">
            <asp:Repeater ID="repeatThanhToan" runat="server">
                <HeaderTemplate>
                    <table class="table table-responsive-sm table-bordered table-hover" id="tableHoaDon">
                        <thead class =" bg-dark text-white">
                            <tr>
                                <th>Số thẻ</th>
                                <th>Mã đơn hàng</th>
                                <th>Tên món ăn</th>
                                <th>Giá</th>
                                <th>Số lượng</th>
                                <th>Tổng tiền:</th>
                            </tr>
                        </thead>
                        <tbody>

                        
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <%-- số seri để định danh hóa đơn --%>
                        <td><%# Eval("soseri") %></td>
                        <td><%# Eval("madonhang") %></td>
                        <td><%# Eval("ten") %></td> 
                        <td><%# string.IsNullOrEmpty(Eval("gia").ToString()) ? "" :"VNĐ"+Eval("gia") %></td>
                        <td><%# Eval("soluong") %></td>
                        <td><%# Eval("tongtien") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <div class="text-center">
                <asp:LinkButton ID="lbTaiXuongHoaDon" runat="server" CssClass="btn btn-info">
                    <i class="fa fa-file-pdf-o mr-2"></i> Tải xuống</asp:LinkButton>
            </div>
            
        </div>
        </section>
</asp:Content>
