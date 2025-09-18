using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private RoomManager roomManager;
    private 
    // Start is called before the first frame update
    void Start()
    {
        roomManager=transform.parent.parent.parent.gameObject.GetComponent(typeof(RoomManager)) as RoomManager;
    }
    
    private void OnTriggerEnter(Collider other) {
        GameObject otherObj=other.gameObject;
        if(roomManager.Finished==false && otherObj.layer==LayerMask.NameToLayer("Player")){
            Invoke("closeDoors", 0.5f);
        }
    }

    private void closeDoors(){
        roomManager.closeDoors();
        return;
    }
    

}
