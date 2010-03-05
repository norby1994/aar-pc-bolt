drop table szamla_tab;
drop sequence szamla_seq;

create sequence szamla_seq
	start with 1
	increment by 1
	nomaxvalue
;

create table szamla_tab (
	id			number primary key,
	felhasz		number references felhasznalo_tab(id),
	ido			timestamp
);

create trigger szamla_tri 
	before insert on szamla_tab
	for each row
	begin
		select szamla_seq.nextval into :new.id from dual;
	end;
/