<%@ Page Title="" Language="C#" MasterPageFile="~/NguoiDung/NguoiDung.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="DatMonOnline.NguoiDung.TrangChu" %>
<%@ Import Namespace="DatMonOnline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- offer section -->

    <section class="offer_section layout_padding-bottom">
        <div class="offer_container">
            <div class="container ">
                <div class="row">
                    <asp:Repeater ID="repeatCategory" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6  ">
                                <div class="box ">
                                    <div class="img-box">
                                        <a href="Menu.aspx?id=<%# Eval("categoryID") %>">
                                            <img src="<%# Utils.LayURLHinhAnh(Eval("imageURL")) %>" alt="">

                                        </a>
                                    </div>
                                    <div class="detail-box">
                                        <h5><%# Eval("name") %></h5>
                                        <h6>
                                            <span>20%</span> Giảm giá
                                        </h6>
                                        <a href="Menu.aspx?id=<%# Eval("categoryID") %>">Đặt hàng ngay
                                            <svg version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 456.029 456.029" style="enable-background: new 0 0 456.029 456.029;" xml:space="preserve">
                                                <g>
                                                    <g>
                                                        <path d="M345.6,338.862c-29.184,0-53.248,23.552-53.248,53.248c0,29.184,23.552,53.248,53.248,53.248
                     c29.184,0,53.248-23.552,53.248-53.248C398.336,362.926,374.784,338.862,345.6,338.862z" />
                                                    </g>
                                                </g>
                                                <g>
                                                    <g>
                                                        <path d="M439.296,84.91c-1.024,0-2.56-0.512-4.096-0.512H112.64l-5.12-34.304C104.448,27.566,84.992,10.67,61.952,10.67H20.48
                     C9.216,10.67,0,19.886,0,31.15c0,11.264,9.216,20.48,20.48,20.48h41.472c2.56,0,4.608,2.048,5.12,4.608l31.744,216.064
                     c4.096,27.136,27.648,47.616,55.296,47.616h212.992c26.624,0,49.664-18.944,55.296-45.056l33.28-166.4
                     C457.728,97.71,450.56,86.958,439.296,84.91z" />
                                                    </g>
                                                </g>
                                                <g>
                                                    <g>
                                                        <path d="M215.04,389.55c-1.024-28.16-24.576-50.688-52.736-50.688c-29.696,1.536-52.224,26.112-51.2,55.296
                     c1.024,28.16,24.064,50.688,52.224,50.688h1.024C193.536,443.31,216.576,418.734,215.04,389.55z" />
                                                    </g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                                <g>
                                                </g>
                                            </svg>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>
        </div>
    </section>

    <!-- end offer section -->

    <!-- about section -->

    <section class="about_section layout_padding">
        <div class="container  ">

            <div class="row">
                <div class="col-md-6 ">
                    <div class="img-box">
                        <img src="../Style/images/about-img.png" alt="">
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="detail-box">
                        <div class="heading_container">
                            <h2>Chúng tôi là FoodBook
                            </h2>
                        </div>
                        <ul>
                            <li>Mang đến cho bạn món ăn ưa thích, nóng hổi và ngon lành.</li>
                           <br >
                           <li>Đặt đồ ăn online chỉ sau vài cú chạm.</li>
                            <br />
                           <li>Đa dạng lựa chọn.</li>
                        </ul>
                           
                        <a href="About.aspx">Đọc thêm
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- end about section -->

    <!-- client section -->

    <section class="client_section layout_padding-bottom pt-5">
        <div class="container">
            <div class="heading_container heading_center psudo_white_primary mb_45">
                <h2>Khách hàng của chúng tôi nói những gì
                </h2>
            </div>
            <div class="carousel-wrap row ">
                <div class="owl-carousel client_owl-carousel">
                    <div class="item">
                        <div class="box">
                            <div class="detail-box">
                                <p>
                                    M đi ăn tối r đông mà nv phục vụ siêu nhanh. Cửa hàng đông k còn chỗ ngồi. Gà ngon, k bị bở.
                                    Mì Ý cũng vừa vị. M đang ăn thì có chú ong đáng yêu xuống nhảy và đập tay với mn. M ng lớn cũng
                                    thích còn các bé thì mê tít lunn.
                                    Nch sẽ quay lại
                                </p>
                                <h6>Hoài Anh
                                </h6>
                                <p>
                                    Ngon, nv nhiệt tình
                                </p>
                            </div>
                            <div class="img-box">
                                <img src="../Style/images/demokh1.jpg" alt="" class="box-img">
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="box">
                            <div class="detail-box">
                                <p>
                                    Lần thứ 2 order tại Mc thấy rất hài lòng về chất lượng đồ ăn ở đây.
                                    Thịt gà lớp vỏ giòn, thịt bên trong không bị khô. Mặc dù là gọi về nhưng khi nhận đồ ăn vẫn rất nóng hổi
                                    Thịt heo mềm, đậm vị và phần nước sốt cũng rất ngon.
                                    Sẽ ủng hộ Mc dài dài.
                                    Rate: Chất lượng 8/10
                                </p>
                                <h6>Hoàng Lê
                                </h6>
                                <p>
                                    Hài Lòng
                                </p>
                            </div>
                            <div class="img-box">
                                <img src="../Style/images/demokh.jpg" alt="" class="box-img">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- end client section -->

</asp:Content>
