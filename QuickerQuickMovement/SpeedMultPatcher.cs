using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QuickerQuickMovement
{

    [HarmonyPatch]
    public static class SpeedMultPatcher
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Kingmaker.UnitLogic.Commands.Base.UnitCommand), "Accelerate")]
        public static IEnumerable<CodeInstruction> TranspileAccelerate(IEnumerable<CodeInstruction> instructions) => ReplaceAccelerationConstant(instructions);

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Kingmaker.UnitLogic.Commands.Base.UnitCommand), "Deaccelerate")]
        public static IEnumerable<CodeInstruction> TranspileDeaccelerate(IEnumerable<CodeInstruction> instructions) => ReplaceAccelerationConstant(instructions);

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Kingmaker.EntitySystem.Entities.UnitEntityData), "CalculateCurrentSpeed")]
        public static IEnumerable<CodeInstruction> TranspileCalculateCurrentSpeed(IEnumerable<CodeInstruction> instructions) => ReplaceAccelerationConstant(instructions);

        private static IEnumerable<CodeInstruction> ReplaceAccelerationConstant(IEnumerable<CodeInstruction> instructions)
        {
            var code = new List<CodeInstruction>(instructions);

            for (int i = 0; i < code.Count; i++)
            {
                CodeInstruction instruction = code[i];
                if (instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 1.8f)
                {
                    code[i] = CodeInstruction.Call(typeof(SpeedMultPatcher), nameof(GetSpeedMult));
                    break;
                }
            }

            return code;
        }

        public static float GetSpeedMult()
        {
            return SettingsModMenu.QuickMoveSpeedMult;
        }
    }
}
