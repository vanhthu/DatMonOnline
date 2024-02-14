<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="ThongTinNguoiDung.aspx.cs" Inherits="DatMonOnline.NguoiDung.ThongTinNguoiDung" %>

<%@ Import Namespace="DatMonOnline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%
        string imageURL = Session["imageURL"].ToString();
    %>
    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <h2>Thông tin người dùng</h2>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-title mb-4">
                                <div class="d-flex justify-content-start">
                                    <div class="image-container">
                                        <img src="<% Utils.LayURLHinhAnh(imageURL); %>" id="imgNguoiDung" style="width: 150px; height: 150px;"
                                            class="img-thumbnail" />

                                        <div class="middle pt-2">
                                            <a href="DangKy.aspx?id=<%Response.Write(Session["userID"]); %>" class="btn btn-warning">
                                                <i class="fa fa-pencil"></i>Chỉnh sửa
                                            </a>
                                        </div>
                                    </div>

                                    <div class="userData ml-3">
                                        <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold">
                                            <a href="javascript:void(0);"><%Response.Write(Session["name"]); %></a>
                                        </h2>

                                        <h6 class="d-block" style="font-size: 1.5rem; font-weight: bold">
                                            <a href="javascript:void(0);">
                                                <asp:Label ID="lblUserName" runat="server" ToolTip="Tên người dùng"> 
                                                            @<%Response.Write(Session["userName"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>

                                        <h6 class="d-block">
                                            <a href="javascript:void(0);">
                                                <asp:Label ID="lblEmail" runat="server" ToolTip="Email"> 
                                                            @<%Response.Write(Session["email"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>

                                        <h6 class="d-block">
                                            <a href="javascript:void(0);">
                                                <asp:Label ID="lblNgayTao" runat="server" ToolTip="Ngày tạo"> 
                                                            @<%Response.Write(Session["ngaytao"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <%-- thông tin cơ bản  --%>
                                            <a class="nav-link active text-info" id="basicInfor-tab" data-toggle="tab"
                                                href="#ThongTinNguoiDung" aria-selected="true"><i class="fa fa-id-badge mr-2"></i>Thông tin người dùng</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="connectedServices-tab" data-toggle="tab"
                                                href="#KetNoi" aria-selected="false"><i class="fa fa-clock-o mr-2"></i>Lịch sử mua hàng</a>
                                        </li>
                                    </ul>


                                    <div class="tab-content ml-1" id="myTabContent">
                                        <%-- tab thông tin người dùng start --%>
                                        <div class="tab-pane fade show active" id="basicInfor" role="tabpanel" aria-labelledby="basicInfor-tab">
                                            <asp:Repeater ID="repeatThongTinNguoiDung" runat="server">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight:bold">Tên đầy đủ</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("name") %>
                                                        </div>
                                                    </div>
                                                    <hr />

                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight:bold">Tên người dùng</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("userName") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight:bold">Số điện thoại</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("sdt") %>
                                                        </div>
                                                    </div>
                                                    <hr />

                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight:bold">Email</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("email") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight:bold">Mã bưu chính</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("postCode") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-2 col-5">
                                                            <label style="font-weight:bold">Địa chỉ</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("diachi") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <%-- tab thông tin người dùng end --%>


                                        <%-- lịch sử mua hàng start --%>
                                        <div class="tab-pane fade" id="connectedServices" role="tabpanel" aria-labelledby="connectedServices-tab"></div>
                                        <%-- lịch sử mua hàng end --%>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
