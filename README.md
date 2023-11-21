# Pasos para ejecutar la aplicación de ejemplo

## 1. Dependencias e instalaciones

-   Asegúrate de tener Rabbit MQ instalado y en funcionamiento.
-   Asegúrate de tener .NET 8 LTS instalado.
-   Asegúrate de tener acceso a internet.

## 2. Ejecutar las aplicaciones

### 2.1. Ejecutar la aplicación frontend (Cliente)

-   Abre una terminal y navega hasta el directorio "MessageQDemo" usando el comando `cd src/MessageQDemo`.
-   Ejecuta el comando `dotnet run` para iniciar la aplicación.
-   Abre la aplicación en el localhost indicado en la consola.

### 2.2. Ejecutar la aplicación backend (Web API)

-   Abre otra terminal y navega hasta el directorio "EmailSenderApi" usando el comando `cd src/EmailSenderApi`.
-   Ejecuta el comando `dotnet run` para iniciar la aplicación.
-   Abre la aplicación en el localhost indicado en la consola.

## 3. En la interfaz gráfica, introduce los datos necesarios para enviar el correo electrónico.

-   Verifica que el correo electrónico se haya enviado correctamente.

## Diseño de la arquitectura de la aplicación.

![Arquitectura](docs/out/docs/uml/Email%20Service.png)
