using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectB : MonoBehaviour
{
    public CubePlayer player;
    public void OnMouseDown()
    {
        player.Update1Point(gameObject);
        Destroy(gameObject);
    }
}
