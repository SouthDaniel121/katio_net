## Documentación técnica katio
### Introducción 
Una aplicación de biblioteca, desde una perspectiva técnica, es una compleja interacción de diversos componentes tecnológicos que trabajan en conjunto para ofrecer una interfaz intuitiva y un acceso eficiente a los recursos de una biblioteca.

#### FrontEnd 

- React REMIX.

#### BackEnd

- Csharp.
- PostgreSQL.


## Esto es lo que debes tener en tu ambiente

### Aplicaciones y extensiones

- [`.NET`](https://dotnet.microsoft.com/en-us/download)  
- [`Visual Studio Code`](https://code.visualstudio.com/)
- [`NET Install Tool`](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
- [`C# Dev Kit`](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) 
- La base de datos utilizada, con la ultima versión del momento [`PostgreSQL`](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) 17.2v

## Como deberas clonar

Nota: Debes tener tu [`GIT`](https://git-scm.com/) configurado.

Primero, ejecute el servidor de desarrollo (CMD o PowerShell):

```bash
  git clone https://github.com/SouthDaniel121/katio_net
```
 
## Base de datos configuración

Nota: Instala [`PostgreSQL`](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) en su ultima version.

Ubicate en la carpeta con:
```bash
  cd katio_net
```
## Conexión a la base de datos

Nota: Los datos que debes ingresar son los que utilizaste al crear la base de datos.

- Edita el archivo appsettings.json para configurar la conexión a la base de datos:

```bash
 {   "ConnectionStrings": {     "DefaultConnection": "Server=localhost;Database=DatabaseName;User Id=username;Password=password;"   } }
```

## Migracion de base de datos, Esto lo que hara es crear las tablas que ya estan por defecto.
Ejecuta el siguiente comando para restaurar las dependencias del proyecto:
```bash
    dotnet restore
```

### Aplicar migraciones de la base de datos

Necesitas añadir las migraciones, ejecute el servidor de desarrollo (CMD o PowerShell):
```bash
     cd katio_net.API
```
Despues ejecutar este codigo, que daras con esta ruta

```bash
     katio_csharp\katio_net\Katio_net.API>
```
Ahi deberas escribir este comando, esto lo que hace es crear un registro de migracion.

```bash
     dotnet ef migrations add InitialCreate --project ../Katio_net.Data
```

Siguiente comando:
```bash
     database update
```
   

Para crear las tablas deberas estar ubicado:
```bash
     katio_csharp\katio_net\Katio_net.API>
```

Al estar aqui deberas quitar el comentario que esta en el program.cs llamado "//PopulateDB(app); → PopulateDB(app);".

despues de este momento deberas copiar este comando:
```bash
     dotnet run
```
Ya una vez compilado este comando, debes comentar el "PopulateDB(app); → //PopulateDB(app);" Esto es para que no duplique la información cada vez que el arranque.

## Iniciar o Ejecución del proyecto

Este comando te permitira iniciar la aplicación.
```bash
    dotnet watch --project katio_net.API
```
Sino te funciona pon la siguiente ruta.
```bash
    cd Katio_NET.api
```
Y luego el comando
```bash
    dotnet wacth
```

## ¿Que tiene el BackEnd?

Nuestro backEnd tiene los metodos CRUD → (Create,Read,Update y Delete).

Nuestras rutas son 

http://localhost:5125/api/(Controlador)/(Metodo)




## FrontEnd 

Aqui esta nuestro repositorio de la pagina donde se hace la conexion y la magia.

[`Katio Verso`](https://github.com/SouthDaniel121/katio_verso)




