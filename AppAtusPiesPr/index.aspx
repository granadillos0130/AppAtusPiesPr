<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AppAtusPiesPr.Index2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='vista/css/main.css' />
    <script src="Vista/js/main.js"></script>
    <link rel="shortcut icon" href="vista/recursos/ATP.png" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">

    <!-- Incluye SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">

    <!-- Incluye SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <!-- Filtros -->
    <center>
        <div class="navbarFiltros">
            <nav>

                <ul class="menuFiltros">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "vista/moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
                                    <%# Eval("descripcion") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>
    </center>

    <center>
        <div class="navbarFiltros">
            <nav>

                <ul class="menuFiltros">
                    <asp:Repeater ID="RepeaterMarca" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "vista/moduloMarcaFiltrada.aspx?id=" + Eval("idMarca") %>'>
                                    <%# Eval("nombreMarca") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>
    </center>

    <div id="carouselExampleControls" class="carousel slide" data-bs-ride="carousel" data-bs-interval="3000">
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="https://cdn.sanity.io/images/pu5wtzfc/production/9554b0a644e5ee45676ce176b13235a77a184ce6-2000x1251.gif" class="d-block w-100" alt="...">
            </div>
            <div class="carousel-item">
                <img src="https://www.manelsanchez.com/uploads/media/images/1R_20.gif" class="d-block w-100" alt="...">
            </div>
            <div class="carousel-item">
                <img src="https://s3-eu-west-3.amazonaws.com/web-magazines/entornos/deployment/mine/wp-content/uploads/2019/11/11132507/new_balance_trail_revista_mine_style.gif" class="d-block w-100" alt="...">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

    <style>
        /* Define un tamaño fijo para el carrusel */
        #carouselExampleControls {
            height: 500px; /* Altura fija del carrusel */
            border-top: none; /* Elimina el borde superior */
            overflow: hidden; /* Oculta cualquier contenido desbordante */
        }

        /* Asegura que las imágenes se ajusten completamente al carrusel */
        .carousel-inner img {
            width: 100%; /* Ajusta el ancho de la imagen al carrusel */
            height: 100%; /* Ajusta la altura de la imagen al carrusel */
            object-fit: contain; /* La imagen se ajusta dentro del espacio sin recortarse */
            object-position: center; /* Centra la imagen en el carrusel */
        }
    </style>


    <br />
    <div class="containerr">

        <center>
            <h4>Conoce Nuestros Productos</h4>
        </center>
        <br />

        
        <p style="padding-left: 10%;">
            <hr>
            <div style="padding-left: 10%;" >Productos mejor valorados</div><hr>
        </p>

        <!-- Contenedor para las tarjetas -->
        <div id="cardsContainer" class="cards-container">

            <asp:Repeater ID="Repeater3" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                        <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                        <div class="card-info">
                            <div class="card-details">
                                <a class="cardseller" href='<%# "vista/perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
                                    <%# Eval("NombreVendedor") %>
                                    <%# Eval("apellidos") %><br>
                                </a>
                                <div class="rating">
                                    <span class="heart" data-value="1">&#10084;<%# Eval("ValoracionPromedio") %></span>


                                </div>
                                <div class="cardprice">
                                    <p>$<%# Eval("precio") %></p>
                                </div>
                                <div class="cardButtons">
                                    <a class="buy-button" href='Vista/moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                    <a class="save-button"
                                        data-id='<%# Eval("idProdctoEmpresa") %>'
                                        data-nombre='<%# HttpUtility.HtmlAttributeEncode(Eval("nombreProducto").ToString()) %>'
                                        data-imagen='<%# ResolveUrl(Eval("imagen").ToString()) %>'
                                        data-precio='<%# Eval("precio") %>'
                                        data-vendedor='<%# HttpUtility.HtmlAttributeEncode(Eval("NombreVendedor").ToString()) %>'
                                        data-apellidos='<%# HttpUtility.HtmlAttributeEncode(Eval("apellidos").ToString()) %>'
                                        data-idvendedor='<%# Eval("idVendedor") %>'>
                                        <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                    </a>

                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <p>
            <hr>
            <div style="padding-left: 10%;" >Productos más vendidos</div><hr>
        <//p>
        <!-- Contenedor para las tarjetas -->
        <div id="cardsContainer" class="cards-container">

            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                        <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                        <h4 class="card-title"></h4>
                        <div class="card-info">
                            <div class="card-details">
                                <a class="cardseller" href='<%# "vista/perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
                                    <%# Eval("NombreVendedor") %>
                                    <%# Eval("apellidos") %><br>
                                </a>
                                <div class="rating">
                                    <span class="heart" data-value="1">&#10084;<%# Eval("ValoracionPromedio") %></span>


                                </div>
                                <div class="cardprice">
                                    <p>$<%# Eval("precio") %></p>
                                </div>
                                <div class="cardButtons">
                                    <a class="buy-button" href='Vista/moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                    <a class="save-button"
                                        data-id='<%# Eval("idProdctoEmpresa") %>'
                                        data-nombre='<%# HttpUtility.HtmlAttributeEncode(Eval("nombreProducto").ToString()) %>'
                                        data-imagen='<%# ResolveUrl(Eval("imagen").ToString()) %>'
                                        data-precio='<%# Eval("precio") %>'
                                        data-vendedor='<%# HttpUtility.HtmlAttributeEncode(Eval("NombreVendedor").ToString()) %>'
                                        data-apellidos='<%# HttpUtility.HtmlAttributeEncode(Eval("apellidos").ToString()) %>'
                                        data-idvendedor='<%# Eval("idVendedor") %>'>
                                        <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                    </a>

                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <hr style="20px">

        <p style="padding-left: 10%;">
            Productos más recientes<hr>
        </p>

        <!-- Contenedor para las tarjetas -->
        <div id="cardsContainer" class="cards-container">

            <asp:Repeater ID="Repeater4" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                        <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                        <h4 class="card-title"></h4>
                        <div class="card-info">
                            <div class="card-details">
                                <a class="cardseller" href='<%# "vista/perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
                                    <%# Eval("NombreVendedor") %>
                                    <%# Eval("apellidos") %><br>
                                </a>
                                <div class="rating">
                                    <span class="heart" data-value="1">&#10084;<%# Eval("ValoracionPromedio") %></span>


                                </div>
                                <div class="cardprice">
                                    <p>$<%# Eval("precio") %></p>
                                </div>
                                <div class="cardButtons">
                                    <a class="buy-button" href='Vista/moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                    <a class="save-button"
                                        data-id='<%# Eval("idProdctoEmpresa") %>'
                                        data-nombre='<%# HttpUtility.HtmlAttributeEncode(Eval("nombreProducto").ToString()) %>'
                                        data-imagen='<%# ResolveUrl(Eval("imagen").ToString()) %>'
                                        data-precio='<%# Eval("precio") %>'
                                        data-vendedor='<%# HttpUtility.HtmlAttributeEncode(Eval("NombreVendedor").ToString()) %>'
                                        data-apellidos='<%# HttpUtility.HtmlAttributeEncode(Eval("apellidos").ToString()) %>'
                                        data-idvendedor='<%# Eval("idVendedor") %>'>
                                        <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                    </a>

                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>



        <!-- Pie de página -->

    </div>
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
    <script src="Vista/js/carrito.js"></script>
    <script src="Vista/js/main.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>

    <script src="https://cdn.botpress.cloud/webchat/v2.2/inject.js"></script>
    <script src="https://files.bpcontent.cloud/2024/12/04/03/20241204035742-QVX7PO4S.js"></script>


</asp:Content>
