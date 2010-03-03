drop table foglalatok_tab;
drop type cpu_typ;

create table foglalatok_tab(
	id		number primary key,
	nev		char(20))
;


create type cpu_typ under arucikk_typ (
	sebesseg		number,
	foglalat		number,
	magok_szama		number,
	dobozos			number(1)	
	)
		
;
/