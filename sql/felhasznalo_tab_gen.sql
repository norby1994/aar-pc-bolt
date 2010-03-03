drop sequence felhasznalo_seq;
drop trigger felhasznalo_tri;
drop table felhasznalo_tab;

create sequence felhasznalo_seq
	start with 1
	increment by 1
	nomaxvalue
;

create table felhasznalo_tab (
	id				number primary key,
	felhasznev		varchar2(20),
	teljesnev		varchar2(100),
	varos			varchar2(20),
	utca			varchar2(50),
	torolt			number(1))
;

create trigger felhasznalo_tri
	before insert on felhasznalo_tab
	for each row
	begin
		select felhasznalo_seq.nextval into :new.id from dual;
	end;
/