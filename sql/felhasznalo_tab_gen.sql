drop sequence felhasznalo_seq;
drop table felhasznalo_tab;

create sequence felhasznalo_seq
	start with 1
	increment by 1
	nomaxvalue
;

create table felhasznalo_tab (
	id				number primary key,
	felhasznev		varchar2(20) not null unique,
	teljesnev		varchar2(100),
	varos			varchar2(20),
	utca			varchar2(50),
	iranyitoszam	varchar2(4),
	torolt			number(1),
	jelszo			varchar2(20) not null
);

create trigger felhasznalo_tri
	before insert on felhasznalo_tab
	for each row
	begin
		select felhasznalo_seq.nextval into :new.id from dual;
		:new.torolt := 0;
		--select 0 into :new.id from dual;
		
	end;
/