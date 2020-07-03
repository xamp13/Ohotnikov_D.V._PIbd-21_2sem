using FlowerShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace FlowerShopClientView
{
    public partial class FormMessages : Form
    {
        int curPage = 0;
        int perPage = 4;
        bool blocked = false;

        public FormMessages()
        {
            InitializeComponent();
            labelPage.Text = "Страница: 0";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = (ApiClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessagespage?clientId={Program.Client.Id}&skip={curPage * perPage}&take={perPage}"));

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