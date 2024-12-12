create user John identified by JOHN;
grant create session to John;

alter user John quota 10M on USERS;
------------------------------------------------
create or replace package pkg_CrUser
as
  procedure pro_create_user(username in varchar2, pass in varchar2);
  procedure pro_alter_user(username in varchar2, pass in varchar2);
  function fun_check_account(user in varchar2) return int;
  procedure Pro_CrUser(username in varchar2, pass in varchar2);
end pkg_CrUser;

--------------------------------------
--------------------------------Bai 2-----------------------------
grant create any procedure, execute any procedure, drop any procedure, alter any procedure to hr;
-------------------------------------
create or replace package pkg_PhanQuyen
as
  --1
  procedure pro_select_procedure_user(useowner in varchar2, pro_type in varchar2, cur out sys_refcursor);
  --2
  procedure pro_select_user(cur out sys_refcursor);
  --3
  procedure pro_select_reoes(cur out sys_refcursor);
  --4
  procedure pro_user_roles(username in varchar2, cur out sys_refcursor);
  --5
  procedure pro_user_roles_check(username in varchar2, roles in varchar2, cout out number);
  --6
  procedure pro_select_table(username in varchar2, cur out sys_refcursor);
  --7
  procedure pro_select_grant(username in varchar2, userschema in varchar2, tablename in varchar2, cur out sys_refcursor);
  --8
  procedure pro_grant_revoke(username in varchar2, schema_user in varchar2, pro_tab in varchar2, type_pro in varchar2, dk in number);
  --10
  procedure pro_grant_revoke_Roles(username in varchar2, roles in varchar2, dk in number);

  procedure pro_select_grant_user(username in varchar2, cur out sys_refcursor);
end;

------------------------
--1--
create or replace procedure pro_select_procedure_user(userowner in varchar2, pro_type in varchar2, cur out sys_refcursor)
is
begin
    open cur for
      select object_name from dba_procedures where owner = userowner and object_type = pro_type;
end;
--2--
create or replace procedure pro_select_user(cur out sys_refcursor)
is
begin
  open cur for
      select username from dba_users where account_status = 'OPEN';
end;
--3--
create or replace procedure pro_select_roles(cur out sys_refcursor)
is 
begin
  open cur for
    select role from dba_roles;
end;
--4--
create or replace procedure pro_user_roles(username in varchar2, cur out sys_refcursor)
is 
begin
  open cur for
    select granted_role from dba_role_privs where grantee = username;
end;
--5--
create or replace procedure pro_user_roles_chek(username in varchar2, roles in varchar2, cout out number)
is
begin
  select COUNT(*) into cout from dba_role_privs where grantee = username and granted_role = roles;
end;
--6--
create or REPLACE PROCEDURE pro_select_table(username in varchar2, cur out sys_refcursor)
is
begin
  open cur for
    select table_name from dba_all_tables where owner = username;
end;
--7--
create or REPLACE procedure pro_select_grant(username in varchar2, userschema in varchar2, tablename in varchar2, cur out sys_refcursor)
is
begin
  open cur for
    select privilege from dba_tab_privs where grantee = username and 
    table_name = tablename and owner = userschema;
end;
--8--
create or REPLACE procedure pro_select_grant_user(username in varchar2, cur sys_refcursor)
is
begin
    open cur for
      select table_name, type, owner from dba_tab_privs where grantee = username and
      type in ('PROCEDURE','FUNCTION','PACKAGE');
end;
--9--
create or REPLACE PROCEDURE pro_grant_revoke(username in varchar2, schema_user in varchar2,
pro_tab in varchar2, type_pro in varchar2, dk in number)
is
begin
  if dk = 1 then
    execute IMMEDIATE 'grant ' || type_pro || ' on ' || schema_user ||'.' || ' to ' || username;
  else
    execute IMMEDIATE 'rewoke ' || type_pro || ' on ' || schema_user ||'.' || ' to ' || username;
  end if;
  end;
  --10--
create or replace procedure pro_grant_revoke_Roles(username in varchar2, roles in varchar2, dk in number)
is 
begin
  if dk = 1 then
    execute immediate 'grant' || roles || 'to' || username;
  else
    execute immediate 'revoke' || roles || 'from' || username;
  end if;
end;

create role sysuser;
grant execute on sys.pkg_PhanQuyen to sysuser;
--Connect sys------------
create role DataEntry;
create role Supervisor;
create role Management;
-------------------------

grant DataEntry to John, Joe, Lynn;
grant Supervisor to Fred;
grant Management to Amy, Beth;
--------------------------
grant select, insert , update on hr.Attendance to DataEntry;
--------------------------
grant select,delete on hr.Attendance to Supervisor;
---------------------------
grant select on hr.Attendance to Management;