using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ook_pos_program
{

    class order
    {
        public int order_id = 0;
        public Dictionary<string, int> information = new Dictionary<string, int>(); // 메뉴이름과 가격을 담음

        public int button_click = 0; //주문 들어왔을때 뜨는 버튼을 눌렀는지 여부를 알려주는 변수

        public int HowManyInsert=0;

        

        //콜드브루
        public int[] coldbrew_1 = new int[3] { 0, 0, 0 };
        public int[] coldbrew_2 = new int[3] { 0, 0, 0 };
        public int[] coldbrew_3 = new int[3] { 0, 0, 0 };


        //커피&에스프레소
        public int[] coffee_capu = new int[3] { 0, 0, 0 };
        public int[] coffee_dolche = new int[3] { 0, 0, 0 };
        public int[] coffee_maggi = new int[3] { 0, 0, 0 };
        public int[] coffee_moka = new int[3] { 0, 0, 0 };
        public int[] coffee_rate = new int[3] { 0, 0, 0 };
        public int[] coffee_americano = new int[3] { 0, 0, 0 };
        public int[] coffee_icecoffee = new int[3] { 0, 0, 0 };

        //블렌디드
        public int[] blendid_green = new int[3] { 0, 0, 0 };
        public int[] blendid_mango = new int[3] { 0, 0, 0 };
        public int[] blendid_choba = new int[3] { 0, 0, 0 };
        public int[] blendid_yo = new int[3] { 0, 0, 0 };

        //스타벅스 피지오
        public int[] pgio_pink = new int[3] { 0, 0, 0 };
        public int[] pgio_raim = new int[3] { 0, 0, 0 };
        public int[] pgio_blacktea = new int[3] { 0, 0, 0 };
        public int[] pgio_greentea = new int[3] { 0, 0, 0 };
        public int[] pgio_passion = new int[3] { 0, 0, 0 };

        //프라푸치노
        public int[] pra_java = new int[3] { 0, 0, 0 };
        public int[] pra_white = new int[3] { 0, 0, 0 };
        public int[] pra_moka = new int[3] { 0, 0, 0 };
        public int[] pra_esp = new int[3] { 0, 0, 0 };
        public int[] pra_greentea = new int[3] { 0, 0, 0 };
        public int[] pra_choco = new int[3] { 0, 0, 0 };
        public int[] pra_brerry = new int[3] { 0, 0, 0 };
        public int[] pra_chococream = new int[3] { 0, 0, 0 };

        //초콜릿
        public int[] choco_sig = new int[3] { 0, 0, 0 };
        public int[] choco_banila = new int[3] { 0, 0, 0 };
        public int[] choco_apo = new int[3] { 0, 0, 0 };

        //스무디
        public int[] smudi_taro = new int[3] { 0, 0, 0 };
        public int[] smudi_mango = new int[3] { 0, 0, 0 };
        public int[] smudi_choco = new int[3] { 0, 0, 0 };
        public int[] smudi_moca = new int[3] { 0, 0, 0 };

        public int price = 0;

        public order()
        {
            //콜드브루
            information.Add("콜드 폼 콜드브루 스몰", 4500);
            information.Add("콜드 폼 콜드브루 미디움", 5000);
            information.Add("콜드 폼 콜드브루 라지", 5500);

            information.Add("바닐라크림 콜드브루 스몰", 5000);
            information.Add("바닐라크림 콜드브루 미디움", 5500);
            information.Add("바닐라크림 콜드브루 라지", 6000);

            information.Add("콜드브루 스몰", 5500);
            information.Add("콜드브루 미디움", 6000);
            information.Add("콜드브루 라지", 6500);

            //스무디
            information.Add("타로스무디 스몰", 4300);
            information.Add("타로스무디 미디움", 4800);
            information.Add("타로스무디 라지", 5300);

            information.Add("망고스무디 스몰", 4500);
            information.Add("망고스무디 미디움", 5500);
            information.Add("망고스무디 라지", 6000);

            information.Add("초콜렛스무디 스몰", 4400);
            information.Add("초콜렛스무디 미디움", 4900);
            information.Add("초콜렛스무디 라지", 5400);

            information.Add("모카스무디 스몰", 4600);
            information.Add("모카스무디 미디움", 5100);
            information.Add("모카스무디 라지", 5600);
        }
    }
}
