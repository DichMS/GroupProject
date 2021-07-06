using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.IO;

namespace Group_1
{
    class Auth
    {
        public static string getAuthForUsers()
        {
            string token = "";
            string fileName = "C://Users/Dusha/Desktop/auth_vk.txt"; // файл с токенами
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    token = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }

        public static string getAuthForGroups()
        {
            string token = "";
            string fileName = "C://Users/Dusha/Desktop/auth_vk.txt";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    sr.ReadLine();
                    token = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }
    }
}
