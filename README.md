# Sophos Reto #3

## REQUERIMIENTOS TECNICOS

Para el desarrollo exitoso del reto debe contar mínimo con un computador que tenga la siguiente
configuración:

- LENGUAGE_BACKEND: C#
- FRAMEWORK_BACKEND: .NET CORE 7.0
- IDE: Visual Studio Code
- BASE_DE_DATOS: PostgreSQL
- GIT: 2.34.1
- DOCKER: 24.0.6

## Enunciado

Usted ha sido contratado por la universidad Sophos con el fin de desarrollar una aplicación web que permita gestionar la información de su institución, especialmente los cursos ofrecidos, estudiantes y maestros, el sistema debe permitir:

- listar los cursos ofrecidos mostrando su nombre, nombre del curso prerrequisito, número de créditos y cupos disponibles.
- listar los alumnos matriculados mostrando nombre, facultad a la que pertenece, cantidad de créditos inscritos.
- listar los profesores, mostrando nombre, máximo titulo académico, años de experiencia en docencia y nombre del curso o de los cursos que dicta.
- Agregar nuevos cursos, alumnos y/o profesores.
- Actualizar información de cursos, alumnos y/o profesores.
- Eliminar cursos, alumnos y/o profesores.
- Buscar curso, alumno y/o docente por nombre.
- Buscar curso por estado de cupos (si tiene o no cupos disponibles)
- Buscar alumnos por facultad a la que pertenece.
- Seleccionar un curso y mostrar la información de este (nombre, número de estudiantes inscritos, profesor que dicta el curso, cantidad de créditos, entre otro) además un listado de los alumnos que están cursando dicha asignatura.
- Seleccionar un Alumno y mostrar la información de este (nombre, número de créditos
  inscritos, semestre que cursa, entre otros datos que considere relevantes) además un
  listado de los cursos que tiene matriculados y un listado de las asignaturas que ya cursó.

## Ejecución

Una vez instalados los requerimientos técnicos, correr en la terminal el comando:

```
docker compose up -d
```

Esto creará dos contenedores en docker, una base de datos en postgresql y un servicio backend. Estos van a ser configurados y conectados automáticamente en el proceso de build junto a la creación de los datos de prueba. Para acceder al servicio backend se utilizará el puerto 5189, desde este se podrán acceder a todas las funcionalidades.
