# User Microservice

Microservicio encargado de la autenticación y emisión de tokens JWT.  
Provee endpoints para login, registro y administración de usuarios.

---

## Requisitos previos
- .NET 8 SDK
- SQL Server

## Configuración
ajusatr las variables de entornos en el archivo .env, en este archivo esta la cadena de conexión a la db y otros datos, (se subío en archivo .env al repo)
validar el archivo Program.cs de la api, y ajustar los cors, actualmente recibe peticiones de http://localhost:4200
swagger configurado para el consumo de los end points
