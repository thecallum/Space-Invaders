namespace Space_Invaders
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
            this.score_label = new System.Windows.Forms.Label();
            this.health_label = new System.Windows.Forms.Label();
            this.start_game_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // score_label
            // 
            this.score_label.AutoSize = true;
            this.score_label.BackColor = System.Drawing.Color.Black;
            this.score_label.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_label.ForeColor = System.Drawing.Color.White;
            this.score_label.Location = new System.Drawing.Point(12, 11);
            this.score_label.Name = "score_label";
            this.score_label.Size = new System.Drawing.Size(67, 18);
            this.score_label.TabIndex = 0;
            this.score_label.Text = "Score: 0";
            // 
            // health_label
            // 
            this.health_label.AutoSize = true;
            this.health_label.BackColor = System.Drawing.Color.Black;
            this.health_label.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.health_label.ForeColor = System.Drawing.Color.White;
            this.health_label.Location = new System.Drawing.Point(12, 36);
            this.health_label.Name = "health_label";
            this.health_label.Size = new System.Drawing.Size(69, 18);
            this.health_label.TabIndex = 1;
            this.health_label.Text = "Health: 0";
            // 
            // start_game_btn
            // 
            this.start_game_btn.BackColor = System.Drawing.Color.Black;
            this.start_game_btn.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_game_btn.ForeColor = System.Drawing.Color.White;
            this.start_game_btn.Location = new System.Drawing.Point(290, 442);
            this.start_game_btn.Name = "start_game_btn";
            this.start_game_btn.Size = new System.Drawing.Size(195, 72);
            this.start_game_btn.TabIndex = 2;
            this.start_game_btn.TabStop = false;
            this.start_game_btn.Text = "Start New Game";
            this.start_game_btn.UseVisualStyleBackColor = false;
            this.start_game_btn.Click += new System.EventHandler(this.start_game_btn_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.start_game_btn);
            this.Controls.Add(this.health_label);
            this.Controls.Add(this.score_label);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label score_label;
        private System.Windows.Forms.Label health_label;
        private System.Windows.Forms.Button start_game_btn;
    }
}

