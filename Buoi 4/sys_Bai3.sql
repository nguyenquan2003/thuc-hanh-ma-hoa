-- Connect SYS -- 
create user Join identified by JOHN;
grant create session to John;

-- �am bao c�c user n�y c� the tao bat ki bang n�o trong tablespace voi quota 10M.--
alter user John quota 10M on USERS;

-- Cai dat package dung de check, tao va sua doi User
CREATE or replace package pkg_CrUser
as
  procedure pro_create_user(username in varchar2, pass in varchar2);
  PROCEDURE pro_alter_user( username in varchar2, pass in varchar2);
  FUNCTION fun_check_account(user in varchar2) return int;
  procedure pro_CrUser(username in varchar2,pass in varchar2);
end pkg_CrUser;

-- Thu tuc trong package create user dung de tao user
create or replace package body pkg_CrUser
as
    --Procedure create user--
    procedure pro_create_user(username in varchar2, pass in varchar2)
    is
    begin
      execute immediate 'create user ' || username || 'identified by ' ||  pass || 'quota 10M on USERS' ;
      execute immediate 'grant create session to ' ||username; end pro_create_user;
    end pro_create_user;

    -- Thu tuc trong package alter user d�ng de doi mat khau user
    -- Procedure alter user--
    procedure pro_alter_user(username in varchar2, pass in varchar2)
    is
    begin
      execute immediate 'alter user ' || username || 'identified by ' || pass;
      commit work;
    end pro_alter_user;

    -- H�m check user d�ng de kiem tra user d� ton tai hay chua
    function fun_check_account (user in varchar2)
    return int
    is
        t varchar2 (50);
        kq int;
    begin
        select account_status into t from dba_users where username=user;
        if t is null then
            kq := 0;
        else
            kq := 1;
        end if;
        return kq;
    exception when others then kq := 0;
        return kq;
    end fun_check_account;

    -- Thu tuc d�ng dd goi chay tat ca c�c h�m v� thu tuc tr�n dua v�o ket qua tra ve cua h�m check user
    procedure Pro_CrUser (username in varchar2 , pass in varchar2)
    is
      ckUer int := fun_check_account(username);
    begin
        if ckUser = 0 then
            pro_create_user(username, pass);
        else
            pro_create_user(username, pass);
        end if;
        commit work;
    end Pro_CrUser;
end pkg_CrUser;

-- G�n quyen goi chay package cho User: sysuser
grant execute on pkg_cruser to sysuser;

begin
    sys.pkg_CrUser.Pro_CrUser('John', '123');
end;