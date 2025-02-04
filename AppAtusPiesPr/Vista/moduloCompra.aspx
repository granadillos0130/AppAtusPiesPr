<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloCompra.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloCompra2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
    <link href="css/carrito.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css">

    
    <!-- Incluye SweetAlert2 CSS -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">

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
    <br />

    <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>
    <!-- Imagen del producto -->
    <div class="producto-detalle">
        <!-- Imagen del producto -->
        <div class="producto-imagen">
            <asp:Image alt="Imagen 1" ID="ImgProducto" runat="server" />
        </div>

        <!-- Información del producto -->
        <div class="producto-info">
            <h1>
                <asp:Label ID="lblTituloProducto" runat="server"></asp:Label></h1>
            <ul>
                <li><strong>Vendedor:</strong>
                    <asp:Label ID="nombreVendedor" runat="server"></asp:Label><strong> </strong>
                    <asp:Label ID="apellidoVendedor" runat="server"></asp:Label></li>
                <li><strong>Stock:</strong>
                    <asp:Label ID="stockProducto" runat="server"></asp:Label></li>
                <li><strong>Num. Referencia:</strong>
                    <asp:Label ID="referenciaProducto" runat="server"></asp:Label></li>
                <li><strong>Descuento:</strong>
                    <asp:Label ID="descuento" runat="server"></asp:Label></li>
                <li><strong>Descripcion Producto:</strong>
                    <asp:Label ID="productoDescripcion" runat="server"></asp:Label></li>
                <li><strong>Tallas Disponibles: </strong>
                    <asp:DropDownList ID="ddlTallas" runat="server" CssClass="dropdown"></asp:DropDownList></li>
                <li><strong>Marca:</strong>
                    <asp:Label ID="marcaProducto" runat="server"></asp:Label></li>
                <li><strong>Precio:</strong>
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
                    <label for="comentarioInput" class="form-label">¿Qué opinas sobre este producto? Escribe tu comentario</label>
                    <textarea class="form-control" id="comentarioInput" rows="3" placeholder="Deja tu opinión aquí..."></textarea>
                </div>
                <button type="submit" class="btn btn-dark">Enviar comentario</button>


                <!-- Lista de reseñas (solo 3 visibles) -->
                <div class="comentarios-list mt-3">
                    <!-- Reseña 1 -->
                    <div class="comentario">
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#reseña1" aria-expanded="false" aria-controls="reseña1">
                            Juan Pérez
                        </button>
                        <div id="reseña1" class="collapse">
                            <p>Excelente producto, muy recomendado.</p>
                        </div>
                    </div>

                    <!-- Reseña 2 -->
                    <div class="comentario">
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#reseña2" aria-expanded="false" aria-controls="reseña2">
                            Ana García
                        </button>
                        <div id="reseña2" class="collapse">
                            <p>El precio es muy competitivo, ¡me encanta!</p>
                        </div>
                    </div>

                    <!-- Reseña 3 -->
                    <div class="comentario">
                        <button class="btn btn-link" type="button" data-bs-toggle="collapse" data-bs-target="#reseña3" aria-expanded="false" aria-controls="reseña3">
                            Laura Martínez
                        </button>
                        <div id="reseña3" class="collapse">
                            <p>Muy buena calidad, definitivamente volveré a comprar.</p>
                        </div>
                    </div>
                </div>

                <!-- Botón "Ver más" para abrir el modal -->
                <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modalReseñas">Ver más reseñas</button>
            </div>
        </div>
    </div>

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
                        <!-- Reseña 1 -->
                        <div class="comentario">
                            <strong>Juan Pérez</strong>
                            <p>Excelente producto, muy recomendado.</p>
                        </div>

                        <!-- Reseña 2 -->
                        <div class="comentario">
                            <strong>Ana García</strong>
                            <p>El precio es muy competitivo, ¡me encanta!</p>
                        </div>

                        <!-- Reseña 3 -->
                        <div class="comentario">
                            <strong>Laura Martínez</strong>
                            <p>Muy buena calidad, definitivamente volveré a comprar.</p>
                        </div>

                        <!-- Agrega más reseñas aquí -->
                        <div class="comentario">
                            <strong>Carlos Ruiz</strong>
                            <p>Muy satisfecho con la compra, todo perfecto.</p>
                        </div>
                        <div class="comentario">
                            <strong>Patricia Gómez</strong>
                            <p>El producto llegó a tiempo y es tal como lo esperaba.</p>
                        </div>
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
        <p>Más Productos de: 
        <strong><asp:Label ID="nombreV" runat="server"></asp:Label><strong> </strong>
        <asp:Label ID="apellidoV" runat="server"></asp:Label></strong></p>
    </center>
    <br />

    <div id="cardsContainer" class="cards-container">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="card">
                    <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                    <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                    <h4 class="card-title"></h4>
                    <div class="card-info">
                        <div class="card-details">
                            <a class="cardseller" href='<%# "perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
                                <%# Eval("nombres") %>
                                <%# Eval("apellidos") %><br>
                            </a>
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
