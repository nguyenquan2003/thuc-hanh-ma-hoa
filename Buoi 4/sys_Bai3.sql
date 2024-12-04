-- Connect SYS -- 
create user Join identified by JOHN;
grant create session to John;

-- Ðam bao các user này có the tao bat ki bang nào trong tablespace voi quota 10M.--
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

    -- Thu tuc trong package alter user dùng de doi mat khau user
    -- Procedure alter user--
    procedure pro_alter_user(username in varchar2, pass in varchar2)
    is
    begin
      execute immediate 'alter user ' || username || 'identified by ' || pass;
      commit work;
    end pro_alter_user;

    -- Hàm check user dùng de kiem tra user dã ton tai hay chua
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

    -- Thu tuc dùng dd goi chay tat ca các hàm và thu tuc trên dua vào ket qua tra ve cua hàm check user
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

-- Gán quyen goi chay package cho User: sysuser
grant execute on pkg_cruser to sysuser;

begin
    sys.pkg_CrUser.Pro_CrUser('John', '123');
end;