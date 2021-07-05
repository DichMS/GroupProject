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
using ReqPar = VkNet.Model.RequestParams;
using System.IO;

namespace Group_1
{
    public partial class Work : Form
    {
        public Work()
        {
            InitializeComponent();
        }

        private void Work_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("Получить список друзей");
            comboBox1.Items.Add("Получить список участников сообщества");
            button1.Enabled = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var api_group = new VkApi();
            var api_user = new VkApi();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    api_user.Authorize(new ApiAuthParams
                    { AccessToken = Auth.getAuthForUsers() });
                    var get_Friends = api_user.Friends.Get(new ReqPar.FriendsGetParams
                    { Fields = VkNet.Enums.Filters.ProfileFields.All });
                    foreach (User user in get_Friends)
                        listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName + " " + user.LastName)));
                    break;
                case 1:
                    api_group.Authorize(new ApiAuthParams
                    { AccessToken = Auth.getAuthForGroups() });
                    var getFollowers = api_group.Groups.GetMembers(new ReqPar.GroupsGetMembersParams() { GroupId = "204320459", Fields = VkNet.Enums.Filters.UsersFields.FirstNameAbl });
                    foreach (User user in getFollowers)
                        listBox1.Items.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(user.FirstName + " " + user.LastName)));
                    break;
            }
        }

        private void Work_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
