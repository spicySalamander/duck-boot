using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawn : MonoBehaviour
{
    // What's being spawned
    public GameObject spawnee;

    public DuckType[] type = new DuckType[3];
    private List<DuckType> typeTemp = new List<DuckType>(3);

    public int numOfTanks;
    public int numOfRanged;
    public int numOfMelee;

    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);

        for (int i = 0; i < type.Length; i++)
        {
            typeTemp.Add(type[i]);
        }
    }

    public int SubtractFromNumToSpawn(int num, DuckType unit)
    {
        num--;
        if (num <=0)
        {
            if (unit == type[0])
            {
                typeTemp.RemoveAt(0);
            }

            if (unit == type[1])
            {
                typeTemp.RemoveAt(1);
            }

            if (unit == type[2])
            {
                typeTemp.RemoveAt(2);
            }
        }

        if (typeTemp.Count == 0)
        {
            CancelInvoke("SpawnObject");
        }

        return num;
    }

    public void SpawnObject()
    {
        int temp = Random.Range(0, typeTemp.Count);
        spawnee.GetComponent<UnitController>().type = type[temp];
        Instantiate(spawnee, transform.position, Quaternion.identity);

        switch (temp)
        {
            case 0: //melee
                numOfMelee = SubtractFromNumToSpawn(numOfMelee, type[0]);
                break;
            case 1: //ranged
                numOfRanged = SubtractFromNumToSpawn(numOfRanged, type[1]);
                break;
            case 2: //tank
                numOfTanks = SubtractFromNumToSpawn(numOfTanks, type[2]);
                break;
            default:
                break;
        }

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
