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
    
    public class UnlockOnKill : MonoBehaviour //this is a component i would attach to things and define the key to unlock things, amazing
    {
        public bool IsBluntWeapon = true;
        public string UnlockableKey;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var limb = collision.gameObject.GetComponent<LimbBehaviour>();

            if (limb && !limb.IsConsideredAlive && IsBluntWeapon)
                UnlockManager.Unlock(UnlockableKey);
        }

        private void Use()
        {
            if (IsBluntWeapon) return;

            var firearm = GetComponent<FirearmBehaviour>();
            var projectileLauncher = GetComponent<ProjectileLauncherBehaviour>();
            var objectLayer = LayerMask.GetMask("Objects");

            if (firearm)
            {
                var hit = Physics2D.Raycast(firearm.BarrelPosition, firearm.BarrelDirection, Mathf.Infinity, objectLayer);
                var limb = hit.transform.GetComponent<LimbBehaviour>();

                if (limb && !limb.IsConsideredAlive)
                    UnlockManager.Unlock(UnlockableKey);
            }
            else if (projectileLauncher)
            {
                var hit = Physics2D.Raycast(projectileLauncher.GetBarrelPosition(), projectileLauncher.GetBarrelDirection(), Mathf.Infinity, objectLayer);
                var limb = hit.transform.GetComponent<LimbBehaviour>();

                if (limb && !limb.IsConsideredAlive)
                    UnlockManager.Unlock(UnlockableKey);
            }
            else
            {
                ModAPI.Notify("if you see this, it's because i didn't implement enough fail-safes, and never checked this.");
            }
        }
    }
}
