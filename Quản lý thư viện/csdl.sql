  CREATE TABLE sach (
    MaSach INT PRIMARY KEY ,
    TenSach NVARCHAR2(255) NOT NULL,
    MaTacGia INT,
    TheLoai NVARCHAR2(100),
    GiaTien DECIMAL(10, 2),
    SoLuong INT,
    NamXuatBan INT,
    NhaXuatBan VARCHAR(255),
    FOREIGN KEY (MaTacGia) REFERENCES tacgia(MaTacGia)
);

CREATE TABLE tacgia (
    MaTacGia INT PRIMARY KEY,
    TenTacGia NVARCHAR2(255) NOT NULL,
    QuocTich NVARCHAR2(100)
);


CREATE TABLE nhanvien (
    MaNhanVien INT PRIMARY KEY ,
    TenNhanVien NVARCHAR2(255) NOT NULL,
    ChucVu NVARCHAR2(100),
    NgaySinh DATE,
    SDT VARCHAR(20),
    Email VARCHAR(100),
    DiaChi NVARCHAR2(255)
);

CREATE TABLE khachhang (
    MaKhachHang INT PRIMARY KEY ,
    TenKhachHang NVARCHAR2(255) NOT NULL,
    SDT VARCHAR(20),
    Email VARCHAR(100),
    DiaChi NVARCHAR2(255)
);


CREATE TABLE hoadon (
    MaHoaDon INT PRIMARY KEY ,
    NgayLap DATE,
    MaKhachHang INT,
    MaNhanVien INT,
    TongTien DECIMAL(15, 2),
    FOREIGN KEY (MaKhachHang) REFERENCES khachhang(MaKhachHang),
    FOREIGN KEY (MaNhanVien) REFERENCES nhanvien(MaNhanVien)
);


CREATE TABLE chitiethoadon (
    MaHoaDon INT,
    MaSach INT,
    SoLuong INT,
    DonGia DECIMAL(10, 2),
    PRIMARY KEY (MaHoaDon, MaSach),
    FOREIGN KEY (MaHoaDon) REFERENCES hoadon(MaHoaDon),
    FOREIGN KEY (MaSach) REFERENCES sach(MaSach)
);

ALTER TABLE sach ADD CONSTRAINT chk_giatien CHECK (GiaTien > 0);
ALTER TABLE sach ADD CONSTRAINT chk_soluong CHECK (SoLuong >= 0);

ALTER TABLE chitiethoadon ADD CONSTRAINT chk_soluong_cthd CHECK (SoLuong > 0);
ALTER TABLE chitiethoadon ADD CONSTRAINT chk_dongia_cthd CHECK (DonGia > 0);

ALTER TABLE sach MODIFY TenSach NVARCHAR2(255) NOT NULL;
ALTER TABLE sach MODIFY MaTacGia INT NOT NULL;


ALTER TABLE sach MODIFY SoLuong INT DEFAULT 0;


-- tacgia
INSERT INTO tacgia (MaTacGia, TenTacGia, QuocTich) VALUES (1, 'Nguy?n Nh?t Ánh', 'Vi?t Nam');
INSERT INTO tacgia (MaTacGia, TenTacGia, QuocTich) VALUES (2, 'J.K. Rowling', 'Anh');
INSERT INTO tacgia (MaTacGia, TenTacGia, QuocTich) VALUES (3, 'Stephen King', 'M?');
INSERT INTO tacgia (MaTacGia, TenTacGia, QuocTich) VALUES (4, 'George Orwell', 'Anh');
INSERT INTO tacgia (MaTacGia, TenTacGia, QuocTich) VALUES (5, 'Haruki Murakami', 'Nh?t B?n');
-- sach
INSERT INTO sach (MaSach, TenSach, MaTacGia, TheLoai, GiaTien, SoLuong, NamXuatBan, NhaXuatBan) 
VALUES (1, 'Tôi Th?y Hoa Vàng Trên C? Xanh', 1, 'Ti?u thuy?t', 120000, 50, 2010, 'NXB Tr?');
INSERT INTO sach (MaSach, TenSach, MaTacGia, TheLoai, GiaTien, SoLuong, NamXuatBan, NhaXuatBan) 
VALUES (2, 'Harry Potter và Hòn ?á Phù Th?y', 2, 'Fantasy', 250000, 100, 1997, 'Bloomsbury');
INSERT INTO sach (MaSach, TenSach, MaTacGia, TheLoai, GiaTien, SoLuong, NamXuatBan, NhaXuatBan) 
VALUES (3, 'The Shining', 3, 'Kinh d?', 180000, 20, 1977, 'Doubleday');
INSERT INTO sach (MaSach, TenSach, MaTacGia, TheLoai, GiaTien, SoLuong, NamXuatBan, NhaXuatBan) 
VALUES (4, '1984', 4, 'Chính tr? vi?n t??ng', 150000, 60, 1949, 'Secker & Warburg');
INSERT INTO sach (MaSach, TenSach, MaTacGia, TheLoai, GiaTien, SoLuong, NamXuatBan, NhaXuatBan) 
VALUES (5, 'Kafka Bên B? Bi?n', 5, 'Ti?u thuy?t', 200000, 30, 2002, 'Shinchosha');
--nhanvien
INSERT INTO nhanvien (MaNhanVien, TenNhanVien, ChucVu, NgaySinh, SDT, Email, DiaChi) 
VALUES (1, 'Nguy?n V?n A', 'Qu?n lý', TO_DATE('1985-05-12', 'YYYY-MM-DD'), '0901234567', 'a.nguyen@example.com', 'Hà N?i');

