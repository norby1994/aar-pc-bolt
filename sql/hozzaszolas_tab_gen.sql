drop table hozzaszolas_tab;
drop sequence hozzaszolas_seq;

create sequence hozzaszolas_seq
	start with 1
	increment by 1
	nomaxvalue
;


create table hozzaszolas_tab (
	id 			number primary key,
	aru			number references raktar_tab(id),
	felhasz		number references felhasznalo_tab(id),
	datum		timestamp,
	szoveg		varchar2(4000),
	ellenorzott number(1)
);

create or replace trigger hozzaszolas_tri
	before insert on hozzaszolas_tab
	for each row
	begin
		select hozzaszolas_seq.nextval into :new.id from dual;
		select sysdate into :new.datum from dual;
		:new.ellenorzott := 0;
	end;
/
