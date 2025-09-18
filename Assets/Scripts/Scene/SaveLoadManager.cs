using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine.SceneManagement;
using UnityEngine;
using Managers;
using UIFramework.Managers;

public class SaveLoadManager : MonoBehaviour
{
    public Transform levelTran;

    public Transform UITran;
    private GameObject curPlayer;
    public static string fileName = "Assets/Datas/save.json";
    private static String oriWeaponName="PrototypePistol";

    private const int MaxMapLength = 7;

    private static readonly string[] roomTypeSet = new string[3] {"Room1", "Room2", "Room3"};
    
    private static readonly String[] levelSet = new String[3] {"Level1","Level2", "Level3"};
    private static readonly int[] roomNumSet = new int[3] {6, 8, 10};
    
    public static SaveLoadManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public void save()
    {

        Transform roomsTran = levelTran.Find("Rooms");
        Transform corridorsTran = levelTran.Find("Corridors");
        int nUnit = roomsTran.childCount+corridorsTran.childCount;
        
        SaveData saveData = new SaveData();
        LevelData levelData = new LevelData();
        UnitData[] unitDataSet = new UnitData[nUnit];
        PlayerData playerData = new PlayerData();
        
        Scene curScene= SceneManager.GetActiveScene();

        levelData.levelName = curScene.name;

        int n = 0;
        UnitData unitData = new UnitData();
        
        for (int i = 0; i < roomsTran.childCount; i++)
        {
            
            unitData = recordRoom(roomsTran.GetChild(i));
            unitDataSet[n] = unitData;
            n++;
        }
        
        for (int i = 0; i < corridorsTran.childCount; i++)
        {
            unitData = recordCorridor(corridorsTran.GetChild(i));
            unitDataSet[n] = unitData;
            n++;
        }

        levelData.unitDataSet = unitDataSet;

        Player playerScript = curPlayer.GetComponent(typeof(Player)) as Player;
        playerData.hp = playerScript.GetHp();

        playerData.position = curPlayer.transform.position;
        
        WeaponHolder wphdrScript = playerScript.weaponHolder.GetComponent(typeof(WeaponHolder)) as WeaponHolder;
        playerData.weaponName = wphdrScript.getWeaponName();

        
        levelData.unitDataSet = unitDataSet;

        saveData.levelData = levelData;
        saveData.playerData = playerData;
        
        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText("Assets/Datas/save.json", saveDataJson);
        Debug.Log($"SaveDefult: {saveDataJson}");
    }

    private UnitData recordRoom(Transform roomTran)
    {
        UnitData unitData = new UnitData();

        RoomManager roomManager = roomTran.gameObject.GetComponent(typeof(RoomManager)) as RoomManager;
        if (roomManager!=null)
        {
            unitData.finished = roomManager.Finished;
            unitData.ID = roomManager.ID;
        }
        else
        {
            unitData.finished = false;
            unitData.ID = 0;
        }

        int x = (int) roomTran.position.x/60;
        int y = (int) roomTran.position.z/60;
        unitData.position = new int[2] {x, y};

        unitData.type = roomTran.name.Split('(')[0];

        

        return unitData;
    }

    UnitData recordCorridor(Transform roomTran)
    {
        UnitData unitData = new UnitData();

        unitData.finished = false;
        int x = (int) roomTran.position.x/60;
        int y = (int) roomTran.position.z/60;
        unitData.position = new int[2] {x, y};

        unitData.type = "Corridor";

        unitData.ID = -1;
        
        return unitData;
    }

    public void clearSaveData()
    {
        File.Delete(fileName);
    }

    public static bool haveSaveData()
    {
        return File.Exists(fileName);
    }

    public static String savePath()
    {
        return fileName;
    }

