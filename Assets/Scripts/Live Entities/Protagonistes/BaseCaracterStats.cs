using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class BaseCaracterStats 
{
    public BaseCaracterStats()
    {
        EntityName = "";
        EntityDescription = "";
        Protagonist = false;
        Attack = 0;
        OiLife = 0;
        MaxOiLife = 0;
        BaseSpeed = 0;
        AttackCadence = 0;
        RedHearts = 0;
        BlueHearts = 0;
        YellowHearts = 0;
        OldTools = false;
    }

    //Entity Data
    public string EntityName { get; set; }

    public string EntityDescription { get; set; }

    public bool Protagonist { get; set; }

    //Entity propieties
    public int Attack { get; set; }

    public int OiLife { get; set; }

    public int MaxOiLife { get; set; }

    public float BaseSpeed { get; set; }

    public float AttackCadence { get; set; }

    //items quantity

    public int YellowHearts { get; set; }
    public int RedHearts { get; set; }
    public int BlueHearts { get; set; }
    public bool OldTools { get; set; }

    public virtual void UpdateStats(BaseCaracterStats statData, IEventAggregator messenger)
    {
        if (!Protagonist)
        {
            Debug.Log("someone tried to update the stats and it's not a protagonist");
            return;
        }
        Attack += statData.Attack;
        MaxOiLife += statData.MaxOiLife;
        OiLife += statData.OiLife;
        if (MaxOiLife < OiLife) OiLife = MaxOiLife;
        if (OiLife <= 0)
        {
            OiLife = 0;
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
