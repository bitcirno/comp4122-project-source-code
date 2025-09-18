using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour
{
    public int ID;
    public bool Finished;
    public SaveLoadManager SaveLoadManager;

    private Transform doorsTran;

    private readonly string[] weaponSet = new string[7]
    {
        "CaterpillarRifle", "FireBallPistol", "FireNetShotgun", "LaserRifle", "PrototypePistol", "PrototypeRifle",
        "PrototypeShotgun"
    };

    // Start is called before the first frame update
    void Start()
    {
        doorsTran=transform.Find("Collisions").Find("Doors");
        creatRandomWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Finished)
        {
            Finished=checkFinished();
            if (Finished)
            {
                openDoors();
                SaveLoadManager.save();
            }
        }
        
    }

    private bool checkFinished(){
        return transform.Find("Enemies").childCount==0;
    }

    public void clearEnemy(){

        Transform enemiesTransform=this.gameObject.transform.Find("Enemies");

        for(int i=0; i<enemiesTransform.gameObject.transform.childCount; i++){
            GameObject enemy=enemiesTransform.GetChild(i).gameObject;
            Destroy(enemy);
        }
    }

    public void openDoors(){
        
        for(int i=0; i<doorsTran.childCount; i++){
            Transform doorTran = doorsTran.GetChild(i);
            doorTran.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            doorTran.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("RoomManager: open door");
        }
    }

    public void closeDoors(){
         for(int i=0; i<doorsTran.childCount; i++)
         {
             Transform doorTran = doorsTran.GetChild(i);
             doorTran.gameObject.GetComponent<BoxCollider>().isTrigger=false;
             doorTran.gameObject.GetComponent<MeshRenderer>().enabled = true;
             Debug.Log("RoomManager: close door");
         }
    }
    
    private void creatRandomWeapon()
    {
        Vector3 position = transform.position + new Vector3(Random.Range(-25, 25), 0.5f, Random.Range(-25, 25));
        String weaponName = weaponSet[Random.Range(0, weaponSet.Length)];
        GameObject weapon =
            Resources.Load($"Prefabs/Weapons/GunsToCreate/{weaponName}") as GameObject;
        Instantiate(weapon, position, Quaternion.identity);
    }
}
