using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//This code is pretty gross now that I'm looking at it with more knowledge, but here is the entire upgrade system!

namespace Greenbrick
{
    class GlowingBrickBehaviour : MonoBehaviour
    {
        bool Glowing = false;

        bool Used = false;

        GameObject FusionObject;

        Dictionary<string, RecipeStruct> Recipe =
            new Dictionary<string, RecipeStruct>();

        string[] Ingredients = {
            "Mjolnir -GB",
            "Greenbrick Blade -GB",
            "The Peashooter -GB",
            "Green Brick Gorgon Skull -GB",
            "Green Brick Launcher -GB"
        };

        public void Awake()
        {
            Recipe.Add("Mjolnir -GB", new RecipeStruct
            { 
                NewName = "True Mjolnir -GB",
                NewSprite = ModAPI.LoadSprite("TrueMjolnir.png", 2f),
                NewScript = typeof(TrueMjolnirBehaviour)
            });            
            
            Recipe.Add("Greenbrick Blade -GB", new RecipeStruct
            { 
                NewName = "Greenbrick Greatsword -GB",
                NewSprite = ModAPI.LoadSprite("GreenbrickGreatsword.png", 1.5f),
                NewScript = typeof(GreenbrickGreatswordBehaviour)
            });

            Recipe.Add("The Peashooter -GB", new RecipeStruct
            {
                NewName = "Photosynthesis -GB",
                NewSprite = ModAPI.LoadSprite("photosynthesis.png", 1.5f),
                NewScript = typeof(PhotosynthesisBehaviour)
            });

            Recipe.Add("Green Brick Gorgon Skull -GB", new RecipeStruct
            {
                NewName = "???",
                NewSprite = ModAPI.LoadSprite("Gorgon.png"),
                NewScript = typeof(GorgonBehaviour)
            });

            Recipe.Add("Green Brick Launcher -GB", new RecipeStruct
            {
                NewName = "Green Brick Shard Launcher",
                NewSprite = ModAPI.LoadSprite("greenbrickshardlauncher.png", 2f),
                NewScript = typeof(ChargedGreenBrickLauncherBehaviour)
            });
        }

        public void FixedUpdate()
        {
            if(GetComponent<PhysicalBehaviour>().charge != 0f && !Glowing)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            GetComponent<SpriteRenderer>().sprite = ModAPI.LoadSprite("glowbrick.png", 2f);
            GetComponent<SpriteRenderer>().material = ModAPI.FindMaterial("VeryBright");
            Glowing = true;
            ModAPI.CreateParticleEffect("IonExplosion", transform.position);
        }

        public struct RecipeStruct
        {
            public string NewName;
            public Sprite NewSprite;
            public Type NewScript;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Objects") && (Array.IndexOf(Ingredients, collision.gameObject.transform.root.name) >= 0) && Used == false && Glowing == true)
            {
                Used = true;

                FusionObject = collision.gameObject;

                StartCoroutine(Fuse());
                FusionObject.GetComponent<SpriteRenderer>().material = ModAPI.FindMaterial("VeryBright");
                this.GetComponent<SpriteRenderer>().enabled = false;
                foreach (var hitbox in this.GetComponents<Collider2D>())
                {
                    hitbox.enabled = false;
                }
            }
        }

        IEnumerator Fuse()
        {
            yield return new WaitForSeconds(0.5f);
            FusionObject.GetComponent<SpriteRenderer>().enabled = false;
            foreach(var hitbox in FusionObject.GetComponents<Collider2D>())
            {
                hitbox.enabled = false;
            }
            var NewObject = ModAPI.CreatePhysicalObject(Recipe[FusionObject.name].NewName, Recipe[FusionObject.name].NewSprite);
            NewObject.AddComponent(Recipe[FusionObject.name].NewScript);
            NewObject.GetComponent<PhysicalBehaviour>().SpawnSpawnParticles = false;
            NewObject.transform.position = FusionObject.transform.position;
            NewObject.transform.localRotation = FusionObject.transform.localRotation;
            NewObject.transform.localScale = new Vector3(FusionObject.transform.localScale.x, NewObject.transform.localScale.y);
            NewObject.GetComponent<SpriteRenderer>().material = ModAPI.FindMaterial("VeryBright");
            yield return new WaitForSeconds(0.5f);
            NewObject.GetComponent<SpriteRenderer>().material = ModAPI.FindMaterial("Sprites-Default");
            StopCoroutine(Fuse());
        }
    }
}
