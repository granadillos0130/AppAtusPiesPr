<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloSobreNosotros.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloSobreNosotros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="vista/recursos/ATP.png" />
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
    <div class="container">

        <br />
        <center>
            <h2>¿Quienes Somos?
            </h2>

            <p style="margin-top: 10px;">
                ¡Bienvenidos a nuestra página! Somos un grupo de estudiantes apasionados del programa de Análisis y Desarrollo de Software del SENA, ubicado en Sogamoso, Boyacá, Colombia.

Nuestro equipo está conformado por cinco integrantes, quienes trabajamos juntos para desarrollar proyectos innovadores que combinan creatividad y tecnología. Durante nuestra formación, hemos adquirido conocimientos en programación, bases de datos, diseño de software y metodologías ágiles, habilidades que aplicamos en cada uno de nuestros desarrollos.

Este espacio refleja nuestra dedicación y compromiso con el aprendizaje continuo. Aquí encontrarás proyectos, herramientas y recursos diseñados para mostrar lo que somos capaces de hacer como futuros desarrolladores de software.

Gracias por visitarnos y ser parte de este emocionante viaje. ¡Esperamos que encuentres inspiración en nuestro trabajo!
            </p>

        </center>

        <img src="recursos/logo sena.png" class="img-thumbnail" alt="50%" style="margin-top: 10px;">
    </div>

    <!-- Pie de página -->
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>

</asp:Content>
