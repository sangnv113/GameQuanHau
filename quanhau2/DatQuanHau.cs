using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanhau2
{
    public partial class DatQuanHau : Form
    {
        Player May = new Player(0,Image.FromFile(Application.StartupPath + "\\Resources\\hauden.png"), Image.FromFile(Application.StartupPath
            + "\\Resources\\duongden.png"), "Máy", new PictureBox(), Image.FromFile(Application.StartupPath + "\\Resources\\danhdauden.png"), new List<PictureBox>(), 0,new Stack<Point>());//1
        Player Nguoi_Choi = new Player(0, Image.FromFile(Application.StartupPath + "\\Resources\\hautrang.png"), Image.FromFile(Application.StartupPath + "\\Resources\\duongtrang.png"), "Người", new PictureBox(),
            Image.FromFile(Application.StartupPath + "\\Resources\\danhdautrang.png"), new List<PictureBox>(), 0, new Stack<Point>());//2
        //bool chayTiep = true;
        bool boolDangChay = true;
        int diemBatDau; int bienChaySauKhiLap = 0; int trung;
        int demTongSoChuSo = 0;
        bool bTrungRoi;
        private List<List<PictureBox>> Ma_Tran;
        int[] mangKiemSoatCot = new int[8];
        int[] mangLuuLai = new int[8];
        int[] mangDuongCheoChinh = new int[100];//dấu huyền
        int[] mangDuongCheoPhu = new int[100];//dấu sắc
        int[] mangInRoi = new int[500];
        bool[,] Ma_Tran_Da_Chon = new bool[8, 8];
        private bool bKiemTra;

        public DatQuanHau()
        {
            InitializeComponent();
            Ve_ban_co();
            KhoiTaoGiaTriBanCo();
        }

        private void DatQuanHau_Load(object sender, EventArgs e)
        {
            if (!rbnDatHien.Checked)
            {
                for (int i = 0; i < 8; i++)
                {
                    Ma_Tran[0][i].Image = May.Anh_Duong_Di;
                }
            }
        }
        void KhoiTaoGiaTriBanCo()
        {
            for (int i = 0; i < 8; i++)
            {
                mangKiemSoatCot[i] = 1;//tất cả các cột đều có thể đặt hậu
                //chayTiep[i] = false;
            }
            for (int i = 0; i <= 15; i++)
            {
                mangDuongCheoChinh[i] = 1;
                mangDuongCheoPhu[i] = 1;
            }

        }
        bool KiemTra(int x2, int y2)
        {
            for (int i = 0; i < x2; i++)
            {
                if (mangLuuLai[i] == y2 || Math.Abs(i - x2) == Math.Abs(mangLuuLai[i] - y2))
                {
                    return false;
                }
            }
            return true;
        }
        public void Ve_ban_co()
        {
            Ma_Tran = new List<List<PictureBox>>();
            PictureBox pic_cu = new PictureBox()
            {
                Width = 0,
                Location = new Point(0, 0)
            };
            for (int k = 0; k < Hang_So.chieucao; k++)
            {
                Ma_Tran.Add(new List<PictureBox>());
                for (int i = 0; i < Hang_So.chieungang; i++)// o tren da add 1
                {
                    PictureBox pic = new PictureBox()
                    {
                        Width = Hang_So.chieu_rong_o,
                        Height = Hang_So.chieu_cao_o,
                        Location = new Point(pic_cu.Location.X + pic_cu.Width, pic_cu.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = k.ToString()//xac dinh hang cho PictureBox
                    };
                    if (k % 2 == 0 && i % 2 == 0 || k % 2 != 0 && i % 2 != 0)
                    {
                        pic.BackColor = Color.Blue;
                    }

                    pic.Click += pic_Click;
                    pnlBan_Co.Controls.Add(pic);
                    pic_cu = pic;
                    Ma_Tran[k].Add(pic);//them vao hang thu k;
                }
                pic_cu.Location = new Point(0, pic_cu.Location.Y + Hang_So.chieu_cao_o);
                pic_cu.Width = 0;
                pic_cu.Height = 0;
            }

        }

        private void pic_Click(object sender, EventArgs e)
        {
            if (rbnDatHien.Checked == true)
            {
                Reset();
                boolDangChay = true;
                PictureBox pic = sender as PictureBox;
                Point diemHienTai = Lay_Toa_do(pic);
                if (diemHienTai.Y == mangLuuLai[0])//chạy trường hợp cũ nhưng cách khác
                {
                    mangKiemSoatCot[mangLuuLai[0]] = 0;
                    ThuDat(1);
                }
                else
                {
                    Array.Clear(mangInRoi, 0, 500);
                    demTongSoChuSo = 0;
                    mangLuuLai[0] = diemHienTai.Y;
                    mangKiemSoatCot[mangLuuLai[0]] = 0;
                    ThuDat(1);
                }
                if (bKiemTra == false && (diemBatDau + 1) * 8 == demTongSoChuSo)//điều kiện lặp lại đã in ra hết các cách rồi ((diemBatDau + 1) * 8 == demTongSoChuSo) và không thể tìm thêm bất kỳ cách nào khác để đặt nữa (bKiemTra == false)
                {
                    LapLai();
                    #region bỏ
                    //if (bienChaySauKhiLap < dem / 8)
                    //{
                    //    for (int p = 0; p < 8; p++)
                    //    {
                    //        Ma_Tran[p][mangInRoi[p + 8 * bienChaySauKhiLap]].Image = Nguoi_Choi.Anh_Hau;
                    //    }
                    //    bienChaySauKhiLap++;
                    //}
                    //else if (bienChaySauKhiLap == dem / 8)
                    //{
                    //    bienChaySauKhiLap = 0;
                    //    for (int p = 0; p < 8; p++)
                    //    {
                    //        Ma_Tran[p][mangInRoi[p + 8 * bienChaySauKhiLap]].Image = Nguoi_Choi.Anh_Hau;
                    //    }
                    //    bienChaySauKhiLap++;
                    //}
                    #endregion
                }
            }
            else
            {
                PictureBox pic = sender as PictureBox;
                Point diemHienTai = Lay_Toa_do(pic);
                if (KiemTraDatTrenHangHienTai(diemHienTai.X, diemHienTai.Y))
                {
                    lblThongBao.Text = "Vị trí này không thể đặt";
                }
                else
                {
                    if (Ma_Tran[diemHienTai.X][diemHienTai.Y].Image != null)
                    {
                        Dat(diemHienTai.X, diemHienTai.Y);
                        lblThongBao.Text = "Đặt thành công";
                        if (diemHienTai.X == 7)
                        {
                            if (MessageBox.Show("Chúc mừng, bạn có muốn chơi lại", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                rbnTuanTu_CheckedChanged(sender, e);
                            }
                        }
                        else
                        {
                            if (KiemTraDongTiepTheoCoTheDatDuoc(diemHienTai.X + 1))
                            {
                                if (MessageBox.Show("Đã hết đường đi. Bạn có muốn quay lại bước trước đó để tìm đường đi khác?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    QuayLai(diemHienTai.X - 1);
                                }
                                else
                                {
                                    MessageBox.Show("Kết thúc", "Thông báo");
                                }
                            }
                        }
                    }
                }
            }
        }


        public void Reset()
        {
            for (int k = 0; k < 8; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    Ma_Tran[k][i].BackgroundImage = null;
                    Ma_Tran[k][i].Image = null;
                }
            }
        }
        public Point Lay_Toa_do(PictureBox pic)
        {
            int Hang = Convert.ToInt32(pic.Tag);
            int Cot = Ma_Tran[Hang].IndexOf(pic);
            Point diem = new Point(Hang, Cot);
            return diem;
        }
        void ThuDat(int i)
        {
            int j;
            bKiemTra = false;
            for (j = 0; j < 8; j++)
            {
                if (boolDangChay == true)
                {
                    if (KiemTra(i, j))
                    {
                        bKiemTra = true;
                        mangLuuLai[i] = j;
                        if (i != 7)
                        {
                            mangKiemSoatCot[j] = 0;
                            ThuDat(i + 1);
                            mangKiemSoatCot[j] = 1;//cột j đã được trống 
                        }
                        else
                        {
                            InRa();
                            #region bỏ
                            //while (diemBatDau * 8 < demTongSoChuSo && boolDangChay && !bTrungRoi || diemBatDau == 0)
                            //{
                            //    trung = 0;
                            //    for (int q = 0; q < 8; q++)
                            //    {
                            //        if (mangInRoi[q + 8 * diemBatDau] == mangLuuLai[q])
                            //        {
                            //            trung++;
                            //        }
                            //    }
                            //    if (trung == 8)
                            //    {
                            //        bTrungRoi = true;
                            //        bKiemTra = false;
                            //        break;
                            //    }
                            //    else
                            //    {
                            //        if ((diemBatDau + 1) * 8 < demTongSoChuSo)
                            //        {
                            //            diemBatDau++;
                            //        }
                            //        else
                            //        {
                            //            bTrungRoi = false;
                            //            for (int p = 0; p < 8; p++)
                            //            {
                            //                Ma_Tran[p][mangLuuLai[p]].Image = Nguoi_Choi.Anh_Hau;
                            //                mangInRoi[demTongSoChuSo] = mangLuuLai[p];
                            //                demTongSoChuSo++;
                            //            }
                            //            boolDangChay = false;
                            //            break;
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                    }
                }
            }
        }
        public void InRa()
        {
            diemBatDau = 0;
            bTrungRoi = false;
            while (diemBatDau * 8 < demTongSoChuSo && boolDangChay && !bTrungRoi || diemBatDau == 0)
            {
                trung = 0;
                //Kiểm tra xem thử cách hiện tại có trùng với cách đã in ra rồi hay không?
                for (int q = 0; q < 8; q++)
                {
                    if (mangInRoi[q + 8 * diemBatDau] == mangLuuLai[q])
                    {
                        trung++;
                    }
                }
                if (trung == 8)//trùng thì break ra ngoài quay lui tìm cách khác
                {
                    bTrungRoi = true;
                    bKiemTra = false;
                    break;
                }
                else //không trùng thì in nó ra
                {
                    if ((diemBatDau + 1) * 8 < demTongSoChuSo)//
                    {
                        diemBatDau++;
                    }
                    else
                    {
                        bTrungRoi = false;
                        for (int p = 0; p < 8; p++)
                        {
                            Ma_Tran[p][mangLuuLai[p]].Image = Nguoi_Choi.Anh_Hau;
                            mangInRoi[demTongSoChuSo] = mangLuuLai[p];
                            demTongSoChuSo++;
                        }
                        boolDangChay = false;//dừng lại để in không chạy nữa
                        break;
                    }
                }
            }
        }
        public void LapLai()
        {
            if (bienChaySauKhiLap < demTongSoChuSo / 8)
            {
                for (int p = 0; p < 8; p++)
                {
                    Ma_Tran[p][mangInRoi[p + 8 * bienChaySauKhiLap]].Image = Nguoi_Choi.Anh_Hau;
                }
                bienChaySauKhiLap++;
            }
            else if (bienChaySauKhiLap == demTongSoChuSo / 8)
            {
                bienChaySauKhiLap = 0;
                for (int p = 0; p < 8; p++)
                {
                    Ma_Tran[p][mangInRoi[p + 8 * bienChaySauKhiLap]].Image = Nguoi_Choi.Anh_Hau;
                }
                bienChaySauKhiLap++;
            }
        }
        public void QuayLai(int x)
        {
            bool danhDauCoDuongDi = false;
            Ma_Tran[x + 1][mangLuuLai[x + 1]].Image = null;
            for (int i = 0; i < 8; i++)
            {
                if (KiemTra(x, i) && !Ma_Tran_Da_Chon[x, i] && Ma_Tran[x][i].Image != May.Anh_Hau)
                {
                    Ma_Tran[x][i].Image = May.Anh_Duong_Di;
                    danhDauCoDuongDi = true;
                }
            }
            Ma_Tran[x][mangLuuLai[x]].Image = null;
            Ma_Tran_Da_Chon[x, mangLuuLai[x]] = true;
            if (!danhDauCoDuongDi)
            {
                QuayLai(x - 1);
                Ma_Tran_Da_Chon[x, mangLuuLai[x]] = false;
            }
        }
        public bool KiemTraDongTiepTheoCoTheDatDuoc(int x)
        {
            if (x == 8)
            {
                return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if (Ma_Tran[x][i].Image != null)
                {
                    return false;
                }
            }
            return true;//không có đường đi nữa rồi
        }
        public void HienDuong(int x, int i)
        {
            if (KiemTra(x + 1, i))
            {
                Ma_Tran[x + 1][i].Image = May.Anh_Duong_Di;
            }
        }
        public void Dat(int x, int y)
        {
            Ma_Tran[x][y].Image = May.Anh_Hau;
            mangLuuLai[x] = y;
            for (int i = 0; i < 8; i++)
            {
                if (Ma_Tran[x][i].Image != May.Anh_Hau)
                {
                    Ma_Tran[x][i].Image = null;
                }
                HienDuong(x, i);
            }
        }

        public bool KiemTraDatTrenHangHienTai(int x, int y)
        {
            bool b = false;
            for (int j = 0; j < 8; j++)
            {
                if (Ma_Tran[x][j].Image == May.Anh_Hau || Ma_Tran[x][y].Image != May.Anh_Duong_Di)
                {
                    b = true;
                    break;
                }
            }
            return b;
        }

        private void rbnDatHien_CheckedChanged(object sender, EventArgs e)
        {
            Reset();
        }

        private void rbnTuanTu_CheckedChanged(object sender, EventArgs e)
        {
            Reset();
            for (int i = 0; i < 8; i++)
            {
                Ma_Tran[0][i].Image = May.Anh_Duong_Di;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Game 2 quân hậu")
            {
                Form1 dq = new Form1();
                dq.Show();
                this.Hide();

            }
        }
    }
}
