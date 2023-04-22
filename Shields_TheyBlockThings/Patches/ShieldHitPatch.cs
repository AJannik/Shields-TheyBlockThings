using HarmonyLib;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace Shields_TheyBlockThings.Patches
{
    [HarmonyPatch]
    public class ShieldHitPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Mission), "RegisterBlow")]
        private static bool RegisterBlowPatch(Mission __instance, Agent attacker, Agent victim,
            GameEntity realHitEntity, Blow b,
            ref AttackCollisionData collisionData, in MissionWeapon attackerWeapon, ref CombatLogData combatLogData)
        {
            b.VictimBodyPart = collisionData.VictimHitBodyPart;
            if (collisionData.AttackBlockedWithShield || collisionData.CollidedWithShieldOnBack)
            {
                foreach (MissionBehavior missionBehavior in __instance.MissionBehaviors)
                    missionBehavior.OnRegisterBlow(attacker, victim, realHitEntity, b, ref collisionData,
                        in attackerWeapon);

                return false;
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Mission), "DecideAgentHitParticles")]
        private static bool DecideAgentHitParticlesPatch(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, ref HitParticleResultData hprd)
        {
            if (collisionData.CollidedWithShieldOnBack)
            {
                MissionGameModels.Current.DamageParticleModel.GetMeleeAttackSweatParticles(attacker, victim, in blow, in collisionData, out hprd);
                return false;
            }

            return true;
        }
    }
}