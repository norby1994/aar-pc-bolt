drop table video_foglalat_tab;
drop sequence video_foglalat_seq;


create sequence video_foglalat_seq
	start with 1
	increment by 1
	nomaxvalue
;

create table video_foglalat_tab (
	id		number primary key,
	nev	varchar2(20)
);

create trigger video_foglalat_tri
	before insert on video_foglalat_tab
	for each row
	begin
		select video_foglalat_seq.nextval into :new.id from dual;
	end;
/

create or replace type video_typ under arucikk_typ (
	foglalat	number,
	memoria		number,
	constructor function video_typ(
		nev		varchar2,
		gyarto	number,
		ar		number,
		darabszam	number,
		foglalat	number,
		memoria		number		
	) return self as result
);
/

create or replace type body video_typ as
	constructor function video_typ(
		nev		varchar2,
		gyarto	number,
		ar		number,
		darabszam	number,
		foglalat	number,
		memoria		number		
	) return self as result is
	BEGIN
		self.nev := nev;
		self.gyarto := gyarto;
		self.ar := ar;
		self.darabszam := darabszam;
		self.foglalat := foglalat;
		self.memoria := memoria;
		
		self.akcio := 0;
		self.atlag := 0;
		self.ertekeles_szam := 0;
		self.leiras := 'Meg nincs';
		return;
	END;
		

end;
/