drop table foglalatok_tab;

create table foglalatok_tab(
	id		number primary key,
	nev		char(20))
;


create or replace type cpu_typ under arucikk_typ (
	sebesseg		number,
	foglalat		number,
	magok_szama		number,
	dobozos			number(1)	
	)
		
;
/