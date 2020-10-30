using UnityEngine;

/*
This is a component I slapped onto a physical Greenlock object

everything would appear in the context menus or ui, with some things I imagine people would like if they make a mod with unlocks in it
*/

namespace Mod
{
    public class GreenlockSettings : MonoBehaviour
    {
        public void Awake()
        {
            this.GetComponent<PhysicalBehaviour>().ContextMenuOptions.Buttons.Add(new ContextMenuButton("reset", "Reset Save", "Start over?", () =>
            {
                DialogBox dialog = new DialogBox();
                dialog = DialogBoxManager.TextEntry("Delete Save?", "Type \"YES\" to confirm.", new DialogButton("Delete my save!", true, ()=>
                {
                    if(dialog.EnteredText == "YES")
                    {
                        for (int i = 0; i < UnlockManager.Unlockables.Count; i++)
                        {
                            PlayerPrefs.DeleteKey(UnlockManager.Unlockables[i].NameOverride);
                        }
                        ModAPI.Notify(PlayerPrefs.GetInt("UnlockedItems") + " items deleted. Please restart your map.");
                        PlayerPrefs.SetInt("UnlockedItems", 0);
                    }
                }));
            }));

            this.GetComponent<PhysicalBehaviour>().ContextMenuOptions.Buttons.Add(new ContextMenuButton("count", "See Item Progress", "See how many items you've unlocked in total", () =>
            {
                string message;

                if (PlayerPrefs.GetInt("UnlockedItems", 0) == 1)
                {
                    message = ". Nice one, dumby.";
                } else if(PlayerPrefs.GetInt("UnlockedItems", 0) == (UnlockManager.Unlockables.Count))
                {
                    message = "! You've 100% the mod! Thank you for playing, and I hope you enjoyed what it had to offer.";
                } else if(PlayerPrefs.GetInt("UnlockedItems", 0) >= (UnlockManager.Unlockables.Count / 2))
                {
                    message = ". You're over half-way!";
                }
                else
                {
                    message = "";
                }

                ModAPI.Notify(PlayerPrefs.GetInt("UnlockedItems", 0) + "/" + UnlockManager.Unlockables.Count + message);
            }));

            this.GetComponent<PhysicalBehaviour>().ContextMenuOptions.Buttons.Add(new ContextMenuButton("unlock", "Unlock All Items", "Cheat?", () =>
            {
                DialogBox dialog = new DialogBox();
                dialog = DialogBoxManager.TextEntry("Unlock All Items", "Password", new DialogButton("Unlock", true, () =>
                {
                    ModAPI.Notify("Don't cheat :("); //lol
                }));
            }));

        }
    }
}
