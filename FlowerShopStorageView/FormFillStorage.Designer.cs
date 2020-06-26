namespace FlowerShopStorageView
{
    partial class FormFillStorage
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxFlower = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelFlower = new System.Windows.Forms.Label();
            this.labelStorages = new System.Windows.Forms.Label();
            this.labelStorage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(173, 93);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 26);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(91, 93);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(79, 26);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxFlower
            // 
            this.comboBoxFlower.FormattingEnabled = true;
            this.comboBoxFlower.Location = new System.Drawing.Point(97, 33);
            this.comboBoxFlower.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxFlower.Name = "comboBoxFlower";
            this.comboBoxFlower.Size = new System.Drawing.Size(165, 21);
            this.comboBoxFlower.TabIndex = 9;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(97, 60);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(165, 20);
            this.textBoxCount.TabIndex = 8;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(8, 63);
            this.labelCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 7;
            this.labelCount.Text = "Количество:";
            // 
            // labelFlower
            // 
            this.labelFlower.AutoSize = true;
            this.labelFlower.Location = new System.Drawing.Point(9, 36);
            this.labelFlower.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFlower.Name = "labelFlower";
            this.labelFlower.Size = new System.Drawing.Size(66, 13);
            this.labelFlower.TabIndex = 6;
            this.labelFlower.Text = "Цветок:";
            // 
            // labelStorages
            // 
            this.labelStorages.AutoSize = true;
            this.labelStorages.Location = new System.Drawing.Point(9, 10);
            this.labelStorages.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStorages.Name = "labelStorages";
            this.labelStorages.Size = new System.Drawing.Size(41, 13);
            this.labelStorages.TabIndex = 12;
            this.labelStorages.Text = "Склад:";
            // 
            // labelStorage
            // 
            this.labelStorage.AutoSize = true;
            this.labelStorage.Location = new System.Drawing.Point(101, 10);
            this.labelStorage.Name = "labelStorage";
            this.labelStorage.Size = new System.Drawing.Size(0, 13);
            this.labelStorage.TabIndex = 13;
            // 
            // FormFillStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 140);
            this.Controls.Add(this.labelStorage);
            this.Controls.Add(this.labelStorages);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxFlower);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelFlower);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormFillStorage";
            this.Text = "Пополнить склад";
            this.Load += new System.EventHandler(this.FormFillStorage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox comboBoxFlower;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelFlower;
        private System.Windows.Forms.Label labelStorages;
        public System.Windows.Forms.Label labelStorage;
    }
}