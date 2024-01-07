using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace HomeV2
{
    public class HomeManager
    {
        public static List<Home> Homes { get; set; } = new List<Home>();
        public static Vector3 backPos = Vector3.zero;
        public static EntityPlayerLocal player = new EntityPlayerLocal();
        public static string dllPath = Assembly.GetExecutingAssembly().Location.Replace("HomeV2.dll", "");
        public static string homestring = dllPath+"\\config\\homes.json";



        public static void GoHome(Home home)
        {
            home = Homes.Find((h) => h.Name == home.Name);
            if (home != null)
            {
                player.SetPosition(home.Position);
            }
        }

        public static void AddHome(Home home)
        {
            Home _home = Homes.Find((h) => h.Name == home.Name);
            if (_home != null)
                GameManager.ShowTooltip(player, $"{home.Name}添加失败 已经存在");
            else
            {
                Homes.Add(home);
                GameManager.ShowTooltip(player, $"{home.Name}添加成功");
                SaveHome();
                LoadHome();
            }
        }

        public static void RemoveHome(Home home)
        {
            home = Homes.Find((h) => h.Name == home.Name);
            if (home == null)
                GameManager.ShowTooltip(new EntityPlayerLocal(), $"{home.Name}删除失败 不存在");
            else
            {
                GameManager.ShowTooltip(new EntityPlayerLocal(), $"{home.Name}删除成功");
                Homes.Remove(home);
                SaveHome();
                LoadHome();
            }
        }

        public static void Back()
        {
            if (backPos != Vector3.zero)
            {
                player.SetPosition(backPos);
            }
        }

        public static void RemoveAll()
        {
            Homes.Clear();
            SaveHome();
            LoadHome();
        }

        public static void SaveHome()
        {

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string contents = JsonConvert.SerializeObject(HomeManager.Homes, settings);
            File.WriteAllText(homestring, contents);
        }
        

        public static void InitHome()
        {
            
            if (!File.Exists(homestring))
            {
                try
                {
                    Directory.CreateDirectory(HomeManager.dllPath + "config");
                    try
                    {
                        File.Create(homestring).Close();
                    }
                    catch (Exception ex)
                    {
                        Log.Out("创建文件失败" + ex.Message);
                    }
                }
                catch (Exception ex2)
                {
                    Log.Out("创建文件夹失败" + ex2.Message);
                }
               
            }
            LoadHome();
        }


        private static void LoadHome()
        {
            HomeManager.Homes.Clear();
            string text = File.ReadAllText(HomeManager.homestring);
            bool flag = text.Length >= 10;
            if (flag)
            {
                HomeManager.Homes = JsonConvert.DeserializeObject<List<Home>>(text);
            }
        }
    }
}
