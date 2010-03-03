drop table csatolok_tab;
create  table csatolok_tab (
	id		number primary key,
	nev		char(20)
);

create or replace type hdd_typ under arucikk_typ(
	meret		number,
	csatolo		number
);
/