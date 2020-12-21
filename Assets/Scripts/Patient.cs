using System;

[Serializable]
public abstract class Patient
{
    protected readonly IVirus Virus;
    private readonly Action _infected;
    private readonly Action _recovered;
    private readonly Action _dead;
    private bool isInfected;
    private bool hasImmunity;
    private bool isDead;

    protected Patient(IVirus virus, Action infected, Action recovered, Action dead)
    {
        Virus = virus;
        _infected = infected;
        _recovered = recovered;
        _dead = dead;
    }

    public bool IsInfected
    {
        get => isInfected;
        set
        {
            if (value) _infected?.Invoke();
            isInfected = value;
        }
    }

    public bool HasImmunity
    {
        get => hasImmunity;
        set
        {
            if (value) _recovered?.Invoke();
            hasImmunity = value;
        }
    }

    public bool HasDied
    {
        get => isDead;
        set
        {
            if (value) _dead?.Invoke();
            isDead = value;
        }
    }


    public abstract void Infect();
    public abstract void Recovery();
    public abstract void WillDie();

    protected int InfectOffset;
    protected int RecoveryOffset;
    protected int DeadOffset;
}