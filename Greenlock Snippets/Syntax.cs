using UnityEngine;

namespace Mod
{
    public class Mod : MonoBehaviour
    {
        public static string ModTag = " -GL";

        public static void Main()
        {
            UnlockManager.InitialPopulate();

            ModAPI.OnDeath += (sender, victim) => //exampleeee
            {
                if (victim.name.ToLower() == "human")
                {
                    UnlockManager.Unlock("Greenlock");
                }
            };

        }

        public static void OnLoad() //notice that it registers modifications here, since it's not actually just adding things to the menu
        {
            GameObject manager = new GameObject("Manager");
            manager.AddComponent<UnlockManager>();

            UnlockManager.Unlockables.Add(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Rod"),
                    NameOverride = "Greenlock" + ModTag,
                    DescriptionOverride = "A strange, green lock. Check context menu for settings. \n \n<b>Unlocked by:</b> killing your first person, monster.",
                    CategoryOverride = ModAPI.FindCategory("Misc."),
                    ThumbnailOverride = ModAPI.LoadSprite("GreenlockView.png"),
                    AfterSpawn = (Instance) =>
                    {
                        Instance.GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("Greenlock.png");
                        Instance.AddComponent<GreenlockSettings>();
                        foreach (var hitbox in Instance.GetComponents<Collider2D>()) { Destroy(hitbox); }
                        var lockbody = Instance.AddComponent<BoxCollider2D>();
                        var lockthing = Instance.AddComponent<CapsuleCollider2D>();

                        lockbody.offset = new Vector2(0f, -0.08493f);
                        lockbody.size = new Vector2(0.3141123f, 0.25986f);

                        lockthing.size = new Vector2(0.1744949f, 0.4049919f);
                    }
                }
            );
        }
    }
}
