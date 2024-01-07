using HarmonyLib;
using ModInfo;
using UnityEngine;

namespace HomeV2
{
    #region 打开/关闭地图
    [HarmonyPatch(typeof(XUiC_MapArea), "OnOpen")]
    public class Prefix_XUiC_MapArea_OnOpen
    {
        public static void Prefix()
        {
            HomeUi.isShow = true;
            Log.Out("打开地图");
        }
    }

    [HarmonyPatch(typeof(XUiC_MapArea), "OnClose")]
    public class Prefix_XUiC_MapArea_OnClose
    {
        public static void Prefix()
        {
            HomeUi.isShow = false;
            Log.Out("关闭地图");
        }
    }
    #endregion

    [HarmonyPatch(typeof(LocalPlayerUI), "DispatchNewPlayerForUI")]
    public class Prefix_LocalPlayerUI_DispatchNewPlayerForUI
    {
        public static void Prefix(EntityPlayerLocal _entityPlayer)
        {
            HomeManager.player = _entityPlayer;
        }
    }
    

    [HarmonyPatch(typeof(GameManager), "ChatMessageServer")]
    public class Prefix_GameManager_ChatMessageServer
    {
        public static bool Prefix(int _senderEntityId, string _msg)
        {
            if (_msg.StartsWith("/"))
            {
                EntityPlayerLocal entityPlayerLocal = GameManager.Instance.World.GetEntity(_senderEntityId) as EntityPlayerLocal;
                HomeManager.player = entityPlayerLocal;
                Vector3 position = entityPlayerLocal.GetPosition();
                string[] array = _msg.Replace("/", "").Split(' ');
                if (array.Length >= 2)
                {
                    Home home = new Home();
                    home.Name = array[1];
                    home.Position = position;
                    switch (array[0])
                    {
                        case "set":
                        case "sethome":
                            HomeManager.AddHome(home);
                            return false;
                        case "del":
                        case "delhome":
                            HomeManager.RemoveHome(home);
                            return false;
                        case "home":
                            HomeManager.backPos = position;
                            HomeManager.GoHome(home);
                            return false;
                        case "spawn":
                            break;
                        case "back":
                            HomeManager.backPos = position;
                            HomeManager.Back();
                            return false;
                        default:
                            return true;
                    }
                }
            }
            return false;
        }
    }

    // 记录死亡坐标
    [HarmonyPatch(typeof(EntityAlive), "OnEntityDeath")]
    public class Prefix_EntityAlive_OnEntityDeath
    {
       
        public static void Prefix(EntityAlive __instance)
        {
            if (__instance.EntityName == HomeManager.player.name)
            {
                EntityPlayerLocal entityPlayerLocal = GameManager.Instance.World.GetEntity(__instance.entityId) as EntityPlayerLocal;
                HomeManager.backPos = entityPlayerLocal.GetPosition();
            }
        }
    }
}
