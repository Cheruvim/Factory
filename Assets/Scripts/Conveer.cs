using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveer : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Grab")
        {

            Vector3 pos = collision.gameObject.GetComponent<Rigidbody>().position;
            
            collision.gameObject.GetComponent<Rigidbody>().velocity=transform.forward * speed;
            

        }


    }
}
