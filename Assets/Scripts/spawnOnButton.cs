using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnButton : MonoBehaviour
{
    public Transform cloneObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 10; i++)
                Instantiate(cloneObject, transform.position,   
            transform.rotation);
        }
    }
}
