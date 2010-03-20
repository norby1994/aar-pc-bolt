drop table memoria_tip_tab;
create table memoria_tip_tab (
	id			number,
	nev			char(20)
);

create or replace type memoria_typ under arucikk_typ (
	tipus		number,
	meret		number,
	sebesseg	number,
	constructor function memoria_typ(
		nev varchar2,
		gyarto number,
		ar number,
		darabszam number,
		tipus number,
		meret number,
		sebesseg number	
	) return self as result
	
);
/

create or replace type body memoria_typ as
	constructor function memoria_typ(
		nev varchar2,
		gyarto number,
		ar number,
		darabszam number,
		tipus number,
		meret number,
		sebesseg number	
	) return self as result is
	begin
		self.nev := nev;
		self.gyarto := gyarto;
		self.ar := ar;
		self.darabszam := darabszam;
		self.tipus := tipus;
		self.meret := meret;
		self.sebesseg := sebesseg;
		
		self.atlag := 0;
		self.ertekeles_szam := 0;
		self.akcio := 0;
		self.leiras := 'leiras';		
		return;
	end;

end;
/