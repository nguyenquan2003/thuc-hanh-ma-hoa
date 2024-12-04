-- connect: sys--
create profile MyPassword limit
    password_life_time 60
    password_grace_time 10
    password_reuse_time 1
    password_reuse_max 5 failed_login_attempts 3;
    
-- Tao thu tuc hien thi profile len form--
-- connect: sys--
-- Thu tuc lay ten profile--
create or replace procedure pro_profile_Name (cur out sys_refcursor)
is
begin
    open cur for
        select distinct profile from dba_profiles;
end;

--thu tuc xem chi tiet profile--
create or replace procedure pro_profile (cur out sys_refcursor, profile_name in varchar2)
is
begin
    open cur for
        select profile, resource_name, resource_type, limit from dba_profiles where profile = profile_name;
end;

--chay thu thuc tren sql developer
--connect: sys--
set SERVEROUTPUT ON;
declare
    cur sys_refcursor;
    c varchar2(100);
begin
    pro_profile_name (cur);
    loop
        fetch cur into c;
        exit when cur%notfound;
        dbms_output.put_line ('Profile name: '||c);
    end loop;
end;

declare
    profile_name varchar2(100) := 'DEFAULT';
    cur sys_refcursor;
    out1 varchar2(128);
    out2 varchar2(32);
    out3 varchar2(8);
    out4 varchar2(128);
begin
    pro_profile (cur=>cur,profile_name=>profile_name);
    loop
        fetch cur into out1, out2, out3, out4;
        exit when cur%notfound;
        dbms_output.put_line ('profile: '|| out1 ||
                              '; resource_name: '|| out2||
                              '; resource_type: '|| out3||
                              '; limit: ' || out4);
    end loop;
end;
--------------------------------------------------------------------------------

-- Tao user va gan profile "MyPassword"
create user John identified by p123;
alter user John profile MyPassword;

-- Mo khoa tai khoan--
-- conncet sys--
alter user John account unlock;

--T?o user và view truy v?n ğ?n b?ng dba_users
create view view_users as
select account_status, username
from sys.dba_users;

-- T?o hàm tr? v? tr?ng thái user
-- connect: sysuser--
create or replace function fun_account_status (user in varchar2) return varchar2
is
    t varchar2(50);
begin
    select account_status into t from view_users where username = user;
    return t;
exception when others then t := ' ';
    return t;
end;

-- Th?c thi hàm trên sql developer
-- connect: sysuser--
set SERVEROUTPUT ON;
declare
    user_name varchar2(100) := 'AN';
    account_status varchar2(100);
begin
    account_status := fun_account_status (user_name); 
    DBMS_OUTPUT.PUT_LINE('User: '|| user_name || '->' || account_status);
end;
