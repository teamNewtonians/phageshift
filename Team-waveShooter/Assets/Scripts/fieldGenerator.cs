using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldGenerator : MonoBehaviour
{
    int rndWall = 0;  //To wall or not to wall.
    int rndTree = 0;
    int rndYofs = 0;
    List<int> wallList = new List<int>();
    public GameObject cellWallZ;
    public GameObject cellWallX;
    public GameObject optTree;
    public GameObject wallClone;
    public GameObject exitdoor;
    public ParticleSystem bloodstream;
    Vector3 clonePos;
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> trees = new List<GameObject>();
    public Animator doorAnim;
    public float counter = 10.0f;
    public bool doorOpen = false;
    private int grid = 10;
    private int size = 100;

    // Start is called before the first frame update
    void Start()
    {
        exitdoor = GameObject.Find("exitdoor");
        doorAnim = exitdoor.GetComponent<Animator>();
        bloodstream = GameObject.Find("bloodstream").GetComponent<ParticleSystem>();
        bloodstream.Stop();
        doorAnim.SetBool("doorOpen", doorOpen);
        fieldMaker();
    }

    // Update is called once per frame
    void Update()
    {

        counter -= Time.deltaTime;
        if ((int)counter == 0 && !doorOpen)
        {
            doorOpen = true;
            doorAnim.SetBool("doorOpen", doorOpen);
            bloodstream.Play();
            counter = 10.0f;
        }
        if ((int)counter == 0 && doorOpen)
        {
            fieldMaker();
            doorOpen = false;
            doorAnim.SetBool("doorOpen", doorOpen);
            bloodstream.Stop();
            counter = 10.0f;
        }
    }
    void fieldMaker()
    {   
        for(int i = 0; i< walls.Count; i++)
        {
            Destroy(walls[i]);
        }
        for (int i = 0; i < trees.Count; i++)
        {
            Destroy(trees[i]);
        }
        walls = new List<GameObject>(0);
        trees = new List<GameObject>(0);
        for (int x = -size / 2; x <= size / 2; x += grid)
        {
            for (int z = -size / 4; z <= size / 4; z += grid)
            {
                rndWall = Random.Range(0, 2);
                rndTree = Random.Range(0, 3);
                rndYofs = Random.Range(0, 3);
                if (rndWall == 1)
                {
                    clonePos = new Vector3(x, rndYofs, z);
                    walls.Add(Instantiate(cellWallZ, clonePos, cellWallZ.transform.rotation));
                }
                if (rndTree == 1)
                {
                    clonePos = new Vector3(x, rndYofs - 1, z + 4);
                    trees.Add(Instantiate(optTree, clonePos, optTree.transform.rotation));
                }
            }

        }
        for (int z = -size / 2; z <= size / 2; z += grid)
        {
            for (int x = -size / 4; x <= size / 4; x += grid)
            {
                rndWall = Random.Range(0, 2);
                rndYofs = Random.Range(0, 3);
                if (rndWall == 1)
                {
                    clonePos = new Vector3(x, rndYofs, z);
                    walls.Add(Instantiate(cellWallX, clonePos, cellWallX.transform.rotation));
                }
            }

        }
    }
}
