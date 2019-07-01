public class MatchException : BaseException
{
    public readonly Unit attacker;
    public readonly Unit defender;

    public MatchException(Unit attacker, Unit defender) : base(false)
    {
        this.attacker = attacker;
        this.defender = defender;
    }
}
