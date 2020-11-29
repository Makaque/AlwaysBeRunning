using Harmony;

namespace Mktsk.AlwaysBeRunning
{

    public static class Constants
    {
        public const float FAST_SPEED = 12f;
        public const float SLOW_SPEED = 2f;
    }
    
    [HarmonyPatch(typeof(PlayerCharacter))]
    [HarmonyPatch("SetMaxSpeed")]
    public class PlayerSetSpeedPatch
    {
        static void Postfix(PlayerCharacter __instance, ref float ____curMaxSpeed, float speed)
        {
            __instance.runMaxSpeed = speed;
            ____curMaxSpeed = speed;
            __instance.initMaxSpeed = speed;
        }
    }
    
    [HarmonyPatch(typeof(PlayerCharacter))]
    [HarmonyPatch("Awake")]
    public class PlayerAwakePatch
    {
        static void Postfix(PlayerCharacter __instance)
        {
            __instance.SetMaxSpeed(Constants.FAST_SPEED);
        }
    }
    
    [HarmonyPatch(typeof(PlayerCharacter))]
    [HarmonyPatch("Update")]
    public class PlayerUpdatePatch
    {
        static void Postfix(PlayerCharacter __instance, PlayerActions ____playerActions)
        {
            if (____playerActions.Enabled)
            {
                if (__instance.curMaxSpeed != Constants.FAST_SPEED && !____playerActions.interact.IsPressed)
                {
                    __instance.SetMaxSpeed(Constants.FAST_SPEED);
                } else if (__instance.curMaxSpeed != Constants.SLOW_SPEED && ____playerActions.interact.IsPressed)
                {
                    __instance.SetMaxSpeed(Constants.SLOW_SPEED);
                }
            }
        }
    }
}