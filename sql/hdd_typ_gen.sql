drop sequence hdd_csatolok_seq;
drop table hdd_csatolok_tab;

create  table hdd_csatolok_tab (
	id		number primary key,
	nev		char(20)
);

create sequence hdd_csatolok_seq
	start with 1
	increment by 1
	nomaxvalue
;

create trigger hdd_csatolok_trigger
	before insert on hdd_csatolok_tab
	for each row
	begin
		select hdd_csatolok_seq.nextval into :new.id from dual;
	end;
/

create or replace type hdd_typ under arucikk_typ(
	csatolo		number,
	meret		number,
	constructor function hdd_typ (
		gyarto number,
		nev varchar2,
		ar number,
		darabszam number,
		csatolo number,
		meret number	
	)return self as result)
;
/

create or replace type body hdd_typ as
	constructor function hdd_typ (
		gyarto number,
		nev varchar2,
		ar number,
		darabszam number,
		csatolo number,
		meret number
	)return self as result is	
	begin
		self.nev := nev;
		self.gyarto := gyarto;
		self.ar := ar;
		self.darabszam := darabszam;
		self.csatolo := csatolo;
		self.meret := meret;
		
		self.atlag := 0;
		self.ertekeles_szam := 0;
		self.akcio := 0;
		self.leiras := 'leiras';
		return;
	end;
end;
/