INSERT INTO nhanvien (MaNhanVien, TenNhanVien, ChucVu, NgaySinh, SDT, Email, DiaChi) 
VALUES (2, 'Tr?n Th? B', 'Nhân viên bán hàng', TO_DATE('1990-07-22', 'YYYY-MM-DD'), '0912345678', 'b.tran@example.com', 'H? Chí Minh');

INSERT INTO nhanvien (MaNhanVien, TenNhanVien, ChucVu, NgaySinh, SDT, Email, DiaChi) 
VALUES (3, 'Lê V?n C', 'Nhân viên kho', TO_DATE('1987-09-10', 'YYYY-MM-DD'), '0987654321', 'c.le@example.com', '?à N?ng');

INSERT INTO nhanvien (MaNhanVien, TenNhanVien, ChucVu, NgaySinh, SDT, Email, DiaChi) 
VALUES (4, 'Ph?m Th? D', 'Nhân viên thu ngân', TO_DATE('1992-01-15', 'YYYY-MM-DD'), '0945678901', 'd.pham@example.com', 'C?n Th?');

INSERT INTO nhanvien (MaNhanVien, TenNhanVien, ChucVu, NgaySinh, SDT, Email, DiaChi) 
VALUES (5, 'Ngô V?n E', 'Nhân viên bán hàng', TO_DATE('1988-11-05', 'YYYY-MM-DD'), '0976543210', 'e.ngo@example.com', 'H?i Phòng');

--khachhang
INSERT INTO khachhang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi) 
VALUES (1, 'Tr?n V?n A', '0901111222', 'a.tran@example.com', 'Hà N?i');

INSERT INTO khachhang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi) 
VALUES (2, 'Lê Th? B', '0902222333', 'b.le@example.com', 'H? Chí Minh');

INSERT INTO khachhang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi) 
VALUES (3, 'Ph?m V?n C', '0903333444', 'c.pham@example.com', '?à N?ng');

INSERT INTO khachhang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi) 
VALUES (4, 'Nguy?n Th? D', '0904444555', 'd.nguyen@example.com', 'C?n Th?');

INSERT INTO khachhang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi) 
VALUES (5, 'Hoàng V?n E', '0905555666', 'e.hoang@example.com', 'H?i Phòng');
--Hoadon

INSERT INTO hoadon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, TongTien) 
VALUES (1, TO_DATE('2024-10-01', 'YYYY-MM-DD'), 1, 1, 370000);

INSERT INTO hoadon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, TongTien) 
VALUES (2, TO_DATE('2024-10-02', 'YYYY-MM-DD'), 2, 2, 250000);

INSERT INTO hoadon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, TongTien) 
VALUES (3, TO_DATE('2024-10-03', 'YYYY-MM-DD'), 3, 3, 400000);

INSERT INTO hoadon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, TongTien) 
VALUES (4, TO_DATE('2024-10-04', 'YYYY-MM-DD'), 4, 4, 150000);

INSERT INTO hoadon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, TongTien) 
VALUES (5, TO_DATE('2024-10-05', 'YYYY-MM-DD'), 5, 5, 600000);

--chitiethoadon

INSERT INTO chitiethoadon (MaHoaDon, MaSach, SoLuong, DonGia) 
VALUES (1, 1, 2, 120000);

INSERT INTO chitiethoadon (MaHoaDon, MaSach, SoLuong, DonGia) 
VALUES (2, 2, 1, 250000);

