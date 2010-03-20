

create or replace type alaplap_typ under arucikk_typ (
	foglalat		number,
	mem_foglalat	number,
	mem_fog_szam	number,
	video_foglalat	number,
	sata			number,
	ide				number,
	
	constructor function alaplap_typ (
		gyarto number,
		nev varchar2,
		ar number,
		darabszam number,
		foglalat number,
		mem_foglalat number,
		mem_fog_szam number,
		video_foglalat number,
		sata number,
		ide number
	) return self as result
);
/

create or replace type body alaplap_typ as
	constructor function alaplap_typ (
		gyarto number,
		nev varchar2,
		ar number,
		darabszam number,
		foglalat number,
		mem_foglalat number,
		mem_fog_szam number,
		video_foglalat number,
		sata number,
		ide number
	) return self as result is
	BEGIN
		self.gyarto := gyarto;
		self.nev := nev;
		self.ar := ar;
		self.darabszam :=darabszam;
		self.foglalat := foglalat;
		self.mem_foglalat := mem_foglalat;
		self.mem_fog_szam := mem_fog_szam;
		self.video_foglalat :=video_foglalat;
		self.sata := sata;
		self.ide := ide;
		
		self.atlag := 0;
		self.ertekeles_szam := 0;
		self.akcio := 0;
		self.leiras := 'leiras';
		return;
	END;

end;
/