    private void load()
    {

        string saveDataJson = File.ReadAllText(fileName);
        SaveData saveData = new SaveData();
        JsonUtility.FromJsonOverwrite(saveDataJson, saveData);

        LevelData levelData = saveData.levelData;
        UnitData[] unitDataSet = levelData.unitDataSet;
        int n = unitDataSet.Length;

        GameObject roomPrefab;
        GameObject room;

        Vector3 position;
        string type;
        Quaternion rotation;
        bool finished;
        Transform parent;

        for (int i = 0; i < n; i++)
        {

            position = new Vector3(unitDataSet[i].position[0] * 60, 0, unitDataSet[i].position[1] * 60);
            type = unitDataSet[i].type;
            rotation = Quaternion.identity;
            parent = levelTran.Find("Rooms");
            finished = unitDataSet[i].finished;

            if (String.Equals(type, "Corridor"))
            {
                parent = levelTran.Find("Corridors");
            }
                
            if (String.Equals(type,"Corridor")&&unitDataSet[i].position[0]%2==1)
            {
                rotation=Quaternion.Euler(0,90,0);
            }
            


            roomPrefab = Resources.Load($"Prefabs/Map/{type}") as GameObject;
            room = Instantiate(roomPrefab, position, rotation, parent);


            RoomManager roomManager=room.GetComponent(typeof(RoomManager)) as RoomManager;

            if(roomManager!=null){
                  roomManager.ID=unitDataSet[i].ID;
                  roomManager.SaveLoadManager = this;
                  if (finished)
                  {
                      roomManager.clearEnemy();
                  }
            }

        }
        
         curPlayer = Instantiate(Resources.Load("Prefabs/Characters/Hero1"), saveData.playerData.position, Quaternion.identity) as GameObject;
         Player playerScript = curPlayer.GetComponent(typeof(Player)) as Player;

         if(saveData.playerData.hp>0){
             PlayerInfo.Instance.init(saveData.playerData.hp,100);
             playerScript.SetCurrentHp(saveData.playerData.hp);
         }
         
         GlobalManager globalManager = gameObject.GetComponent(typeof(GlobalManager)) as GlobalManager;
         globalManager.mainPlayer = curPlayer.GetComponent<Player>();
         


         WeaponHolder wphdrScript = playerScript.weaponHolder.GetComponent(typeof(WeaponHolder)) as WeaponHolder;
         GameObject weapon=Resources.Load($"Prefabs/Weapons/Guns/{saveData.playerData.weaponName}") as GameObject;
         Debug.Log(weapon.name);
         wphdrScript.oriWeapon = weapon;

         AimCrossHairManager ACHMScript = UITran.Find("Canvas").Find("aim").GetComponent(typeof(AimCrossHairManager)) as AimCrossHairManager;
         ACHMScript.virtualCamera = curPlayer.transform.Find("CM");
         
         GlobalManager.Instance.ScanSceneMap();

    }


    private void Start()
    {
        load();
        
    }

    public static void generateDefultJson()
    {

        SaveData saveData = new SaveData();
        LevelData levelData = new LevelData();
        PlayerData playerData = new PlayerData();

        levelData.levelName = levelSet[0];

        UnitData[] unitDataSet=generateMap(roomNumSet[0]);
        levelData.unitDataSet = unitDataSet;

        playerData.hp = -1;
        playerData.position = new Vector3(unitDataSet[0].position[0]*60, 1, unitDataSet[0].position[1]*60);
        playerData.weaponName = oriWeaponName;

        saveData.levelData = levelData;
        saveData.playerData = playerData;

        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText("Assets/Datas/save.json", saveDataJson);
        Debug.Log($"SaveDefult: {saveDataJson}");
    }

    public void generateNextJson()
    {
        
        SaveData saveData = new SaveData();
        LevelData levelData = new LevelData();
        PlayerData playerData = new PlayerData();
        
        Scene curScene= SceneManager.GetActiveScene();
        string levelName = curScene.name;
        int levelIndex = levelName.Split('l')[1][0]-48;
        levelIndex++;
        levelData.levelName = "Level"+levelIndex.ToString();

        UnitData[] unitDataSet=generateMap(roomNumSet[levelIndex-1]);
        

        Player playerScript = curPlayer.GetComponent(typeof(Player)) as Player;
        playerData.hp = playerScript.GetHp();

        playerData.position = new Vector3(unitDataSet[0].position[0]*60, 1, unitDataSet[0].position[1]*60);
        
        WeaponHolder wphdrScript = playerScript.weaponHolder.GetComponent(typeof(WeaponHolder)) as WeaponHolder;
        playerData.weaponName = wphdrScript.getWeaponName();
        

        levelData.unitDataSet = unitDataSet;
        saveData.levelData = levelData;
        saveData.playerData = playerData;

        saveData.levelData = levelData;
        saveData.playerData = playerData;

        string saveDataJson = JsonUtility.ToJson(saveData);
        File.WriteAllText("Assets/Datas/save.json", saveDataJson);
        Debug.Log($"SaveDefult: {saveDataJson}");
    }
    
    
    

    public static string getLevelName()
    {
        string saveDataJson = File.ReadAllText(fileName);
        SaveData saveData = new SaveData();
        JsonUtility.FromJsonOverwrite(saveDataJson, saveData);
        return saveData.levelData.levelName;
    }

    
    

    public static bool[] canGetNeighbor(bool[,] map, int[] position)
    {
        int x = position[0];
        int y = position[1];
        bool bottom=false, top=false, left=false, right=false;
        if (y < MaxMapLength - 2)
        {
            if (map[x, y + 2] == false)
            {
                bottom = true;
            }
        }

        if (y > 1)
        {
            if (map[x, y - 2]==false)
            {
                top = true;
            }
        }

        if (x>1)
        {
            if (map[x - 2, y]==false)
            {
                left = true;
            }
        }
        if (x < MaxMapLength - 2)
        {
            if (map[x + 2, y] == false)
            {
                right = true;
            }
        }

        return new bool[] { top, bottom, left, right };
    }

    public static string getRandomRoom()
    {
        return roomTypeSet[UnityEngine.Random.Range(0, roomTypeSet.Length)];
    }


