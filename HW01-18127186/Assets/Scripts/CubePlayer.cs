using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MSSV: 18127186
// Tran Dinh Phuoc
public class CubePlayer : MonoBehaviour
{
    float speedMoveM = 8f;
    public float speed = 1.5f;
    public GameObject cap;
    private static List<GameObject> objClone = new List<GameObject>();
    private bool rotate = false;
    private bool rotateB = false;
    private bool moveA = false, moveLeft = false;
    Color defaultColor;
    MeshRenderer render;
    public static int point = 0;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        defaultColor = render.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, v, 0);
        if (move != Vector3.zero)
        {
            transform.position += move * speed;
        }
        if (transform.position.x > 20) transform.position = new Vector3 (20f, transform.position.y, transform.position.z);
        if (transform.position.x < -20) transform.position = new Vector3(-20f, transform.position.y, transform.position.z);
        if (transform.position.y > 25) transform.position = new Vector3(transform.position.x, 25f, transform.position.z);
        if (transform.position.y < 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 newObjPos = new Vector3(Random.Range(1, 5), Random.Range(10, 16), Random.Range(1, 5));
            GameObject newOBJ = Instantiate(cap, newObjPos, Quaternion.identity);
            objClone.Add(newOBJ);
            newOBJ.GetComponent<Rigidbody>().AddForce(newObjPos * 15f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (objClone.Count > 0)
            {
                int length = objClone.Count;
                for(int i=0; i < length; i++)
                {
                    int random = Random.RandomRange(0, objClone.Count);
                    Destroy(objClone[random], 2f);
                    objClone.Remove(objClone[random]);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotate = !rotate;
        }
        if (rotate)
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            rotateB = !rotateB;
            if (objClone.Count > 0 && rotateB)
            {
                int length = objClone.Count;
                for (int i = 0; i < length; i++)
                {
                    Vector3 newPosForB = new Vector3(objClone[i].transform.position.x, 10f, objClone[i].transform.position.z);
                    objClone[i].transform.position = newPosForB;
                    objClone[i].GetComponent<Rigidbody>().useGravity = false;
                    objClone[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
            }
            else if (objClone.Count > 0 && !rotateB)
            {
                int length = objClone.Count;
                for (int i = 0; i < length; i++)
                {
                    objClone[i].GetComponent<Rigidbody>().useGravity = true;
                    objClone[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
        }
        if (rotateB)
        {
            if (objClone.Count > 0)
            {
                Vector3 posRotate = new Vector3(1, 1, 1);
                int length = objClone.Count;
                for (int i = 0; i < length; i++)
                {
                    objClone[i].transform.Rotate(posRotate * 100f * Time.deltaTime);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            moveA = !moveA;
        }
        if (moveA)
        {
            if (!moveLeft)
            {
                transform.Translate(Vector3.right * speedMoveM * Time.deltaTime, Space.World);
            } else
            {
                transform.Translate(Vector3.right * -speedMoveM * Time.deltaTime, Space.World);
            }
            if (transform.position.x > 20 || transform.position.x < -20) moveLeft = !moveLeft;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (objClone.Count > 0)
            {
                int length = objClone.Count;
                for (int i = 0; i < length; i++)
                {
                    objClone[i].GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                }
            }
        }
        
    }
    void OnMouseOver()
    {
        render.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        render.material.color = defaultColor;
    }
    public void Update1Point (GameObject other)
    {
        point += 1;
        objClone.Remove(other);
    }
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;
        style.normal.textColor = Color.black;
        GUI.TextField(new Rect(10, 10, 100, 20), "Points: " + point, style);
        
    }
}
