namespace PiratesVisualization
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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.checkBoxVisualize = new System.Windows.Forms.CheckBox();
            this.numericUpDownIslands = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.numericUpDownEvaporation = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownIterations = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPirates = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBestGrade = new System.Windows.Forms.Label();
            this.labelIterationNum = new System.Windows.Forms.Label();
            this.simulationTimer = new System.Windows.Forms.Timer(this.components);
            this.radioButtonACO = new System.Windows.Forms.RadioButton();
            this.radioButtonBT = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIslands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEvaporation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPirates)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGraph
            // 
            this.pictureBoxGraph.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxGraph.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxGraph.Location = new System.Drawing.Point(195, 0);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(604, 450);
            this.pictureBoxGraph.TabIndex = 0;
            this.pictureBoxGraph.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.radioButtonBT);
            this.panel1.Controls.Add(this.radioButtonACO);
            this.panel1.Controls.Add(this.buttonEnd);
            this.panel1.Controls.Add(this.checkBoxVisualize);
            this.panel1.Controls.Add(this.numericUpDownIslands);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Controls.Add(this.numericUpDownEvaporation);
            this.panel1.Controls.Add(this.numericUpDownIterations);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericUpDownPirates);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 450);
            this.panel1.TabIndex = 1;
            // 
            // buttonEnd
            // 
            this.buttonEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEnd.Location = new System.Drawing.Point(8, 376);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(75, 23);
            this.buttonEnd.TabIndex = 14;
            this.buttonEnd.Text = "end";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // checkBoxVisualize
            // 
            this.checkBoxVisualize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxVisualize.AutoSize = true;
            this.checkBoxVisualize.Checked = true;
            this.checkBoxVisualize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxVisualize.Location = new System.Drawing.Point(6, 239);
            this.checkBoxVisualize.Name = "checkBoxVisualize";
            this.checkBoxVisualize.Size = new System.Drawing.Size(81, 20);
            this.checkBoxVisualize.TabIndex = 13;
            this.checkBoxVisualize.Text = "visualize";
            this.checkBoxVisualize.UseVisualStyleBackColor = true;
            // 
            // numericUpDownIslands
            // 
            this.numericUpDownIslands.Location = new System.Drawing.Point(89, 76);
            this.numericUpDownIslands.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownIslands.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownIslands.Name = "numericUpDownIslands";
            this.numericUpDownIslands.Size = new System.Drawing.Size(78, 22);
            this.numericUpDownIslands.TabIndex = 12;
            this.numericUpDownIslands.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label4.Location = new System.Drawing.Point(5, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "islands";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(8, 338);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 10;
            this.buttonStart.Text = "start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReset.Location = new System.Drawing.Point(8, 415);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 9;
            this.buttonReset.Text = "reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // numericUpDownEvaporation
            // 
            this.numericUpDownEvaporation.DecimalPlaces = 2;
            this.numericUpDownEvaporation.Location = new System.Drawing.Point(92, 157);
            this.numericUpDownEvaporation.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEvaporation.Name = "numericUpDownEvaporation";
            this.numericUpDownEvaporation.Size = new System.Drawing.Size(75, 22);
            this.numericUpDownEvaporation.TabIndex = 6;
            this.numericUpDownEvaporation.Value = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            // 
            // numericUpDownIterations
            // 
            this.numericUpDownIterations.Location = new System.Drawing.Point(92, 118);
            this.numericUpDownIterations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownIterations.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownIterations.Name = "numericUpDownIterations";
            this.numericUpDownIterations.Size = new System.Drawing.Size(78, 22);
            this.numericUpDownIterations.TabIndex = 5;
            this.numericUpDownIterations.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Location = new System.Drawing.Point(-3, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "evapotation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(3, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "iterations";
            // 
            // numericUpDownPirates
            // 
            this.numericUpDownPirates.Location = new System.Drawing.Point(89, 38);
            this.numericUpDownPirates.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownPirates.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPirates.Name = "numericUpDownPirates";
            this.numericUpDownPirates.Size = new System.Drawing.Size(78, 22);
            this.numericUpDownPirates.TabIndex = 1;
            this.numericUpDownPirates.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(7, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "pirates";
            // 
            // labelBestGrade
            // 
            this.labelBestGrade.AutoSize = true;
            this.labelBestGrade.BackColor = System.Drawing.SystemColors.HighlightText;
            this.labelBestGrade.Location = new System.Drawing.Point(207, 52);
            this.labelBestGrade.Name = "labelBestGrade";
            this.labelBestGrade.Size = new System.Drawing.Size(72, 16);
            this.labelBestGrade.TabIndex = 11;
            this.labelBestGrade.Text = "best grade";
            // 
            // labelIterationNum
            // 
            this.labelIterationNum.AutoSize = true;
            this.labelIterationNum.BackColor = System.Drawing.SystemColors.HighlightText;
            this.labelIterationNum.Location = new System.Drawing.Point(207, 18);
            this.labelIterationNum.Name = "labelIterationNum";
            this.labelIterationNum.Size = new System.Drawing.Size(54, 16);
            this.labelIterationNum.TabIndex = 12;
            this.labelIterationNum.Text = "iteration";
            // 
            // radioButtonACO
            // 
            this.radioButtonACO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonACO.AutoSize = true;
            this.radioButtonACO.Checked = true;
            this.radioButtonACO.Location = new System.Drawing.Point(8, 275);
            this.radioButtonACO.Name = "radioButtonACO";
            this.radioButtonACO.Size = new System.Drawing.Size(56, 20);
            this.radioButtonACO.TabIndex = 15;
            this.radioButtonACO.Text = "ACO";
            this.radioButtonACO.UseVisualStyleBackColor = true;
            this.radioButtonACO.CheckedChanged += new System.EventHandler(this.radioButtonACO_CheckedChanged);
            // 
            // radioButtonBT
            // 
            this.radioButtonBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonBT.AutoSize = true;
            this.radioButtonBT.Location = new System.Drawing.Point(8, 301);
            this.radioButtonBT.Name = "radioButtonBT";
            this.radioButtonBT.Size = new System.Drawing.Size(115, 20);
            this.radioButtonBT.TabIndex = 16;
            this.radioButtonBT.Text = "Back Tracking";
            this.radioButtonBT.UseVisualStyleBackColor = true;
            this.radioButtonBT.CheckedChanged += new System.EventHandler(this.radioButtonBT_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelBestGrade);
            this.Controls.Add(this.labelIterationNum);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBoxGraph);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIslands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEvaporation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPirates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGraph;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericUpDownEvaporation;
        private System.Windows.Forms.NumericUpDown numericUpDownIterations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPirates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelBestGrade;
        private System.Windows.Forms.Label labelIterationNum;
        private System.Windows.Forms.Timer simulationTimer;
        private System.Windows.Forms.NumericUpDown numericUpDownIslands;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxVisualize;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.RadioButton radioButtonACO;
        private System.Windows.Forms.RadioButton radioButtonBT;
    }
}

