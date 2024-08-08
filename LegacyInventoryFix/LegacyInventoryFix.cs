using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;
using System.Collections.Generic;
using System.Linq;


namespace LegacyInventoryFix
{
    public class LegacyInventoryFix : ResoniteMod
    {
        public override string Name => "LegacyInventoryFix";
        public override string Author => "kka429";
        public override string Version => "0.0.2";
        public override string Link => "https://github.com/rassi0429/LegacyInventoryFix";

        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("dev.kokoa.LegacyInventoryFix");
            harmony.PatchAll();
        }


        [HarmonyPatch(typeof(UserspaceRadiantDash), "ToggleLegacyInventory")]
        class LegacyInventoryFixPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);
                for(int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == System.Reflection.Emit.OpCodes.Ldc_R4 && (float)codes[i].operand == 700f
                         && codes[i + 1].opcode == System.Reflection.Emit.OpCodes.Ldc_R4 && (float)codes[i + 1].operand == 700f)
                    {
                        codes[i].operand = 1140f;
                        codes[i + 1].operand = 700f;
                        break;
                    }

                }
                return codes.AsEnumerable();
            }
        }


        [HarmonyPatch(typeof(BrowserDialog), "OnAttach")]
        class InventoryBrowserFixPatch
        {
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);
                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == System.Reflection.Emit.OpCodes.Ldc_R4 && (float)codes[i].operand == 1.2f)
                    {
                        codes[i].operand = 1.66667f;
                        break;
                    }
                }
                return codes.AsEnumerable();
            }
        }
    }
}
