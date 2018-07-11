using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace quanhau2
{
   public class Hang_So
    {
       public static int chieu_rong_o=80;
       public static int chieu_cao_o=80;
       public static int chieungang = 9;//so button
       public static int chieucao = 8;
       public static int vang50 = 10;
       public static int vang30 = 20;
       //public static int vang10 = 15;
       public static Image anh_vang50 = Image.FromFile(Application.StartupPath + "\\Resources\\vang50.png");
       public static Image anh_vang30 = Image.FromFile(Application.StartupPath + "\\Resources\\vang30.png");
       public static Image anh_trang_thang = Image.FromFile(Application.StartupPath + "\\Resources\\trangthang.jpg");
       public static Image anh_den_thang= Image.FromFile(Application.StartupPath + "\\Resources\\denthang.jpg");
       public static Image anh_hoanhau = Image.FromFile(Application.StartupPath + "\\Resources\\hoa.jpg");
       public static Image anh_goiY = Image.FromFile(Application.StartupPath + "\\Resources\\goiy.png");
    }
}
