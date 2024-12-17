BEGIN
    XemUser('BUIHUNGPHUONG');
END;
/
--tim tat ca user
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

--tim kiem user
CREATE OR REPLACE PROCEDURE TimUSER(
    v_username IN VARCHAR2,        -- Tham s? ??u vào nh?n tên ng??i dùng
    v_cursor OUT SYS_REFCURSOR     -- Con tr? tr? v? k?t qu?
)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * 
    FROM dba_users
    WHERE username = v_username;  -- Tìm ng??i dùng theo tên
END TimUSER;
SELECT * FROM dba_users WHERE username = 'BUIHUNGPHUONG';
SELECT * FROM dba_users WHERE UPPER(username) = UPPER(v_username);

DECLARE
    v_cursor SYS_REFCURSOR;  -- Bi?n con tr?
    v_username VARCHAR2(50) := 'BUIHUNGPHUONG';  -- Tên ng??i dùng b?n mu?n tìm
    v_user dba_users%ROWTYPE;  -- Bi?n ?? ch?a m?i dòng d? li?u
BEGIN
    -- G?i th? t?c TimUSER
    TimUSER(v_username, v_cursor);

    -- L?y d? li?u t? con tr?
    LOOP
        FETCH v_cursor INTO v_user;  -- L?y d? li?u vào bi?n v_user
        EXIT WHEN v_cursor%NOTFOUND;  -- Thoát kh?i vòng l?p n?u không còn d? li?u

        -- Hi?n th? thông tin t? k?t qu?
        DBMS_OUTPUT.PUT_LINE('Username: ' || v_user.username);
        DBMS_OUTPUT.PUT_LINE('Lock Date: ' || v_user.lock_date);
        DBMS_OUTPUT.PUT_LINE('User ID: ' || v_user.user_id);
    END LOOP;

    -- ?óng con tr? sau khi xong
    CLOSE v_cursor;
END;
/

-- Tat ca cac quyen 
SELECT * FROM DBA_SYS_PRIVS 
CREATE OR REPLACE PROCEDURE XEMQUYEN (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM DBA_SYS_PRIVS;
END XEMQUYEN;


CREATE OR REPLACE PROCEDURE TimQUYENUSER(
    v_username IN VARCHAR2,        -- Tham s? ??u vào nh?n tên ng??i dùng
    v_cursor OUT SYS_REFCURSOR     -- Con tr? tr? v? k?t qu?
)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * 
    FROM DBA_SYS_PRIVS
    WHERE grantee = v_username;  -- Tìm ng??i dùng theo tên
END TimQUYENUSER;


