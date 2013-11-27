namespace GraphicsEngine
{
	partial class Form1
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
			this.moneyLbl = new System.Windows.Forms.Label();
			this.towerHealthPb = new System.Windows.Forms.ProgressBar();
			this.sentryPurchaseBtn = new System.Windows.Forms.Button();
			this.nextRoundBtn = new System.Windows.Forms.Button();
			this.statusLbl = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.levelLbl = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// moneyLbl
			// 
			this.moneyLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.moneyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.moneyLbl.Location = new System.Drawing.Point(335, 9);
			this.moneyLbl.Name = "moneyLbl";
			this.moneyLbl.Size = new System.Drawing.Size(564, 33);
			this.moneyLbl.TabIndex = 0;
			this.moneyLbl.Text = "You have - NK Won";
			this.moneyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// towerHealthPb
			// 
			this.towerHealthPb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.towerHealthPb.Location = new System.Drawing.Point(12, 524);
			this.towerHealthPb.Name = "towerHealthPb";
			this.towerHealthPb.Size = new System.Drawing.Size(145, 29);
			this.towerHealthPb.TabIndex = 1;
			this.towerHealthPb.Value = 100;
			// 
			// sentryPurchaseBtn
			// 
			this.sentryPurchaseBtn.Location = new System.Drawing.Point(6, 19);
			this.sentryPurchaseBtn.Name = "sentryPurchaseBtn";
			this.sentryPurchaseBtn.Size = new System.Drawing.Size(147, 23);
			this.sentryPurchaseBtn.TabIndex = 2;
			this.sentryPurchaseBtn.Text = "Sentry - 50 NK Won";
			this.sentryPurchaseBtn.UseVisualStyleBackColor = true;
			this.sentryPurchaseBtn.Click += new System.EventHandler(this.sentryPurchaseBtn_Click);
			// 
			// nextRoundBtn
			// 
			this.nextRoundBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nextRoundBtn.Location = new System.Drawing.Point(792, 524);
			this.nextRoundBtn.Name = "nextRoundBtn";
			this.nextRoundBtn.Size = new System.Drawing.Size(107, 29);
			this.nextRoundBtn.TabIndex = 3;
			this.nextRoundBtn.Text = "Next Round >>";
			this.nextRoundBtn.UseVisualStyleBackColor = true;
			this.nextRoundBtn.Visible = false;
			this.nextRoundBtn.Click += new System.EventHandler(this.nextRoundBtn_Click);
			// 
			// statusLbl
			// 
			this.statusLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.statusLbl.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusLbl.Location = new System.Drawing.Point(163, 524);
			this.statusLbl.Name = "statusLbl";
			this.statusLbl.Size = new System.Drawing.Size(623, 29);
			this.statusLbl.TabIndex = 4;
			this.statusLbl.Text = "Goomba Defense - Ready!";
			this.statusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.sentryPurchaseBtn);
			this.groupBox1.Location = new System.Drawing.Point(12, 9);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 77);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Weapon Shop";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(6, 48);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(147, 23);
			this.button3.TabIndex = 5;
			this.button3.Text = "Ultra Sentry - 500 NK Won";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(159, 19);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(147, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Double Sentry - 125 NK Won";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(159, 48);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(147, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Ray Gun - 750 NK Won";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// levelLbl
			// 
			this.levelLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.levelLbl.Font = new System.Drawing.Font("Segoe WP SemiLight", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.levelLbl.Location = new System.Drawing.Point(330, 42);
			this.levelLbl.Name = "levelLbl";
			this.levelLbl.Size = new System.Drawing.Size(569, 23);
			this.levelLbl.TabIndex = 6;
			this.levelLbl.Text = "Level 1";
			this.levelLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(911, 565);
			this.Controls.Add(this.levelLbl);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusLbl);
			this.Controls.Add(this.nextRoundBtn);
			this.Controls.Add(this.towerHealthPb);
			this.Controls.Add(this.moneyLbl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Goomba Defense <Andrew Kim>";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label moneyLbl;
		private System.Windows.Forms.ProgressBar towerHealthPb;
		private System.Windows.Forms.Button sentryPurchaseBtn;
		private System.Windows.Forms.Button nextRoundBtn;
		private System.Windows.Forms.Label statusLbl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label levelLbl;
	}
}

