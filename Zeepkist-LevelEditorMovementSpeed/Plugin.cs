using System;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace TNRD.Zeepkist.LevelEditorMovementSpeed
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static int Multiplier { get; private set; } = 1;

        private ConfigEntry<KeyCode> plusKey;
        private ConfigEntry<KeyCode> minusKey;

        private Harmony harmony;

        private void Awake()
        {
            harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            plusKey = Config.Bind(new ConfigDefinition("General", "plusKey"),
                KeyCode.KeypadPlus,
                ConfigDescription.Empty);
            minusKey = Config.Bind(new ConfigDefinition("General", "minusKey"),
                KeyCode.KeypadMinus,
                ConfigDescription.Empty);

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
            harmony = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(plusKey.Value))
            {
                Multiplier++;
            }

            if (Input.GetKeyDown(minusKey.Value))
            {
                Multiplier--;

                if (Multiplier < 1)
                    Multiplier = 1;
            }
        }
    }
}
