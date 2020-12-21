using System;
using Random = UnityEngine.Random;

public class NormalPatient : Patient
{
    public NormalPatient(IVirus virus, Action infected, Action recovered, Action dead) : base(virus, infected, recovered, dead)
    {
        InfectOffset = 10;
        RecoveryOffset = 5;
        DeadOffset = 0;
    }

    public override void Infect()
    {
        if (!HasImmunity && !IsInfected && !HasDied)
        {
            var infectChance = Random.Range(0, 100);
            if (infectChance < Virus.InfectChance - InfectOffset)
            {
                IsInfected = true;
            }
        }
    }

    public override void Recovery()
    {
        if (!HasImmunity && IsInfected)
        {
            var recoveryChance = Random.Range(0, 100);
            if (recoveryChance < Virus.RecoveryChance - RecoveryOffset)
            {
                HasImmunity = true;
                IsInfected = false;
            }
        }
    }

    public override void WillDie()
    {
        if (!HasImmunity && IsInfected && !HasDied)
        {
            var deadChance = Random.Range(0, 100);
            if (deadChance < Virus.DeadChance - DeadOffset)
            {
                HasDied = true;
                IsInfected = false;
            }
        }
    }
}
