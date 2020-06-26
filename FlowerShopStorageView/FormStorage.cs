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
    public partial class FormStorage : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        private List<StorageFlowerViewModel> storageFlowers;
        public FormStorage()
        {
            InitializeComponent();
        }
        private void FormStorage_Load(object sender, EventArgs e)
        {
            dataGridView.Columns.Add("Id", "Id");
            dataGridView.Columns.Add("Цветок", "Цветок");
            dataGridView.Columns.Add("Количество", "Количество");
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (id.HasValue)
            {
                try
                {
                    StorageViewModel view = ApiStorage.GetRequest<StorageViewModel>($"api/storage/getstorage?storageId={id.Value}");
                    if (view != null)
                    {
                        storageNameTextBox.Text = view.StorageName;
                        storageFlowers = view.StorageFlowers;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                storageFlowers = new List<StorageFlowerViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (storageFlowers != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var storageFlower in storageFlowers)
                    {
                        dataGridView.Rows.Add(new object[] { storageFlower.Id, storageFlower.FlowerName, storageFlower.Count });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(storageNameTextBox.Text))
            {
                MessageBox.Show("Заполните поле Название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ApiStorage.PostRequest("api/Storage/createorupdatestorage", new StorageBindingModel
                {
                    Id = id,
                    StorageName = storageNameTextBox.Text
                });
                MessageBox.Show("Успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}