using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 newObjPos = transform.position = new Vector3(0, Random.Range(10f, 20f), 0); ;
        GameObject newOBJ = Instantiate(player, newObjPos, Quaternion.identity);
    }

}
