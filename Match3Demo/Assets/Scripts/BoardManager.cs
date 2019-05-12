using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Tile
{
    public GameObject tileObj;
    public string type;
    public Tile(GameObject obj, string t)
    {
        tileObj = obj;
        type = t;
    }

}


public class BoardManager : MonoBehaviour
{

    GameObject tile1 = null;
    GameObject tile2 = null;
    public Animator anim;
    public GameObject[] tile;
    public Transform startTran;
    public GameObject m_tileParent;
    static int rows = 6;
    static int cols = 6;
    Tile[,] tiles = new Tile[cols, rows];
    int xsize = 0;
    int ysize = 0;
    public Camera camera;
    public List<GameObject> tilebank = new List<GameObject>();
    int name = 0;
    void GenerateItem()
    {
        for (int r = 0; r < rows; r++)
        {
            xsize = 0;
            for (int c = 0; c < cols; c++)
            {
                // Vector3 tilepos = new Vector3(, r, 0);
                name++;
                int ranindx = Random.Range(0, tile.Length);
                Vector3 tilpos = new Vector3(c, r, 0);
                GameObject gameobj = (GameObject)Instantiate(tile[ranindx], tilpos, tile[ranindx].transform.rotation);


                // gameobj.transform.position = new Vector3(startTran.position.x + xsize, startTran.position.y+ysize,0);
                //gameobj.transform.SetParent(m_tileParent.transform, false);
                //gameobj.name = "g"+name;
                //gameobj.transform.position = new Vector3(startTran.transform.position.x+ xsize, startTran.transform.position.y + ysize, startTran.transform.position.z);
                tiles[c, r] = new Tile(gameobj, gameobj.name);
                tilebank.Add(gameobj);
                //xsize += 10;//53

            }
            // ysize += 10;//53
            //yield return null;

        }

    }
    bool renewboard = false;

    public GameObject[] m_tempTile;
    private void Start()
    {
        int numcopies = (rows * cols) / 3;
        for (int i = 0; i < numcopies; i++)
        {
            for (int j = 0; j < m_tempTile.Length; j++)//tile
            {
                if (m_tempTile[j] != null)
                {
                    GameObject o = (GameObject)Instantiate(m_tempTile[j], new Vector3(-10, 10, 0), m_tempTile[j].transform.rotation);
                    o.SetActive(false);
                    tilebank.Add(o);
                }
            }
        }


        GenerateItem();
    }



