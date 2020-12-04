using System;

[Serializable]
public abstract class Patient
{
    protected readonly IVirus Virus;
    private readonly Action _infected;
    private readonly Action _recovered;
    private bool isInfected;
    private bool hasImmunity;

    protected Patient(IVirus virus, Action infected, Action recovered)
    {
        Virus = virus;
        _infected = infected;
        _recovered = recovered;
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

    public abstract void Infect();
    public abstract void Recovery();

    protected int InfectOffset;
    protected int RecoveryOffset;
}