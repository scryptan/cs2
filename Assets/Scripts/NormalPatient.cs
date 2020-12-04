using System;
using Random = UnityEngine.Random;

public class NormalPatient : Patient
{
    public NormalPatient(IVirus virus, Action infected, Action recovered) : base(virus, infected, recovered)
    {
        InfectOffset = 10;
        RecoveryOffset = 5;
    }

    public override void Infect()
    {
        if (!HasImmunity && !IsInfected)
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
}
