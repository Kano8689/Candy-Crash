using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int rows, cols;
    public GameObject[] allBeanPrefab;
    public GameObject[] Teddy;
    public GameObject[,] allCandies;
    public GameObject crntMoveCandy;
    public Sprite[] Bomb;
    public Sprite Blue, Green, Orange, Pink, Purple, Red, White, Yellow;
    Candy candy;
    Find_Match find_match;
    //bool isValid = false;
    // Start is called before the first frame update
    void Start()
    {
        candy = FindObjectOfType<Candy>();
        find_match = FindObjectOfType<Find_Match>();
        //findMatch = GetComponent<Find_Match>();
        allCandies = new GameObject[cols, rows];
        boardGenerator();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    void boardGenerator()
    {
        for(int i=0; i<cols; i++)
        {
            for(int j=0; j<rows; j++)
            {
                int rndmNum = Random.Range(0, allBeanPrefab.Length);

                while (checkMatchCandie(i, j, allBeanPrefab[rndmNum]))
                {
                    rndmNum = Random.Range(0, allBeanPrefab.Length);
                }

                Vector2 pos = new Vector2(i, j);
                GameObject g = Instantiate(allBeanPrefab[rndmNum], pos, Quaternion.identity);
                g.GetComponent<Candy>().rows = j;
                g.GetComponent<Candy>().cols = i;
                g.transform.SetParent(transform);
                g.name = "("+i+","+j+")";
                allCandies[i, j] = g;
                
            }
        }
        generateTeddy();
    }

    public void generateTeddy()
    {
        int rndmCol= Random.Range(0, cols);
        Destroy(allCandies[rndmCol,rows-1]);
        Vector2 pos = new Vector2(rndmCol, rows-1);
        GameObject teddy = Instantiate(Teddy[0],pos,Quaternion.identity);
        teddy.GetComponent<Candy>().cols = rndmCol;
        teddy.GetComponent<Candy>().rows = rows-1;
        teddy.transform.SetParent(transform);
        teddy.name="("+rndmCol+","+(rows-1)+")";
        allCandies[rndmCol, (rows-1)] = teddy;
    }

    bool checkMatchCandie(int col, int row, GameObject g)
    {
        if(col>=2 && row>=2)
        {
            if (allCandies[col - 1, row].tag == g.tag && allCandies[col - 2, row].tag == g.tag)
            {
                return true;
            }

            if (allCandies[col, row - 1].tag == g.tag && allCandies[col, row - 2].tag == g.tag)
            {
                return true;
            }
        }
        else if(col>=2)
        {
            if (allCandies[col - 1, row].tag == g.tag && allCandies[col - 2, row].tag == g.tag)
            {
                return true;
            }
        }
        else if(row>=2)
        {
            if (allCandies[col, row - 1].tag == g.tag && allCandies[col, row - 2].tag == g.tag)
            {
                return true;
            }
        }

        return false;
    }
    public void destroyMatchCandy()
    {
        if (find_match.matchCandies.Count > 3)
        {
            Debug.Log("Hello....");
            find_match.checkForBomb();
        }

        for (int i=0;i<cols;i++)
        {
            for(int j=0;j<rows;j++)
            {
                if (allCandies[i,j]!=null)
                {
                    if (allCandies[i,j].GetComponent<Candy>().isMatched)
                    {
                        Destroy(allCandies[i, j]);
                        allCandies[i, j] = null;
                    }
                }
            }
        }
        find_match.matchCandies.Clear();
        StartCoroutine(descreaseRowNum());
    }

    IEnumerator descreaseRowNum()
    {
        yield return new WaitForSeconds(0.5f);
        int nullCnt = 0;
        for(int i=0;i<cols;i++)
        {
            for(int j=0;j<rows;j++)
            {
                if (allCandies[i,j] == null)
                {
                    nullCnt++;
                }
                else if(nullCnt>0)
                {
                    allCandies[i, j].GetComponent<Candy>().rows -= nullCnt;
                    allCandies[i, j] = null;
                }
            }
            nullCnt = 0;
        }
        StartCoroutine(generateNewCandy());
    }
    
    void refilBoard()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (allCandies[i, j] == null)
                {
                    int rndmNum = Random.Range(0, allBeanPrefab.Length);
                    Vector2 pos = new Vector2(i, j+2);
                    GameObject g = Instantiate(allBeanPrefab[rndmNum], pos, Quaternion.identity);
                    g.GetComponent<Candy>().rows = j;
                    g.GetComponent<Candy>().cols = i;
                    g.transform.SetParent(transform);
                    g.name = "(" + i + "," + j + ")";
                    allCandies[i, j] = g;
                }
            }
        }
    }
    IEnumerator generateNewCandy()
    {
        yield return new WaitForSeconds(0.7f);
        refilBoard();
        yield return new WaitForSeconds(0.4f);

        for(int i=0;i< cols;i++)
        {
            for(int j=0;j< rows;j++)
            {
                if (allCandies[i, j] != null)
                {
                    if (allCandies[i,j].GetComponent<Candy>().isMatched)
                    {
                        destroyMatchCandy();
                    }
                }
            }
        }
    }
}
