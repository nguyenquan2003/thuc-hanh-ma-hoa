CREATE OR REPLACE FUNCTION shiftChar(c CHAR, k NUMBER)
RETURN CHAR
IS
  t NUMBER(2);
  shiftedChar CHAR(1);
BEGIN
  t := ASCII('A');
  
  IF REGEXP_LIKE(c, '[A-Z]') THEN
    shiftedChar := CHR(t + MOD((ASCII(c) - t) * k, 26));
    RETURN shiftedChar;
  ELSE
    RETURN c; 
  END IF;
END shiftChar;

CREATE OR REPLACE FUNCTION encryptMultiplicativeCipher(str VARCHAR, k NUMBER)
RETURN VARCHAR
AS
  i NUMBER(2);
  len NUMBER(5);
  kq VARCHAR(100) := '';
  plainText VARCHAR(250);
BEGIN 
  plainText := UPPER(str);
  len := LENGTH(str);

  FOR i IN 1..len LOOP
    kq := kq || shiftChar(SUBSTR(plainText, i, 1), k);
  END LOOP;

  RETURN kq;
END;

CREATE OR REPLACE FUNCTION modInverse(a NUMBER, m NUMBER)
RETURN NUMBER
IS
  x NUMBER;
BEGIN
  FOR x IN 1..m LOOP
    IF (a * x) MOD m = 1 THEN
      RETURN x;
    END IF;
  END LOOP;

  RETURN -1; 
END modInverse;

CREATE OR REPLACE FUNCTION decryptMultiplicativeCipher(str VARCHAR, k NUMBER)
RETURN VARCHAR
AS
  inverseKey NUMBER;
BEGIN 
  inverseKey := modInverse(k, 26);

  IF inverseKey = -1 THEN
    RAISE_APPLICATION_ERROR(-20001, 'Không thể tìm thấy nghịch đảo của khóa');
  END IF;

  RETURN encryptMultiplicativeCipher(str, inverseKey);
END;

CREATE OR REPLACE PROCEDURE encryptMultiplicativeProcedure (
    str IN VARCHAR,
    k IN NUMBER,
    encryptedText OUT VARCHAR
)
IS
  i NUMBER(2);
  len NUMBER(5);
  kq VARCHAR(100) := '';
  plainText VARCHAR(250);
BEGIN 
  plainText := UPPER(str);
  len := LENGTH(str);

  FOR i IN 1..len LOOP
    kq := kq || shiftChar(SUBSTR(plainText, i, 1), k);
  END LOOP;

  encryptedText := kq;
END encryptMultiplicativeProcedure;

CREATE OR REPLACE PROCEDURE decryptMultiplicativeProcedure (
    str IN VARCHAR,
    k IN NUMBER,
    decryptedText OUT VARCHAR
)
IS
  inverseKey NUMBER;
BEGIN 
  inverseKey := modInverse(k, 26);

  IF inverseKey = -1 THEN
    RAISE_APPLICATION_ERROR(-20001, 'Không thể tìm thấy nghịch đảo của khóa');
  END IF;

  encryptMultiplicativeProcedure(str, inverseKey, decryptedText);
END decryptMultiplicativeProcedure;
