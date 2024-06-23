
create database IETELSolar
go

use IETELSolar
go


------------------------------------------------------------------------------------------
--CRIAR TABELAS
------------------------------------------------------------------------------------------


create table Pessoas
(
	id			int			not null	primary key identity,
	nome		varchar(50),
	cpf			varchar(14)		unique,
)
go

create table Telefones
(
	id			int			not null	primary key identity,
	pessoaId	int			not null,	
	telefone	varchar(15)		unique,
	foreign key (pessoaId) references Pessoas(id)
)
go

create table Enderecos 
(
	id			int		 not null	       primary key identity,
	pessoaId	int		 not null,
	cidade		varchar(50),
	CEP			varchar(9),
	bairro		varchar(50),
	rua			varchar(50),
	numero		varchar(6),
	foreign key (pessoaId) references Pessoas(id)
)
go

create table Clientes
(
	id			int		not null             primary key identity,
	pessoaId	int		not null,
	foreign key(pessoaId) references Pessoas(id),
)
go

create table Candidatos
(
	id			int			not null      primary key identity,
	pessoaId	int			not null,
	email		varchar(50),
	linkedIn	varchar(50),
	curriculo	varchar(50),
	mensagem	text,
	foreign key (pessoaId) references Pessoas(id)
)

create table Administradores
(
	id			int			not null	primary key identity,
	pessoaId	int			not null,
	email		varchar(50),
	senha		varchar(60),
	foreign key(pessoaId) references Pessoas(id)
)
go

create table Agendamentos

(
	id			int			not null	primary key identity,
	ClienteId	int,
	AdminId		int,
	data		date,
	horario		varchar(5),
	status		int,
	check		(status in(0,1)),
	foreign key(ClienteId) references Clientes(id),
	foreign key(AdminId) references Administradores(id)
)
go

create table Vagas
(
	id			int		not null	primary key identity,
	AdminId		int		not null,
	cargo		varchar(50),
	modelo		varchar(50),
	tipo		varchar(50),
	local		varchar(50),
	descricao	text,
	foreign key (AdminId) references Administradores(id)
)

create table Concorre
(
	Candidatoid	int                 not null,
	VagaId		int                 not null,
	AdminId		int                 not null,
	status		int,
	check		(status in(0,1)),
	foreign key (Candidatoid) references Candidatos(id),
	foreign key (VagaId)	references Vagas(id),
	foreign key (AdminId) references Administradores(id)
)
go


------------------------------------------------------------------------------------------
--INSERT
------------------------------------------------------------------------------------------


insert into Pessoas (nome,cpf) 
values  ('Priscila Silva','123.456.789-10'),
		('José Lopes','234.986.981-32')
go


insert into Telefones (pessoaId,telefone)
values  (1,'(17)99677-6562'),
		(2,'(17)99156-8763')
go

insert into Enderecos (pessoaId,cidade,CEP,bairro,rua,numero)
values	(1,'Potirendaba','15105-000','Jardim dos Eucaliptos','Pedro Garcia Peres','1665'),
		(2,'Potirendaba','15105-000','Jardim do Bosque','Rua das Margaridas','5278')
go

insert into Administradores (pessoaId,email,senha)
values  (1,'priscila@gmail.com','12345678'),
		(2,'jlopes@gmail.com','12345678')
go

insert into Agendamentos(ClienteId,AdminId,data,horario,status)
values  (null,1,'2023-12-05','15:00',0),
		(null,1,'2023-12-05','16:30',0),
		(null,2,'2023-12-06','14:10',0),
		(null,2,'2023-12-06','15:45',0)
go


insert into Vagas(AdminId,cargo,modelo,tipo,local,descricao)
values  (1,'Vendedor',  'Presencial','Integral',    'Potirendaba',           'Responsabilidades: garantir que as metas sejam atingidas, analisar preços e táticas de vendas, e elaborar relatórios para a tomada de decisão. Benefícios: seguro de vida, vale refeição, convênio médico e convênio odontológico.'),
		(2,'Recrutador','Híbrido',   'Meio Período','São José do Rio Preto', 'Requisitos: boas habilidades de comunicação, motivação para atingir metas, conseguir trabalhar de forma independente e utilizar metodologias de seleção eficazes.')

go


------------------------------------------------------------------------------------------
--VIEWS
------------------------------------------------------------------------------------------


--View para exibir as informações dos Candidatos.

CREATE VIEW View_Candidato 
(nome, cpf, telefone, cidade, email, linkedin, curriculo, mensagem, vagaId)
AS
SELECT p.nome, p.cpf, t.telefone, e.cidade, c.email, c.linkedin, c.curriculo, c.mensagem, co.VagaId
FROM Pessoas p, Telefones t, Enderecos e, Candidatos c, Concorre co
WHERE p.id = t.id
AND p.id = e.pessoaId
AND p.id = c.pessoaId
AND c.id = co.Candidatoid
go

--TESTE!!
select * from View_Candidato
go


--View para exibir as informações dos Clientes.

CREATE VIEW View_Orcamento 
(nome, cpf, telefone, rua, bairro, numero, cep, cidade, data, horario, id)
AS
SELECT p.nome, p.cpf, t.telefone, e.rua, e.bairro, e.numero, e.CEP, e.cidade, a.data, a.horario, c.id
from Pessoas p, Telefones t, Enderecos e, Agendamentos a, Clientes c
WHERE p.id = t.id
AND p.id = e.id
AND p.id = c.pessoaId
AND c.id = a.ClienteId

