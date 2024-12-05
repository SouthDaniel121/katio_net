## Documentación técnica katio

## Equipo

`Alejandro Buitrago `
`J Daniel Orrego `

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

### Todos los controladores de las tablas existentes tienen.

- Crear.
- Actualizar.
- Borrar.
- Busqueda, Esto puede ser por su id, nombre, Isbn, por fecha de publicación, Autor, por duracion e identificación.

#### Estas tablas son:
`Autores` `Audiolibro` `Libros` `Usuarios` `Narradores`


## Version FrontEnd

[`Katio Verso`](https://github.com/SouthDaniel121/katio_verso)  `(Actual)`

## Version Backend

- Version 1 en Java → [`Katio Jardin`](https://github.com/SouthDaniel121/katio-Jardin)

- Version 1.5 en Csharp → `(Actual)`

## Errores frecuentes

- Tener la base de datos apagada en servicios de windows.

- Conexión del appsettings.json no esta vinculada o diferente (Nombre o contraseña).

- Estar en un puerto diferente o cambiarlo sin darse cuenta.

- No tener las extensiones o herramientas necesarias como por ejemplo; La base de datos, .Net o/u no se ha hecho la migración correctamente.








# Documentation in English

## Technical documentation katio

## Team

`Alejandro Buitrago `
`J Daniel Orrego `

### Introduction
A library application, from a technical perspective, is a complex interaction of various technological components that work together to offer an intuitive interface and efficient access to a library's resources.

#### FrontEnd

- React REMIX.

#### BackEnd

- Csharp.
- PostgreSQL.

## This is what you should have in your environment

### Applications and extensions

- [`.NET`](https://dotnet.microsoft.com/en-us/download)
- [`Visual Studio Code`](https://code.visualstudio.com/)
- [`NET Install Tool`](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
- [`C# Dev Kit`](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- The database used, with the latest version at the moment [`PostgreSQL`](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) 17.2v

## How you should clone

Note: You must have your [`GIT`](https://git-scm.com/) configured.

First, run the development server (CMD or PowerShell):

```bash
git clone https://github.com/SouthDaniel121/katio_net
```

## Database configuration

Note: Install [`PostgreSQL`](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) in its latest version.

Go to the folder with:
```bash
cd katio_net
```
## Database connection

Note: The data you must enter is the one you used when creating the database.

- Edit the appsettings.json file to configure the database connection:

```bash
{ "ConnectionStrings": { "DefaultConnection": "Server=localhost;Database=DatabaseName;User Id=username;Password=password;" } }
```

## Database migration, This will create the tables that are already there by default.
Run the following command to restore the project dependencies:
```bash
dotnet restore
```

### Apply database migrations

You need to add the migrations, run the development server (CMD or PowerShell):
```bash
cd katio_net.API
```
After running this code, you will get this path

```bash
katio_csharp\katio_net\Katio_net.API>
```
There you should write this command, this is what it does is create a migration record.

```bash
dotnet ef migrations add InitialCreate --project ../Katio_net.Data
```

Next command:
```bash
database update
```

To create the tables you must be located:
```bash
katio_csharp\katio_net\Katio_net.API>
```

Once you are here you must remove the comment that is in the program.cs called "//PopulateDB(app); → PopulateDB(app);".

after this moment you must copy this command:
```bash
dotnet run
```
Once this command is compiled, you must comment out the "PopulateDB(app); → //PopulateDB(app);" This is so that it does not duplicate the information every time it starts.

## Starting or Running the project

This command will allow you to start the application.
```bash
dotnet watch --project katio_net.API
```
If it doesn't work, use the following route.
```bash
cd Katio_NET.api
```
And then the command
```bash
dotnet wacth
```

## What does the BackEnd have?

Our backEnd has the CRUD methods → (Create, Read, Update and Delete).

Our routes are

http://localhost:5125/api/(Controller)/(Method)

### All the controllers of the existing tables have:

- Create.
- Update.
- Delete.
- Search, This can be by ID, name, ISBN, by publication date, Author, by duration and identification.

#### These tables are:
`Authors` `Audiobook` `Books` `Users` `Narrators`


## Common errors

- Having the database turned off in Windows services.

- Connection of appsettings.json is not linked or different (Name or password).

- Being on a different port or changing it without realizing it.

- Not having the necessary extensions or tools such as; The database, .Net or/and the migration has not been done correctly.

## FrontEnd Version

[`Katio Verso`](https://github.com/SouthDaniel121/katio_verso) `(Current)`

## Backend Version

- Version 1 in Java → [`Katio Jardin`](https://github.com/SouthDaniel121/katio-Jardin)

- Version 1.5 in Csharp → `(Current)`