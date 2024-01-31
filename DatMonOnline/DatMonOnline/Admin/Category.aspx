﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="DatMonOnline.Admin.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var giay = 5;
            setTimeout(function () {
                document.getElementById("<%=lbMessage.ClientID%>").style.display = "none";
            }, giay * 1000);
        };
    </script>

    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgCategory.ClientID%>').prop('src', e.target.result).width(200).height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pcoded-inner-content pt-0">

        <div class="align-align-self-end">
            <asp:Label ID="lbMessage" runat="server" Visible="false"></asp:Label>
        </div>

        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">


                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Danh mục</h4>
                                            <div>
                                                <div class="form-group">
                                                    <label>Tên món ăn</label>
                                                    <div>
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                                                            placeholder="Nhập vào tên món ăn..." required></asp:TextBox>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value="0" />

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Hình ảnh món ăn</label>
                                                    <div>
                                                        <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control"
                                                            onchange="ImagePreview(this);" />
                                                    </div>
                                                </div>
                                                <div class="form-check pl-4">
                                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive"
                                                        CssClass="form-check-input" />
                                                </div>

                                                <div class="pb-5">
                                                    <asp:Button ID="btnAdd0rUpdate" runat="server" Text="Thêm" CssClass="btn btn-primary"
                                                        OnClick="btnAdd0rUpdate_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnClear" runat="server" Text="Xóa" CssClass="btn btn-primary"
                                                        CausesValidation="false" OnClick="btnClear_Click"/>
                                                </div>
                                                <div>
                                                    <asp:Image ID="imgCategory" runat="server" CssClass="img-thumbnail" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Bảng thông tin</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">
                                                    <asp:Repeater ID="repeatCategory" runat="server">
                                                        <HeaderTemplate>
                                                            <table>
                                                                <tr>
                                                                    <th>Tên</th>
                                                                    <th>Hình ảnh</th>
                                                                    <th>IsActive</th>
                                                                    <th>Ngày tạo</th>
                                                                    <th>Hành động</th>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate> 

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td> <%# Eval("name") %> </td>
                                                                <td> <%# Eval("imageURL") %> </td>
                                                                <td> <%# Eval("isActive") %> </td>
                                                                <td> <%# Eval("ngaytao") %> </td>
                                                                <td></td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
