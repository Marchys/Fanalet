using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviourEx {

    private BaseCaracterStats _MaterialStats = new BaseCaracterStats();
    private BaseCaracterStats _BaseStats = new BaseCaracterStats();
    private BaseProtagonistStats ProtaStats;
	
    void Start()
    {
        _MaterialStats.MaxLife = 2000;
        _MaterialStats.Life = 2000;
        _MaterialStats.RedHearts = 99;
        _MaterialStats.BlueHearts = 99;
        _MaterialStats.YellowHearts = 99;

        _BaseStats.BaseSpeed = 5f;
        _BaseStats.Attack = 5;
    }
	

	void Update () {
        //dev cheats
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (ProtaStats == null)
            {
                ProtaStats = GameObject.FindWithTag("Prota").GetComponent<Protas>().Character;
            }
             ProtaStats.UpdateStats(_MaterialStats,Messenger);
            
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (ProtaStats == null)
            {
                ProtaStats = GameObject.FindWithTag("Prota").GetComponent<Protas>().Character;
            }
            ProtaStats.UpdateStats(_BaseStats, Messenger);
            
        }

	}
}
