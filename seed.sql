insert into "Facultad" ("Id", "Nombre") values ('abfd9035-4d52-441f-b844-71bb24ee2278', 'Ingeniería');
insert into "Facultad" ("Id", "Nombre") values ('f01f8fa0-e944-4b62-b49b-8c7f1d34cc65', 'Música');

insert into "Alumno" ("Id", "Nombre", "FacultadId") values ('54e1e829-d7d2-4b20-b266-8dad5fb3d7b0', 'Juan Perez', 'abfd9035-4d52-441f-b844-71bb24ee2278');
insert into "Alumno" ("Id", "Nombre", "FacultadId") values ('32577785-96f0-4ab1-a46f-abd9048a2827', 'Luisa Vargas', 'abfd9035-4d52-441f-b844-71bb24ee2278'); 
insert into "Alumno" ("Id", "Nombre", "FacultadId") values ('e80ca0f1-fa39-4017-84ca-1f1a50bfb85d', 'José Perdomo', 'abfd9035-4d52-441f-b844-71bb24ee2278'); 
insert into "Alumno" ("Id", "Nombre", "FacultadId") values ('3c260206-daaa-4f81-b921-c721935ffa83', 'Santiago Piñerez', 'f01f8fa0-e944-4b62-b49b-8c7f1d34cc65'); 
insert into "Alumno" ("Id", "Nombre", "FacultadId") values ('2f6401af-4446-401a-812f-5dcf0524b9f0', 'Luis Ramos', 'f01f8fa0-e944-4b62-b49b-8c7f1d34cc65');

insert into "Profesor"  ("Id", "Nombre", "Titulo", "Experiencia") values ('3c260206-daaa-4f81-b921-c721935ffa83', 'Julián Perea', 'Master', 5);
insert into "Profesor"  ("Id", "Nombre", "Titulo", "Experiencia") values ('a7929a9c-8c55-4f35-afe4-a80163bc8aed', 'Marissa Arteta', 'Doctorado', 15);

insert  into "Curso" ("Id", "Nombre", "Descripcion", "Cupos", "PreRequisitoId", "ProfesorId", "Creditos") values ('62176c30-0da4-4706-ae1f-69d758c4c683', 'Calculo I', 'Curso de cálculo', 15, null, 'a7929a9c-8c55-4f35-afe4-a80163bc8aed', 5);
insert  into "Curso" ("Id", "Nombre", "Descripcion", "Cupos", "PreRequisitoId", "ProfesorId", "Creditos") values ('d444bcc6-077f-4d29-bbb3-88adfa812768', 'Calculo II', 'Curso de cálculo', 15, '62176c30-0da4-4706-ae1f-69d758c4c683', 'a7929a9c-8c55-4f35-afe4-a80163bc8aed', 5);
insert  into "Curso" ("Id", "Nombre", "Descripcion", "Cupos", "PreRequisitoId", "ProfesorId", "Creditos") values ('62c72dca-12f1-4cce-bf62-b2708b33d7e4', 'Teoría de la música', 'Curso de música', 1, null, 'a7929a9c-8c55-4f35-afe4-a80163bc8aed', 3);

insert into "CursoAlumno" ("Id", "AlumnoId", "CursoId","Estado") values ('546306fa-67a4-40e3-a7a5-ca6917eb5f00', '54e1e829-d7d2-4b20-b266-8dad5fb3d7b0', '62176c30-0da4-4706-ae1f-69d758c4c683', 1);
insert into "CursoAlumno" ("Id", "AlumnoId", "CursoId","Estado") values ('a4dcae21-6c18-44aa-bbba-1b0b65c38362', '32577785-96f0-4ab1-a46f-abd9048a2827', '62176c30-0da4-4706-ae1f-69d758c4c683', 1);
insert into "CursoAlumno" ("Id", "AlumnoId","CursoId", "Estado") values ('5b80e236-2cae-4a4d-ad97-1f426df02d44', 'e80ca0f1-fa39-4017-84ca-1f1a50bfb85d', '62176c30-0da4-4706-ae1f-69d758c4c683', 2);

insert into "CursoAlumno" ("Id","AlumnoId", "CursoId","Estado") values ('1f55b3ff-035d-4223-a659-261038182cbc', '3c260206-daaa-4f81-b921-c721935ffa83','62c72dca-12f1-4cce-bf62-b2708b33d7e4', 1);