    void RenewBoard()
    {
        bool anymoved = false;
        for (int r = 1; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (r == rows - 1 && tiles[c, r] == null)
                {
                    Vector3 tilepos = new Vector3(c, r, 0);
                    //if (tiles[c, r - 1] != null)
                    {

                        for (int n = 0; n < tilebank.Count; n++)
                        {
                            int indx = Random.Range(0, tilebank.Count);
                            GameObject o = tilebank[indx];
                            if (!o.activeSelf)
                            {
                                // o.transform.SetParent(m_tileParent.transform, false);
                                 o.transform.position = new Vector3(tilepos.x, tilepos.y, tilepos.z);//need this line
                               
                                o.SetActive(true);
                                tiles[c, r] = new Tile(o, o.name);

                                GameObject par = Instantiate(tiles[c, r].tileObj.GetComponent<CheckCollision>().particles);
                                par.transform.position = new Vector3(tiles[c, r].tileObj.transform.position.x + 0.5f,
                                                                     tiles[c, r].tileObj.transform.position.y, tiles[c, r].tileObj.transform.position.z);

                                Destroy(par, 0.3f);

                                //       anim.SetBool("play",true);
                                anim.Rebind();
                                n = tilebank.Count + 1;
                            }
                        }

                    }
                }
                if (tiles[c, r] != null)
                {
                    if (tiles[c, r - 1] == null)
                    {

                        tiles[c, r - 1] = tiles[c, r];

                        //  tiles[c, r - 1].tileObj.transform.position = new Vector3(c, r - 1, 0);//nedd this line
                        tiles[c, r - 1].tileObj.transform.position = Vector3.Lerp(tiles[c, r - 1].tileObj.transform.position, new Vector3(c, r - 1, 0), 70 * Time.deltaTime); ;
                        renewboard = false;
                        GameObject par = Instantiate(tiles[c, r - 1].tileObj.GetComponent<CheckCollision>().particles);
                        par.transform.position =new Vector3( tiles[c, r - 1].tileObj.transform.position.x+0.5f,
                                                           tiles[c, r - 1].tileObj.transform.position.y,
                                                            tiles[c, r - 1].tileObj.transform.position.z);
                        Destroy(par, 0.3f);
                        anim.Rebind();
                        // anim.SetBool("play", true);
                        if (tiles[c, r - 1].tileObj.transform.position.y == r - 1)
                        {
                          //  anim.enabled = false;
                            tiles[c, r] = null;
                            anymoved = true;
                            renewboard = true;
                        }
                    }

                }
            }
        }
        if (anymoved)
        {
            RenewBoard();

          // Invoke("RenewBoard",0f);//0.5f
            //   anymoved = false;
        }
    }

    //public int counter = 1;
    void CheckGrid()
    {
        //columns
        int counter = 1;

        for (int r = 0; r < rows; r++)
        {
            counter = 1;
            for (int c = 1; c < cols; c++)
            {
                if (tiles[c, r] != null && tiles[c - 1, r] != null)
                {
                    if (tiles[c, r].type == tiles[c - 1, r].type)
                        counter++;
                    else
                        counter = 1;

                    if (counter == 3)
                    {

                        if (tiles[c, r] != null)
                            tiles[c, r].tileObj.SetActive(false);
                        if (tiles[c - 1, r] != null)
                            tiles[c - 1, r].tileObj.SetActive(false);
                        if (tiles[c - 2, r] != null)
                            tiles[c - 2, r].tileObj.SetActive(false);

                        tiles[c, r] = null;
                        tiles[c - 1, r] = null;
                        tiles[c - 2, r] = null;
                        renewboard = true;

                    }

                }
            }
        }
        // int counter1 = 1;
        for (int c = 0; c < cols; c++)
        {
            counter = 1;
            for (int r = 1; r < rows; r++)
            {
                if (tiles[c, r] != null && tiles[c, r - 1] != null)
                {
                    if (tiles[c, r].type == tiles[c, r - 1].type)
                        counter++;
                    else
                        counter = 1;

                    if (counter == 3)
                    {

                        if (tiles[c, r] != null)
                            tiles[c, r].tileObj.SetActive(false);
                        if (tiles[c, r - 1] != null)
                            tiles[c, r - 1].tileObj.SetActive(false);
                        if (tiles[c, r - 2] != null)
                            tiles[c, r - 2].tileObj.SetActive(false);

                        tiles[c, r] = null;
                        tiles[c, r - 1] = null;
                        tiles[c, r - 2] = null;
                        renewboard = true;

                    }

                }
            }
        }
        if (renewboard)
        {
            RenewBoard();
            renewboard = false;
        }


    }

    int pr = 1;

    public void StopAnnim()
    {
        anim.enabled = false;

    }

    private void Update()
    {


        CheckGrid();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, 1000);
            if (hit2D)
            {
                tile1 = hit2D.collider.gameObject;
                

                //print(tile1.name);
                print("ray");
            }

            //hit2D.transform.position = Input.mousePosition;

        }
        else
        if (Input.GetMouseButtonUp(0) && tile1)
        {

            Ray ray1 = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D1 = Physics2D.GetRayIntersection(ray1, 1000);
            if (hit2D1)
            {
                tile2 = hit2D1.collider.gameObject;
                print(tile2.name);
            }



            if (pr == 1)
            {
                m_tempTile[5] = Resources.Load<GameObject>("sp4");
                GameObject o = (GameObject)Instantiate(m_tempTile[5], new Vector3(-10, 10, 0), m_tempTile[5].transform.rotation);
                o.SetActive(false);
                tilebank.Add(o);
                pr = 0;
            }

            if (tile1 && tile2)
            {
                int hordist = (int)Mathf.Abs(tile1.transform.position.x - tile2.transform.position.x);
                int verdist = (int)Mathf.Abs(tile1.transform.position.y - tile2.transform.position.y);

                // print("hordst"+hordist+"verdts"+verdist);
                // if (hordist == 1 ^ verdist == 1)
                {
                    Tile temp = tiles[(int)tile1.transform.position.x, (int)tile1.transform.position.y];
                    tiles[(int)tile1.transform.position.x, (int)tile1.transform.position.y] =
                    tiles[(int)tile2.transform.position.x, (int)tile2.transform.position.y];

                    tiles[(int)tile2.transform.position.x, (int)tile2.transform.position.y] = temp;

                    //Tile temp = GetTile(tile1.name);
                    //Tile t1= GetTile(tile1.name);
                    //Tile t2 = GetTile(tile2.name);
                    //print(t2.type);
                    //SetTile(t1, t2);
                    //SetTile(t2, temp);

                    //Vector2 temp = get2dIndex(tile1.name);
                    //Vector2 t1 = get2dIndex(tile1.name);
                    //Vector2 t2 = get2dIndex(tile2.name);
                    //tiles[(int)t1.x, (int)t1.y] = tiles[(int)t2.x, (int)t2.y];
                    //tiles[(int)t2.x, (int)t2.y] = tiles[(int)temp.x, (int)temp.y];

                    Vector3 temppos = tile1.transform.position;
                    tile1.transform.position = tile2.transform.position;
                    tile2.transform.position = temppos;

                    tile1 = null;
                    tile2 = null;

                }
            }

        }


    }

    //void SetTile(Tile tile,Tile settile)
    //{
    //    int c = tiles.GetLength(0);
    //    int r = tiles.GetLength(1);
    //    for (int x = 0; x < r; x++)
    //    {
    //        for (int y = 0; y < c; y++)
    //        {
    //            if(tiles[x, y].type == tile.type)
    //            tiles[x, y] =settile;

    //        }
    //    }
    //}

    //Tile GetTile(string name)
    //{
    //    Tile tile=null;
    //    int c = tiles.GetLength(0);
    //    int r = tiles.GetLength(1);

    //    for (int x = 0; x < r; x++)
    //    {
    //        for (int  y = 0; y < c; y++)
    //        {
    //            if (name == tiles[x, y].type)
    //                tile= tiles[x, y];

    //        }
    //    }
    //    return tile;
    //}

    int t1indx1;
    int t1indx2;

    Vector3 getpos(int tempx, int tempy)
    {
        int c = tiles.GetLength(0);
        int r = tiles.GetLength(1);
        Vector3 vx = new Vector3();
        for (int x = 0; x < r; x++)
        {
            for (int y = 0; y < c; y++)
            {
                if (x == tempx && y == tempy)
                {
                    vx = tiles[x, y].tileObj.transform.position;
                }

            }
        }
        return vx;
    }


    Vector2 get2dIndex(string name)
    {

        int c = tiles.GetLength(0);
        int r = tiles.GetLength(1);
        Vector2 vx = new Vector2();
        for (int x = 0; x < r; x++)
        {
            for (int y = 0; y < c; y++)
            {
                if (name == tiles[x, y].type)
                {
                    vx.x = x;
                    vx.y = y;
                }

            }
        }
        return vx;
    }






}