INSERT INTO chitiethoadon (MaHoaDon, MaSach, SoLuong, DonGia) 
VALUES (3, 3, 2, 180000);

INSERT INTO chitiethoadon (MaHoaDon, MaSach, SoLuong, DonGia) 
VALUES (4, 4, 1, 150000);

INSERT INTO chitiethoadon (MaHoaDon, MaSach, SoLuong, DonGia) 
VALUES (5, 5, 3, 200000);

select * from nguoidung

--lay du lieu sach
CREATE OR REPLACE FUNCTION GetBooks RETURN SYS_REFCURSOR IS
    v_cursor SYS_REFCURSOR;
BEGIN
    OPEN v_cursor FOR
        SELECT MaSach, TenSach, TheLoai, GiaTien, SoLuong, NamXuatBan, MaTacGia, NhaXuatBan FROM sach;
    RETURN v_cursor;
END;
/

SHOW ERRORS FUNCTION GetBooks;

drop FUNCTION GetBooks;


SELECT MaSach, TenSach, TheLoai, GiaTien, SoLuong, NamXuatBan, MaTacGia, NhaXuatBan FROM sach;


CREATE OR REPLACE PROCEDURE LoadSachData (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM sach;
END LoadSachData;

VAR v_cursor REFCURSOR;


EXEC GetAllBooks(:v_cursor);


PRINT v_cursor;


SELECT column_name, data_type 
FROM user_tab_columns 
WHERE table_name = 'SACH';



---------------------------- HOADON-----------------------
--------------hoadon------------------
CREATE OR REPLACE PROCEDURE GetAllHoaDons (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM HOADON;
END GetAllHoaDons;


--------------------------------NHANVIEN------------------------------
CREATE OR REPLACE PROCEDURE GetAllNhanViens (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM NHANVIEN;
END GetAllNhanViens;
---------------------------------TACGIA------------
CREATE OR REPLACE PROCEDURE GetAllTacGias (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM TACGIA;
END GetAllTacGias;

---------------------------------CHITIETHOADON------------
CREATE OR REPLACE PROCEDURE GetAllChiTietHoaDons (v_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN v_cursor FOR
    SELECT * FROM buihungphuong.CHITIETHOADON;
END GetAllChiTietHoaDons;

------------------------THEM KHACH HANG-------------
CREATE OR REPLACE PROCEDURE InsertKhachHang (
    p_MaKhachHang IN khachhang.MaKhachHang%TYPE,
    p_TenKhachHang IN khachhang.TenKhachHang%TYPE,
    p_SDT IN khachhang.SDT%TYPE,
    p_Email IN khachhang.Email%TYPE,
    p_DiaChi IN khachhang.DiaChi%TYPE
) AS
BEGIN
    INSERT INTO khachhang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi)
    VALUES (p_MaKhachHang, p_TenKhachHang, p_SDT, p_Email, p_DiaChi);
    
    COMMIT;
END;

-----------------------------------SUA KHACH HANG---------------------
CREATE OR REPLACE PROCEDURE UpdateKhachHang (
    p_MaKhachHang IN khachhang.MaKhachHang%TYPE,
    p_TenKhachHang IN khachhang.TenKhachHang%TYPE DEFAULT NULL,
    p_SDT IN khachhang.SDT%TYPE DEFAULT NULL,
    p_Email IN khachhang.Email%TYPE DEFAULT NULL,
    p_DiaChi IN khachhang.DiaChi%TYPE DEFAULT NULL
) AS
BEGIN
    UPDATE khachhang
    SET 
        TenKhachHang = NVL(p_TenKhachHang, TenKhachHang),
        SDT = NVL(p_SDT, SDT),
        Email = NVL(p_Email, Email),
        DiaChi = NVL(p_DiaChi, DiaChi)
    WHERE MaKhachHang = p_MaKhachHang;

    IF SQL%ROWCOUNT = 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Không tìm th?y khách hàng v?i mã này.');
    END IF;

    COMMIT;
END;
/

---------------------------XOA KHACH HANG---------------
CREATE OR REPLACE PROCEDURE DeleteKhachHang (
    p_MaKhachHang IN khachhang.MaKhachHang%TYPE
) AS
BEGIN
    DELETE FROM khachhang
    WHERE MaKhachHang = p_MaKhachHang;

    IF SQL%ROWCOUNT = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Không tìm th?y khách hàng v?i mã này.');
    END IF;

    COMMIT;
END;
/


