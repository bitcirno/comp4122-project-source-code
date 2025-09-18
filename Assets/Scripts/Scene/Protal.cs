using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Protal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            Debug.Log("enter protal");
            bool allFinished = true;
            string saveDataJson = File.ReadAllText(SaveLoadManager.savePath());
            SaveData saveData = new SaveData();
            JsonUtility.FromJsonOverwrite(saveDataJson, saveData);
            UnitData[] unitDataSet = saveData.levelData.unitDataSet;

            for (int i = 0; i < unitDataSet.Length; i++)
            {
                if (unitDataSet[i].type != "Corridor" && unitDataSet[i].type != "Room0" && unitDataSet[i].finished == false)
                {
                    allFinished = false;
                    PlayerInfo.Instance.post("You Should Finished All Rooms At First");
                    break;
                }
            }

            if (allFinished)
            {
                Debug.Log(saveData.levelData.levelName);
                if (String.Equals(saveData.levelData.levelName, "Level3"))
                {
                    SaveLoadManager.Instance.clearSaveData();
                    GameRoot.Instance.SceneSystem.SetScene(new EndScene("Congratulations!"));
                }
                else if (String.Equals(saveData.levelData.levelName, "Level2"))
                {
                    SaveLoadManager.Instance.generateNextJson();
                    GameRoot.Instance.SceneSystem.SetScene(new LevelScene("Level3"));
                }
                else
                {
                    SaveLoadManager.Instance.generateNextJson();
                    GameRoot.Instance.SceneSystem.SetScene(new LevelScene("Level2"));
                }

            }
        }
    }
}
