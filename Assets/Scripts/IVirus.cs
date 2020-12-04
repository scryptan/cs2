public interface IVirus
{
    int InfectChance { get; }
    int RecoveryChance { get; }
    int DeadChance { get; }
}