--TESTE!!
select * from View_Orcamento
go


------------------------------------------------------------------------------------------
--PROCEDURES
------------------------------------------------------------------------------------------


--Procedure para cadastrar um candidato.

create procedure sp_cadCandidatos
(
	@nomeCand	varchar(50),	
	@cpfCand	varchar(14),
	@telefone   varchar(15),
	@cidade	    varchar(50),
	@email		varchar(50),
	@linkedIn	varchar(50), 
	@curriculo	varchar(60),
	@mensagem	varchar(300),
	@VagaId	    int,
	@AdminId	int,
	@status	    int
)
as
begin
	insert into Pessoas(nome,cpf)
	values (@nomeCand, @cpfCand)

	insert into Telefones(pessoaId,telefone)
	values(@@IDENTITY, @telefone)

	insert into Enderecos(pessoaId, cidade)
	values(@@IDENTITY, @cidade)

	insert into Candidatos(pessoaId, email, linkedIn, curriculo, mensagem)
	values(@@IDENTITY, @email, @linkedIn, @curriculo, @mensagem)

	insert into Concorre(Candidatoid, VagaId, AdminId, status)
	values(@@IDENTITY, @VagaId, @AdminId, @status)
end
go 


-- Teste de execução –
exec sp_cadCandidatos 'Rafael Andrade', '675.098.097-00', '(17)99126-8703', 'Cedral', 'Rafa98@hotmail.com', 'linkedIn/rafa-123', 
'curriculo-rafa.pdf', 'Profissional que realiza instalações e manutenções de placas solares fotovoltaicas.', 1, 2, 0
go


--TESTE!!
select * from Pessoas
select * from Enderecos
select * from Telefones
select * from Candidatos
select * from Concorre


--Procedure para cadastrar um cliente.

create procedure sp_cadOrcamentos
(
	@nomeCand	varchar(50),	
	@cpfCand	varchar(14),
	@telefone   varchar(15),
	@cidade     varchar(50),
	@cep	    varchar(8),
	@bairro     varchar(50),
	@rua		varchar(50),
	@numero		varchar(6),
	@data		date,
	@horario	varchar(5),
	@status		int,
	@AdminId	int
)
as
begin
	insert into Pessoas(nome,cpf)
	values (@nomeCand, @cpfCand)

	insert into Enderecos(pessoaId, cidade, CEP, bairro, rua, numero)
	values(@@IDENTITY, @cidade, @cep, @bairro, @rua, @numero)

	insert into Telefones(pessoaId, telefone)
	values(@@IDENTITY, @telefone)

	insert into Clientes(pessoaId)
	values(@@IDENTITY)

	insert into Agendamentos(ClienteId, AdminId, data, horario, status)
	values(@@IDENTITY, @AdminId, @data, @horario, @status)

end
go

-- Teste de execução –
exec sp_cadOrcamentos 'Maria Julia Silva', '605.984.034-81', '(17)99876-5321', 'Potirendaba', '15105-000', 'Jardim das Flores', 
'Rua Independência', '1894', '2023-11-30', '13:30', 0, 1;

--TESTE!!
select * from Pessoas
select * from Enderecos
select * from Telefones
select * from Clientes
select * from Agendamentos
go


--Procedure para editar um cliente.

create procedure sp_editOrcamentos
(
	@nomeCliente varchar(50),	
	@cpfCliente  varchar(14),
	@telefone	 varchar(15),
	@cidade	     varchar(50),
	@cep		 varchar(8),
	@bairro		 varchar(50),
	@rua		 varchar(50),
	@numero		 varchar(6),
	@data		 date,
	@horario	 varchar(5),
	@status		 int,
	@idCliente	 int
)
as
begin
	update Pessoas 
	set nome = @nomeCliente, cpf = @cpfCliente
	from Pessoas p, Clientes c
	where p.id = c.pessoaId
	and c.id = @idCliente

	update Enderecos
	set cidade = @cidade, cep = @cep, bairro = @bairro, rua = @rua, numero = @numero
	from Pessoas p, Clientes c, Enderecos e
	where p.id = c.pessoaId
	and p.id = c.pessoaId
	and c.id = @idCliente

	update Telefones
	set telefone = @telefone
	from Pessoas p, Clientes c, Telefones t
	where p.id = c.pessoaId
	and p.id = t.pessoaId
	and c.id = @idCliente

	update Agendamentos
	set data = @data, horario = @horario, status = @status
	where ClienteId = @idCliente
end
go

-- Teste de execução –
exec sp_editOrcamentos 'Cecília Vasconcelos', '197.675.953-03', '(17)99675-3022', 'Potirendaba', '15105-000', 'Liberdade',
'Nova York', '0986', '2023-12-05', '15:40', 1, 1
go

--TESTE!!
select * from Pessoas
select * from Enderecos
select * from Telefones
select * from Agendamentos
go


--Procedure para deletar uma vaga.

create procedure sp_delVagas
(
	@id int
)
as
begin
	delete from Concorre where vagaId = @id
	delete from Vagas where id = @id
end
go

-- Teste de execução –
exec sp_delVagas 1

--TESTE!!
select * from Concorre
select * from Vagas