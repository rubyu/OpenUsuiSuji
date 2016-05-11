namespace OpenUsuiSuji
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (clients != null)
            {
                foreach (var name in clients.Keys)
                {
                    clients[name].Dispose();
                }
            }
            
            if (watchers != null)
            {
                foreach (var name in watchers.Keys)
                {
                    watchers[name].Dispose();
                }
            }
            
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelT1 = new System.Windows.Forms.Label();
            this.LabelT2 = new System.Windows.Forms.Label();
            this.T1Value = new System.Windows.Forms.Label();
            this.T2Value = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelT1
            // 
            this.LabelT1.AutoSize = true;
            this.LabelT1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelT1.Location = new System.Drawing.Point(106, 104);
            this.LabelT1.Name = "LabelT1";
            this.LabelT1.Size = new System.Drawing.Size(30, 19);
            this.LabelT1.TabIndex = 0;
            this.LabelT1.Text = "t1:";
            // 
            // LabelT2
            // 
            this.LabelT2.AutoSize = true;
            this.LabelT2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelT2.Location = new System.Drawing.Point(106, 123);
            this.LabelT2.Name = "LabelT2";
            this.LabelT2.Size = new System.Drawing.Size(30, 19);
            this.LabelT2.TabIndex = 1;
            this.LabelT2.Text = "t2:";
            // 
            // T1Value
            // 
            this.T1Value.AutoSize = true;
            this.T1Value.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T1Value.Location = new System.Drawing.Point(142, 104);
            this.T1Value.Name = "T1Value";
            this.T1Value.Size = new System.Drawing.Size(19, 19);
            this.T1Value.TabIndex = 3;
            this.T1Value.Text = "-";
            // 
            // T2Value
            // 
            this.T2Value.AutoSize = true;
            this.T2Value.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.T2Value.Location = new System.Drawing.Point(142, 124);
            this.T2Value.Name = "T2Value";
            this.T2Value.Size = new System.Drawing.Size(19, 19);
            this.T2Value.TabIndex = 4;
            this.T2Value.Text = "-";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.T2Value);
            this.Controls.Add(this.T1Value);
            this.Controls.Add(this.LabelT2);
            this.Controls.Add(this.LabelT1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelT1;
        private System.Windows.Forms.Label LabelT2;
        private System.Windows.Forms.Label T1Value;
        private System.Windows.Forms.Label T2Value;
    }
}

