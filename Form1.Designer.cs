
namespace GiveMeTheMoney
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.상위값조회 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.계좌정보호출 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.구매창 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.SuspendLayout();
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(997, 572);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(100, 50);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "로그인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 74);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(193, 52);
            this.listBox1.TabIndex = 2;
            // 
            // 상위값조회
            // 
            this.상위값조회.Location = new System.Drawing.Point(27, 202);
            this.상위값조회.Name = "상위값조회";
            this.상위값조회.Size = new System.Drawing.Size(163, 70);
            this.상위값조회.TabIndex = 8;
            this.상위값조회.Text = "상위값조회";
            this.상위값조회.UseVisualStyleBackColor = true;
            this.상위값조회.Click += new System.EventHandler(this.상위값조회_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(227, 202);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(395, 400);
            this.listBox2.TabIndex = 9;
            // 
            // 계좌정보호출
            // 
            this.계좌정보호출.Location = new System.Drawing.Point(303, 12);
            this.계좌정보호출.Name = "계좌정보호출";
            this.계좌정보호출.Size = new System.Drawing.Size(125, 50);
            this.계좌정보호출.TabIndex = 10;
            this.계좌정보호출.Text = "계좌정보호출";
            this.계좌정보호출.UseVisualStyleBackColor = true;
            this.계좌정보호출.Click += new System.EventHandler(this.계좌정보호출_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(979, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 구매창
            // 
            this.구매창.FormattingEnabled = true;
            this.구매창.ItemHeight = 12;
            this.구매창.Location = new System.Drawing.Point(642, 202);
            this.구매창.Name = "구매창";
            this.구매창.Size = new System.Drawing.Size(336, 400);
            this.구매창.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(979, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(979, 84);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 624);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.구매창);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.계좌정보호출);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.상위값조회);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "Form1";
            this.Text = "알바비충당";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button 상위값조회;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button 계좌정보호출;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox 구매창;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

