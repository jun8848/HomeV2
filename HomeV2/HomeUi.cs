using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System.Xml.Linq;

namespace HomeV2
{
    public class HomeUi : MonoBehaviour
    {
        public static GameObject homeUi;
        public static bool isShow = false;
        private Rect rect = new Rect(Screen.width - 800, 0, 500, 800);
        private Vector2 vector2 ;
        private static string name = "";
        void Update()
        {
            
        }

        void OnGUI()
        {
            if (isShow)
            {
                Color color = Color.white;
                rect = GUILayout.Window(1, rect, HomeWindow, "homelist");
            }
        }


        private void HomeWindow(int id)
        {

            GUILayout.BeginHorizontal();
           
            GUILayout.Label("输入家的名字:(可空)");
            name = GUILayout.TextField(name);

            if (GUILayout.Button("设置家"))
            {
                Home home = new Home();
                if (name=="")
                    home.Name = $"Home{HomeManager.Homes.Count}";
                else
                    home.Name = name;
                home.Position = HomeManager.player.GetPosition();
                name = "";
                HomeManager.AddHome(home);
            }
            GUILayout.EndHorizontal();

            vector2 = GUILayout.BeginScrollView(vector2);
            for (int i = 0; i < HomeManager.Homes.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(HomeManager.Homes[i].Name);
                GUILayout.Label("X:");
                GUILayout.Label(HomeManager.Homes[i].Position.x.ToString());
                GUILayout.Label("Y:");
                GUILayout.Label(HomeManager.Homes[i].Position.y.ToString());
                GUILayout.Label("Z:");
                GUILayout.Label(HomeManager.Homes[i].Position.z.ToString());
                if (GUILayout.Button("传送"))
                {
                    HomeManager.GoHome(HomeManager.Homes[i]);
                }
                if (GUILayout.Button("删除"))
                {
                    HomeManager.RemoveHome(HomeManager.Homes[i]);
                }

                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();
            if (GUILayout.Button("清空"))
            {
                
                HomeManager.SaveHome();
            }
        }
        void Start()
        {
            
        }
    }
}
