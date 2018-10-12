--drop database PhanPhoiVeSo
create database PhanPhoiVeSo
go
use PhanPhoiVeSo

create table LoaiVeSo
(
	LoaiVeSoId int IDENTITY(1,1) PRIMARY KEY,
	TenTinh nvarchar(120) not null

)

create table DaiLy
(
	DaiLyId int IDENTITY(1,1) PRIMARY KEY,
	TenDaiLy nvarchar(120) not null
)

create table PhieuDangKy
(
	PhieuDangKyId int IDENTITY(1,1) PRIMARY KEY,
	LoaiVeSoId int FOREIGN KEY REFERENCES LoaiVeSo(LoaiVeSoId) not null,
	DaiLyId int FOREIGN KEY REFERENCES DaiLy(DaiLyId) not null,
	NgayDangKy date not null,
	SLDangKy decimal not null
)

create table PhieuPhatHanh
(	PhieuPhatHanhId int IDENTITY(1,1) PRIMARY KEY,
	DaiLyId int FOREIGN KEY REFERENCES DaiLy(DaiLyId) not null,
	LoaiVeSoId int FOREIGN KEY REFERENCES LoaiVeSo(LoaiVeSoId) not null,
	NgayPhat date not null,
	SLPhat decimal not null,
	SLBanDuoc decimal
	
)
create table Giai
(
	GiaiId int IDENTITY(1,1) PRIMARY KEY,
	TenGiai nvarchar(120),
	TienThuong decimal
)
create table KetQuaXoSo
(
	KetQuaXoSoId int IDENTITY(1,1) PRIMARY KEY,
	LoaiVeSoId int FOREIGN KEY REFERENCES LoaiVeSo(LoaiVeSoId) not null,
	NgayXoSo date not null,
	GiaiId int FOREIGN KEY REFERENCES Giai(GiaiId) not null,
	SoTrung int not null
)	
create table PhieuThu
(
	PhieuThuId int IDENTITY(1,1) PRIMARY KEY,
	DaiLyId int FOREIGN KEY REFERENCES DaiLy(DaiLyId) not null,
	NgayThu date not null,
	TienThu decimal not null
)

INSERT into LoaiVeSo(  TenTinh)
VALUES  (N'Bình Dương'),
		(N'Vĩnh Long'),
		(N'TP.Hồ Chí Minh'),
		(N'Vũng Tàu'),
		(N'Sóc Trăng'),
		(N'Hà Nội')

INSERT into DaiLy(  TenDaiLy)
VALUES  (N'Phúc Long chi nhánh 1'),
		(N'Phúc Long chi nhánh 2'),
		(N'Phúc Long chi nhánh 3'),
		(N'Long An chi nhánh 1'),
		(N'Long An chi nhánh 2'),
		(N'Long An chi nhánh 3')

INSERT into PhieuDangKy( LoaiVeSoId, DaiLyId,NgayDangKy,SLDangKy)
VALUES  (3,1,'2018/10/02',100),
		(3,2,'2018/10/02',120),
		(3,3,'2018/10/02',150),
		(3,4,'2018/10/02',80),
		(3,5,'2018/10/02',100),
		(3,6,'2018/10/02',120)

INSERT into PhieuPhatHanh(  DaiLyId,LoaiVeSoId,NgayPhat,SLPhat,SLBanDuoc)
VALUES  (1,3,'2018/11/02',80,80),
		(1,3,'2018/11/03',100,80),
		(1,3,'2018/11/12',80,70),
		(2,3,'2018/11/15',120,100),
		(2,3,'2018/11/20',100,90),
		(2,3,'2018/10/10',95,95),
		(2,3,'2018/10/20',95,90),
		(2,3,'2018/10/21',91,91),
		(3,3,'2018/10/04',150,150),
		(3,3,'2018/10/05',150,120),
		(3,3,'2018/10/06',135,135),
		(3,3,'2018/10/07',135,110),
		(4,3,'2018/10/22',80,70),
		(4,3,'2018/10/23',70,60),
		(5,3,'2018/10/24',100,90),
		(5,3,'2018/10/25',90,80),
		(5,3,'2018/10/26',85,85),
		(6,3,'2018/10/27',120,120)

		select * from PhieuDangKy
