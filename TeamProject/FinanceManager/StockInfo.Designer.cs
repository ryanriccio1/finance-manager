namespace FinanceManager
{
    partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGetData = new System.Windows.Forms.Button();
            this.labelSymbol = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxSymbol = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.labelChangePercent = new System.Windows.Forms.Label();
            this.textBoxChange = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonGetData
            // 
            this.buttonGetData.Location = new System.Drawing.Point(15, 32);
            this.buttonGetData.Name = "buttonGetData";
            this.buttonGetData.Size = new System.Drawing.Size(178, 32);
            this.buttonGetData.TabIndex = 0;
            this.buttonGetData.Text = "GetData";
            this.buttonGetData.UseVisualStyleBackColor = true;
            this.buttonGetData.Click += new System.EventHandler(this.buttonGetData_Click);
            // 
            // labelSymbol
            // 
            this.labelSymbol.AutoSize = true;
            this.labelSymbol.Location = new System.Drawing.Point(12, 9);
            this.labelSymbol.Name = "labelSymbol";
            this.labelSymbol.Size = new System.Drawing.Size(75, 13);
            this.labelSymbol.TabIndex = 1;
            this.labelSymbol.Text = "Stock Symbol:";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(12, 73);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(65, 13);
            this.labelPrice.TabIndex = 2;
            this.labelPrice.Text = "Stock Price:";
            // 
            // textBoxSymbol
            // 
            this.textBoxSymbol.Location = new System.Drawing.Point(93, 6);
            this.textBoxSymbol.Name = "textBoxSymbol";
            this.textBoxSymbol.Size = new System.Drawing.Size(100, 20);
            this.textBoxSymbol.TabIndex = 3;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(93, 70);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(100, 20);
            this.textBoxPrice.TabIndex = 4;
            // 
            // labelChangePercent
            // 
            this.labelChangePercent.AutoSize = true;
            this.labelChangePercent.Location = new System.Drawing.Point(12, 99);
            this.labelChangePercent.Name = "labelChangePercent";
            this.labelChangePercent.Size = new System.Drawing.Size(47, 13);
            this.labelChangePercent.TabIndex = 5;
            this.labelChangePercent.Text = "Change:";
            // 
            // textBoxChange
            // 
            this.textBoxChange.Location = new System.Drawing.Point(93, 96);
            this.textBoxChange.Name = "textBoxChange";
            this.textBoxChange.Size = new System.Drawing.Size(100, 20);
            this.textBoxChange.TabIndex = 6;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 126);
            this.Controls.Add(this.textBoxChange);
            this.Controls.Add(this.labelChangePercent);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.textBoxSymbol);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.labelSymbol);
            this.Controls.Add(this.buttonGetData);
            this.Name = "formMain";
            this.Text = "Finance Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetData;
        private System.Windows.Forms.Label labelSymbol;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxSymbol;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label labelChangePercent;
        private System.Windows.Forms.TextBox textBoxChange;
    }
}

