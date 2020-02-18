using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelFollow : MonoBehaviour
{
    public Transform wooperBody;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(wooperBody.position.x, wooperBody.position.y + 0.170f, wooperBody.position.z);
        transform.rotation = wooperBody.rotation;
    }
}
