using System.Collections.Generic;
using UnityEngine;

namespace PC1
{
    public enum BehaviourState { IDLE, CODING, BEINGANNOYING }
    public abstract class Human
    {
        public BehaviourState behaviourState;

        public Vector3 position;

        public Human() { HumanManager.AddHuman(this); }

        internal abstract void UpdateBehaviour();
        public void SetState(BehaviourState state) { }
        public void SayLine(string line) { }
        public void MakeNoise(string noiseID) { }

        public void Hear(string line) { }

        public void CleanUp() { HumanManager.RemoveHuman(this); }
    }

    public static class HumanManager
    {
        public static List<Human> humans = new List<Human>();

       /* public void TriggerHear(string sound, float range)
        {
            foreach(Human human in humans)
            {
                if()
            }
        }*/

        public static void AddHuman(Human human) { humans.Add(human); }
        public static void RemoveHuman(Human human) { humans.Remove(human); }
    }

    internal class Ivar : Human
    {
        internal override void UpdateBehaviour()
        {
            behaviourState = BehaviourState.CODING;
        }

    }

    internal class Sam : Human
    {
        override internal void UpdateBehaviour()
        {
            if (behaviourState == BehaviourState.IDLE)
                MakeNoise("🍅");
        }
    }

    internal class Alex : Human
    {
        public string[] NPCLines = new string[] { "Skibidi", "NI-", "actually.", "for real" };
        override internal void UpdateBehaviour()
        {
            if (behaviourState == BehaviourState.IDLE)
                SetState(BehaviourState.BEINGANNOYING);
            if (behaviourState == BehaviourState.BEINGANNOYING)
                SayRandomNPCLine();
        }

        void SayRandomNPCLine()
        {
            string line = NPCLines[Random.Range(0, NPCLines.Length)];
            SayLine(line);
        }
    } 
}