ShoppingCartSANA es una aplicación de comercio electrónico simple que permite a los usuarios agregar productos al carrito y procesar órdenes. 
El sistema está desarrollado con React en el frontend y ASP.NET Core con Entity Framework en el backend.

# 🛠 ShoppingCartSANA - Backend

## 📌 Descripción
Este es el backend de **ShoppingCartSANA**, una API desarrollada con **ASP.NET Core y Entity Framework** para gestionar órdenes de compra. Se conecta a una base de datos **SQL Server** y expone endpoints para procesar pedidos.

---

## 📦 Tecnologías Utilizadas
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **C#**
- **JWT (Opcional, si usas autenticación)**

---

## 🚀 Instalación y Configuración

1️⃣ Clonar el repositorio
git clone https://github.com/fernandagasca/cartSANA.git
cd ShoppingCartSANA/Backend

2️⃣ Configurar la base de datos
Dentro de la carpeta Scripts/ encontrarás el archivo schema.sql.
Ejecuta el script en SQL Server para crear las tablas necesarias.

3️⃣ Configurar la cadena de conexión
Abre appsettings.json y edita la conexión a la base de datos:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ShoppingCartSANA;User Id=tuusuario;Password=tupassword;"
}




