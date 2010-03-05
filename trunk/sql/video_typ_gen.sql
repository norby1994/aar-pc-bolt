drop table video_foglalat_tab;
drop sequence video_foglalat_seq;


create sequence video_foglalat_seq
	start with 1
	increment by 1
	nomaxvalue
;

create table video_foglalat_tab (
	id		number primary key,
	neve	varchar2(20)
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
	memoria		number
);
/