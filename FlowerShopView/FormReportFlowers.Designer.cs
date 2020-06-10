namespace FlowerShopView
{
    partial class FormReportFlowers
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportFlowersViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonSaveToPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ReportFlowersViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            reportDataSource2.Name = "DataSetFlowers";
            reportDataSource2.Value = this.ReportFlowersViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "FlowerShopView.ReportFlowers.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(31, 89);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1148, 884);
            this.reportViewer.TabIndex = 0;
            // 
            // ReportFlowersViewModelBindingSource
            // 
            this.ReportFlowersViewModelBindingSource.DataSource = typeof(FlowerShopBusinessLogic.ViewModels.ReportFlowersViewModel);
            // 
            // buttonSaveToPdf
            // 
            this.buttonSaveToPdf.Location = new System.Drawing.Point(933, 20);
            this.buttonSaveToPdf.Name = "buttonSaveToPdf";
            this.buttonSaveToPdf.Size = new System.Drawing.Size(245, 51);
            this.buttonSaveToPdf.TabIndex = 1;
            this.buttonSaveToPdf.Text = "Сохранить в PDF";
            this.buttonSaveToPdf.UseVisualStyleBackColor = true;
            this.buttonSaveToPdf.Click += new System.EventHandler(this.buttonSaveToPdf_Click);
            // 
            // FormReportFlowers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 1008);
            this.Controls.Add(this.buttonSaveToPdf);
            this.Controls.Add(this.reportViewer);
            this.Name = "FormReportFlowers";
            this.Text = "Отчёт по цветам со складами";
            this.Load += new System.EventHandler(this.FormReportFlowers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportFlowersViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportFlowersViewModelBindingSource;
        private System.Windows.Forms.Button buttonSaveToPdf;
    }
}