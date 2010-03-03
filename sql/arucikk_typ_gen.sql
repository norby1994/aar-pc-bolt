drop type arucikk_typ;

create or replace type arucikk_typ as object (
	id 				number,
	gyarto			varchar2(20),
	nev				varchar2(30),
	ar				number,
	darabszam		number,
	akcio			number,
	atlag			number,
	ertekeles_szam	number,
	leiras			varchar2(500))
	not final;
/
