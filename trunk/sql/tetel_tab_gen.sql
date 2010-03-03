drop table tetel_tab;

create table tetel_tab (
	aru			number references raktar_tab(id),
	szamla		number references szamla_tab(id),
	darab		number,
	ar			number,
	primary key (aru, szamla)
	)
;
	