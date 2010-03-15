drop sequence cpu_foglalat_seq;
drop table cpu_foglalat_tab;

create table cpu_foglalat_tab(
	id		number primary key,
	nev		char(20) unique)
;

create sequence cpu_foglalat_seq
	start with 1
	increment by 1
	nomaxvalue
;

create trigger cpu_foglalat_trigger
	before insert on cpu_foglalat_tab
	for each row
	begin
		select cpu_foglalat_seq.nextval into :new.id from dual;
	end;
	
/

create or replace type cpu_typ under arucikk_typ (
	sebesseg		number,
	foglalat		number,
	magok_szama		number,
	dobozos			number(1),
	CONSTRUCTOR FUNCTION cpu_typ(
	nev varchar2,
	gyarto number,
	ar number,
	darab number,
	sebesseg number,
	foglalat number,
	magok number,
	dobozos number
	) return self as result)
			
;
/

create or replace type body cpu_typ as
	CONSTRUCTOR FUNCTION cpu_typ(
	nev varchar2,
	gyarto number,
	ar number,
	darab number,
	sebesseg number,
	foglalat number,
	magok number,
	dobozos number
	) return self as result is
	begin
		self.nev := nev;
		self.gyarto := gyarto;
		self.ar := ar;
		self.darabszam := darab;
		self.sebesseg := sebesseg;
		self.foglalat := foglalat;
		self.magok_szama := magok;
		self.dobozos := dobozos;

		self.atlag := 0;
		self.ertekeles_szam := 0;
		self.akcio := 0;
		self.leiras := 'leiras';
		return;
	end;
end;
/