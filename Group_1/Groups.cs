using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace Group_1
{
    public partial class Groups : Form
    {
        public Groups()
        {
            InitializeComponent();
        }

        Dictionary<string, Group> pubs = new Dictionary<string, Group>();

        private void Groups_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        VkApi api;

        private void Groups_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string likes, vievs, reposts;
            textBox1.Text = "";
            api = new VkApi();
            api.Authorize(new ApiAuthParams
            { AccessToken = Auth.getAuthForUsers() });
            try
            {
                WallGetObject Wall = api.Wall.Get(new WallGetParams
                {
                    OwnerId = pubs[comboBox1.SelectedItem.ToString()].Id * -1,
                    Extended = true,
                    Count = 10
                });
                foreach (VkNet.Model.Attachments.Post Post in Wall.WallPosts)
                {
                    if (Post.Likes == null)
                        likes = "Отсутствует";
                    else
                        likes = Post.Likes.Count.ToString();
                    if (Post.Views == null)
                        vievs = "Отстуствует";
                    else
                        vievs = Post.Views.Count.ToString();
                    if (Post.Reposts == null)
                        reposts = "Отсутствует";
                    else
                        reposts = Post.Reposts.Count.ToString();
                    textBox1.Text += $"Дата поста: {Post.Date}, Количество просмотров: {vievs}, Количество репостов: {reposts}, Количество лайков: {likes} \r\n";
                    if (Post.Text == "")
                        textBox1.Text += "\r\n \r\n \r\n \r\n";
                    else
                        textBox1.Text += Encoding.UTF8.GetString(Encoding.Default.GetBytes(Post.Text)) + "\r\n" + "\r\n";
                }
            }
            catch
            {
                textBox1.Text = "Группа закрытая";
            }
        }
            

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                api = new VkApi();
                api.Authorize(new ApiAuthParams
                { AccessToken = Auth.getAuthForUsers() });
                var Groups = api.Groups.Get(new GroupsGetParams
                {
                    UserId = Convert.ToInt32(textBox2.Text),
                    Extended = true
                });

                foreach (Group gr in Groups)
                {
                    pubs.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(gr.Name)), gr);
                    comboBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(gr.Name)));
                }
            }
            catch
            {
                textBox1.Text = "Профиль скрыт";
            }
            button1.Enabled = true;
        }
    }
}
