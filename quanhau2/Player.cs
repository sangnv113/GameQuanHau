using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanhau2
{
   public class Player
    {
        private int diem;

        public int Diem_So
        {
            get { return diem; }
            set { diem = value; }
        }

        private int SoLuot;

        public int So_Luot
        {
            get { return SoLuot; }
            set { SoLuot = value; }
        }
        private Image mark;

        public Image Anh_Hau
        {
            get { return mark; }
            set { mark = value; }
        }
       //
        private Image AnhDuongDi;

        public Image Anh_Duong_Di
        {
            get { return AnhDuongDi; }
            set { AnhDuongDi = value; }
        }
       //
        private Image Anh_danh_dau;

        public Image Anh_Danh_Dau
        {
            get { return Anh_danh_dau; }
            set { Anh_danh_dau = value; }
        }
       //
        private List<PictureBox> List_danh_dau;

        public List<PictureBox> List_Danh_Dau
        {
            get { return List_danh_dau; }
            set { List_danh_dau = value; }
        }
       //
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
       //
        private PictureBox Quan_hau;

        public PictureBox Quan_Hau
        {
            get { return Quan_hau; }
            set { Quan_hau = value; }
        }

        private Stack<Point> Diem_chua_duong;

        public Stack<Point> Diem_Chua_Duong
        {
            get { return Diem_chua_duong; }
            set { Diem_chua_duong = value; }
        }
       public Player(int diem, Image anhhau,Image anhduongdi, string name, PictureBox quanhau, Image anhdanhdau, 
           List<PictureBox> listdanhdau, int soluot, Stack<Point> diemchuaduong)
        {
            this.Diem_So = diem;
            this.Anh_Hau = anhhau;
            this.Name = name;
            this.Quan_Hau = quanhau;
            this.Anh_Duong_Di = anhduongdi;
            this.Anh_Danh_Dau = anhdanhdau;
            this.List_Danh_Dau = listdanhdau;
            this.So_Luot = soluot;
            this.Diem_Chua_Duong = diemchuaduong;
        }

       
    }
}