    public static UnitData[] generateMap(int num)
    {
        UnitData[] unitDataSet = new UnitData[num * 2 - 1];
        bool[,] map = new bool[MaxMapLength , MaxMapLength ];

        int x = UnityEngine.Random.Range(0, 4);
        int y = UnityEngine.Random.Range(0, 4);

        map[x*2 , y*2] = true;

        UnitData unitData = new UnitData();
        unitData.position = new int[2] { x*2 , y*2  };
        unitData.ID = 0;
        unitData.type = "Room0";

        unitDataSet[0] = unitData;


        bool[] allOccupied = new bool[4] { false, false, false, false };
        string corridor = "Corridor";
        int iRoomGetNewNeighbor;
        int[] pRoomGetNewNeighbor;
        bool[] possibleNeighbor;
        string type;
        bool cannotAdd;

        int i;



        for (i = 1; i < num; i++){

            iRoomGetNewNeighbor = UnityEngine.Random.Range(0, i);
            pRoomGetNewNeighbor = unitDataSet[iRoomGetNewNeighbor].position;
            possibleNeighbor = canGetNeighbor(map, pRoomGetNewNeighbor);
            type = unitDataSet[iRoomGetNewNeighbor].type;
            cannotAdd = (!possibleNeighbor[0]&&!possibleNeighbor[1]&&!possibleNeighbor[2]&&!possibleNeighbor[3]) || String.Equals(type, corridor);



            while (cannotAdd){
            iRoomGetNewNeighbor = UnityEngine.Random.Range(0, i)*2;
            pRoomGetNewNeighbor = unitDataSet[iRoomGetNewNeighbor].position;
            possibleNeighbor = canGetNeighbor(map, pRoomGetNewNeighbor);
            type = unitDataSet[iRoomGetNewNeighbor].type;
            cannotAdd = (!possibleNeighbor[0]&&!possibleNeighbor[1]&&!possibleNeighbor[2]&&!possibleNeighbor[3])|| String.Equals(type, corridor);
            }

            int directionToAdd = UnityEngine.Random.Range(0, 4);
            while (possibleNeighbor[directionToAdd] == false)
            {
                directionToAdd = UnityEngine.Random.Range(0, 4);
            }

            switch (directionToAdd)
            {
                case 0:
                    Debug.Log("top");

                    map[pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] - 2] = true;

                    unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] - 2 };
                    unitData.ID = i;
                    unitData.type = getRandomRoom();

                    unitDataSet[i * 2] = unitData;

                    map[pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] - 1] = true;

                    unitData = unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] - 1 };
                    unitData.ID = -1;
                    unitData.type = "Corridor";

                    unitDataSet[i * 2 - 1] = unitData;

                    break;

                case 1:
                    Debug.Log("bottom");

                    map[pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] + 2] = true;

                    unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] + 2 };
                    unitData.ID = i;
                    unitData.type = getRandomRoom();

                    unitDataSet[i * 2] = unitData;

                    map[pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] + 1] = true;

                    unitData = unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0], pRoomGetNewNeighbor[1] + 1 };
                    unitData.ID = -1;
                    unitData.type = "Corridor";

                    unitDataSet[i * 2 - 1] = unitData;

                    break;

                case 2:
                    Debug.Log("left");

                    map[pRoomGetNewNeighbor[0] - 2, pRoomGetNewNeighbor[1]] = true;

                    unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0] - 2, pRoomGetNewNeighbor[1] };
                    unitData.ID = i;
                    unitData.type = getRandomRoom();

                    unitDataSet[i * 2] = unitData;

                    map[pRoomGetNewNeighbor[0] - 1, pRoomGetNewNeighbor[1]] = true;

                    unitData = unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0] - 1, pRoomGetNewNeighbor[1] };
                    unitData.ID = -1;
                    unitData.type = "Corridor";

                    unitDataSet[i * 2 - 1] = unitData;

                    break;

                case 3:
                    Debug.Log("right");

                    map[pRoomGetNewNeighbor[0] + 2, pRoomGetNewNeighbor[1]] = true;

                    unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0] + 2, pRoomGetNewNeighbor[1] };
                    unitData.ID = i;
                    unitData.type = getRandomRoom();

                    unitDataSet[i * 2] = unitData;

                    map[pRoomGetNewNeighbor[0] + 1, pRoomGetNewNeighbor[1]] = true;

                    unitData = unitData = new UnitData();
                    unitData.position = new int[2] { pRoomGetNewNeighbor[0] + 1, pRoomGetNewNeighbor[1] };
                    unitData.ID = -1;
                    unitData.type = "Corridor";

                    unitDataSet[i * 2 - 1] = unitData;

                    break;
            }
            Debug.Log(iRoomGetNewNeighbor);
        }

        return unitDataSet;
    }


    
}

[Serializable]
public class UnitData
{
    public string type;
    public int ID;
    public int[] position;
    public bool finished;
}

[Serializable]
public class SaveData
{
    public LevelData levelData;
    public PlayerData playerData;
}

[Serializable]
public class LevelData
{
    public string levelName;

    public UnitData[] unitDataSet;
}

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public int hp;
    public string weaponName;

}

