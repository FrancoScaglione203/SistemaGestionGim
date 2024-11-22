<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="SistemaGestionGim.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center mt-4">
            <!-- Columna izquierda: Datos del perfil -->
            <div class="col-md-8">
                <div class="text-center">
                    <h6 class="fw-bold my-3">Mi Perfil</h6>

                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" ReadOnly="true" />
                        <label for="txtEmail" class="form-label">Email</label>
                    </div>

                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" ReadOnly="true" />
                        <label for="txtApellido" class="form-label">Apellido</label>
                    </div>

                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" ReadOnly="true" />
                        <label for="txtNombre" class="form-label">Nombre</label>
                    </div>

                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPassword" ReadOnly="true" />
                        <label for="txtPassword" class="form-label">Contraseña</label>
                    </div>

                    <div class="form-floating mb-3">
                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtConfirmarPassword" ReadOnly="true" Style="display: none;" />
                        <label for="txtConfirmarPassword" class="form-label">Confirmar Contraseña</label>
                    </div>

                    <!-- Tarjeta para mostrar el plan seleccionado -->
                    <div class="card my-4" style="width: 100%;">
                        <div class="card-body">
                            <h5 class="card-title">Plan Seleccionado</h5>
                            <p class="card-text">
                                <asp:Label runat="server" ID="lblDescripcionPlan" />
                                <br />
                                <asp:Label runat="server" ID="lblImportePlan" />
                            </p>
                            <asp:Button Text="Cambiar Plan" CssClass="btn btn-warning" runat="server" ID="btnCambiarPlan" OnClick="btnCambiarPlan_Click" />
                        </div>
                    </div>

                    <asp:Button Text="Modificar Perfil" CssClass="btn btn-primary w-50 p-3 mt-4 mb-2" runat="server" ID="btnModificarPerfil" OnClick="btnModificarPerfil_Click" />
                    <asp:Button Text="Cancelar Suscripción/Usuario" CssClass="btn btn-danger w-50 p-3 mt-2" runat="server" ID="btnCancelarUsuario" OnClick="btnCancelarUsuario_Click" />
                    <asp:Button Text="Confirmar baja" CssClass="btn btn-secondary w-50 p-3 mt-2" runat="server" ID="btnConfirmarCancelacion" OnClick="btnConfirmarCancelacion_Click" Style="display: none;" />
                    <asp:Button Text="Cancelar" CssClass="btn btn-secondary w-50 p-3 mt-2" runat="server" ID="btnCancelar2" OnClick="btnCancelar2_Click" Style="display: none;" />

                    <!-- Botones ocultos de Cancelar y Confirmar cambios -->
                    <asp:Button Text="Cancelar" CssClass="btn btn-secondary w-50 p-3 mt-2" runat="server" ID="btnCancelar" OnClick="btnCancelar_Click" Style="display: none;" />
                    <asp:Button Text="Confirmar Cambios" CssClass="btn btn-success w-50 p-3 mt-2" runat="server" ID="btnConfirmarCambios" OnClick="btnConfirmarCambios_Click" Style="display: none;" />
                    <div>
                        <% if ((Session["validacionModificacion"] != null))
                            { %>
                        <label for="txtClave2" class="text-danger form-label"><%= Session["validacionModificacion"] %></label>
                        <% } %>
                    </div>
                </div>
            </div>

            <!-- Columna derecha: Imagen de perfil -->




            <div class="col-md-4 text-center">
                <h6 class="fw-bold my-3">Imagen de Perfil</h6>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:Label class="form-label" runat="server" ID="lblTxtImagen" Visible="false">Seleccionar Imagen</asp:Label>
                            <input type="file" id="txtImagen" runat="server" class="form-control" style="display: none" />
                        </div>

                        <asp:Image ID="imgPerfil" Style="height: 150px; width: 150px; border-radius: 50%;" 
                            ImageUrl="https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png" runat="server" class="my-3" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>
