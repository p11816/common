namespace TicTacToe
{
    partial class GameField
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
            this.components = new System.ComponentModel.Container();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выборИгроковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.игрокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.компьютерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.AutoSize = false;
            this.MenuStrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.играToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MenuStrip.Size = new System.Drawing.Size(284, 20);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadToolStripMenuItem1,
            this.SaveToolStripMenuItem});
            this.файлToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 16);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // LoadToolStripMenuItem1
            // 
            this.LoadToolStripMenuItem1.Name = "LoadToolStripMenuItem1";
            this.LoadToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.LoadToolStripMenuItem1.Text = "Загрузить";
            this.LoadToolStripMenuItem1.Click += new System.EventHandler(this.LoadToolStripMenuItem1_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.SaveToolStripMenuItem.Text = "Сохранить как...";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выборИгроковToolStripMenuItem});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(46, 16);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // выборИгроковToolStripMenuItem
            // 
            this.выборИгроковToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.игрокToolStripMenuItem,
            this.компьютерToolStripMenuItem});
            this.выборИгроковToolStripMenuItem.Name = "выборИгроковToolStripMenuItem";
            this.выборИгроковToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.выборИгроковToolStripMenuItem.Text = "Выбор противника";
            // 
            // игрокToolStripMenuItem
            // 
            this.игрокToolStripMenuItem.Name = "игрокToolStripMenuItem";
            this.игрокToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.игрокToolStripMenuItem.Text = "Игрок";
            // 
            // компьютерToolStripMenuItem
            // 
            this.компьютерToolStripMenuItem.Name = "компьютерToolStripMenuItem";
            this.компьютерToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.компьютерToolStripMenuItem.Text = "Компьютер";
            this.компьютерToolStripMenuItem.Click += new System.EventHandler(this.ComputerToolStripMenuItem_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog";
            // 
            // timer1
            // 
            this.timer1.Interval = 1300;
            this.timer1.Tick += new System.EventHandler(this.changeStaticPicture);
            // 
            // GameField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "GameField";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Крестики Нолики";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.GameField_Resize);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выборИгроковToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem игрокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem компьютерToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
       
    }
}

