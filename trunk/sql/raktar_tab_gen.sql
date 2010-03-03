drop sequence raktar_seq;
drop table raktar_tab;
drop trigger raktar_tri;

create sequence raktar_seq
	start with 1
	increment by 1
	nomaxvalue
;

create table raktar_tab of arucikk_typ
	(id primary key)
;	

create trigger raktar_tri
	before insert on raktar_tab
	referencing new as sor
	for each row
	begin
		select raktar_seq.nextval into :sor.id from dual;
	end raktar_tri;
/