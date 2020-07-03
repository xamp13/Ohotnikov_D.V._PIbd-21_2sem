using FlowerShopBusinessLogic.Interfaces;
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
using FlowerShopBusinessLogic.ViewModels;

namespace FlowerShopView
{
    public partial class FormMessages : Form
    { 
        int curPage = 0;
        int perPage = 4;
        bool blocked = false;

        private readonly IMessageInfoLogic logic;

        public FormMessages(IMessageInfoLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            labelPage.Text = "Страница: 0";
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null).Skip(curPage * perPage).Take(perPage).ToList();

                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                }

                blocked = list.Count < perPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (blocked)
            {
                return;
            }

            curPage++;
            LoadData();
        }

        private void buttonNazad_Click(object sender, EventArgs e)
        {
            curPage = Math.Max(0, curPage - 1);
            LoadData();
        }
    }
}