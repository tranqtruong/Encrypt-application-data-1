﻿/*---------------------------------------------------------- 
MASV: N18DCAT100
HO TEN: TRẦN QUỐC TRƯỢNG
LAB: 04
NGAY: 9/17/2021
----------------------------------------------------------*/ 
CREATE PROC SP_INS_ENCRYPT_SINHVIEN 
	@MASV NVARCHAR(20), 
	@HOTEN NVARCHAR(100), 
	@NGAYSINH DATETIME ,
	@DIACHI NVARCHAR(200), 
	@MALOP VARCHAR(20), 
	@TENDN NVARCHAR(100), 
	@MATKHAU VARBINARY(MAX)
AS
	INSERT INTO SINHVIEN
	VALUES (@MASV, @HOTEN, @NGAYSINH, @DIACHI, @MALOP, @TENDN, @MATKHAU)
GO

EXEC SP_INS_ENCRYPT_SINHVIEN 'SV01', 'TRAN QUOC TRUONG', '2000-11-20', 'QUAN 9 HCM', 'AT02-N', 'qwertytrg', 0x7C4A8D09CA3762AF61E59520943DC26494F8941B

SELECT * FROM SINHVIEN
sELECT * FROM NHANVIEN
SELECT * FROM LOP

DROP PROC SP_INS_ENCRYPT_SINHVIEN 

INSERT INTO LOP VALUES('AT02-N', 'AN TOAN THONG TIN 2', 'NV01')