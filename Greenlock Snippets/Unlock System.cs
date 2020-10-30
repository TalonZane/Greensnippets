using System.Collections.Generic;
using UnityEngine;

namespace Mod
{
    class UnlockManager : MonoBehaviour
    {
        public static List<Modification> Unlockables = new List<Modification>();
        public static void Unlock(string unlock)
        {

            for (int i = 0; i < Unlockables.Count; i++)
            {
                if (Unlockables[i].NameOverride == (unlock + Mod.ModTag)) //ModTag is obviously a static string defined in the mod's root file
                {
                    if (PlayerPrefs.GetInt(Unlockables[i].NameOverride, 0) == 1) return;

                    PlayerPrefs.SetInt(Unlockables[i].NameOverride, 1);
                    ModAPI.Register(Unlockables[i]);
                    ModAPI.Notify("<i>Unlocked: " + Unlockables[i].NameOverride.TrimEnd(Mod.ModTag.ToCharArray()) + " in " + Unlockables[i].CategoryOverride.name + "!</i>");
                    PlayerPrefs.SetInt("UnlockedItems", PlayerPrefs.GetInt("UnlockedItems", 0) + 1);
                    ModAPI.Notify("<i>" + PlayerPrefs.GetInt("UnlockedItems", 0) + "/" + Unlockables.Count + "</i>");

                }
            }

        }

        public static void InitialPopulate()
        {
            for (int i = 0; i < Unlockables.Count; i++)
            {
                if (PlayerPrefs.GetInt(Unlockables[i].NameOverride, 0) == 1)
                {
                    ModAPI.Register(Unlockables[i]);
                }
            }
        }
    }
}
