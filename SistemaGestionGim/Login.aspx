<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SistemaGestionGim.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <% if (Session["usuario"] == null)
      {
  %>
  <div class="row justify-content-center mt-4">

      <div class="col-5">

          <img class="my-3" src="https://myrenault.com.ar/vendor/template/assets/img/renault_black.svg" alt="Renault" style="height: 9em;" />

          <h6 class="fw-bold my-3 text-center">Bienvenido a MY RENAULT</h6>
          <h6 class="fw-bold my-3 text-center">Donde quiera que vayas, disfrutá los beneficios</h6>

          <div class="form-floating my-4">
              <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" type="email" />
              <label for="txtEmail" class="form-label">
                  Email
                  <span class="d-flex">
                      <asp:RequiredFieldValidator CssClass="d-block" ID="vEmail" runat="server"
                          ControlToValidate="txtEmail"
                          ErrorMessage="Debe ingresar un Email"
                          ForeColor="Red">
                      </asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="reEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Debe ingresar una direccion de email"
                          ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$" ForeColor="Red"> 
                      </asp:RegularExpressionValidator>
                  </span>
              </label>
          </div>
          <div class="form-floating my-3">
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
          <div class="d-flex justify-content-between">
              <div class="form-group form-check">
                  <input id="Checkbox1" type="checkbox" cssclass="form-check-input" />
                  <%--       <input type="checkbox" class="form-check-input" id="exampleCheck1">--%>
                  <label class="form-check-label" for="exampleCheck1">Recordar sesión</label>
              </div>
              <div>
                  <a class="nav-item nav-link" href="recuperoClave.aspx">Recuperar contraseña</a>
              </div>
          </div>
          <%--<asp:Label ID="lblRecuperoClave" runat="server" CssClass="btn btn-light border-0 w-50 p-3" Text="Olvidé mi contraseña" />--%>
          <div class="text-center my-2">

              <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
                      <div>
                          <%if (!(Session["validacionLogin"] == null))
                              {%>
                          <label for="txtClave2" class="text-danger form-label">Email o contraseña incorrectos</label>
                          <%}%>
                      </div>
                      <asp:Button Text="Ingresar" CssClass="btn btn-warning w-50 p-3 mt-4 mb-2" runat="server" ID="btnIngresar" OnClick="btnIngresar_Click" />
                  </ContentTemplate>
              </asp:UpdatePanel>

              <a class="nav-item nav-link" href="signin.aspx">
                  <asp:Label ID="lblRegistro" runat="server" CssClass="btn btn-light w-50 p-3" Text="Todavía no estoy registrado" />
              </a>
          </div>
      </div>
  </div>
  <%}
      else
      {
  %>

  <div class="row justify-content-center mt-4">

      <div class="col-5 text-center">
          <h6 class="fw-bold my-3">PERFIL</h6>

          <div class="row justify-content-center">

             <div class="form-floating mb-3">
                  <input readonly class="form-control-plaintext" id="fltEmail" value="<%= Email() %>">
                  <label for="fltEmail">Email</label>
              </div>
              <asp:Button Text="Modificar Perfil" CssClass="btn btn-warning w-50 p-3 mt-4 mb-2" runat="server" ID="btnModificar" OnClick="btnModificar_Click" />

          </div>
      </div>
  </div>

  <%}%>


</asp:Content>
