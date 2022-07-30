using System.Reflection;
using HarmonyLib;

namespace TNRD.Zeepkist.LevelEditorMovementSpeed.Patches
{
    [HarmonyPatch(typeof(LEV_MoveCamera), "TranslateCamera")]
    public class LEV_MoveCamera_TranslateCamera
    {
        private static FieldInfo useMoveSpeedField;

        private static void Prefix(LEV_MoveCamera __instance)
        {
            if (useMoveSpeedField == null)
            {
                useMoveSpeedField =
                    typeof(LEV_MoveCamera).GetField("useMoveSpeed", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            float useMoveSpeed = (float)useMoveSpeedField.GetValue(__instance);
            useMoveSpeedField.SetValue(__instance, useMoveSpeed * Plugin.Multiplier);
        }
    }
}
