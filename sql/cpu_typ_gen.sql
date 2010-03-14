drop sequence cpu_foglalat_seq;
drop table cpu_foglalat_tab;

create table cpu_foglalat_tab(
	id		number primary key,
	nev		char(20) unique)
;

create sequence cpu_foglalat_seq
	start with 1
	increment by 1
	nomaxvalue
;

create trigger cpu_foglalat_trigger
	before insert on cpu_foglalat_tab
	for each row
	begin
		select cpu_foglalat_seq.nextval into :new.id from dual;
	end;
	
/

create or replace type cpu_typ under arucikk_typ (
	sebesseg		number,
	foglalat		number,
	magok_szama		number,
	dobozos			number(1),
	CONSTRUCTOR FUNCTION cpu_typ(neve varchar2) return self as result
	)
		
;
/

create or replace type body cpu_typ as
	constructor function cpu_typ(neve varchar2) return self as result is
	begin
		
		return;
	end;
end;
/