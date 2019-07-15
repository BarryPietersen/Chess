namespace ChessGui
{
    partial class FrmMainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainMenu));
            this.BtnNewGame = new System.Windows.Forms.Button();
            this.CboColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asWhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asBlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkMateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enPassantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.castlingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staleMateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pawnPromotionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChkEnforce = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnNewGame
            // 
            this.BtnNewGame.Location = new System.Drawing.Point(82, 162);
            this.BtnNewGame.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNewGame.Name = "BtnNewGame";
            this.BtnNewGame.Size = new System.Drawing.Size(90, 28);
            this.BtnNewGame.TabIndex = 0;
            this.BtnNewGame.Text = "New Game";
            this.BtnNewGame.UseVisualStyleBackColor = true;
            this.BtnNewGame.Click += new System.EventHandler(this.BtnNewGame_Click);
            // 
            // CboColor
            // 
            this.CboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboColor.FormattingEnabled = true;
            this.CboColor.Items.AddRange(new object[] {
            "White",
            "Black"});
            this.CboColor.Location = new System.Drawing.Point(82, 75);
            this.CboColor.Margin = new System.Windows.Forms.Padding(4);
            this.CboColor.Name = "CboColor";
            this.CboColor.Size = new System.Drawing.Size(90, 24);
            this.CboColor.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Color";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.newGameToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(584, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem1
            // 
            this.newGameToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asWhiteToolStripMenuItem,
            this.asBlackToolStripMenuItem});
            this.newGameToolStripMenuItem1.Name = "newGameToolStripMenuItem1";
            this.newGameToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.newGameToolStripMenuItem1.Text = "New Game";
            this.newGameToolStripMenuItem1.Click += new System.EventHandler(this.BtnNewGame_Click);
            // 
            // asWhiteToolStripMenuItem
            // 
            this.asWhiteToolStripMenuItem.Name = "asWhiteToolStripMenuItem";
            this.asWhiteToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.asWhiteToolStripMenuItem.Text = "as white";
            this.asWhiteToolStripMenuItem.Click += new System.EventHandler(this.MenuItemNewGame_Click);
            // 
            // asBlackToolStripMenuItem
            // 
            this.asBlackToolStripMenuItem.Name = "asBlackToolStripMenuItem";
            this.asBlackToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.asBlackToolStripMenuItem.Text = "as black";
            this.asBlackToolStripMenuItem.Click += new System.EventHandler(this.MenuItemNewGame_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.ruleBookToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.newGameToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.DisplayAbout_Click);
            // 
            // ruleBookToolStripMenuItem
            // 
            this.ruleBookToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkToolStripMenuItem,
            this.checkMateToolStripMenuItem,
            this.enPassantToolStripMenuItem,
            this.castlingToolStripMenuItem,
            this.staleMateToolStripMenuItem,
            this.pawnPromotionsToolStripMenuItem});
            this.ruleBookToolStripMenuItem.Name = "ruleBookToolStripMenuItem";
            this.ruleBookToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ruleBookToolStripMenuItem.Text = "Rule Book";
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.checkToolStripMenuItem.Text = "check";
            // 
            // checkMateToolStripMenuItem
            // 
            this.checkMateToolStripMenuItem.Name = "checkMateToolStripMenuItem";
            this.checkMateToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.checkMateToolStripMenuItem.Text = "check mate";
            // 
            // enPassantToolStripMenuItem
            // 
            this.enPassantToolStripMenuItem.Name = "enPassantToolStripMenuItem";
            this.enPassantToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.enPassantToolStripMenuItem.Text = "en passant";
            // 
            // castlingToolStripMenuItem
            // 
            this.castlingToolStripMenuItem.Name = "castlingToolStripMenuItem";
            this.castlingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.castlingToolStripMenuItem.Text = "castling";
            // 
            // staleMateToolStripMenuItem
            // 
            this.staleMateToolStripMenuItem.Name = "staleMateToolStripMenuItem";
            this.staleMateToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.staleMateToolStripMenuItem.Text = "stale mate";
            // 
            // pawnPromotionsToolStripMenuItem
            // 
            this.pawnPromotionsToolStripMenuItem.Name = "pawnPromotionsToolStripMenuItem";
            this.pawnPromotionsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pawnPromotionsToolStripMenuItem.Text = "pawn promotions";
            // 
            // ChkEnforce
            // 
            this.ChkEnforce.AutoSize = true;
            this.ChkEnforce.Location = new System.Drawing.Point(82, 135);
            this.ChkEnforce.Name = "ChkEnforce";
            this.ChkEnforce.Size = new System.Drawing.Size(110, 20);
            this.ChkEnforce.TabIndex = 6;
            this.ChkEnforce.Text = "Enforce Turns";
            this.ChkEnforce.UseVisualStyleBackColor = true;
            // 
            // FrmMainMenu
            // 
            this.AcceptButton = this.BtnNewGame;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(584, 515);
            this.Controls.Add(this.ChkEnforce);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CboColor);
            this.Controls.Add(this.BtnNewGame);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnNewGame;
        private System.Windows.Forms.ComboBox CboColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asWhiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asBlackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ruleBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkMateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enPassantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem castlingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staleMateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pawnPromotionsToolStripMenuItem;
        private System.Windows.Forms.CheckBox ChkEnforce;
    }
}

