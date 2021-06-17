using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Constants
    {
        public static int LAYER_MASK = LayerMask.GetMask("Ground", "Jumpable");
        public static string TAG_JUMPABLE = "Jumpable";
        public static string TAG_GHOST = "Enemy";
        public static string TAG_PLAYER = "Player";

        public static float SPAWNING_PERIOD = 3f;
        public static float SPAWN_COUNT = 999999;
    }
}
