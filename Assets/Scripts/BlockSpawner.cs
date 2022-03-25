using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private bool _canCreat = true;
    public Transform SpawnPoint;
    public GameObject Block;
    // Update is called once per frame
    void Update()
    {
        if (_canCreat)
        {
            Instantiate(Block, SpawnPoint.transform.position,transform.rotation);

        }
    }
}
