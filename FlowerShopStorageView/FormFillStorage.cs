using FlowerShopBusinessLogic.BindingModels;
using FlowerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlowerShopStorageView
{
    public partial class FormFillStorage : Form
    {
        private int id;

        public FormFillStorage(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        private void FormFillStorage_Load(object sender, System.EventArgs e)
        {
            try
            {
                List<FlowerViewModel> list = ApiStorage.GetRequest<List<FlowerViewModel>>($"api/Storage/getflowerslist");
                if (list != null)
                {
                    comboBoxFlower.DisplayMember = "FlowerName";
                    comboBoxFlower.ValueMember = "Id";
                    comboBoxFlower.DataSource = list;
                    comboBoxFlower.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFlower.SelectedValue == null)
            {
                MessageBox.Show("Выберите цветок", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ApiStorage.PostRequest("api/Storage/fillstorage", new StorageFlowerBindingModel
                {
                    Id = 0,
                    StorageId = id,
                    FlowerId = Convert.ToInt32(comboBoxFlower.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}