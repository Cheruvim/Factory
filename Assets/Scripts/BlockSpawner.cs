using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private bool _canCreat = true;
    public Transform SpawnPoint;
    public float TimeScun;
    public float Cooldown;
    public GameObject Block;
    // Update is called once per frame
    void Update()
    {
        if (Cooldown<=TimeScun)
        {
            Instantiate(Block, SpawnPoint.transform.position,transform.rotation);
            TimeScun = 0;
        }
        TimeScun = TimeScun + Time.deltaTime;
    }
}
