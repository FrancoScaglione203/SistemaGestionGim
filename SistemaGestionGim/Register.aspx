<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SistemaGestionGim.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center mt-4">
            <div class="text-center">
                <h6 class="fw-bold my-3">Ingresá tus datos</h6>

                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" type="email" />
                    <label for="txtEmail" class="form-label">
                        Email
                       
                        <span class="d-flex">
                            <asp:RequiredFieldValidator CssClass="d-block" ID="vEmail" runat="server"
                                ControlToValidate="txtEmail"
                                ErrorMessage="Debe ingresar un email"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="string" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Debe tener el formato de email"
                                ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </span>
                    </label>
                </div>
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" type="text" />
                    <label for="txtApellido" class="form-label">
                        Apellido
                       
                        <asp:RequiredFieldValidator CssClass="d-block" ID="vApellido" runat="server"
                            ControlToValidate="txtApellido"
                            ErrorMessage="Debe ingresar un apellido"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </label>
                </div>
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" type="text" />
                    <label for="txtNombre" class="form-label">
                        Nombre
                       
                        <asp:RequiredFieldValidator CssClass="d-block" ID="vNombre" runat="server"
                            ControlToValidate="txtNombre"
                            ErrorMessage="Debe ingresar un nombre"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </label>
                </div>

                <div class="form-floating mb-3">
                </div>
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtClave" type="password" />
                    <label for="txtClave" class="form-label">
                        Contraseña
                       
                        <asp:RequiredFieldValidator CssClass="d-block" ID="vClave" runat="server"
                            ControlToValidate="txtClave"
                            ErrorMessage="Debe ingresar una contraseña"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </label>
                </div>

                <!-- TextBox para confirmar la contraseña -->
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtClave2" type="password" />
                    <label for="txtClave2" class="form-label">
                        Confirmar Contraseña
       
                        <asp:RequiredFieldValidator CssClass="d-block" ID="vClave2" runat="server"
                            ControlToValidate="txtClave2"
                            ErrorMessage="Debe confirmar la contraseña"
                            ForeColor="Red">
        </asp:RequiredFieldValidator>
                    </label>
                </div>

                <!-- TextBox para mostrar el plan seleccionado -->
                <div class="form-floating mb-3">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPlanSeleccionado" ReadOnly="true" />
                    <label for="txtPlanSeleccionado" class="form-label">
                        Plan Seleccionado
                        
                        <asp:RequiredFieldValidator CssClass="d-block" ID="vPlan" runat="server"
                            ControlToValidate="txtPlanSeleccionado"
                            ErrorMessage="Debe seleccionar un plan"
                            ForeColor="Red">
                         </asp:RequiredFieldValidator>
                    </label>
                </div>

                <!-- Campo oculto para almacenar el ID del plan seleccionado -->
                <asp:HiddenField runat="server" ID="hiddenFieldPlanId" />

                <div class="form-floating mb-3">
                    <div class="row">
                        <!-- Contenedor de filas para las tarjetas -->
                        <asp:Repeater ID="outerRepeater" runat="server">
                            <ItemTemplate>
                                <div class="col-md-4">
                                    <!-- Cada tarjeta ocupará 4 columnas -->
                                    <div class="card my-4">
                                        <img class="card-img-top img-concesionaria" src=" " alt="<%# Eval("Descripcion") %>">
                                        <div class="card-body">
                                            <h5 class="card-title"><%# Eval("Descripcion") %></h5>
                                            <p class="card-text">
                                                Importe: $<%# Eval("Importe") %><br />
                                                Máquinas: <%# (bool)Eval("Maquinas") ? "Incluye" : "No Incluye" %><br />
                                                Seguimiento: <%# (bool)Eval("Seguimiento") ? "Incluye" : "No Incluye" %><br />
                                                Locker: <%# (bool)Eval("Locker") ? "Incluye" : "No Incluye" %><br />
                                                Descuento en Clases: <%# Eval("DescuentoClases") %>%
                                           
                                            </p>
                                            <div class="row my-1">
                                                <div class="col-12 d-flex justify-content-center">
                                                    <asp:Button runat="server" ID="btnSeleccionar" Text='<%# Eval("Descripcion") %>' CssClass="btn btn-success w-50"
                                                        CommandArgument='<%# Eval("Id") %>' OnClick="btnSeleccionar_Click" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- Fin del contenedor de filas -->
                </div>

                <asp:Button Text="Registrarme" CssClass="btn btn-warning w-50 p-3 mt-4 mb-2" runat="server" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" />

                <div>
                    <% if ((Session["validacionRegister"] != null))
                        { %>
                    <label for="txtClave2" class="text-danger form-label"><%= Session["validacionRegister"] %></label>
                    <% } %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
