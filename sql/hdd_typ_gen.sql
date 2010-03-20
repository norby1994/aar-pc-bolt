drop table hdd_csatolok_tab;
create  table hdd_csatolok_tab (
	id		number primary key,
	nev		char(20)
);

insert into hdd_csatolok_tab values(1, 'IDE');
insert into hdd_csatolok_tab values(2, 'SATA');

create or replace type hdd_typ under arucikk_typ(
	meret		number,
	csatolo		number,
	constructor function hdd_typ (
		gyarto number,
		nev varchar2,
		ar number,
		darabszam number,
		meret number,
		csatolo number
	)return self as result
);
/

create or replace type body hdd_typ as
	constructor function hdd_typ (
		gyarto number,
		nev varchar2,
		ar number,
		darabszam number,
		meret number,
		csatolo number
	)return self as result is	
	begin
		self.nev := nev;
		self.gyarto := gyarto;
		self.ar := ar;
		self.darabszam := darabszam;
		self.meret := meret;
		self.csatolo := csatolo;
		
		self.atlag := 0;
		self.ertekeles_szam := 0;
		self.akcio := 0;
		self.leiras := 'leiras';
		return;
	end;
end;
/