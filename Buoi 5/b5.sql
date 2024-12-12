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
create or REPLACE pro_select_grant_user(username in varchar2, cur sys_refcursor)
is
begin
    open cur for
      select table_name. type, owner from dba_tab_privs where grantee = username and
      type in ('PROCEDURE','FUNCTION','PACKAGE');
end;
--9--
create or REPLACE PROCEDURE pro_grant_revoke(username in varchar2, scheme_user in varchar2,
pro_tab in varchar2, type_pro in varchar2, dk in number)
is
begin
  if dl = 1 then
    execute IMMEDIATE 'grant ' || type_pro || ' on ' || schema_user ||'.' || ' to ' || usermame;
  else
    execute IMMEDIATE 'rewoke ' || type_pro || ' on ' || schema_user ||'.' || ' to ' || usermame;
  end if;
  end;
  --10--