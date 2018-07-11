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
    
    public partial class Form1 : Form
    {
        int demtg_goiY;
        int so_o_con_lai;
        int sovang50_conlai ;
        int sovang30_conlai;
        bool choi_voi_May ;
        int muc_Vs_May;
        int x, y;
        int luot;

        Player May = new Player(10, Image.FromFile(Application.StartupPath + "\\Resources\\hauden.png"), Image.FromFile(Application.StartupPath + "\\Resources\\duongden.png"),"Đen",new PictureBox(),
            Image.FromFile(Application.StartupPath + "\\Resources\\danhdauden.png"), new List<PictureBox>(), 0, new Stack<Point>());//1
        Player Nguoi_Choi = new Player(10, Image.FromFile(Application.StartupPath + "\\Resources\\hautrang.png"),Image.FromFile(Application.StartupPath + "\\Resources\\duongtrang.png"), "Trắng", new PictureBox(),
            Image.FromFile(Application.StartupPath + "\\Resources\\danhdautrang.png"), new List<PictureBox>(), 0, new Stack<Point>());//2
        
       
        Random rd1 = new Random();
        Random rd2 = new Random();
        private List<List<PictureBox>> Ma_Tran;
        public Form1()
        {
            InitializeComponent();
            Ve_ban_co();
        }
        private void load()
        {
            demtg_goiY = 0;
            so_o_con_lai = Hang_So.chieucao * Hang_So.chieucao-2;
             sovang50_conlai = Hang_So.vang50;
            sovang30_conlai = Hang_So.vang30;
            choi_voi_May = true;
            muc_Vs_May = 1;
            x = 5; y = 5;
            luot = 2;
            Nguoi_Choi.Quan_Hau = Ma_Tran[0][0];
            May.Quan_Hau = Ma_Tran[6][7];
            May.List_Danh_Dau.Add(May.Quan_Hau);
            Nguoi_Choi.List_Danh_Dau.Add(Nguoi_Choi.Quan_Hau);

            Nguoi_Choi.Quan_Hau.BackgroundImage = Nguoi_Choi.Anh_Hau;

            May.Quan_Hau.BackgroundImage = May.Anh_Hau;

            hien_duong_Add_stack(Nguoi_Choi, May);//hien duong dau tien

            hien_duong_Khong_Add_stack_tren_o_da_danh(May, Nguoi_Choi);
            xoa_duong(May);

            tao_Vang_50(Hang_So.vang50, Hang_So.anh_vang50);
            Tao_Vang(Hang_So.vang30, Hang_So.anh_vang30);
            btn_goi_Y.Enabled = true;
            ptrThang.Hide();
            btnDong.Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            load();

            ptrMay.Image = May.Anh_Hau;
            ptrNguoi.Image = Nguoi_Choi.Anh_Hau;
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
                        Tag=k.ToString()//xac dinh hang cho PictureBox
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
               // pic_cu.Height = 0;
            }
           
           
        }

        private void Tao_Vang(int so_luong_vang, Image Anh_Vang)
        {
            int dem_Vang = 0;
            while(dem_Vang<so_luong_vang)
            {
                int x = rd1.Next(0, 8);
                int y = rd2.Next(0, 8);
                if (x != 0)
                {
                    y = rd2.Next(0, 8);
                }
                if (Ma_Tran[y][x].BackgroundImage == null)
                {
                    Ma_Tran[y][x].BackgroundImage = Anh_Vang;
                    dem_Vang++;

                }
            }
            
        }
        private void tao_Vang_50(int so_luong_vang, Image Anh_Vang)
        {
            int dem_Vang50 = 0;
            while (dem_Vang50 < so_luong_vang)
            {
                int x = rd1.Next(0, 8);
                int y = rd2.Next(0, 8);
                if (x != 0)
                {
                    y = rd2.Next(0, 8);
                }
                if (Ma_Tran[y][x].BackgroundImage == null)
                {
                    Ma_Tran[y][x].BackgroundImage = Anh_Vang;
                    dem_Vang50++;

                }
            }
        }
        public Point Lay_Toa_do(PictureBox pic)
        {
            
            int Hang = Convert.ToInt32(pic.Tag);
            int Cot = Ma_Tran[Hang].IndexOf(pic);
            Point diem = new Point(Cot, Hang);

            return diem;
        }
        private void pic_Click(object sender, EventArgs e)
        {
           
            grbCheDoChoi.Enabled = false;
            PictureBox pic = sender as PictureBox;
            if (choi_voi_May == true)
            {
                if(luot_danh(pic, Nguoi_Choi, May))
                {
                    if (muc_Vs_May == 1)
                    {
                        AI_May_De(May, Nguoi_Choi);
                    }
                    else
                    {
                        if (muc_Vs_May == 2)
                        {
                                AI_May_Kho(May, Nguoi_Choi);
                        }
                       
                    }
                    luot_danh(Ma_Tran[y][x], May, Nguoi_Choi);
                }
               
            }
            else
            {
                if (luot == 1)//may
                {
                    luot_danh(pic, May, Nguoi_Choi);
                }
                else
                    if (luot == 2)//nguoi
                    {
                        luot_danh(pic, Nguoi_Choi, May);
                    }
            }

            if (May.List_Danh_Dau.Count + Nguoi_Choi.List_Danh_Dau.Count >= Hang_So.chieucao * Hang_So.chieucao)
            {
                xu_ly_thang();
            }

        }

        private void congdiem(Player dang_danh, PictureBox pic)
        {
            if (pic.BackgroundImage == null)
            {
                dang_danh.Diem_So += 10;
            }
            else
            {
                if (pic.BackgroundImage != null && sosanh_anh(pic.BackgroundImage, Hang_So.anh_vang50))
                {
                    dang_danh.Diem_So += 60;
                    sovang50_conlai--;
                }
                else
                {
                    if (pic.BackgroundImage != null && sosanh_anh(pic.BackgroundImage, Hang_So.anh_vang30))
                    {
                        dang_danh.Diem_So += 40;
                        sovang30_conlai--;
                    }
                }
            }
        }
        private bool sosanhdieukien_odaan(PictureBox pic,  Player luot_phu)
        {
            bool flag=false;
            
                if (pic.BackgroundImage != null && sosanh_anh(pic.BackgroundImage, luot_phu.Anh_Danh_Dau))
                {
                    flag = true;
                }
            
            return flag;
        }
        private bool luot_danh(PictureBox pic, Player Luot_Dang_danh, Player luot_phu)
        {
            
            if (pic.Image == null)
            {
                lblThong_Bao.Text = "Không có đường!";
                return false;
            }
            if (pic.BackgroundImage != null && sosanhdieukien_odaan(pic, luot_phu))
            {
                lblThong_Bao.Text = "Ô này đã được đối thủ ăn!";
                return false;
            }
            if (pic.Image != null && sosanh_anh(pic.Image, Luot_Dang_danh.Anh_Duong_Di) )
            {
                congdiem(Luot_Dang_danh, pic);//cong diem vang
              
                them_vao_list_oDaAn(pic, Luot_Dang_danh);
                lblThong_Bao.Text = "Số ô còn lại chưa ăn: " + so_o_con_lai.ToString();
                Luot_Dang_danh.Quan_Hau.BackgroundImage = null;

                xoa_duong(Luot_Dang_danh);//

                Luot_Dang_danh.Quan_Hau = pic;
                hien_duong_Khong_Add_stack_tren_o_da_danh(Luot_Dang_danh, luot_phu);

                if (choi_voi_May == true && luot == 2)
                {
                    hien_duong_Add_stack(luot_phu, Luot_Dang_danh);
                }
                else
                {
                    hien_duong_Add_stack(luot_phu, Luot_Dang_danh);
                }
               
                xoa_duong(Luot_Dang_danh);//

                ptrLuot_Danh.Image = luot_phu.Anh_Hau;
                lbtennguoichoi.Text = "Đến lượt " + luot_phu.Name;
                in_o_da_an(Luot_Dang_danh);
                in_o_da_an(luot_phu);


                pic.BackgroundImage = Luot_Dang_danh.Anh_Hau;
                luot_phu.Quan_Hau.BackgroundImage = luot_phu.Anh_Hau;

                if (luot == 2)
                {
                    Nguoi_Choi.So_Luot++;
                    lbldiem_Nguoi.Text = Nguoi_Choi.Diem_So.ToString();
                    lblLuot_Nguoi.Text = Nguoi_Choi.So_Luot.ToString();
                    label6.Text = Luot_Dang_danh.List_Danh_Dau.Count.ToString();
                    luot = 1;
                }
                else
                {
                    if (luot == 1)
                    {
                        May.So_Luot++;
                        lbldiem_may.Text = May.Diem_So.ToString();
                        lblLuot_may.Text = May.So_Luot.ToString();
                        label5.Text = Luot_Dang_danh.List_Danh_Dau.Count.ToString();
                        luot = 2;
                    }
                }
            }
            return true;
        }
        private void them_vao_list_oDaAn(PictureBox pic, Player Luot_Dang_danh)
        {
            if (pic.BackgroundImage == null || pic.BackgroundImage != null && sosanh_anh(Hang_So.anh_vang50, pic.BackgroundImage)
                || pic.BackgroundImage != null && sosanh_anh(Hang_So.anh_vang30, pic.BackgroundImage)/* || pic.BackgroundImage != null  && sosanh_anh(Hang_So.anh_vang10, pic.BackgroundImage)*/)
            {
                Luot_Dang_danh.List_Danh_Dau.Add(pic);//them vao list da an
                --so_o_con_lai;
            }
            
        }
        public bool sosanh_anh(Image anh_1, Image anh_2)
        {
            
            string img_1, img_2;
            Bitmap bmp1, bmp2;
            bmp1 = new Bitmap(anh_1);
            bmp2 = new Bitmap(anh_2);
            if (bmp1.Width == bmp2.Width && bmp1.Height == bmp2.Height)
            {
                for (int i = 0; i < bmp1.Width; i++)
                    for (int j = 0; j < bmp1.Height; j++)
                    {
                        img_1 = bmp1.GetPixel(i, j).ToString();
                        img_2 = bmp2.GetPixel(i, j).ToString();
                        if (img_1 != img_2)
                            return false;
                    }
                return true;
            }
            else
                return false;
        }
        private void in_o_da_an(Player dang_danh)
        {
            for (int i = 0; i < dang_danh.List_Danh_Dau.Count ; i++)
            {
                dang_danh.List_Danh_Dau[i].BackgroundImage = dang_danh.Anh_Danh_Dau;

            }
        }
        private void xu_ly_thang()
        {
            ptrThang.Show();
            btnDong.Show();
            if(May.Diem_So>Nguoi_Choi.Diem_So)
            {
                ptrThang.Image = Hang_So.anh_den_thang;

            }
            else
            {
                if(May.Diem_So==Nguoi_Choi.Diem_So)
                {
                    ptrThang.Image = Hang_So.anh_hoanhau;
                }
                else
                {
                    ptrThang.Image = Hang_So.anh_trang_thang;
                }
            }
            btn_goi_Y.Enabled = false;

        }
        private void AI_May_De(Player Luot_Dang_danh, Player luot_phu)
        {

            int so_ptu = Luot_Dang_danh.Diem_Chua_Duong.Count;
            label7.Text = so_ptu.ToString() + Luot_Dang_danh.Name;
            int diem_hientai = -1, diemtrave = -1;

            Point diem_xetAI;
            for (int i = 0; i < so_ptu; i++)
            {

                diem_xetAI = Luot_Dang_danh.Diem_Chua_Duong.Pop();
                Player AI_may = new Player(0, null, Luot_Dang_danh.Anh_Duong_Di, "", Ma_Tran[diem_xetAI.Y][diem_xetAI.X], Luot_Dang_danh.Anh_Danh_Dau, null, 0, new Stack<Point>());

                //
                label7.Text = label7.Text + diem_xetAI.Y.ToString() + diem_xetAI.X.ToString() + ", ";/////hien cac o trong pop
                if (sovang50_conlai > 0)
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang50))
                    {
                        x = diem_xetAI.X;
                        y = diem_xetAI.Y;
                        return;
                    }
                }
                else
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang30))
                    {
                        x = diem_xetAI.X;
                        y = diem_xetAI.Y;
                        return;
                    }
                }
                if (sovang30_conlai > 0)
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang30))
                    {
                        diem_hientai = 30;


                    }
                }
                else
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage == null)
                    {
                        x = diem_xetAI.X;
                        y = diem_xetAI.Y;
                        return;
                    }
                }
                if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage == null)
                {
                    diem_hientai = 10;

                }
                if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Luot_Dang_danh.Anh_Danh_Dau))
                {
                    diem_hientai = 5;

                }

                if (diem_hientai > diemtrave)
                {
                    diemtrave = diem_hientai;
                    x = diem_xetAI.X;
                    y = diem_xetAI.Y;
                }

            }

        }
       
        private void rdbVoiMay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbVoiMay.Checked == true)
            {
                choi_voi_May = true;
                comboBox1.Enabled = true;
            }
            else
            {
                choi_voi_May = false;
                comboBox1.Enabled = false;
            }
                

        }
       


        private void xoa_duong(Player Luot_Danh_hien_tai)
        {
            Point O_co = Lay_Toa_do(Luot_Danh_hien_tai.Quan_Hau);
            for (int i = O_co.X; i >= 0; i--)
            {
                Ma_Tran[O_co.Y][i].Image = null;
            }
            //sau -hang
            for (int i = O_co.X; i < Hang_So.chieungang; i++)
            {
                Ma_Tran[O_co.Y][i].Image = null;
            }
            //truoc-cot
            for (int i = O_co.Y - 1; i >= 0; i--)
            {
                Ma_Tran[i][O_co.X].Image = null;
            }
            //sau -cot
            for (int i = O_co.Y + 1; i < Hang_So.chieucao; i++)
            {
                Ma_Tran[i][O_co.X].Image = null;
            }

            //
            // DUONG CHEO
            for (int i = 1; i <= O_co.X; i++)
            {
                if (O_co.X - i < 0 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                Ma_Tran[O_co.Y - i][O_co.X - i].Image = null;
            }
            for (int i = 1; i < Hang_So.chieungang - O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                Ma_Tran[O_co.Y + i][O_co.X + i].Image = null;
            }

            for (int i = 1; i <= O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang || O_co.Y - i < 0)
                {
                    break;

                }
                //
                Ma_Tran[O_co.Y - i][O_co.X + i].Image = null;
            }
            for (int i = 1; i <= Hang_So.chieucao - O_co.Y; i++)
            {
                if (O_co.X - i < 0 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                Ma_Tran[O_co.Y + i][O_co.X - i].Image = null;
            }

            Luot_Danh_hien_tai.Diem_Chua_Duong.Clear();
        }
        public void hien_duong_Add_stack(Player Luot_Danh_hien_tai, Player Luot_Phu)
        {
            Point O_co = Lay_Toa_do(Luot_Danh_hien_tai.Quan_Hau);
            //truoc-hang
            for (int i = O_co.X - 1; i >= 0; i--)
            {
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y, i);
            }
            //sau -hang
            for (int i = O_co.X + 1; i < Hang_So.chieungang - 1; i++)
            {
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y, i);
            }
            //truoc-cot
            for (int i = O_co.Y - 1; i >= 0; i--)
            {
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, i, O_co.X);
            }
            //sau -cot
            for (int i = O_co.Y + 1; i < Hang_So.chieucao; i++)
            {
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, i, O_co.X);
            }

            // DUONG CHEO
            for (int i = 1; i <= O_co.X; i++)
            {
                if (O_co.X - i < 0 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y - i, O_co.X - i);
            }
            for (int i = 1; i < Hang_So.chieungang - O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang - 1 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y + i, O_co.X + i);
            }

            for (int i = 1; i <= O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang - 1 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y - i, O_co.X + i);
            }
            for (int i = 1; i <= Hang_So.chieucao - O_co.Y; i++)
            {
                if (O_co.X - i < 0 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                DK_hien_duong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y + i, O_co.X - i);
            }
        }
        private void DK_hien_duong_Add_stack(Player Luot_Danh_hien_tai, Player Luot_Phu, int x, int y)
        {
            if (Ma_Tran[x][y].BackgroundImage == null && Ma_Tran[x][y].Image == null ||
                    Ma_Tran[x][y].BackgroundImage != null && !sosanh_anh(Ma_Tran[x][y].BackgroundImage, Luot_Phu.Anh_Danh_Dau) && Ma_Tran[x][y].Image == null)
            {
                Ma_Tran[x][y].Image = Luot_Danh_hien_tai.Anh_Duong_Di;
                Luot_Danh_hien_tai.Diem_Chua_Duong.Push(Lay_Toa_do(Ma_Tran[x][y]));
            }
        }



        public void hien_duong_Khong_Add_stack_tren_o_da_danh(Player Luot_Danh_hien_tai, Player Luot_Phu)
        {
            Point O_co = Lay_Toa_do(Luot_Danh_hien_tai.Quan_Hau);
            //truoc-hang
            for (int i = O_co.X - 1; i >= 0; i--)
            {
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y, i);
            }
            //sau -hang
            for (int i = O_co.X + 1; i < Hang_So.chieungang - 1; i++)
            {
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y, i);
            }
            //truoc-cot
            for (int i = O_co.Y - 1; i >= 0; i--)
            {
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, i, O_co.X);
            }
            //sau -cot
            for (int i = O_co.Y + 1; i < Hang_So.chieucao; i++)
            {
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, i, O_co.X);
            }

            // DUONG CHEO
            for (int i = 1; i <= O_co.X; i++)
            {
                if (O_co.X - i < 0 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y - i, O_co.X - i);
            }
            for (int i = 1; i < Hang_So.chieungang - O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang - 1 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y + i, O_co.X + i);
            }

            for (int i = 1; i <= O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang - 1 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y - i, O_co.X + i);
            }
            for (int i = 1; i <= Hang_So.chieucao - O_co.Y; i++)
            {
                if (O_co.X - i < 0 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                DK_hien_duong_Khong_Add_stack(Luot_Danh_hien_tai, Luot_Phu, O_co.Y + i, O_co.X - i);
            }
        }
        private void DK_hien_duong_Khong_Add_stack(Player Luot_Danh_hien_tai, Player Luot_Phu, int x, int y)
        {
            if (Ma_Tran[x][y].Image == null)
            {
                Ma_Tran[x][y].Image = Luot_Danh_hien_tai.Anh_Duong_Di;
               
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (comboBox1.Text == "Dễ")
            {
                muc_Vs_May = 1;
            }
            if (comboBox1.Text == "Khó")
            {
                muc_Vs_May = 2;
            }
        }

        private void btn_goi_Y_Click(object sender, EventArgs e)
        {
            
            if (choi_voi_May == true)
            {
                if (luot == 2)
                {
                    AI_May_De(Nguoi_Choi, May);
                }
            }
            else
            {
                if (luot == 1)//may
                {
                    AI_May_De(May, Nguoi_Choi);
                }
                else
                    if (luot == 2)//nguoi
                    {
                        AI_May_De(Nguoi_Choi, May);
                    }
            }
            Ma_Tran[y][x].Image = Hang_So.anh_goiY;
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            demtg_goiY++;
            if(demtg_goiY==3)
            {
                if (luot == 1)//may
                {
                    Ma_Tran[y][x].Image = May.Anh_Duong_Di;
                }
                else
                    if (luot == 2)//nguoi
                    {
                        Ma_Tran[y][x].Image = Nguoi_Choi.Anh_Duong_Di;
                    }
                demtg_goiY = 0;
                timer1.Stop();
               
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < Hang_So.chieucao; k++)
            {
                for (int i = 0; i < Hang_So.chieungang; i++)// o tren da add 1
                {
                    Ma_Tran[k][i].BackgroundImage = null;
                    Ma_Tran[k][i].Image = null;
                }
            }
            lblThong_Bao.Text="Bắt đầu!";
            lblLuot_may.Text = "0";
            lbldiem_may.Text = "0";
            label6.Text = "0";

            lbldiem_Nguoi.Text = "0";
            lblLuot_Nguoi.Text = "0";
            label5.Text = "0";
            grbCheDoChoi.Enabled = true;

            May.Diem_Chua_Duong.Clear();
            May.Diem_So = 0;
            May.So_Luot = 0;
            May.List_Danh_Dau.Clear();

            Nguoi_Choi.Diem_Chua_Duong.Clear();
            Nguoi_Choi.Diem_So = 0;
            Nguoi_Choi.So_Luot = 0;
            Nguoi_Choi.List_Danh_Dau.Clear();
            load();

            ptrLuot_Danh.Image = Nguoi_Choi.Anh_Hau;
            lbtennguoichoi.Text = "Đến lượt " + Nguoi_Choi.Name;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            ptrThang.Hide();
            btnDong.Hide();
        }



        public void AI_hien_duong_may(Player Luot_Danh_hien_tai, Player Luot_Phu)
        {
            Point O_co = Lay_Toa_do(Luot_Danh_hien_tai.Quan_Hau);
            //truoc-hang
            for (int i = O_co.X - 1; i >= 0; i--)
            {
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, O_co.Y, i);
            }
            //sau -hang
            for (int i = O_co.X + 1; i < Hang_So.chieungang - 1; i++)
            {
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, O_co.Y, i);
            }
            //truoc-cot
            for (int i = O_co.Y - 1; i >= 0; i--)
            {
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, i, O_co.X);
            }
            //sau -cot
            for (int i = O_co.Y + 1; i < Hang_So.chieucao; i++)
            {
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, i, O_co.X);
            }

            // DUONG CHEO
            for (int i = 1; i <= O_co.X; i++)
            {
                if (O_co.X - i < 0 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, O_co.Y - i, O_co.X - i);
            }
            for (int i = 1; i < Hang_So.chieungang - O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang - 1 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, O_co.Y + i, O_co.X + i);
            }

            for (int i = 1; i <= O_co.Y; i++)
            {
                if (O_co.X + i >= Hang_So.chieungang - 1 || O_co.Y - i < 0)
                {
                    break;

                }
                //
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, O_co.Y - i, O_co.X + i);
            }
            for (int i = 1; i <= Hang_So.chieucao - O_co.Y; i++)
            {
                if (O_co.X - i < 0 || O_co.Y + i >= Hang_So.chieucao)
                {
                    break;

                }
                //
                AI_ham_con_hien_duong(Luot_Danh_hien_tai, Luot_Phu, O_co.Y + i, O_co.X - i);
            }
        }
        private void AI_ham_con_hien_duong(Player Luot_Danh_hien_tai, Player Luot_Phu, int x, int y)
        {
            if (Ma_Tran[x][y].BackgroundImage == null ||
                    Ma_Tran[x][y].BackgroundImage != null && !sosanh_anh(Ma_Tran[x][y].BackgroundImage, Luot_Phu.Anh_Danh_Dau))
            {
                Ma_Tran[x][y].Image = Luot_Danh_hien_tai.Anh_Duong_Di;
                Luot_Danh_hien_tai.Diem_Chua_Duong.Push(Lay_Toa_do(Ma_Tran[x][y]));
            }
        }



        private void AI_May_Kho(Player Luot_Dang_danh, Player luot_phu)
        {

            int so_ptu = Luot_Dang_danh.Diem_Chua_Duong.Count;
            label7.Text = so_ptu.ToString() + Luot_Dang_danh.Name;
            int diem_hientai = -1, diemtrave = -1;

            Point diem_xetAI;
            for (int i = 0; i < so_ptu; i++)
            {


                diem_xetAI = Luot_Dang_danh.Diem_Chua_Duong.Pop();
                Player AI_may = new Player(0, null, Luot_Dang_danh.Anh_Duong_Di, "", Ma_Tran[diem_xetAI.Y][diem_xetAI.X], Luot_Dang_danh.Anh_Danh_Dau, null, 0, new Stack<Point>());

                //
                label7.Text = label7.Text + diem_xetAI.Y.ToString() + diem_xetAI.X.ToString() + ", ";/////hien cac o trong pop

                if (sovang50_conlai > 0)
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang50))
                    {
                        diem_hientai = 50;

                        diem_hientai = diem_hientai + HamCon_AI_May(AI_may, Ma_Tran[diem_xetAI.Y][diem_xetAI.X], luot_phu);

                    }
                }
                if (sovang30_conlai > 0)
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang30))
                    {
                        diem_hientai = 30;

                        diem_hientai = diem_hientai + HamCon_AI_May(AI_may, Ma_Tran[diem_xetAI.Y][diem_xetAI.X], luot_phu);


                    }
                }
                if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage == null)
                {
                    diem_hientai = 10;
                    diem_hientai = diem_hientai + HamCon_AI_May(AI_may, Ma_Tran[diem_xetAI.Y][diem_xetAI.X], luot_phu);

                }
                if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Luot_Dang_danh.Anh_Danh_Dau))
                {
                    diem_hientai = 0;

                    diem_hientai = diem_hientai + HamCon_AI_May(AI_may, Ma_Tran[diem_xetAI.Y][diem_xetAI.X], luot_phu);

                }

                if (diem_hientai > diemtrave)
                {
                    diemtrave = diem_hientai;
                    x = diem_xetAI.X;
                    y = diem_xetAI.Y;
                }

            }
            hien_duong_Add_stack(luot_phu, Luot_Dang_danh);
            hien_duong_Khong_Add_stack_tren_o_da_danh(Luot_Dang_danh, luot_phu);
            xoa_duong(luot_phu);

        }
        private int HamCon_AI_May(Player Luot_Dang_danh, PictureBox pic_QuanHau, Player luot_phu)
        {

            AI_hien_duong_may(Luot_Dang_danh, luot_phu);
            //
            int dt = -1, kt = -1;
            //
            int AI_so_ptu = Luot_Dang_danh.Diem_Chua_Duong.Count;


            Point diem_xetAI;
            for (int i = 0; i < AI_so_ptu; i++)
            {
                diem_xetAI = Luot_Dang_danh.Diem_Chua_Duong.Pop();
                if (sovang50_conlai > 1)
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang50))
                    {
                        dt = 45;

                    }
                }
                if (sovang30_conlai > 1)
                {
                    if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Hang_So.anh_vang30))
                    {
                        dt = 25;
                    }
                }

                if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage == null)
                {
                    dt = 5;
                }
                if (Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage != null && sosanh_anh(Ma_Tran[diem_xetAI.Y][diem_xetAI.X].BackgroundImage, Luot_Dang_danh.Anh_Danh_Dau))
                {
                    dt = 0;
                }
                if (dt > kt)
                {
                    kt = dt;
                }
            }
            xoa_duong(Luot_Dang_danh);
            
            return kt;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text=="Đặt 8 quân hậu")
            {
                DatQuanHau dq = new DatQuanHau();
                dq.Show();
                this.Hide();

            }
        }

    }
}
