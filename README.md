# Biblioteca Digital Secretos Para Contar.

## Proyecto FrontEnd  [`Katio_Verso`](https://github.com/SouthDaniel121/katio_verso)


## Informacion 

Un proyecto basado en una bibloteca virtual dedicado para los usuarios en zonas rurales de antioquia principalmente con los libros y colecciones de Fundacion secretos para contar.

## Versiones

- Version 1 en Java → [`Katio_Jardin`](https://github.com/SouthDaniel121/katio-Jardin)

- Version 1.5 en C# (Actual)


## Equipo

`Alejandro Buitrago `
`Santiago Muñoz `
`J Daniel Orrego `


## Start-up

Primero, ejecute el servidor de desarrollo (CMD):

```bash
 dotnet watch --project Katio_net.API
```
 


### Libros

- [x] Crear un libro.
- [x] Editar un libro.
- [x] Buscar por id
- [x] Buscar libro por nombre, de forma relativa.
- [x] Buscar libro por autor, de forma relativa, por nombre y apellido del autor.
- [x] Buscar libro por editorial.
- [x] Buscar libro por genero.
- [x] Buscar libro por fecha de publicación.
- [ ] Subir un libro en PDF a la biblioteca.
- [ ] Servir un libro en PDF al cliente.
- [ ] Agregar varios géneros a un libro.
- [ ] Los libros pueden tener varios autores.
- [ ] Agregar temas al libro.
- [ ] un libro puede tener varios temas.
- [x] No pueden haber dos versiones del mismo libro.
- [ ] Agregar libros relacionados a un libro principal

### Autores

- [x] Crear un Autor
- [x] Buscar por id
- [x] Editar un Autor
- [x] Buscar un autor por nombre y apellido de forma relativa.
- [x] Buscar un autor por fecha de nacimiento
- [x] Buscar un autor por país de procedencia

### Usuarios

- [ ] Crear un usuario, utilizar el registro.
- [ ] Login del usuario: debe regresar un token bearer. Al hacer login, debo poder usar el token para cuaquier otra acción.
- [ ] Todas mis acciones deben quedar bajo llave, con la sola excepción de: Login, Signup.
- [ ] Editar un usuario.
- [ ] Debo poder reiniciar mi clave, solo mi clave. Debo colocar la clave anterior, y dos veces la clave nueva.
- [ ] Listar todos mis usuarios.
- [ ] Listar todos mis usuarios por email, o identificación.
- [ ] Agregar un username. No todos los usuarios tienen un correo hábil. Ambos campos son distintos, pero puede repetir la información.
- [ ] Manejar los perfiles (Roles) del usuario.


### Audiolibros

- [x] Crear un audiolibro.
- [x] Editar un audiolibro.
- [x] Buscar por id
- [x] Buscar Audiolibro por nombre, de forma relativa.
- [x] Buscar Audiolibro por autor, de forma relativa, por nombre y apellido del autor.
- [x] Buscar Audiolibro por editorial.
- [x] Buscar Audiolibro por genero.
- [x] Buscar Audiolibro por fecha de publicación.
- [ ] Subir un audiolibro en MP3/OGG a la biblioteca.
- [ ] Servir un audiolibro en MP3/OGG al cliente.
- [ ] Buscar un audiolibro por narrador.
- [x] Buscar un audiolibro por longitud.
- [ ] Agregar varios géneros a un libro.
- [ ] Los libros pueden tener varios autores.
- [ ] Agregar temas al libro.
- [ ] un libro puede tener varios temas.
- [ ] No pueden haber dos versiones del mismo libro.



### Narradores

- [x] Crear un narrador
- [x] Editar un narrador
- [x] Buscar por id
- [x] Buscar narrador por nombre.
- [ ] Buscar narrador por perfil de voz.
- [ ] Buscar todos los audiolibros de un narradores por relación.



### Admin / Estadísticas

- [ ] Ver mis usuarios, editarlos y desactivarlos.
- [ ] Asignar una clave de forma directa a un usuario a través de la edición
- [ ] El username y el email no son mutables.
- [ ] Agregar estadísticas al sitio.

