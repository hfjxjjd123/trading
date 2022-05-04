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
        int[] time = { 30, 1, 2, 3, 4, 5, 6, 7 };
        List<string> semiList = new List<string>(); // 여기 바꿨어요~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        List<string> buyList = new List<string>();
        int count = 0;
        int counter = 0;
        string accountNo = "8020133911";
        int depoeach;


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
                listBox1.Items.Add("로그인시작");

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
            
            string accountPw = "0000";

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

                for (int nIdx = 0; nIdx <= 4; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();

                    listBox2.Items.Add(code);
                    listBox2.Items.Add(nowprice);

                    semiList.Add(code);
                    semiList.Add(nowprice);

                }
                listBox2.Items.Add(" ");

                Delay(10000);
                button2.PerformClick();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.SetInputValue("시장구분", "001"); axKHOpenAPI1.SetInputValue("정렬구분", "1"); axKHOpenAPI1.SetInputValue("거래량조건", "0050"); axKHOpenAPI1.SetInputValue("종목조건", "16"); axKHOpenAPI1.SetInputValue("상하한포함", "0");

            int nFind = axKHOpenAPI1.CommRqData("전일대비등락률상위2","OPT10027", 0, "2001");
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
                for (int nIdx = 0; nIdx <= 4; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();

                    listBox2.Items.Add(code); 
                    listBox2.Items.Add(nowprice); 
                    bool previous = false;

                    for (int cnt = 0; cnt<10; cnt=cnt+2)
                    {
                        if (code == semiList[cnt]) 
                        {
                            previous = true;
                            if (nIdx * 2 < (cnt))
                            {
                                nowprice.Replace("+","");
                                int value = int.Parse(nowprice);
                                axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value ), 0, "03", ""); //구매요청
                                구매창.Items.Add(code + "를 주문했습니다");
                                if (buyList.Contains(code) == false) { buyList.Add(code); }
                                axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, code, (depoeach /value), (int)(value*1.03), "00", ""); //판매요청
                                counter++;
                            }
                        } 
                          
                    }

                    if (previous == false)
                    {
                        nowprice.Replace("+", "");
                        int value = int.Parse(nowprice);
                        axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                        구매창.Items.Add(code + "를 주문했습니다");
                        if (buyList.Contains(code) == false) { buyList.Add(code); }
                        axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, code, (depoeach / value), (int)(value * 1.03), "00", ""); //판매요청
                        counter++;
                    }

                    semiList.Add(code);
                    semiList.Add(nowprice);
             
                }
                listBox2.Items.Add(" ");
                count = count + 10;
                Delay(20000);
                button3.PerformClick();


            }
        }


        // axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, quan, 0,"03", ""); //구매요청
        //axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, quan, 0, "03", ""); ;//구매요청


        /*바로 매도 주문을 걸지 or 모든 매수주문을 마친 후 매도 주문을 걸지? 
         후 매도주문이 더 나아보임 잠깐이나마 모든 주문이 체결될 때까지 기다리고 어차피 루프도는데 시간 별로 안걸려서
        그사이에 +3퍼가 되는 종목은 없을것으로 판단

        구매한 종목을 보이는 buyList에 종목정보 집어넣고 관리
         */



        //

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
                for (int nIdx = 0; nIdx <= 4; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();


                    listBox2.Items.Add(code);
                    listBox2.Items.Add(nowprice);

                    bool previous = false;
                    if (buyList.Contains(code)==false) {
                    
                    for (int cnt = count; cnt < count+10; cnt = cnt + 2)
                    {


                        if (semiList[cnt] == code)
                        {
                            previous = true;
                            if (nIdx * 2 < (cnt - count))
                            {
                                nowprice.Replace("+", "");
                                int value = int.Parse(nowprice);
                                axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                                구매창.Items.Add(code + "를 주문했습니다");
                                if (buyList.Contains(code) == false) { buyList.Add(code); }
                                axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, code, (depoeach / value), (int)(value * 1.03), "00", ""); //판매요청
                                counter++;

                            }
                        } 

                    }
                    if (previous == false)
                    {
                        nowprice.Replace("+", "");
                        int value = int.Parse(nowprice);
                        axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                        구매창.Items.Add(code + "를 주문했습니다");
                        if (buyList.Contains(code) == false) { buyList.Add(code); }
                        axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, code, (depoeach / value), (int)(value * 1.03), "00", ""); //판매요청
                        counter++;
                    }
                    }

                    semiList.Add(code);
                    semiList.Add(nowprice);

                }
                listBox2.Items.Add(" ");
                count = count + 10;
                Delay(20000);
                button4.PerformClick();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axKHOpenAPI1.SetInputValue("시장구분", "001");
            axKHOpenAPI1.SetInputValue("정렬구분", "1");
            axKHOpenAPI1.SetInputValue("거래량조건", "0050");
            axKHOpenAPI1.SetInputValue("종목조건", "16");
            axKHOpenAPI1.SetInputValue("상하한포함", "0");

            int nFind = axKHOpenAPI1.CommRqData("전일대비등락률상위4", "OPT10027", 0, "2001");
            if (nFind == 0)
                listBox1.Items.Add("전일대비등락률상위 성공");
            else
                listBox1.Items.Add("전일대비등락률상위 실패");
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData상위값4;
        }

        public void onReceiveTrData상위값4(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "전일대비등락률상위4")
            {
                for (int nIdx = 0; nIdx <= 4; nIdx++)
                {
                    string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "종목코드").Trim();
                    string nowprice = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, nIdx, "현재가").Trim();


                    listBox2.Items.Add(code);
                    listBox2.Items.Add(nowprice);

                    bool previous = false;
                    if (buyList.Contains(code) == false)
                    {

                    for (int cnt = count; cnt < count + 10; cnt = cnt + 2)
                    {


                        if (semiList[cnt] == code)
                            previous = true;
                            if (nIdx * 2 < (cnt - count))
                            {
                                nowprice.Replace("+", "");
                                int value = int.Parse(nowprice);
                                axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                                구매창.Items.Add(code + "를 주문했습니다");
                                buyList.Add(code); 
                            axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, code, (depoeach / value), (int)(value * 1.03), "00", ""); //판매요청
                            counter++;
                            } 

                    }
                    if (previous == false)
                    {
                        nowprice.Replace("+", "");
                        int value = int.Parse(nowprice);
                        axKHOpenAPI1.SendOrder("주문", "4001", accountNo, 1, code, (depoeach / value), 0, "03", ""); //구매요청
                        구매창.Items.Add(code + "를 주문했습니다");
                        buyList.Add(code);
                        axKHOpenAPI1.SendOrder("주문", "4002", accountNo, 2, code, (depoeach /value), (int)(value*1.03), "00", ""); //판매요청
                        counter++;
                    }
                    }

                    semiList.Add(code);
                    semiList.Add(nowprice);
                }
                listBox2.Items.Add(" ");
                count = count + 10;


            }
        }
    }
}
