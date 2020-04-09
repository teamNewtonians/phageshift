using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldGenerator : MonoBehaviour
{
    int rndWall = 0;  //To wall or not to wall.
    int rndTree = 0;
    int rndYofs = 0;

    public GameObject cellWallZ;
    public GameObject cellWallX;
    public GameObject optTree;
    public GameObject exitdoor;
    public GameObject startSpawn;
    public GameObject organFloor;
    public GameObject phage;
    public GameObject playerPhage;
    public bool start;

    public ParticleSystem bloodstream;
    Vector3 clonePos;
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> trees = new List<GameObject>();
    public List<GameObject> viruses = new List<GameObject>();
    public Animator doorAnim;

    public int score;
    public int totScore;
    public int vCount;
    public int level;
    public bool doorOpen = false;
    private int grid = 10;
    public int size = 100;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        vCount = level*10;

        exitdoor = GameObject.Find("exitdoor");
        startSpawn = GameObject.Find("startSpawn");
        organFloor = GameObject.Find("organFloor");
        
        doorAnim = exitdoor.GetComponent<Animator>();
        bloodstream = GameObject.Find("bloodstream").GetComponent<ParticleSystem>();
        bloodstream.Stop();
        doorAnim.SetBool("doorOpen", doorOpen);

        //phage = GameObject.Find("Assets/derek/Phage/phageNew");
        playerPhage = Instantiate(phage, startSpawn.transform.position, phage.transform.rotation);

        fieldMaker();
    }

    // Update is called once per frame
    void Update()
    {
        start = GameObject.Find("Menus").GetComponent<menuMaker>().start;
        
        if (score == vCount && !doorOpen)
        {
            doorOpen = true;
            doorAnim.SetBool("doorOpen", doorOpen);
            bloodstream.Play();
        }
        if (score == 0 && doorOpen)
        {
            fieldMaker();
            doorOpen = false;
            doorAnim.SetBool("doorOpen", doorOpen);
            bloodstream.Stop();
        }
    }
    void fieldMaker()
    {
        playerPhage.transform.position = startSpawn.transform.position;

        exitdoor.transform.position = new Vector3((-size / 2) + grid, 1, (-size / 2) + grid);
        startSpawn.transform.position = new Vector3((size / 2) - grid, 2, (size / 2) - grid);
        organFloor.transform.localScale = new Vector3(size, 20, size);

        for (int i = 0; i< walls.Count; i++)
        {
            Destroy(walls[i]);
        }
        for (int i = 0; i < trees.Count; i++)
        {
            Destroy(trees[i]);
        }
        walls = new List<GameObject>(0);
        trees = new List<GameObject>(0);
        for (int x = (-size / 2); x <= (size / 2); x += grid)
        {
            for (int z = (-size / 2) + (grid / 2); z <= (size / 2)- (grid / 2); z += grid)
            {
                rndWall = Random.Range(0, 2);
                rndTree = Random.Range(0, 3);
                rndYofs = Random.Range(0, 3);
                if ((rndWall == 1 || x == -size / 2 || x == size / 2) && !(x == (-size / 2) + grid || x == (size / 2) - grid))
                {
                    clonePos = new Vector3(x, rndYofs, z);
                    walls.Add(Instantiate(cellWallZ, clonePos, cellWallZ.transform.rotation));
                }
                if ((rndTree == 1) && !(x == (-size / 2) + grid || x == (size / 2) - grid))
                {
                    clonePos = new Vector3(x, rndYofs - 1, z + 4);
                    optTree.transform.Rotate((float)0, (float)rndYofs * 45, (float)0, Space.Self);
                    trees.Add(Instantiate(optTree, clonePos, optTree.transform.rotation));
                }
            }

        }
        for (int z = (-size / 2); z <= (size / 2); z += grid)
        {
            for (int x = (-size / 2)+(grid/2); x <= (size / 2)- (grid / 2); x += grid)
            {
                rndWall = Random.Range(0, 2);
                rndYofs = Random.Range(0, 3);
                if ((rndWall == 1 || z == -size / 2 || z == size / 2) && !(z == (-size / 2) + grid || z == (size / 2) - grid))
                {
                    clonePos = new Vector3(x, rndYofs, z);
                    walls.Add(Instantiate(cellWallX, clonePos, cellWallX.transform.rotation));
                }
            }

        }
    }
}
