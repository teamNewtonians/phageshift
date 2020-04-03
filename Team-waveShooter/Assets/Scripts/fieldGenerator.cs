using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldGenerator : MonoBehaviour
{
    int rndWall = 0;  //To wall or not to wall.
    int wallCount = 0; //Walls in a row.
    List<int> wallList = new List<int>();
    public GameObject cellWallZ;
    public GameObject cellWallX;
    public GameObject wallClone;
    public GameObject exitdoor;
    Vector3 clonePos;
    public List<GameObject> walls = new List<GameObject>();
    public Animator doorAnim;
    public float counter = 10.0f;
    public bool doorOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        exitdoor = GameObject.Find("exitdoor");
        doorAnim = exitdoor.GetComponent<Animator>();
        doorAnim.SetBool("doorOpen", doorOpen);
        for (int x = -15; x <= 15; x += 5)
        {
            for (int z = -25; z <= 25; z += 5)
            {
                rndWall = Random.Range(0, 2);
                if(rndWall == 1)
                {
                    clonePos = new Vector3( x, 1, z);
                    walls.Add(Instantiate(cellWallZ, clonePos, cellWallZ.transform.rotation));
                    wallCount++;
                }
            }
            
        }
        for (int z = -15; z <= 15; z += 5)
        {
            for (int x = -25; x <= 25; x += 5)
            {
                rndWall = Random.Range(0, 2);
                if (rndWall == 1)
                {
                    clonePos = new Vector3(x, 1, z);
                    walls.Add(Instantiate(cellWallX, clonePos, cellWallX.transform.rotation));
                    wallCount++;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        counter -= Time.deltaTime;
        if((int)counter == 0 && !doorOpen)
        {
            doorOpen = true;
            doorAnim.SetBool("doorOpen", doorOpen);
            counter = 10.0f;
        }
        if ((int)counter == 0 && doorOpen)
        {
            doorOpen = false;
            doorAnim.SetBool("doorOpen", doorOpen);
            counter = 10.0f;
        }
    }
}
