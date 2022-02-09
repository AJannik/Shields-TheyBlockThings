using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Shields_TheyBlockThings
{
    public class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            // Apply harmony patches
            new Harmony("com.shields_theyblockthings.patches").PatchAll();
        }
    }
}