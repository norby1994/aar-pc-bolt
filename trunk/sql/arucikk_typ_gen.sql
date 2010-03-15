drop type arucikk_typ;
drop table gyarto_tab;
drop sequence gyarto_seq;

create table gyarto_tab (
	id			number primary key,
	nev			varchar2(30)
);

create sequence gyarto_seq 
	start with 1
	increment by 1
	nomaxvalue
;

create trigger gyarto_tri
	before insert on gyarto_tab
	for each row
	begin
		select gyarto_seq.nextval into :new.id from dual;
	end;
/




create or replace type arucikk_typ as object (
	id 				number,
	gyarto			number,
	nev				varchar2(30),
	ar				number,
	darabszam		number,
	akcio			number,
	atlag			number,
	ertekeles_szam	number,
	leiras			varchar2(500),
	CONSTRUCTOR FUNCTION arucikk_typ(neve varchar2) return self as result
	)
	not final;
/
CREATE or replace TYPE BODY arucikk_typ as
	CONSTRUCTOR FUNCTION arucikk_typ(neve varchar2) return self as result is
	BEGIN 
		self.nev :=neve;
		return;
	END;
	
END;	
/	

