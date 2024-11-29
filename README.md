# Sistema de gestion para gimnasio
Proyecto realizado para la aprobacion de la materia Programacion 3, correspondiente a la carrera de Tecnico Universitario en Programacion de la Universidad Tecnologica Nacional Facultad Regional General Pacheco. Profesor: Maximiliano Sar Fernandez.

El princial objetivo del sistema es poder brindar una gestion mas agil y segura sobre usuarios, pagos, clases y cupones de descuento.

---
---
## **Especificación de la Arquitectura**
### **Capa Dominio**
Capa en la que se encuentran las entidades del sistema
​
### **Capa Negocio**
Capa en la cual se encuentran las clases que estabelecen la conexion con la base de datos

### **Capa Apsx**
Capa en la cual se encuentran todas las paginas .aspx

---
---
## **Funcionalidad**
Admin:  
    -un usuario admin puede visualizar todas las clases y crear nuevas. Tambien puede eliminarlas/cancelarlas  
    -un usuario admin puede visualizar todos los pagos concretados, no pagos, mensuales y de clases realizados por todos los usuarios. Posee filtros para buscar mas rapido  
    -un usuario admin puede visualizar los cupones disponibles y crear nuevos. Tambien puede cancelarlos  

Cliente:  
    -un usuario cliente puede visualizar las clases disponibles para inscribirse y tambien las clases a las cuales ya esta inscripto para cancelarlas.  
    -un usuario cliente puede visualizar sus pagos mensuales o de clases ya sean pagos o no pagos. Tambien tiene la posibilidad de pagar los no pagos  
    -un usuario cliente puede ingresar un cupon de descuento a la hora de confrimar un pago  

Admin/Cliente:  
    -un usuario admin o cliente puede consultar los planes y cambiar de plan si asi lo desesa  
    -un usuario admin o cliente puede ingresar a su perfil y modificar sus datos o cambiar su foto de perfil. Tambien puede dar de baja el usuario  
    -un usuario admin o cliente puede restablecer su clave en caso de olvidar su clave actual  
    -un usuario admin o cliente puede visualizar las clases del dia actual y del dia siguiente en la pagina de inicio  



---
---
## **Entidades**
- Usuario
- Pago
- Clase
- InscripcionClase
- Cupon
- Plan
- Seguridad
- Imagen


---
---

## **Paginas ASPX principales**


### **Login**
![0](https://i.imgur.com/fRUiHzT.png)
Pagina en la cual los usuarios o admins pueden loguearse. Ademas del boton ingresar tiene un boton "Recuperar contraseña" el cual te manda a una pagina que te pide el mail de usuario y te envia un mail para restablecer la clave. Por ultimo tiene el boton "Todavia no estoy registrado" el cual te lleva a la pagina para registrarte como usuario.

---
### **Mail recupero de clave**
![1](https://i.imgur.com/ZXESXgR.png)

---
### **Register**
![2](https://i.imgur.com/8Kz0oWq.png)

---
### **Pagina Inicio**
![3](https://i.imgur.com/0POTGzW.png)
En la pagina de inicio se observa una lista de las clases del dia y otras con las del dia siguiente. Tambien hay una cartelera con novedades

---
### **Planes**
![4](https://i.imgur.com/gxXArXa.png)

---
### **Clases Admin**
![5](https://i.imgur.com/q6XgDgb.png)
En esta pagina el Admin puede dar de baja una clase o consultar los inscriptos. El boton "Agregar clase" muestra un formulario para agregar una

---
### **Agregar clase Admin**
![6](https://i.imgur.com/5kAoHym.png)

---
### **Clases cliente**
![7](https://i.imgur.com/ktFhA2L.png)

En esta pagina el cliente visualiza la lista de clases disponibles para inscribirse y la lista de las clases a las cuales se inscribio. Puede inscribirse o cancelar inscripcion. Ademas tiene la opcion de ocultar las listas en caso de que ocupen mucho espacio de la pantalla

---
### **Perfil**
![8](https://i.imgur.com/h68Nc5t.png)
En esta pagina el usuario/admin puede modificar los datos de su perfil o dar de baja el usuarios

---
### **Modificar perfil**
![9](https://i.imgur.com/QXEhKOK.png)

---
### **Pagos admin**
![10](https://i.imgur.com/adGoij0.png)
En esta pagina el admin puede consultar por los pagos mensuales o pagos de las clases. Ademas puede filtrar por apellido de usuario, mes, año, mes y año y nombre de la clase.

---
### **Pagos cliente**
![11](https://i.imgur.com/0DjcEZB.png)
En esta pagina el cliente visualiza los pagos pendientes de pago y los que ya estan pagos correspondientes a las cuotas mensuales o las clases. Ademas puede pagar los que estan impagos clickeando en el boton "Pagar" que te redirige a otra pagina donde se gestiona el pago

---
### **Confirmacion pago cliente**
![12](https://i.imgur.com/SM8zDV0.png)

Ademas de confirmar el pago el cliente puede ingresar un cupon de descuento

---
### **Cupones**
![13](https://i.imgur.com/3sppuZX.png)
En esta pagina el Admin puede visualizar los cupones de descuento disponibles. Tambien tiene un boton que te redirige a un formulario para crear uno

---
### **Agregar cupon**
![14](https://i.imgur.com/nBJLgN9.png)

---
---

## **Autor**​
* Scaglione Franco
---
---
## **Contacto**
franco.scaglione2@gmail.com
