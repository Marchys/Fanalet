public class BaseProtagonistStats : BaseCaracterStats {
    public virtual void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
        MaxLife += statData.MaxLife;
        Life += statData.Life;
        if (MaxLife < Life) Life = MaxLife;
        if (Life <= 0)
        {
            Life = 0;
            messenger.Publish(new StopMessage());
            messenger.Publish(new PlayerDeathMessage());
        }
        RedHearts += statData.RedHearts;
        BlueHearts += statData.BlueHearts;
        YellowHearts += statData.YellowHearts;

        Attack += statData.Attack;
        BaseSpeed += statData.BaseSpeed;
        CurrentSpeed = BaseSpeed;
        AttackCadence += statData.AttackCadence;
        if (statData.OldTools)
        {
            OldTools = statData.OldTools;
        }
        messenger.Publish(new UpdateGuiMessage(this));
    }

}
