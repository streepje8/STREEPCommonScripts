using Openverse.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * LoadGameData
 * streep
 * 01/04/2022
 * 
 * Example Script for saving and loading
 */
public class LoadGameData : MonoBehaviour
{
    public void loadData()
    {
        if (Savedata.Instance.getBool("player.savedData")) {
            GameController.Instance.score = Savedata.Instance.getInt("player.score");
            GameController.Instance.playerstats.HP = Savedata.Instance.getFloat("player.health");
        }
    }

    public void saveData()
    {
        Savedata.Instance.save("player.score", GameController.Instance.score);
        Savedata.Instance.save("player.health", GameController.Instance.playerstats.HP);
        Savedata.Instance.save("player.savedData", true);
        Savedata.Instance.saveAll();
    }
}
