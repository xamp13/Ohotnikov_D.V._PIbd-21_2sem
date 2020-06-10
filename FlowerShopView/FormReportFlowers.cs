using FlowerShopBusinessLogic.BusinessLogics;
using FlowerShopBusinessLogic.BindingModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace FlowerShopView
{
    public partial class FormReportFlowers : Form
    {
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportFlowers(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReportFlowers_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
            try
            {
                Console.WriteLine("set flowers");
                var dataSource = logic.GetStorageFlowers();
                ReportDataSource source = new ReportDataSource("DataSetFlowers",
               dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            this.reportViewer.RefreshReport();
        }

        private void buttonSaveToPdf_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveFlowersToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}