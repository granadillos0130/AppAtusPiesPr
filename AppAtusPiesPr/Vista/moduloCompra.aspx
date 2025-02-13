<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloCompra.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloCompra2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link href="css/carrito.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/jquery-zoom@1.7.21/jquery.zoom.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <!-- Incluye SweetAlert2 CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <!-- Incluye SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">

    <!-- Filtros -->
    <center>
        <div class="navbarFiltros">
            <nav>

                <ul class="menuFiltros">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
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
                                <a href='<%# "moduloMarcaFiltrada.aspx?id=" + Eval("idMarca") %>'>
                                    <%# Eval("nombreMarca") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>
    </center>


    <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>
    <!-- Imagen del producto -->
    <div class="producto-detalle">
        <!-- Imagen del producto -->
        <div class="producto-imagen">
            <asp:Image alt="Imagen del producto" ID="ImgProducto" runat="server" CssClass="imagen-zoom" />
            <div class="overlay">
                <button id="cerrar-zoom">
                    <i class="fas fa-times"></i>
                </button>
            </div>
        </div>

        <!-- Información del producto -->
        <div class="producto-info">
            <h1>
                <asp:Label ID="lblTituloProducto" runat="server"></asp:Label></h1>
            <ul>
                <li><strong>Vendedor</strong>
                    <asp:Label ID="nombreVendedor" runat="server"></asp:Label><strong> </strong>
                    <asp:Label ID="apellidoVendedor" runat="server"></asp:Label></li>
                <li>

                    <div class="rating">
                        <span class="heart" data-value="1">&#10084;</span>
                        <asp:Label ID="lblPromedio" ClientIDMode="Static" runat="server" EnableViewState="false"></asp:Label>
                    </div>
                </li>

                <li><strong>Unidades Disponibles</strong>
                    <asp:Label ID="stockProducto" runat="server"></asp:Label></li>
                <li><strong>Num. Referencia</strong>
                    <asp:Label ID="referenciaProducto" runat="server"></asp:Label></li>
                <li><strong>Descuento</strong>
                    <asp:Label ID="descuento" runat="server"></asp:Label></li>
                <li><strong>Descripcion Producto</strong>
                    <asp:Label ID="productoDescripcion" runat="server"></asp:Label></li>
                <li><strong>Tallas Disponibles </strong>
                    <asp:DropDownList ID="ddlTallas" runat="server" CssClass="dropdown"></asp:DropDownList></li>
                <li><strong>Marca</strong>
                    <asp:Label ID="marcaProducto" runat="server"></asp:Label></li>
                <li><strong>Precio</strong>
                    <asp:Label ID="PrecioProducto" runat="server"></asp:Label></li>
            </ul>
            <p class="devoluciones">Devoluciones y envíos gratuitos</p>

            <!-- Botón para agregar al carrito -->
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
        <div class="col-md-4">
            <!-- Sección de comentarios -->
            <div class="comentarios mt-4">
                <h3>Comentarios</h3>
                <div class="mb-3">
                    <asp:Label ID="lblComentario" for="comentarioInput" class="form-label" runat="server" Text="¿Qué opinas sobre este producto? Escribe tu comentario"></asp:Label>
                    <asp:TextBox ID="txtComentario" class="form-control" Style="resize: none;" Rows="3" TextMode="MultiLine" runat="server" placeholder="Deja tu opinión aquí..."></asp:TextBox>


                    <div class="rating-container" style="margin-top: 10px;">
                        <div class="hearts-rating">
                            <asp:HiddenField ID="hdnValoracion" runat="server" Value="0" />
                            <i class="far fa-heart heart-icon" data-value="1"></i>
                            <i class="far fa-heart heart-icon" data-value="2"></i>
                            <i class="far fa-heart heart-icon" data-value="3"></i>
                            <i class="far fa-heart heart-icon" data-value="4"></i>
                            <i class="far fa-heart heart-icon" data-value="5"></i>
                        </div>
                    </div>

                </div>
                <asp:Button ID="enviarComentario" runat="server" class="btn btn-dark" Text="Enviar comentario" OnClick="enviarComentario_Click" />

            </div>

            <!-- Botón "Ver más" para abrir el modal -->
            <button type="button" style="margin-top: 10px;" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalReseñas">Ver reseñas</button>
        </div>
    </div>

    <style>
        /* Animación de despliegue de la modal desde el botón */
        .modal.fade .modal-dialog {
            transform: scale(0);
            opacity: 0;
            transition: transform 0.1s ease-out, opacity 0.1s ease-out; /* Aceleramos la apertura */
        }

        .modal.fade.show .modal-dialog {
            transform: scale(1);
            opacity: 1;
        }

        /* Animación de cierre de la modal */
        .modal.fade .modal-dialog {
            opacity: 0;
            transform: scale(0);
            transition: transform 0.3s ease-in, opacity 0.3s ease-in; /* Aceleramos el cierre */
        }
    </style>




    <!-- Modal para ver todas las reseñas -->
    <div class="modal fade" id="modalReseñas" tabindex="-1" aria-labelledby="modalReseñasLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalReseñasLabel">Todas las Reseñas</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Todas las reseñas -->
                    <div class="comentarios-list">

                        <asp:Repeater ID="Repeater3" runat="server">
                            <ItemTemplate>
                                <div class="comentario">
                                    <p>
                                        <strong><%# Eval("nombres") %>
                                            <%# Eval("apellidos") %></strong> <%# Eval ("fechaComentario")%>
                                        <span class="heart" data-value="1">&#10084;</span>
                                        <%#Eval("valoracion") %>
                                    </p>
                                    <p><%#Eval("comentario") %></p>

                                    </p>
                                    <hr>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>

    </div>
    <hr>
    <br>
    <center>
        <p>
            Más Productos de: 
        <strong>
            <asp:Label ID="nombreV" runat="server"></asp:Label><strong> </strong>
            <asp:Label ID="apellidoV" runat="server"></asp:Label></strong>
        </p>
    </center>
    <br />

    <div id="cardsContainer" class="cards-container">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="card">
                    <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                    <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                    <div class="card-info">
                        <div class="card-details">
                            <a class="cardseller" href='<%# "perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
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
                                <a class="buy-button" href='moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
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
    <!-- PIE DE PAGINA -->
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>
    <script src="js/carrito.js"></script>
    <script src="js/main.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
</asp:Content>
