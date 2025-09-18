using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ObjectCreateManager : MonoBehaviour
{
    /// <summary>
    /// this manager aims to create a manager for the stuff that player can pick up (health bottle, attack bottle etc)
    /// </summary>
    // Start is called before the first frame update
    public GameObject[] RandomObjects;

    public Transform Field;
    private Vector3 FieldColliderCenter_;
    private Vector3 FieldColliderSize_;
    
    void Start()
    {
        Field = gameObject.GetComponent<Transform>();
        FieldColliderCenter_ = Field.position;
        FieldColliderSize_ = new Vector3(Field.GetComponent<Transform>().localScale.x*10,0,Field.GetComponent<Transform>().localScale.z*10);
        Spawn(FieldColliderCenter_,FieldColliderSize_);
    }

    // Update is called once per frame
    void Spawn( Vector3 fieldColliderCenter, Vector3 fieldColliderSize )
    {
        for (int i = 0; i < RandomObjects.Length; i++)
        {
            var position = RandomPosition(fieldColliderCenter, fieldColliderSize);
            var hitColliders = Physics.OverlapSphere(position, 0.2f);
            if (CheckCollide(hitColliders))
            {
                position = RandomPosition(fieldColliderCenter, fieldColliderSize);
                hitColliders = Physics.OverlapSphere(position, 0.2f);
                Debug.Log("regenerate");
            }
            GameObject choosedObject = RandomObjects[Random.Range(0, RandomObjects.Length - 1)];
            GameObject spawnedObject = Instantiate(choosedObject, position, Quaternion.identity);
        }
    }

    bool CheckCollide(Collider[] collide)
    {
        int checker = 0;
        if (collide.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < collide.Length; i++)
        {
            if (collide[i].gameObject.layer == 11)
            {
                return false;
            }
            else if (collide[i].gameObject.layer == 6)
            {
                checker += 1;
            }
        }

        if (checker > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Vector3 RandomPosition(Vector3 Cen, Vector3 len)
    {
        int x = Random.Range((int) (Cen.x - len.x/2),
            (int) (Cen.x + len.x/2));
        int z = Random.Range((int) (Cen.z - len.z/2),
            (int) (Cen.z + len.z/2));
        Vector3 spawnPos = new Vector3(x, Cen.y, z);
        return spawnPos;
    }
}
