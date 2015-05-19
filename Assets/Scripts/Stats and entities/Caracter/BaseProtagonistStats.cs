public class BaseProtagonistStats : BaseCaracterStats {
    public virtual void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
        Attack += statData.Attack;
        MaxLife += statData.MaxLife;
        Life += statData.Life;
        if (MaxLife < Life) Life = MaxLife;
        if (Life <= 0)
        {
            Life = 0;
            messenger.Publish(new StopMessage());
            messenger.Publish(new PlayerDeathMessage());
        }
        BaseSpeed += statData.BaseSpeed;
        AttackCadence += statData.AttackCadence;
        RedHearts += statData.RedHearts;
        BlueHearts += statData.BlueHearts;
        YellowHearts += statData.YellowHearts;
        if (statData.OldTools)
        {
            OldTools = statData.OldTools;
        }
        messenger.Publish(new UpdateGuiMessage(this));
    }

}
