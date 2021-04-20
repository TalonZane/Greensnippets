using UnityEngine;

namespace Mod
{
    public class Mod : MonoBehaviour
    {
        public static string ModTag = " [talonphobia]";

        public static void Main()
        {
            //Buff Human
            ModAPI.Register(
            new Modification()
            {
                OriginalItem = ModAPI.FindSpawnable("Human"),
                NameOverride = "Buff Human" + ModTag,
                DescriptionOverride = "This is a clone of a man named Beefcake Swellington. He is 32 years old, and runs a bakery and photography studio for a living. Drinks rarely. Hikes often. He is in an extremely robust shape health-wise. Married for 10 years and father of 6. He has donated his characteristics, mannerisms, and overall likeness for this mod.",
                CategoryOverride = ModAPI.FindCategory("Entities"),
                ThumbnailOverride = ModAPI.LoadSprite("sprites/buffThumb.png"),
                AfterSpawn = (Instance) =>
                {
                    Sprite[] skinTextures = new Sprite[] {
                        ModAPI.LoadSprite("sprites/skinhead.png"),

                        ModAPI.LoadSprite("sprites/skinupperbody.png"),
                        ModAPI.LoadSprite("sprites/skinmidbody.png"),
                        ModAPI.LoadSprite("sprites/skinlowerbody.png"),

                        ModAPI.LoadSprite("sprites/skinupperleg.png"),
                        ModAPI.LoadSprite("sprites/skinlowerleg.png"),
                        ModAPI.LoadSprite("sprites/skinfoot.png"),
                        ModAPI.LoadSprite("sprites/skinupperleg.png"),
                        ModAPI.LoadSprite("sprites/skinlowerleg.png"),
                        ModAPI.LoadSprite("sprites/skinfoot.png"),

                        ModAPI.LoadSprite("sprites/skinupperarm.png"),
                        ModAPI.LoadSprite("sprites/skinlowerarm.png"),
                        ModAPI.LoadSprite("sprites/skinupperarm.png"),
                        ModAPI.LoadSprite("sprites/skinlowerarm.png")

                    };

                    Texture2D[] meattextures = new Texture2D[] {
                        ModAPI.LoadTexture("sprites/meathead.png"),

                        ModAPI.LoadTexture("sprites/meatupperbody.png"),
                        ModAPI.LoadTexture("sprites/meatmidbody.png"),
                        ModAPI.LoadTexture("sprites/meatlowerbody.png"),

                        ModAPI.LoadTexture("sprites/meatupperleg.png"),
                        ModAPI.LoadTexture("sprites/meatlowerleg.png"),
                        ModAPI.LoadTexture("sprites/meatfoot.png"),
                        ModAPI.LoadTexture("sprites/meatupperleg.png"),
                        ModAPI.LoadTexture("sprites/meatlowerleg.png"),
                        ModAPI.LoadTexture("sprites/meatfoot.png"),

                        ModAPI.LoadTexture("sprites/meatupperarm.png"),
                        ModAPI.LoadTexture("sprites/meatlowerarm.png"),
                        ModAPI.LoadTexture("sprites/meatupperarm.png"),
                        ModAPI.LoadTexture("sprites/meatlowerarm.png")
                    };

                    Texture2D[] bonetextures = new Texture2D[] {
                        ModAPI.LoadTexture("sprites/bonehead.png"),

                        ModAPI.LoadTexture("sprites/boneupperbody.png"),
                        ModAPI.LoadTexture("sprites/bonemidbody.png"),
                        ModAPI.LoadTexture("sprites/bonelowerbody.png"),

                        ModAPI.LoadTexture("sprites/boneupperleg.png"),
                        ModAPI.LoadTexture("sprites/bonelowerleg.png"),
                        ModAPI.LoadTexture("sprites/bonefoot.png"),
                        ModAPI.LoadTexture("sprites/boneupperleg.png"),
                        ModAPI.LoadTexture("sprites/bonelowerleg.png"),
                        ModAPI.LoadTexture("sprites/bonefoot.png"),

                        ModAPI.LoadTexture("sprites/boneupperarm.png"),
                        ModAPI.LoadTexture("sprites/bonelowerarm.png"),
                        ModAPI.LoadTexture("sprites/boneupperarm.png"),
                        ModAPI.LoadTexture("sprites/bonelowerarm.png")
                    };


                    var persn = Instance.GetComponent<PersonBehaviour>();
                    var limb = persn.Limbs;
                    for (int i = 0; i < limb.Length; i++)
                    {
                        limb[i].GetComponent<SpriteRenderer>().sprite = skinTextures[i];
                        limb[i].GetComponent<SpriteRenderer>().material.SetTexture("_FleshTex", meattextures[i]);
                        limb[i].GetComponent<SpriteRenderer>().material.SetTexture("_BoneTex", bonetextures[i]);
                        foreach (var collider in limb[i].GetComponents<Collider2D>()) { Destroy(collider); }
                        var hitbox = limb[i].gameObject.AddComponent<BoxCollider2D>();
                    }


                    if (Instance.transform.Find("Head").GetComponent<LimbBehaviour>().BaseStrength == 6)
                    {
                        //parts
                        var armLeft = Instance.transform.Find("FrontArm");
                        var armRight = Instance.transform.Find("BackArm");
                        var legLeft = Instance.transform.Find("FrontLeg");
                        var legRight = Instance.transform.Find("BackLeg");
                        var head = Instance.transform.Find("Head");
                        var daBod = Instance.transform.Find("Body");

                        head.transform.localPosition = new Vector2(0f, 0.625f);

                        daBod.Find("UpperBody").transform.localPosition = new Vector2(0f, 0.2f);
                        daBod.Find("MiddleBody").transform.localPosition = new Vector2(-0.015f, -0.2f);
                        daBod.Find("LowerBody").transform.localPosition = new Vector2(0.015f, -0.6f);

                        legLeft.transform.localPosition = new Vector2(0f, -0.75f);
                        legLeft.Find("LowerLegFront").transform.localPosition = new Vector2(0.018f, -1.15f);
                        legLeft.Find("FootFront").transform.localPosition = new Vector2(0.11f, -1.5f);

                        legRight.transform.localPosition = new Vector2(0f, -0.75f);
                        legRight.Find("LowerLeg").transform.localPosition = new Vector2(0.018f, -1.15f);
                        legRight.Find("Foot").transform.localPosition = new Vector2(0.11f, -1.5f);

                        armLeft.transform.localPosition = new Vector2(-0.05f, 0.3f);
                        armLeft.Find("LowerArmFront").transform.localPosition = new Vector2(0.05f, -0.75f);
                        armRight.transform.localPosition = new Vector2(-0.05f, 0.3f);
                        armRight.Find("LowerArm").transform.localPosition = new Vector2(0.05f, -0.75f);

                        armLeft.Find("LowerArmFront").gameObject.GetComponent<GripBehaviour>().GripPosition = new Vector3(0f, -0.4f, 0.5f);
                        armRight.Find("LowerArm").gameObject.GetComponent<GripBehaviour>().GripPosition = new Vector3(0f, -0.4f, 0.5f);

                        armLeft.Find("LowerArmFront").gameObject.AddComponent<BeefyHitBehaviour>().mult = 2f;
                        armRight.Find("LowerArm").gameObject.AddComponent<BeefyHitBehaviour>(). mult = 2f;
                        legRight.Find("Foot").gameObject.AddComponent<BeefyHitBehaviour>().mult = 1.5f;
                        legLeft.Find("FootFront").gameObject.AddComponent<BeefyHitBehaviour>().mult = 1.5f;
                    }

                    if (Instance.transform.Find("Head").GetComponent<LimbBehaviour>().BaseStrength == 6)
                    {
                        var person = Instance.GetComponent<PersonBehaviour>();
                        var limbs = person.Limbs;
                        for (int i = 0; i < limbs.Length; i++)
                        {
                            limbs[i].BaseStrength *= 5f;
                            limbs[i].BreakingThreshold *= 2f;
                            limbs[i].InitialHealth *= 1.5f;
                            limbs[i].Health *= 1.5f;
                            limbs[i].Vitality *= 0.5f;
                            limbs[i].ImpactPainMultiplier *= 0.5f;
                            limbs[i].ShotDamageMultiplier *= 0.5f;
                            limbs[i].SkinMaterialHandler.intensityMultiplier *= 0.5f;
                        }
                    }
                    var allColliders = Instance.GetComponentsInChildren<Collider2D>(); //thanks mesterdueiez
                    foreach (var a in allColliders)
                        foreach (var b in allColliders)
                            Physics2D.IgnoreCollision(a, b);
                }
            });
        }
    }
}
