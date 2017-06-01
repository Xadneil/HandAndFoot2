namespace HandAndFoot.Client
{
    partial class ChooseTeamControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdOtherPlayers = new System.Windows.Forms.DataGridView();
            this.lstChooseTeam = new System.Windows.Forms.ListBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdOtherPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // grdOtherPlayers
            // 
            this.grdOtherPlayers.AllowUserToAddRows = false;
            this.grdOtherPlayers.AllowUserToDeleteRows = false;
            this.grdOtherPlayers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdOtherPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOtherPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name1,
            this.Team});
            this.grdOtherPlayers.Location = new System.Drawing.Point(10, 10);
            this.grdOtherPlayers.Margin = new System.Windows.Forms.Padding(10);
            this.grdOtherPlayers.Name = "grdOtherPlayers";
            this.grdOtherPlayers.ReadOnly = true;
            this.grdOtherPlayers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdOtherPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdOtherPlayers.Size = new System.Drawing.Size(315, 150);
            this.grdOtherPlayers.TabIndex = 0;
            // 
            // lstChooseTeam
            // 
            this.lstChooseTeam.FormattingEnabled = true;
            this.lstChooseTeam.Location = new System.Drawing.Point(10, 173);
            this.lstChooseTeam.Name = "lstChooseTeam";
            this.lstChooseTeam.Size = new System.Drawing.Size(234, 95);
            this.lstChooseTeam.TabIndex = 1;
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(250, 210);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 2;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // Name1
            // 
            this.Name1.HeaderText = "Name";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            // 
            // Team
            // 
            this.Team.FillWeight = 50F;
            this.Team.HeaderText = "Team";
            this.Team.Name = "Team";
            this.Team.ReadOnly = true;
            // 
            // ChooseTeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.lstChooseTeam);
            this.Controls.Add(this.grdOtherPlayers);
            this.Name = "ChooseTeam";
            this.Size = new System.Drawing.Size(335, 278);
            ((System.ComponentModel.ISupportInitialize)(this.grdOtherPlayers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdOtherPlayers;
        private System.Windows.Forms.ListBox lstChooseTeam;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
    }
}
