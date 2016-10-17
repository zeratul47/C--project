namespace ex5_2
{
    partial class ConnectionForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.LoginLabel = new System.Windows.Forms.Label();
            this.LogintextBox = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordtextBox = new System.Windows.Forms.TextBox();
            this.Connectbutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.StartstatusStrip = new System.Windows.Forms.StatusStrip();
            this.StartStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StartstatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginLabel.Location = new System.Drawing.Point(26, 24);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(47, 17);
            this.LoginLabel.TabIndex = 0;
            this.LoginLabel.Text = "Логин";
            // 
            // LogintextBox
            // 
            this.LogintextBox.Location = new System.Drawing.Point(117, 24);
            this.LogintextBox.Name = "LogintextBox";
            this.LogintextBox.Size = new System.Drawing.Size(218, 20);
            this.LogintextBox.TabIndex = 1;
            this.LogintextBox.MouseEnter += new System.EventHandler(this.LogintextBox_MouseEnter);
            this.LogintextBox.MouseLeave += new System.EventHandler(this.LogintextBox_MouseLeave);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PasswordLabel.Location = new System.Drawing.Point(29, 84);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(57, 17);
            this.PasswordLabel.TabIndex = 2;
            this.PasswordLabel.Text = "Пароль";
            // 
            // PasswordtextBox
            // 
            this.PasswordtextBox.Location = new System.Drawing.Point(117, 84);
            this.PasswordtextBox.Name = "PasswordtextBox";
            this.PasswordtextBox.Size = new System.Drawing.Size(218, 20);
            this.PasswordtextBox.TabIndex = 3;
            this.PasswordtextBox.MouseEnter += new System.EventHandler(this.PasswordtextBox_MouseEnter);
            this.PasswordtextBox.MouseLeave += new System.EventHandler(this.PasswordtextBox_MouseLeave);
            // 
            // Connectbutton
            // 
            this.Connectbutton.Location = new System.Drawing.Point(117, 141);
            this.Connectbutton.Name = "Connectbutton";
            this.Connectbutton.Size = new System.Drawing.Size(102, 23);
            this.Connectbutton.TabIndex = 4;
            this.Connectbutton.Text = "Подключиться";
            this.Connectbutton.UseVisualStyleBackColor = true;
            this.Connectbutton.Click += new System.EventHandler(this.Connectbutton_Click);
            this.Connectbutton.MouseEnter += new System.EventHandler(this.Connectbutton_MouseEnter);
            this.Connectbutton.MouseLeave += new System.EventHandler(this.Connectbutton_MouseLeave);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(225, 141);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(110, 23);
            this.Cancelbutton.TabIndex = 5;
            this.Cancelbutton.Text = "Отмена";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            this.Cancelbutton.MouseEnter += new System.EventHandler(this.Cancelbutton_MouseEnter);
            this.Cancelbutton.MouseLeave += new System.EventHandler(this.Cancelbutton_MouseLeave);
            // 
            // StartstatusStrip
            // 
            this.StartstatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartStripStatusLabel});
            this.StartstatusStrip.Location = new System.Drawing.Point(0, 205);
            this.StartstatusStrip.Name = "StartstatusStrip";
            this.StartstatusStrip.Size = new System.Drawing.Size(399, 22);
            this.StartstatusStrip.TabIndex = 6;
            this.StartstatusStrip.Text = "Статус";
            // 
            // StartStripStatusLabel
            // 
            this.StartStripStatusLabel.Name = "StartStripStatusLabel";
            this.StartStripStatusLabel.Size = new System.Drawing.Size(43, 17);
            this.StartStripStatusLabel.Text = "Статус";
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 227);
            this.Controls.Add(this.StartstatusStrip);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.Connectbutton);
            this.Controls.Add(this.PasswordtextBox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LogintextBox);
            this.Controls.Add(this.LoginLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionForm";
            this.Text = "Подключение";
            this.StartstatusStrip.ResumeLayout(false);
            this.StartstatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.TextBox LogintextBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordtextBox;
        private System.Windows.Forms.Button Connectbutton;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.StatusStrip StartstatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StartStripStatusLabel;
    }
}

