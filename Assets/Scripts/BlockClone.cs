using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClone : MonoBehaviour
{
    private bool _canCreat = true;
    public GameObject Block;

    // Update is called once per frame
    void Update()
    {
        if (_canCreat && gameObject.GetComponent<BoxCollider>().isTrigger)
        {
            Instantiate(Block, gameObject.transform.position, transform.rotation);
            _canCreat = false;
        }
    }
}
