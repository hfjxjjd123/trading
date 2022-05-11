using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiveMeTheMoney
{
    public partial class Form1 : Form
    {
        List<List<string>> hotList = new List<List<string>>();
        // int[] time = { 30, 1, 2, 3, 4, 5, 6};
        List<string> semiList = new List<string>(); 
        List<string> buyList = new List<string>();
        int count = 0;
        static int counter = 0;
        static int countery = 0;
        string accountNo = "8020133911";
        string accountPw = "0000";
        int depoeach;
        string buyConfirm;
        int buyPrice, buyQuan;
        string date = DateTime.Now.ToString("yyyyMMdd");
        int cycle = 0;


        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (axKHOpenAPI1.CommConnect() == 0)
            {

                listBox1.Items.Add(date);
                listBox1.Items.Add("로그인시작");
            }
            else
                listBox1.Items.Add("로그인 실패 : 접속이 안돼있습니다");

            axKHOpenAPI1.OnEventConnect += onEventConnect;
        }
        //
        public void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                listBox1.Items.Add("로그인성공");
                if (axKHOpenAPI1.GetConnectState() == 1)
                    listBox1.Items.Add("접속상태:연결중");
                else if (axKHOpenAPI1.GetConnectState() == 0)
                    listBox1.Items.Add("접속상태:미연결");
                계좌정보호출.PerformClick();
            }
        }
        //
        private void 계좌정보호출_Click(object sender, EventArgs e)
        {
            //계좌정보호출


            axKHOpenAPI1.SetInputValue("계좌번호", accountNo);
            axKHOpenAPI1.SetInputValue("비밀번호", accountPw);
            axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            axKHOpenAPI1.SetInputValue("조회구분", "2");

            int acFind = axKHOpenAPI1.CommRqData("예수금상세현황요청", "opw00001", 0, "3001");

            if (acFind == 0)
                listBox1.Items.Add("예수금상세현황요청 성공");
            else
                listBox1.Items.Add("예수금상세현황요청 실패");
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData예수금;


        }
        public void onReceiveTrData예수금(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "예수금상세현황요청")
            {

                string 예수금 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim();
                // int deposit = int.Parse(예수금);
                int canBuy = (int.Parse(예수금)) / 6;
                listBox1.Items.Add("종목당: " + canBuy);
                depoeach = canBuy;
                상위값조회.PerformClick();
            }
        }
        //
        private void 상위값조회_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.SetInputValue("시장구분", "001"); axKHOpenAPI1.SetInputValue("정렬구분", "1"); axKHOpenAPI1.SetInputValue("거래량조건", "0050"); axKHOpenAPI1.SetInputValue("종목조건", "16"); axKHOpenAPI1.SetInputValue("상하한포함", "0");

            int nFind = axKHOpenAPI1.CommRqData("전일대비등락률상위", "OPT10027", 0, "2001");
            if (nFind == 0)
                listBox1.Items.Add("전일대비등락률상위 성공");
            else
                listBox1.Items.Add("전일대비등락률상위 실패");
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData상위값;
        }
        //
        public void onReceiveTrData상위값(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "전일대비등락률상위")
            {

                for (int nIdx = 0; nIdx < 7; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();

                    listBox2.Items.Add(code);
                    listBox2.Items.Add(nowprice);

                    semiList.Add(code);

                }
                listBox2.Items.Add(" ");

                Delay(30000);
                button2.PerformClick();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.SetInputValue("계좌번호", accountNo); axKHOpenAPI1.SetInputValue("매도수구분", "2"); axKHOpenAPI1.SetInputValue("비밀번호", accountPw); axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            axKHOpenAPI1.SetInputValue("조회구분", "0"); axKHOpenAPI1.SetInputValue("주문일자", date);

            int nFind = axKHOpenAPI1.CommRqData("체결확인", "opw00009", 0, "4003");
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrDataBuyState;
        }


        public void onReceiveTrDataBuyState(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "체결확인")
            {
                buyConfirm = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, countery, "접수구분").Trim();
                buyPrice = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, countery, "체결단가").Trim());
                buyQuan = int.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, countery, "체결수량").Trim());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.SetInputValue("시장구분", "001"); axKHOpenAPI1.SetInputValue("정렬구분", "1"); axKHOpenAPI1.SetInputValue("거래량조건", "0050"); axKHOpenAPI1.SetInputValue("종목조건", "16"); axKHOpenAPI1.SetInputValue("상하한포함", "0");

            int nFind = axKHOpenAPI1.CommRqData("전일대비등락률상위2", "OPT10027", 0, "2001");
            if (nFind == 0)
                listBox1.Items.Add("전일대비등락률상위 성공");
            else
                listBox1.Items.Add("전일대비등락률상위 실패");
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData상위값2;
        }

        public void onReceiveTrData상위값2(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "전일대비등락률상위2")
            {
                for (int nIdx = 0; nIdx < 7; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();

                    listBox2.Items.Add(code);
                    listBox2.Items.Add(nowprice);
                    nowprice.Replace("+", "");
                    int value = int.Parse(nowprice);


                    if ((buyList.Contains(code) == false) && counter < 6)
                    {
                        if (semiList.Contains(code) == true)
                        {
                            for (int cnt = 0; cnt < 7; cnt++)
                            {
                                if (code == semiList[cnt]) //추후 buylist에 없을 때 라는 조건을 추가해야함
                                {
                                    if (nIdx < cnt)
                                    {
                                        axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                                        구매창.Items.Add(code + "를 주문했습니다");
                                        buyList.Add(code);
                                        counter++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                            구매창.Items.Add(code + "를 주문했습니다");
                            buyList.Add(code);
                            counter++;
                        }
                    }
                    semiList.Add(code);
                }

                while (countery != counter)
                {
                    구매창.Items.Add("while countery"); //여기까진 된다
                    do {button4.PerformClick(); Delay(200); } //getBuyState 를 통한 매도주문이 안되고 있는것으로 보임. @일단 이벤트를 받지 못한것으로 판단함 양식을 바꿔줘야함
                    while (buyConfirm != "주문완료") ;
                    구매창.Items.Add(buyList[countery] + " 매도주문을 넣었습니다");
                    buyConfirm = "";
                    axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, buyList[countery], buyQuan, (int)(buyPrice * 1.025), "00", ""); //판매요청
                    countery++;
                }
                listBox2.Items.Add(" ");
                count = count + 7;
                Delay(60000);
                button3.PerformClick();

            }
        }

        public void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.SetInputValue("시장구분", "001");
            axKHOpenAPI1.SetInputValue("정렬구분", "1");
            axKHOpenAPI1.SetInputValue("거래량조건", "0050");
            axKHOpenAPI1.SetInputValue("종목조건", "16");
            axKHOpenAPI1.SetInputValue("상하한포함", "0");

            int nFind = axKHOpenAPI1.CommRqData("전일대비등락률상위3", "OPT10027", 0, "2001");
            if (nFind == 0)
                listBox1.Items.Add("전일대비등락률상위 성공");
            else
                listBox1.Items.Add("전일대비등락률상위 실패");
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData상위값3;
        }

        

        public void onReceiveTrData상위값3(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "전일대비등락률상위3")
            {
                for (int nIdx = 0; nIdx < 7; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();

                    listBox2.Items.Add(code);
                    listBox2.Items.Add(nowprice);
                    nowprice.Replace("+", "");
                    int value = int.Parse(nowprice);


                    if ((buyList.Contains(code) == false) && counter < 6)
                    {
                        if (semiList.Contains(code) == true)
                        {
                            for (int cnt = count; cnt < count + 7; cnt++)
                            {
                                if (code == semiList[cnt]) //추후 buylist에 없을 때 라는 조건을 추가해야함
                                {
                                    if (nIdx < cnt - count)
                                    {
                                        axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                                        구매창.Items.Add(code + "를 주문했습니다");
                                        buyList.Add(code);
                                        counter++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                            구매창.Items.Add(code + "를 주문했습니다");
                            buyList.Add(code);
                            counter++;
                        }
                    }
                    semiList.Add(code);
                }

                while (countery != counter)
                {
                    do { button4.PerformClick(); Delay(200); }
                    while (buyConfirm != "주문완료");
                    buyConfirm = "";
                    axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, buyList[countery], buyQuan, (int)(buyPrice * 1.025), "00", ""); //판매요청
                    countery++;
                }
                listBox2.Items.Add(" ");
                count = count + 7;
                Delay(60000);
                if (cycle < 4)
                {
                    cycle++;
                    button3.PerformClick(); //도르마무
                }

            }
        }
    }
}
  // 이거 뭐냐 .etf로 도배되는 날은 금지
  // 리퀘스트가 많다는 문제를 해결해야함
