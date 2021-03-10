using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseSpawn : MonoBehaviour
{
    public GameObject star;
    public int countX = 20;
    public int countY = 20;
    public int countZ = 20;
    public float spacing = 10.0f;
    
    void Start()
    {
        Vector3 origin = new Vector3((-countX / 2) * spacing, (-countY / 2) * spacing, (-countZ / 2) * spacing);
        Vector3 spawn = new Vector3(-5, 0, 0);
        for (int x = 0; x < countX; ++x)
        {
            for (int y = 0; y < countX; ++y)
            {
                for (int z = 0; z < countX; ++z)
                {
                    spawn = origin + new Vector3(x * spacing, y * spacing, z * spacing);
                    GameObject clone = Instantiate(star, spawn, Quaternion.identity, gameObject.transform);
                    clone.SetActive(true);
                }
            }
        }
    }
}
