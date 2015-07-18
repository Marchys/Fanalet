using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviourEx {

    private BaseCaracterStats _MaterialStats = new BaseCaracterStats();
    private BaseCaracterStats _BaseStats = new BaseCaracterStats();
    private BaseProtagonistStats ProtaStats;
	
    void Start()
    {
        _MaterialStats.MaxLife = 100;
        _MaterialStats.Life = 100;
        _MaterialStats.RedHearts = 10;
        _MaterialStats.BlueHearts = 10;
        _MaterialStats.YellowHearts = 10;

        _BaseStats.BaseSpeed = 75f;
        _BaseStats.Attack = 1;
    }
	

	void Update () {
        //dev cheats
        //if (Input.GetKeyDown(KeyCode.Keypad7))
        //{
        //    if (ProtaStats == null)
        //    {
        //        ProtaStats = GameObject.FindWithTag("Prota").GetComponent<Protas>().Character;
        //    }
        //    ProtaStats.UpdateStats(_MaterialStats, Messenger);

        //}
        //if (Input.GetKeyDown(KeyCode.Keypad8))
        //{
        //    if (ProtaStats == null)
        //    {
        //        ProtaStats = GameObject.FindWithTag("Prota").GetComponent<Protas>().Character;
        //    }
        //    ProtaStats.UpdateStats(_BaseStats, Messenger);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad9))
        //{
        //    Messenger.Publish(new RevealMapMessage());
        //}


	}
}
