//Hey, code lurker! I'm the programmer behind this mod! Talon Zane!
//I'm leaving this message here to ask that you keep any secrets you find here to yourself.
//I love the idea of random events in video games, but they're usually kept to a minimum, since people spoil them too much.
//So, once again, PLEASE keep quiet about anything you find here!
//~TZ

//that was an old message, all hidden events in this mod have been removed from this snippet :D

//this mod rustled way too many jimmies when it launched, people wanted everything to be accessible from a menu
//that defeats the point of making "random events" though, doesnt it?
//when i find new clips of yume nikki or lsd dream simular of random events never before recorded years after those games have fell out of their prime, it's just cool
//the thought of being able to boot up a game and not fully knowing what's going to happen is beautiful, it gets people talking

//maybe some day i'll revisit the idea on my own time...

using System;
using System.Collections;
using UnityEngine;

namespace Mod
{
    class Mod : MonoBehaviour
    {
        public static void Main()
        {
            ModAPI.Register<RandomEvents>();
        }
    }

    class RandomEvents : MonoBehaviour
    {
        public void Awake()
        {
            //haha gottem
            CreateRandom("What's that?", 60, 2000, false, actions: () => //no one ever found this, but it'd spawn a large qr code that lead you to a rickroll
            {
                var haha = ModAPI.CreatePhysicalObject("???", ModAPI.LoadSprite("gottem.png"));
                haha.GetComponent<PhysicalBehaviour>().IsWeightless = true;
                haha.GetComponent<PhysicalBehaviour>().SpawnSpawnParticles = false;
            });


        public void CreateRandom(string title, float Wait, int Chance, bool repeat, Action actions)
        {
            StartCoroutine(EventWait(title, Wait, Chance, repeat, actions));
        }

        public IEnumerator EventWait(string title, float Wait, int Chance, bool repeat, Action actions)
        {
            System.Random num = new System.Random();
            var realNum = 0;
            while(realNum != Chance)
            {
                yield return new WaitForSeconds(Wait);
                if (realNum != Chance)
                {
                    realNum = num.Next(1, Chance + 1);
                }
            }

            actions();
            ModAPI.Notify(title);

            if (repeat == true)
            {
                StartCoroutine(EventWait(title, Wait, Chance, repeat, actions));
            }
            else
            {
                StopCoroutine(EventWait(title, Wait, Chance, repeat, actions));
            }

        }
    }
}
