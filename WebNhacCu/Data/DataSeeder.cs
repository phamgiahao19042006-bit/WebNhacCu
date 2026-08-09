using CoreDatabase.Models;
using WebNhacCu.Models.EF;

namespace WebNhacCu.Data
{
    public static class DataSeeder
    {
        private static void SeedVaiTro(WebHeThongBanNhacCuContext context)
        {
            if (context.VaiTros.Any())
                return;

            context.VaiTros.AddRange(
                new VaiTro
                {
                    MaVaiTro = "VT001",
                    TenVaiTro = "Admin",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new VaiTro
                {
                    MaVaiTro = "VT002",
                    TenVaiTro = "Nhân viên",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new VaiTro
                {
                    MaVaiTro = "VT003",
                    TenVaiTro = "Khách hàng",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedLoaiSP(WebHeThongBanNhacCuContext context)
        {
            if (context.LoaiSPs.Any())
                return;

            context.LoaiSPs.AddRange(
                new LoaiSP
                {
                    MaLoai = "L001",
                    TenLoai = "Guitar Acoustic",
                    MoTa = "Đàn guitar acoustic",
                    TT = true,
                    MetaTitle = "guitar-acoustic",
                    MetaKeyword = "guitar acoustic",
                    MetaDescription = "Danh mục Guitar Acoustic",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new LoaiSP
                {
                    MaLoai = "L002",
                    TenLoai = "Guitar Electric",
                    MoTa = "Đàn guitar điện",
                    TT = true,
                    MetaTitle = "guitar-electric",
                    MetaKeyword = "guitar electric",
                    MetaDescription = "Danh mục Guitar Electric",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new LoaiSP
                {
                    MaLoai = "L003",
                    TenLoai = "Piano",
                    MoTa = "Đàn piano",
                    TT = true,
                    MetaTitle = "piano",
                    MetaKeyword = "piano",
                    MetaDescription = "Danh mục Piano",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new LoaiSP
                {
                    MaLoai = "L004",
                    TenLoai = "Organ",
                    MoTa = "Đàn organ",
                    TT = true,
                    MetaTitle = "organ",
                    MetaKeyword = "organ",
                    MetaDescription = "Danh mục Organ",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new LoaiSP
                {
                    MaLoai = "L005",
                    TenLoai = "Phụ kiện",
                    MoTa = "Phụ kiện nhạc cụ",
                    TT = true,
                    MetaTitle = "phu-kien",
                    MetaKeyword = "phụ kiện",
                    MetaDescription = "Danh mục phụ kiện",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedThuongHieu(WebHeThongBanNhacCuContext context)
        {
            if (context.ThuongHieus.Any())
                return;

            context.ThuongHieus.AddRange(
                new ThuongHieu
                {
                    MaTH = "TH001",
                    TenTH = "Yamaha",
                    QuocGia = "Nhật Bản",
                    TT = true,
                    MetaTitle = "yamaha",
                    MetaKeyword = "yamaha",
                    MetaDescription = "Thương hiệu Yamaha",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new ThuongHieu
                {
                    MaTH = "TH002",
                    TenTH = "Fender",
                    QuocGia = "Hoa Kỳ",
                    TT = true,
                    MetaTitle = "fender",
                    MetaKeyword = "fender",
                    MetaDescription = "Thương hiệu Fender",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new ThuongHieu
                {
                    MaTH = "TH003",
                    TenTH = "Roland",
                    QuocGia = "Nhật Bản",
                    TT = true,
                    MetaTitle = "roland",
                    MetaKeyword = "roland",
                    MetaDescription = "Thương hiệu Roland",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new ThuongHieu
                {
                    MaTH = "TH004",
                    TenTH = "Casio",
                    QuocGia = "Nhật Bản",
                    TT = true,
                    MetaTitle = "casio",
                    MetaKeyword = "casio",
                    MetaDescription = "Thương hiệu Casio",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new ThuongHieu
                {
                    MaTH = "TH005",
                    TenTH = "Ibanez",
                    QuocGia = "Nhật Bản",
                    TT = true,
                    MetaTitle = "ibanez",
                    MetaKeyword = "ibanez",
                    MetaDescription = "Thương hiệu Ibanez",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedNhaCC(WebHeThongBanNhacCuContext context)
        {
            if (context.NhaCCs.Any())
                return;

            context.NhaCCs.AddRange(
                new NhaCC
                {
                    MaNCC = "NCC001",
                    TenNCC = "Yamaha Việt Nam",
                    SDT = "02838221234",
                    Email = "contact@yamaha.vn",
                    DiaChi = "TP. Hồ Chí Minh",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new NhaCC
                {
                    MaNCC = "NCC002",
                    TenNCC = "Việt Thương Music",
                    SDT = "02439456789",
                    Email = "info@vietthuong.vn",
                    DiaChi = "Hà Nội",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new NhaCC
                {
                    MaNCC = "NCC003",
                    TenNCC = "Minh Thanh Piano",
                    SDT = "02363881234",
                    Email = "support@minhthanh.vn",
                    DiaChi = "Đà Nẵng",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedNhanVien(WebHeThongBanNhacCuContext context)
        {
            if (context.NhanViens.Any())
                return;

            context.NhanViens.AddRange(
                new NhanVien
                {
                    MaNV = "NV001",
                    HoTen = "Nguyễn Văn An",
                    NgaySinh = new DateTime(1998, 5, 20),
                    GioiTinh = "Nam",
                    SDT = "0901234567",
                    Email = "an@gmail.com",
                    DiaChi = "TP. Hồ Chí Minh",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new NhanVien
                {
                    MaNV = "NV002",
                    HoTen = "Trần Minh Đức",
                    NgaySinh = new DateTime(1999, 8, 15),
                    GioiTinh = "Nam",
                    SDT = "0912345678",
                    Email = "duc@gmail.com",
                    DiaChi = "Bình Dương",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new NhanVien
                {
                    MaNV = "NV003",
                    HoTen = "Lê Thị Mai",
                    NgaySinh = new DateTime(2000, 3, 10),
                    GioiTinh = "Nữ",
                    SDT = "0987654321",
                    Email = "mai@gmail.com",
                    DiaChi = "Đồng Nai",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedTaiKhoan(WebHeThongBanNhacCuContext context)
        {
            if (context.TaiKhoans.Any())
                return;

            context.TaiKhoans.AddRange(
                new TaiKhoan
                {
                    MaTK = "TK001",
                    TenDangNhap = "admin",
                    MatKhau = "123456",
                    MaNV = "NV001",
                    MaVaiTro = "VT001",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new TaiKhoan
                {
                    MaTK = "TK002",
                    TenDangNhap = "nhanvien1",
                    MatKhau = "123456",
                    MaNV = "NV002",
                    MaVaiTro = "VT002",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new TaiKhoan
                {
                    MaTK = "TK003",
                    TenDangNhap = "nhanvien2",
                    MatKhau = "123456",
                    MaNV = "NV003",
                    MaVaiTro = "VT002",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        // Hash tạm thời (SHA256, không salt) chỉ phục vụ dữ liệu SEED/TEST bằng System.Security.Cryptography
        // có sẵn trong .NET BCL, không thêm package mới theo đúng phạm vi bước này.
        // Account module triển khai sau cần thống nhất cơ chế hashing chính thức
        // (khuyến nghị PBKDF2/BCrypt kèm salt ngẫu nhiên) trước khi dùng cho đăng ký/đăng nhập thật.
        private static string HashPassword(string plainPassword)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(plainPassword);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes);
        }

        private static void SeedKhachHang(WebHeThongBanNhacCuContext context)
        {
            if (context.KhachHangs.Any())
                return;

            context.KhachHangs.AddRange(
                new KhachHang
                {
                    MaKH = "KH001",
                    TenDangNhap = "quan",
                    MatKhau = HashPassword("Khach@123"),
                    HoTen = "Nguyễn Minh Quân",
                    SDT = "0901111111",
                    Email = "quan@gmail.com",
                    DiaChi = "TP. Hồ Chí Minh",
                    DiemTichLuy = 120,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new KhachHang
                {
                    MaKH = "KH002",
                    TenDangNhap = "bao",
                    MatKhau = HashPassword("Khach@123"),
                    HoTen = "Trần Gia Bảo",
                    SDT = "0902222222",
                    Email = "bao@gmail.com",
                    DiaChi = "Bình Dương",
                    DiemTichLuy = 60,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new KhachHang
                {
                    MaKH = "KH003",
                    TenDangNhap = "linh",
                    MatKhau = HashPassword("Khach@123"),
                    HoTen = "Lê Khánh Linh",
                    SDT = "0903333333",
                    Email = "linh@gmail.com",
                    DiaChi = "Đồng Nai",
                    DiemTichLuy = 200,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new KhachHang
                {
                    MaKH = "KH004",
                    TenDangNhap = "hoanganh",
                    MatKhau = HashPassword("Khach@123"),
                    HoTen = "Phạm Hoàng Anh",
                    SDT = "0904444444",
                    Email = "anh@gmail.com",
                    DiaChi = "Hà Nội",
                    DiemTichLuy = 0,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new KhachHang
                {
                    MaKH = "KH005",
                    TenDangNhap = "huy",
                    MatKhau = HashPassword("Khach@123"),
                    HoTen = "Võ Đức Huy",
                    SDT = "0905555555",
                    Email = "huy@gmail.com",
                    DiaChi = "Đà Nẵng",
                    DiemTichLuy = 35,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedSanPham(WebHeThongBanNhacCuContext context)
        {
            if (context.SanPhams.Any())
                return;

            context.SanPhams.AddRange(
                new SanPham
                {
                    MaSP = "SP001",
                    TenSP = "Yamaha F310",
                    MaLoai = "L001",
                    MaTH = "TH001",
                    GiaNhap = 2800000,
                    DonGia = 3500000,
                    SoLuongTon = 15,
                    HinhAnh = "yamaha-f310.jpg",
                    MoTa = "Đàn Guitar Acoustic Yamaha F310.",
                    TT = true,
                    MetaTitle = "yamaha-f310",
                    MetaKeyword = "yamaha f310, guitar acoustic",
                    MetaDescription = "Yamaha F310 chính hãng.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP002",
                    TenSP = "Yamaha C40",
                    MaLoai = "L001",
                    MaTH = "TH001",
                    GiaNhap = 2500000,
                    DonGia = 3200000,
                    SoLuongTon = 12,
                    HinhAnh = "yamaha-c40.jpg",
                    MoTa = "Đàn Guitar Classic Yamaha C40.",
                    TT = true,
                    MetaTitle = "yamaha-c40",
                    MetaKeyword = "yamaha c40",
                    MetaDescription = "Yamaha C40 chính hãng.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP003",
                    TenSP = "Fender Stratocaster",
                    MaLoai = "L002",
                    MaTH = "TH002",
                    GiaNhap = 15000000,
                    DonGia = 18000000,
                    SoLuongTon = 5,
                    HinhAnh = "fender-stratocaster.jpg",
                    MoTa = "Đàn Guitar Electric Fender.",
                    TT = true,
                    MetaTitle = "fender-stratocaster",
                    MetaKeyword = "fender stratocaster",
                    MetaDescription = "Fender Stratocaster.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP004",
                    TenSP = "Roland FP-30X",
                    MaLoai = "L003",
                    MaTH = "TH003",
                    GiaNhap = 16000000,
                    DonGia = 19500000,
                    SoLuongTon = 8,
                    HinhAnh = "roland-fp30x.jpg",
                    MoTa = "Đàn Piano điện Roland.",
                    TT = true,
                    MetaTitle = "roland-fp30x",
                    MetaKeyword = "roland fp30x",
                    MetaDescription = "Roland FP-30X.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP005",
                    TenSP = "Casio CT-X700",
                    MaLoai = "L004",
                    MaTH = "TH004",
                    GiaNhap = 5500000,
                    DonGia = 6800000,
                    SoLuongTon = 10,
                    HinhAnh = "casio-ctx700.jpg",
                    MoTa = "Đàn Organ Casio.",
                    TT = true,
                    MetaTitle = "casio-ctx700",
                    MetaKeyword = "casio ctx700",
                    MetaDescription = "Casio CT-X700.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP006",
                    TenSP = "Ibanez GRG170DX",
                    MaLoai = "L002",
                    MaTH = "TH005",
                    GiaNhap = 7000000,
                    DonGia = 8500000,
                    SoLuongTon = 7,
                    HinhAnh = "ibanez-grg170dx.jpg",
                    MoTa = "Đàn Guitar Electric Ibanez.",
                    TT = true,
                    MetaTitle = "ibanez-grg170dx",
                    MetaKeyword = "ibanez grg170dx",
                    MetaDescription = "Ibanez GRG170DX.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP007",
                    TenSP = "Capo Alice A007",
                    MaLoai = "L005",
                    MaTH = "TH001",
                    GiaNhap = 80000,
                    DonGia = 120000,
                    SoLuongTon = 50,
                    HinhAnh = "capo-alice.jpg",
                    MoTa = "Capo dành cho Guitar.",
                    TT = true,
                    MetaTitle = "capo-alice",
                    MetaKeyword = "capo guitar",
                    MetaDescription = "Capo Alice.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP008",
                    TenSP = "Dây Guitar Elixir 11052",
                    MaLoai = "L005",
                    MaTH = "TH002",
                    GiaNhap = 250000,
                    DonGia = 350000,
                    SoLuongTon = 40,
                    HinhAnh = "elixir-11052.jpg",
                    MoTa = "Dây đàn Guitar Elixir.",
                    TT = true,
                    MetaTitle = "elixir-11052",
                    MetaKeyword = "elixir guitar string",
                    MetaDescription = "Dây Guitar Elixir.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP009",
                    TenSP = "Yamaha PSR-E373",
                    MaLoai = "L004",
                    MaTH = "TH001",
                    GiaNhap = 6500000,
                    DonGia = 7900000,
                    SoLuongTon = 9,
                    HinhAnh = "yamaha-psre373.jpg",
                    MoTa = "Đàn Organ Yamaha.",
                    TT = true,
                    MetaTitle = "yamaha-psre373",
                    MetaKeyword = "yamaha psr e373",
                    MetaDescription = "Yamaha PSR-E373.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP010",
                    TenSP = "Bao đàn Guitar Acoustic",
                    MaLoai = "L005",
                    MaTH = "TH001",
                    GiaNhap = 180000,
                    DonGia = 250000,
                    SoLuongTon = 25,
                    HinhAnh = "bag-acoustic.jpg",
                    MoTa = "Bao đàn Guitar chống nước.",
                    TT = true,
                    MetaTitle = "bao-dan-guitar",
                    MetaKeyword = "bao guitar",
                    MetaDescription = "Bao đàn Guitar Acoustic.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP011",
                    TenSP = "Yamaha Pacifica 112V",
                    MaLoai = "L002",
                    MaTH = "TH001",
                    GiaNhap = 4800000,
                    DonGia = 5900000,
                    SoLuongTon = 6,
                    HinhAnh = "yamaha-pacifica112v.jpg",
                    MoTa = "Đàn Guitar Electric Yamaha Pacifica.",
                    TT = true,
                    MetaTitle = "yamaha-pacifica112v",
                    MetaKeyword = "yamaha pacifica guitar electric",
                    MetaDescription = "Yamaha Pacifica 112V.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP012",
                    TenSP = "Fender Player Telecaster",
                    MaLoai = "L002",
                    MaTH = "TH002",
                    GiaNhap = 17500000,
                    DonGia = 21000000,
                    SoLuongTon = 4,
                    HinhAnh = "fender-player-telecaster.jpg",
                    MoTa = "Đàn Guitar Electric Fender Telecaster.",
                    TT = true,
                    MetaTitle = "fender-player-telecaster",
                    MetaKeyword = "fender telecaster guitar",
                    MetaDescription = "Fender Player Telecaster.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP013",
                    TenSP = "Ibanez AEG50 Guitar Acoustic",
                    MaLoai = "L001",
                    MaTH = "TH005",
                    GiaNhap = 3400000,
                    DonGia = 4200000,
                    SoLuongTon = 11,
                    HinhAnh = "ibanez-aeg50.jpg",
                    MoTa = "Đàn Guitar Acoustic Ibanez AEG50.",
                    TT = true,
                    MetaTitle = "ibanez-aeg50",
                    MetaKeyword = "ibanez aeg50 guitar acoustic",
                    MetaDescription = "Ibanez AEG50.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP014",
                    TenSP = "Casio PX-160 Privia",
                    MaLoai = "L003",
                    MaTH = "TH004",
                    GiaNhap = 10200000,
                    DonGia = 12500000,
                    SoLuongTon = 5,
                    HinhAnh = "casio-px160.jpg",
                    MoTa = "Đàn Piano điện Casio Privia PX-160.",
                    TT = true,
                    MetaTitle = "casio-px160",
                    MetaKeyword = "casio px160 piano",
                    MetaDescription = "Casio PX-160 Privia.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP015",
                    TenSP = "Roland Juno DS61",
                    MaLoai = "L004",
                    MaTH = "TH003",
                    GiaNhap = 18000000,
                    DonGia = 22000000,
                    SoLuongTon = 3,
                    HinhAnh = "roland-juno-ds61.jpg",
                    MoTa = "Đàn Organ Roland Juno DS61.",
                    TT = true,
                    MetaTitle = "roland-juno-ds61",
                    MetaKeyword = "roland juno organ",
                    MetaDescription = "Roland Juno DS61.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP016",
                    TenSP = "Yamaha YPT-270",
                    MaLoai = "L004",
                    MaTH = "TH001",
                    GiaNhap = 2600000,
                    DonGia = 3200000,
                    SoLuongTon = 14,
                    HinhAnh = "yamaha-ypt270.jpg",
                    MoTa = "Đàn Organ Yamaha YPT-270.",
                    TT = true,
                    MetaTitle = "yamaha-ypt270",
                    MetaKeyword = "yamaha ypt270 organ",
                    MetaDescription = "Yamaha YPT-270.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP017",
                    TenSP = "Fender Acoustasonic Guitar",
                    MaLoai = "L001",
                    MaTH = "TH002",
                    GiaNhap = 24000000,
                    DonGia = 28000000,
                    SoLuongTon = 2,
                    HinhAnh = "fender-acoustasonic.jpg",
                    MoTa = "Đàn Guitar Acoustic Fender Acoustasonic.",
                    TT = true,
                    MetaTitle = "fender-acoustasonic",
                    MetaKeyword = "fender acoustasonic guitar",
                    MetaDescription = "Fender Acoustasonic Guitar.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP018",
                    TenSP = "Dây đàn Guitar D'Addario EJ16",
                    MaLoai = "L005",
                    MaTH = "TH005",
                    GiaNhap = 180000,
                    DonGia = 220000,
                    SoLuongTon = 60,
                    HinhAnh = "daddario-ej16.jpg",
                    MoTa = "Dây đàn Guitar D'Addario EJ16.",
                    TT = true,
                    MetaTitle = "daddario-ej16",
                    MetaKeyword = "daddario guitar string",
                    MetaDescription = "Dây đàn Guitar D'Addario EJ16.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP019",
                    TenSP = "Móng gảy Guitar Dunlop (bộ 12 cái)",
                    MaLoai = "L005",
                    MaTH = "TH002",
                    GiaNhap = 30000,
                    DonGia = 45000,
                    SoLuongTon = 100,
                    HinhAnh = "dunlop-pick-set.jpg",
                    MoTa = "Bộ móng gảy Guitar Dunlop 12 cái.",
                    TT = true,
                    MetaTitle = "dunlop-pick-set",
                    MetaKeyword = "dunlop guitar pick",
                    MetaDescription = "Móng gảy Guitar Dunlop.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP020",
                    TenSP = "Yamaha Silent Guitar SLG200S",
                    MaLoai = "L001",
                    MaTH = "TH001",
                    GiaNhap = 12500000,
                    DonGia = 15500000,
                    SoLuongTon = 0,
                    HinhAnh = "yamaha-slg200s.jpg",
                    MoTa = "Đàn Guitar Acoustic Yamaha Silent SLG200S (đang tạm hết hàng).",
                    TT = true,
                    MetaTitle = "yamaha-slg200s",
                    MetaKeyword = "yamaha silent guitar",
                    MetaDescription = "Yamaha Silent Guitar SLG200S.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new SanPham
                {
                    MaSP = "SP021",
                    TenSP = "Casio CTK-3500 (ngừng kinh doanh)",
                    MaLoai = "L004",
                    MaTH = "TH004",
                    GiaNhap = 3600000,
                    DonGia = 4500000,
                    SoLuongTon = 0,
                    HinhAnh = "casio-ctk3500.jpg",
                    MoTa = "Đàn Organ Casio CTK-3500 đã ngừng kinh doanh.",
                    TT = false,
                    MetaTitle = "casio-ctk3500",
                    MetaKeyword = "casio ctk3500 organ",
                    MetaDescription = "Casio CTK-3500.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                }
            );

            context.SaveChanges();
        }

        private static void SeedKhuyenMai(WebHeThongBanNhacCuContext context)
        {
            if (context.KhuyenMais.Any())
                return;

            context.KhuyenMais.AddRange(
                new KhuyenMai
                {
                    MaKM = "KM001",
                    TenKhuyenMai = "Giảm giá Guitar mùa hè",
                    LoaiGiam = "Phần trăm",
                    GiaTriGiam = 10,
                    NgayBatDau = new DateTime(2026, 1, 1),
                    NgayKetThuc = new DateTime(2026, 12, 31),
                    DieuKienApDung = "Áp dụng cho các sản phẩm Guitar.",
                    TT = true,
                    MetaTitle = "giam-gia-guitar-mua-he",
                    MetaKeyword = "khuyến mãi guitar",
                    MetaDescription = "Giảm 10% cho các sản phẩm Guitar.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new KhuyenMai
                {
                    MaKM = "KM002",
                    TenKhuyenMai = "Khuyến mãi Piano",
                    LoaiGiam = "Số tiền",
                    GiaTriGiam = 1000000,
                    NgayBatDau = new DateTime(2026, 1, 1),
                    NgayKetThuc = new DateTime(2026, 12, 31),
                    DieuKienApDung = "Giảm trực tiếp khi mua Piano.",
                    TT = true,
                    MetaTitle = "khuyen-mai-piano",
                    MetaKeyword = "khuyến mãi piano",
                    MetaDescription = "Giảm trực tiếp 1.000.000 VNĐ khi mua Piano.",
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedCTKhuyenMai(WebHeThongBanNhacCuContext context)
        {
            if (context.CTKhuyenMais.Any())
                return;

            context.CTKhuyenMais.AddRange(
                new CTKhuyenMai
                {
                    MaKM = "KM001",
                    MaSP = "SP001", // Yamaha F310
                    HeSo = 0.9m,
                    SoLuong = 20,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTKhuyenMai
                {
                    MaKM = "KM001",
                    MaSP = "SP002", // Yamaha C40
                    HeSo = 0.9m,
                    SoLuong = 15,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTKhuyenMai
                {
                    MaKM = "KM001",
                    MaSP = "SP003", // Fender Stratocaster
                    HeSo = 0.9m,
                    SoLuong = 5,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTKhuyenMai
                {
                    MaKM = "KM002",
                    MaSP = "SP004", // Roland FP-30X
                    HeSo = 1000000m,
                    SoLuong = 3,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedPhieuNhap(WebHeThongBanNhacCuContext context)
        {
            if (context.PhieuNhaps.Any())
                return;

            context.PhieuNhaps.AddRange(
                new PhieuNhap
                {
                    MaPN = "PN001",
                    MaNCC = "NCC001",
                    NgayNhap = new DateTime(2026, 1, 10),
                    TongTien = 117000000m,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new PhieuNhap
                {
                    MaPN = "PN002",
                    MaNCC = "NCC002",
                    NgayNhap = new DateTime(2026, 2, 15),
                    TongTien = 75000000m,
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                }
            );

            context.SaveChanges();
        }

        private static void SeedCTPhieuNhap(WebHeThongBanNhacCuContext context)
        {
            if (context.CTPhieuNhaps.Any())
                return;

            context.CTPhieuNhaps.AddRange(
                // Phiếu nhập PN001
                new CTPhieuNhap
                {
                    MaPN = "PN001",
                    MaSP = "SP001",
                    SoLuong = 15,
                    DonGiaNhap = 2800000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTPhieuNhap
                {
                    MaPN = "PN001",
                    MaSP = "SP002",
                    SoLuong = 12,
                    DonGiaNhap = 2500000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTPhieuNhap
                {
                    MaPN = "PN001",
                    MaSP = "SP009",
                    SoLuong = 9,
                    DonGiaNhap = 6500000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                // Phiếu nhập PN002
                new CTPhieuNhap
                {
                    MaPN = "PN002",
                    MaSP = "SP003",
                    SoLuong = 5,
                    DonGiaNhap = 15000000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTPhieuNhap
                {
                    MaPN = "PN002",
                    MaSP = "SP006",
                    SoLuong = 7,
                    DonGiaNhap = 7000000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new CTPhieuNhap
                {
                    MaPN = "PN002",
                    MaSP = "SP008",
                    SoLuong = 40,
                    DonGiaNhap = 250000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedHoaDon(WebHeThongBanNhacCuContext context)
        {
            if (context.HoaDons.Any())
                return;

            context.HoaDons.AddRange(
                new HoaDon
                {
                    MaHD = "HD001",
                    NgayLap = new DateTime(2026, 3, 10),
                    MaKH = "KH001",
                    MaNV = "NV001",
                    TongTien = 3500000m,
                    GiamGia = 350000m,
                    ThanhTien = 3150000m,
                    PhuongThucTT = "Tiền mặt",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new HoaDon
                {
                    MaHD = "HD002",
                    NgayLap = new DateTime(2026, 3, 15),
                    MaKH = "KH002",
                    MaNV = "NV002",
                    TongTien = 6800000m,
                    GiamGia = 0m,
                    ThanhTien = 6800000m,
                    PhuongThucTT = "Chuyển khoản",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },
                new HoaDon
                {
                    MaHD = "HD003",
                    NgayLap = new DateTime(2026, 3, 20),
                    MaKH = "KH003",
                    MaNV = "NV003",
                    TongTien = 18350000m,
                    GiamGia = 1000000m,
                    ThanhTien = 17350000m,
                    PhuongThucTT = "Thẻ ngân hàng",
                    TT = true,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                });

            context.SaveChanges();
        }

        private static void SeedCTHoaDon(WebHeThongBanNhacCuContext context)
        {
            if (context.CTHoaDons.Any())
                return;

            context.CTHoaDons.AddRange(
                // HD001
                new CTHoaDon
                {
                    MaHD = "HD001",
                    MaSP = "SP001",
                    SoLuong = 1,
                    DonGia = 3500000m,
                    ThanhTien = 3500000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                // HD002
                new CTHoaDon
                {
                    MaHD = "HD002",
                    MaSP = "SP005",
                    SoLuong = 1,
                    DonGia = 6800000m,
                    ThanhTien = 6800000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                // HD003
                new CTHoaDon
                {
                    MaHD = "HD003",
                    MaSP = "SP003",
                    SoLuong = 1,
                    DonGia = 18000000m,
                    ThanhTien = 18000000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                },

                new CTHoaDon
                {
                    MaHD = "HD003",
                    MaSP = "SP008",
                    SoLuong = 1,
                    DonGia = 350000m,
                    ThanhTien = 350000m,
                    CreatedBy = "Seeder",
                    UpdatedBy = "Seeder"
                }
            );

            context.SaveChanges();
        }

        public static void Seed(WebHeThongBanNhacCuContext context)
        {
            SeedVaiTro(context);
            SeedLoaiSP(context);
            SeedThuongHieu(context);
            SeedNhaCC(context);
            SeedNhanVien(context);
            SeedTaiKhoan(context);
            SeedKhachHang(context);
            SeedSanPham(context);
            SeedKhuyenMai(context);
            SeedCTKhuyenMai(context);
            SeedPhieuNhap(context);
            SeedCTPhieuNhap(context);
            SeedHoaDon(context);
            SeedCTHoaDon(context);
        }
    }
}