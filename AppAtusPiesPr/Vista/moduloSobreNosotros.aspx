<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloSobreNosotros.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloSobreNosotros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="vista/recursos/ATP.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container">
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

<hr>
        <br />

        <!-- Pie de página -->
        <div class="pie-pagina">
            <br />
            <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
            <p>Diseñado con amor para brindar estilo y comodidad.</p>
            <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
            <br />
        </div>
    </div>
</asp:Content>
