SELECT * 
FROM dba_users 
WHERE username = 'BUIHUNGPHUONG';

SELECT * 
FROM DBA_TAB_PRIVS 
WHERE GRANTEE = 'BUIHUNGPHUONG';
SELECT * FROM DBA_SYS_PRIVS 
WHERE GRANTEE = 'BUIHUNGPHUONG';
CREATE OR REPLACE PROCEDURE XemUser (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM dba_users;
END XemUser;



