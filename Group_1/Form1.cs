using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.IO;

namespace Group_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form Work = new Work();
        Form Posts = new Posts();
        Form Ages = new Ages();
        Form Groups = new Groups();

        private void button1_Click(object sender, EventArgs e)
        {
            var api_user = new VkApi();
            try
            {
                api_user.Authorize(new ApiAuthParams
                { AccessToken = Auth.getAuthForUsers()});
            }
            catch (Exception q)
            {
                Console.WriteLine(q.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var api_group = new VkApi();
            try
            {
                api_group.Authorize(new ApiAuthParams
                { AccessToken = Auth.getAuthForGroups() });
            }
            catch (Exception q)
            {
                Console.WriteLine(q.Message);
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Work.Show();
                    this.Hide();
                    break;
                case 1:
                    Posts.Show();
                    this.Hide();
                    break;
                case 2:
                    Ages.Show();
                    this.Hide();
                    break;
                case 3:
                    Groups.Show();
                    this.Hide();
                    break;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            comboBox1.Items.Add("Список друзей или список подписчиков сообщества");
            comboBox1.Items.Add("Добавить запись на стену");
            comboBox1.Items.Add("Узнать возраст пользователя");
            comboBox1.Items.Add("Вывести публикации группы");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }
    }
}
