const botonTema = document.getElementById("cambiarTema");

botonTema.addEventListener("click", () => {
  const html = document.documentElement;
  const temaActual = html.getAttribute("data-tema") || "claro";
  const nuevoTema = temaActual === "claro" ? "oscuro" : "claro";

  html.setAttribute("data-tema", nuevoTema);
  localStorage.setItem("tema", nuevoTema); // Guarda la preferencia
});

document.addEventListener("DOMContentLoaded", () => {
    const temaGuardado = localStorage.getItem("tema") || "claro";
    document.documentElement.setAttribute("data-tema", temaGuardado);
});
