using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorPlayerController 
{
    public static class Params
    {
        public const string Running = "Running";
        public const string Attacking = "Attacking";
        public const string JumpStart = "JumpStart";
        public const string JumpProcess = "JumpProcess";
        public const string JumpEnd = "JumpEnd";
        public const string JumpEndSpeed = "JumpEndSpeed";
        public const string Dead = "Dead";
        public const string Win = "Win";
        public const string Lose = "Lose";
        public const string AttackingBoss = "AttackingBoss";
    }

    public static class States
    {
        public const string JumpStart = "JumpStart";
        public const string JumpEnd = "JumpEnd";
    }
}
