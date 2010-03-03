drop table memoria_tip_tab;
create table memoria_tip_tab (
	id			number,
	nev			char(20)
);

create or replace type memoria_typ under arucikk_typ (
	tipus		number,
	meret		number,
	sebesseg	number
);
/