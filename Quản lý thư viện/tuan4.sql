SELECT username, account_status 
FROM dba_users
ORDER BY username;

SELECT username 
FROM dba_users 
ORDER BY username;

BEGIN
    XemUser('BUIHUNGPHUONG');
END;
/
-------------------------tim tat ca user
CREATE OR REPLACE PROCEDURE XemUser (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM dba_users;
END XemUser;

VAR v_cursor REFCURSOR;


EXEC XemUser(:v_cursor);


PRINT v_cursor;

SELECT * FROM user_procedures WHERE object_name = 'TIMUSER';

--------------------tim kiem user
CREATE OR REPLACE PROCEDURE TimUSER(
    v_username IN VARCHAR2,        -- Tham s? ??u v�o nh?n t�n ng??i d�ng
    v_cursor OUT SYS_REFCURSOR     -- Con tr? tr? v? k?t qu?
)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * 
    FROM dba_users
    WHERE username = v_username;  -- T�m ng??i d�ng theo t�n
END TimUSER;
SELECT * FROM dba_users WHERE username = 'BUIHUNGPHUONG';
SELECT * FROM dba_users WHERE UPPER(username) = UPPER(v_username);

DECLARE
    v_cursor SYS_REFCURSOR;  -- Bi?n con tr?
    v_username VARCHAR2(50) := 'BUIHUNGPHUONG';  -- T�n ng??i d�ng b?n mu?n t�m
    v_user dba_users%ROWTYPE;  -- Bi?n ?? ch?a m?i d�ng d? li?u
BEGIN
    -- G?i th? t?c TimUSER
    TimUSER(v_username, v_cursor);

    -- L?y d? li?u t? con tr?
    LOOP
        FETCH v_cursor INTO v_user;  -- L?y d? li?u v�o bi?n v_user
        EXIT WHEN v_cursor%NOTFOUND;  -- Tho�t kh?i v�ng l?p n?u kh�ng c�n d? li?u

        -- Hi?n th? th�ng tin t? k?t qu?
        DBMS_OUTPUT.PUT_LINE('Username: ' || v_user.username);
        DBMS_OUTPUT.PUT_LINE('Lock Date: ' || v_user.lock_date);
        DBMS_OUTPUT.PUT_LINE('User ID: ' || v_user.user_id);
    END LOOP;

    -- ?�ng con tr? sau khi xong
    CLOSE v_cursor;
END;
/

-- ------------------------------Tat ca cac quyen 
SELECT * FROM DBA_SYS_PRIVS 
CREATE OR REPLACE PROCEDURE XEMQUYEN (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM DBA_SYS_PRIVS;
END XEMQUYEN;


CREATE OR REPLACE PROCEDURE TimQUYENUSER(
    v_username IN VARCHAR2,        -- Tham s? ??u v�o nh?n t�n ng??i d�ng
    v_cursor OUT SYS_REFCURSOR     -- Con tr? tr? v? k?t qu?
)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * 
    FROM DBA_SYS_PRIVS
    WHERE grantee = v_username;  -- T�m ng??i d�ng theo t�n
END TimQUYENUSER;

----------------------------TAO TABLESPACE
--------------------------xem tat ca tablespace
CREATE OR REPLACE PROCEDURE LoadALLTablespace (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT file_name, file_id, tablespace_name FROM dba_data_files;
END LoadALLTablespace;
VAR v_cursor REFCURSOR;
EXEC LoadALLTablespace(:v_cursor);
PRINT v_cursor;

-------------------------------------------------------------- Tao tablespace

CREATE OR REPLACE PROCEDURE CreateTablespace (
    p_tablespace_name IN VARCHAR2,
    p_datafile_path IN VARCHAR2,
    p_size IN VARCHAR2
)
AS
    sql_stmt VARCHAR2(1000);
    final_size VARCHAR2(50);
BEGIN
    -- Ki?m tra n?u p_size kh�ng c� 'M' ho?c 'G' ? cu?i, th�m 'M' v�o cu?i
    IF NOT REGEXP_LIKE(p_size, '[[:digit:]]+[MG]$', 'i') THEN
        final_size := p_size || 'M';
    ELSE
        final_size := p_size;
    END IF;

    -- T?o c�u l?nh SQL ?? t?o tablespace v?i k�ch th??c ?� x? l�
    sql_stmt := 'CREATE TABLESPACE ' || p_tablespace_name ||
                ' DATAFILE ''' || p_datafile_path || ''' SIZE ' || final_size;
    
    -- Th?c thi c�u l?nh SQL
    EXECUTE IMMEDIATE sql_stmt;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20003, 'L?i khi t?o tablespace: ' || SQLERRM);
END CreateTablespace;

  CREATE OR REPLACE PROCEDURE loadtablespace (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT tablespace_name FROM dba_data_files;
END loadtablespace;
VAR v_cursor REFCURSOR;
EXEC loadtablespace(:v_cursor);
PRINT v_cursor;


-------------------------------------------------profile----------------------------------------------------
CREATE PROFILE my_new_profile;
SELECT * FROM DBA_USERS WHERE USERNAME = 'SYS';


SELECT * FROM DBA_PROFILES WHERE profile = 'PHUONG'


CREATE OR REPLACE PROCEDURE create_user_profile(
    p_profile_name IN VARCHAR2,
    p_parameter_name IN VARCHAR2,
    p_parameter_value IN VARCHAR2
) AS
BEGIN
    -- T?o c�u l?nh CREATE PROFILE ??ng v?i tham s? ???c nh?p
    EXECUTE IMMEDIATE 'CREATE PROFILE ' || p_profile_name || ' LIMIT ' ||
        p_parameter_name || ' ' || p_parameter_value;
    
    -- Hi?n th? th�ng b�o th�nh c�ng
    DBMS_OUTPUT.PUT_LINE('Profile ' || p_profile_name || ' created successfully with ' || p_parameter_name || ' set to ' || p_parameter_value || '.');
EXCEPTION
    WHEN OTHERS THEN
        -- X? l� l?i n?u c�
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;

CREATE USER ph IDENTIFIED BY ph 
PROFILE phuong1;

SELECT USERNAME, PROFILE 
FROM DBA_USERS
WHERE PROFILE = 'phuong';
SELECT * FROM dba_profiles WHERE profile = 'PHUONG222';

create profile "phuong1" limit
password_lock_time 5;

SELECT * FROM dba_profiles
--Lay Ten Cac PROFILE
CREATE OR REPLACE PROCEDURE get_unique_profiles AS
BEGIN
    FOR rec IN (SELECT DISTINCT profile FROM dba_profiles) LOOP
        -- In ra t�n profile
        DBMS_OUTPUT.PUT_LINE('Profile: ' || rec.profile);
    END LOOP;
END;
/

CREATE OR REPLACE PROCEDURE get_unique_profiles (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT DISTINCT profile FROM dba_profiles;
END get_unique_profiles;
EXEC get_unique_profiles;

CREATE OR REPLACE PROCEDURE alter_user_profile(
    p_profile_name IN VARCHAR2,           -- T�n profile c?n s?a ??i
    p_parameter_name IN VARCHAR2,         -- T�n tham s? c?n th�m v�o profile
    p_parameter_value IN VARCHAR2         -- Gi� tr? c?a tham s? c?n th�m
) AS
BEGIN
    -- T?o c�u l?nh ALTER PROFILE ??ng ?? th�m tham s? v�o profile ?� t?n t?i
    EXECUTE IMMEDIATE 'ALTER PROFILE ' || p_profile_name || ' LIMIT ' ||
        p_parameter_name || ' ' || p_parameter_value;
    
    -- Hi?n th? th�ng b�o th�nh c�ng
    DBMS_OUTPUT.PUT_LINE('Profile ' || p_profile_name || ' updated successfully with ' || p_parameter_name || ' set to ' || p_parameter_value || '.');
EXCEPTION
    WHEN OTHERS THEN
        -- X? l� l?i n?u c�
        DBMS_OUTPUT.PUT_LINE('Error: ' || SQLERRM);
END;
/
-------------TIM PROFILE-------------
CREATE OR REPLACE PROCEDURE Timprofile(
    v_profile_name IN VARCHAR2,        -- Tham s? ??u v�o nh?n t�n ng??i d�ng
    v_cursor OUT SYS_REFCURSOR     -- Con tr? tr? v? k?t qu?
)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM dba_profiles WHERE profile = v_profile_name;  -- T�m ng??i d�ng theo t�n
END Timprofile;


-----------tao user voi tablespace, profile, quota----------------
CREATE OR REPLACE PROCEDURE TaoNguoiDung(
    v_username    IN VARCHAR2,
    v_password    IN VARCHAR2,
    v_tablespace  IN VARCHAR2,
    v_profile     IN VARCHAR2,
    v_quota       IN NUMBER
) AS
BEGIN
    EXECUTE IMMEDIATE 'CREATE USER ' || v_username || ' IDENTIFIED BY ' || v_password ||
                     ' DEFAULT TABLESPACE ' || v_tablespace || 
                     ' PROFILE ' || v_profile || 
                     ' QUOTA ' || v_quota || 'M ON ' || v_tablespace;

    DBMS_OUTPUT.PUT_LINE('Ng??i d�ng ' || v_username || ' ?� ???c t?o th�nh c�ng.');
END;



CREATE USER nv4 
IDENTIFIED BY nv4
DEFAULT TABLESPACE PHUONG
PROFILE phuong1
QUOTA 100M ON PHUONG;


CREATE USER nv4 
IDENTIFIED BY nv4
DEFAULT TABLESPACE PHUONG
PROFILE PHUONG222
QUOTA 100M ON PHUONG;

--------------------tao quyen he thong---------------------
grant create session to phuong1234

CREATE OR REPLACE PROCEDURE Ganquyenhethong (
    p_username IN VARCHAR2,
    p_privilege IN VARCHAR2
) AS
BEGIN
    -- C?p quy?n h? th?ng
    EXECUTE IMMEDIATE 'GRANT ' || p_privilege || ' TO ' || p_username;
END;
/


--- cap quyen tren table
CREATE OR REPLACE PROCEDURE Ganquyentable (
    p_username IN VARCHAR2,  -- T�n user c?n c?p quy?n
    p_table_name IN VARCHAR2, -- T�n b?ng c?n c?p quy?n (kh�ng c?n ch? ??nh schema)
    p_permission IN VARCHAR2  -- Quy?n c? th? c?n c?p (SELECT, INSERT, UPDATE, DELETE)
) AS
    v_sql VARCHAR2(1000);    -- Bi?n ?? l?u c�u l?nh SQL ??ng
BEGIN
    -- X�y d?ng c�u l?nh GRANT v?i schema BUIHUNGPHUONG v� quy?n c? th?
    v_sql := 'GRANT ' || p_permission || ' ON BUIHUNGPHUONG.' || p_table_name || ' TO ' || p_username;

    -- Th?c thi c�u l?nh GRANT
    EXECUTE IMMEDIATE v_sql;

    -- Ki?m tra xem quy?n ?� ???c c?p th�nh c�ng
    DECLARE
        v_count INTEGER;
    BEGIN
        SELECT COUNT(*)
        INTO v_count
        FROM DBA_TAB_PRIVS
        WHERE GRANTEE = p_username
        AND TABLE_NAME = p_table_name
        AND PRIVILEGE = p_permission;

        IF v_count > 0 THEN
            DBMS_OUTPUT.PUT_LINE('Quy?n ' || p_permission || ' ?� ???c c?p cho user: ' || p_username || ' tr�n b?ng: BUIHUNGPHUONG.' || p_table_name);
        ELSE
            RAISE_APPLICATION_ERROR(-20001, 'Quy?n kh�ng ???c c?p th�nh c�ng!');
        END IF;
    END;

EXCEPTION
    WHEN OTHERS THEN
        -- B?t l?i n?u c�
        DBMS_OUTPUT.PUT_LINE('L?i khi c?p quy?n: ' || SQLERRM);
END;
BEGIN
    Ganquyentable('PHUONG1234', 'TACGIA', 'SELECT');
END;

grant select on BUIHUNGPHUONG.SACH to phuong1234;

CREATE OR REPLACE PROCEDURE TimquyentableUSER(
    v_username IN VARCHAR2,        -- Tham s? ??u v�o nh?n t�n ng??i d�ng
    v_cursor OUT SYS_REFCURSOR     -- Con tr? tr? v? k?t qu?
)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * 
    FROM DBA_TAB_PRIVS
    WHERE GRANTEE = v_username;  -- T�m ng??i d�ng theo t�n
END TimquyentableUSER;

SELECT * 
FROM DBA_TAB_PRIVS 
WHERE GRANTEE = 'PHUONG1234';

------------------------dang xuat user---------------------
SELECT sid, serial#, username 
FROM v$session 
WHERE username = 'PHUONG1234';

--- thu tuc dang xuat----------
CREATE OR REPLACE PROCEDURE kill_session_by_user (
    p_username IN VARCHAR2,
    p_password IN VARCHAR2
)
IS
    v_sid      V$SESSION.SID%TYPE;
    v_serial   V$SESSION.SERIAL#%TYPE;
BEGIN
   
    SELECT sid, serial#
    INTO v_sid, v_serial
    FROM v$session
    WHERE username = UPPER(p_username)  
    AND ROWNUM = 1; 

    EXECUTE IMMEDIATE 'ALTER SYSTEM KILL SESSION ''' || v_sid || ',' || v_serial || ''' IMMEDIATE';
END kill_session_by_user;
/
SELECT object_name, status 
FROM user_objects 
WHERE object_name = 'KILL_SESSION_BY_USER';
GRANT EXECUTE ON kill_session_by_user TO buihungphuong;
GRANT SELECT ON v_$session TO buihungphuong;

SELECT sid, serial#, status, username, program 
FROM v$session 
WHERE username = 'BUIHUNGPHUONG';

GRANT ALTER SYSTEM TO buihungphuong;

ALTER USER BUIHUNGPHUONG DEFAULT TABLESPACE PHUONG;
ALTER USER BUIHUNGPHUONG PROFILE PHUONG222;
