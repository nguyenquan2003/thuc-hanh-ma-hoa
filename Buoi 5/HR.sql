create table Attendance
(
  ID int PRIMARY KEY,
  Name NVARCHAR2(100)
);

-------------------select---------
create or replace procedure pro_select_Attendance(cur out sys_refcursor)
is
begin
  open cur for
        select * from attendance;
end;

-----insert---
create or replace procedure pro_insert_Attendance(Id in int, Name in varchar2)
is
begin
  insert into hr.attendance
  values (Id, Name);
  commit;
end;
--------Update-
create or replace procedure pro_update_Attendance(Id_Up in int, Name_up in varchar2)
is
begin
  update hr.attendance
  set Name = Name_Up
  where ID = Id_Up;
  commit;
end;
--------------Delete---------------
create or replace procedure pro_delete_Attendance(Id_Delete in int)
is
begin
  delete hr.attendance
  where ID = Id_Delete;
  commit;
end;