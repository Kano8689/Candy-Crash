using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Find_Match : MonoBehaviour
{
    public static Find_Match inst;
    Board board;
    public List<GameObject> matchCandies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}


    //public void findMatchCandy()
    //{
    //    StartCoroutine(findMatchesCandies());
    //}

    public void findMatchesCandies(int col, int row)
    //IEnumerator findMatchesCandies(int col, int row)
    {
        //yield return new WaitForSeconds(0.2f);

        //for(int i=0;i<board.cols;i++)
        //{
        //    for (int j = 0; j < board.rows; j++)
        //    {
                if (col > 0 && col < board.cols - 1)
                {
                    GameObject curntCandy = board.allCandies[col, row];
                    if (curntCandy != null)
                    {
                        GameObject leftCandy = board.allCandies[col - 1, row];
                        GameObject rightCandy = board.allCandies[col + 1, row];

                        if (leftCandy != null && rightCandy != null)
                        {
                            if (leftCandy.tag == curntCandy.tag && rightCandy.tag == curntCandy.tag)
                            {
                                curntCandy.GetComponent<Candy>().isMatched = true;
                                leftCandy.GetComponent<Candy>().isMatched = true;
                                rightCandy.GetComponent<Candy>().isMatched = true;
                                if (!matchCandies.Contains(curntCandy))
                                {
                                    matchCandies.Add(curntCandy);
                                }
                                if (!matchCandies.Contains(leftCandy))
                                {
                                    matchCandies.Add(leftCandy);
                                }
                                if (!matchCandies.Contains(rightCandy))
                                {
                                    matchCandies.Add(rightCandy);
                                }

                                if (curntCandy.GetComponent<Candy>().isColBomb)
                                {
                                    collectCollumCandies(curntCandy.GetComponent<Candy>().cols);
                                }
                                if (leftCandy.GetComponent<Candy>().isColBomb)
                                {
                                    collectCollumCandies(leftCandy.GetComponent<Candy>().cols);
                                }
                                if (rightCandy.GetComponent<Candy>().isColBomb)
                                {
                                    collectCollumCandies(rightCandy.GetComponent<Candy>().cols);
                                }

                                if (curntCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    collectRowCandies(curntCandy.GetComponent<Candy>().rows);
                                }
                                if (leftCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    collectRowCandies(leftCandy.GetComponent<Candy>().rows);
                                }
                                if (rightCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    collectRowCandies(rightCandy.GetComponent<Candy>().rows);
                                }
                            }
                        }
                    }
                }

                if (row > 0 && row < board.rows - 1)
                {
                    GameObject curntCandy = board.allCandies[col, row];
                    if (curntCandy != null)
                    {
                        GameObject upCandy = board.allCandies[col, row+1];
                        GameObject downCandy = board.allCandies[col, row-1];

                        if (upCandy != null && downCandy != null)
                        {
                            if (upCandy.tag == curntCandy.tag && downCandy.tag == curntCandy.tag)
                            {
                                curntCandy.GetComponent<Candy>().isMatched = true;
                                upCandy.GetComponent<Candy>().isMatched = true;
                                downCandy.GetComponent<Candy>().isMatched = true;
                                if (!matchCandies.Contains(curntCandy))
                                {
                                    matchCandies.Add(curntCandy);
                                }
                                if (!matchCandies.Contains(upCandy))
                                {
                                    matchCandies.Add(upCandy);
                                }
                                if (!matchCandies.Contains(downCandy))
                                {
                                    matchCandies.Add(downCandy);
                                }

                                if (curntCandy.GetComponent<Candy>().isColBomb)
                                {
                                    collectCollumCandies(curntCandy.GetComponent<Candy>().cols);
                                }
                                if (upCandy.GetComponent<Candy>().isColBomb)
                                {
                                    collectCollumCandies(upCandy.GetComponent<Candy>().cols);
                                }
                                if (downCandy.GetComponent<Candy>().isColBomb)
                                {
                                    collectCollumCandies(downCandy.GetComponent<Candy>().cols);
                                }

                                if (curntCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    collectRowCandies(curntCandy.GetComponent<Candy>().rows);
                                }
                                if (upCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    collectRowCandies(upCandy.GetComponent<Candy>().rows);
                                }
                                if (downCandy.GetComponent<Candy>().isRowBomb)
                                {
                                    collectRowCandies(downCandy.GetComponent<Candy>().rows);
                                }
                            }
                        }
                    }
                }
        //    }
        //}
    }

    void collectCollumCandies(int col)
    {
        for (int i = 0; i < board.rows; i++)
        {
            if (!matchCandies.Contains(board.allCandies[col, i]))
            {
                board.allCandies[col, i].GetComponent<Candy>().isMatched = true;
                matchCandies.Add(board.allCandies[col, i]);
            }
        }
        if(board.crntMoveCandy!=null)
        {
            board.crntMoveCandy.GetComponent<Candy>().isMatched = true;
        }
        else if(board.crntMoveCandy.GetComponent<Candy>().oppCandy != null)
        {
            Candy oppCandyScript = board.crntMoveCandy.GetComponent<Candy>().oppCandy.GetComponent<Candy>();
            oppCandyScript.isMatched = true;
        }
    }
    void collectRowCandies(int row)
    {
        for (int i = 0; i < board.cols; i++)
        {
            if (!matchCandies.Contains(board.allCandies[i, row]))
            {
                board.allCandies[i, row].GetComponent<Candy>().isMatched = true;
                matchCandies.Add(board.allCandies[i, row]);
            }
        }
        if (board.crntMoveCandy != null)
        {
            board.crntMoveCandy.GetComponent<Candy>().isMatched = true;
        }
        else if (board.crntMoveCandy.GetComponent<Candy>().oppCandy != null)
        {
            Candy oppCandyScript = board.crntMoveCandy.GetComponent<Candy>().oppCandy.GetComponent<Candy>();
            oppCandyScript.isMatched = true;
        }
    }
    public void checkForBomb()
    {
        Debug.Log("Hellooo");
        if (board.crntMoveCandy != null)
        {
            Debug.Log("Hii");
            if (board.crntMoveCandy.GetComponent<Candy>().isMatched)
            {
                Debug.Log("Hiiiiii");

                board.crntMoveCandy.GetComponent<Candy>().isMatched = false;
                if (board.crntMoveCandy.tag == "blueBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Blue;
                if (board.crntMoveCandy.tag == "greenBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Green;
                if (board.crntMoveCandy.tag == "orangeBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Orange;
                if (board.crntMoveCandy.tag == "pinkBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Pink;
                if (board.crntMoveCandy.tag == "purpleBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Purple;
                if (board.crntMoveCandy.tag == "redBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Red;
                if (board.crntMoveCandy.tag == "whiteBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.White;
                if (board.crntMoveCandy.tag == "yellowBean") board.crntMoveCandy.GetComponent<SpriteRenderer>().sprite = board.Yellow;
                Candy curntCandy = board.crntMoveCandy.GetComponent<Candy>();
                if((curntCandy.angel <= 45f && curntCandy.angel >= -45f) ||(curntCandy.angel <= -135f || curntCandy.angel >= 135f))
                {
                    curntCandy.isRowBomb = true;
                }
                else
                {
                    curntCandy.isColBomb = true;
                }
            }
            else if (board.crntMoveCandy.GetComponent<Candy>().oppCandy != null)
            {
                Candy oppCandyScript = board.crntMoveCandy.GetComponent<Candy>().oppCandy.GetComponent<Candy>();
                if(oppCandyScript.isMatched)
                {
                    oppCandyScript.isMatched = false;
                    if (oppCandyScript.gameObject.tag == "blueBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Blue;
                    if (oppCandyScript.gameObject.tag == "greenBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Green;
                    if (oppCandyScript.gameObject.tag == "orangeBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Orange;
                    if (oppCandyScript.gameObject.tag == "pinkBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Pink;
                    if (oppCandyScript.gameObject.tag == "purpleBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Purple;
                    if (oppCandyScript.gameObject.tag == "redBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Red;
                    if (oppCandyScript.gameObject.tag == "whiteBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.White;
                    if (oppCandyScript.gameObject.tag == "yellowBeen") oppCandyScript.gameObject.GetComponent<SpriteRenderer>().sprite = board.Yellow;
                    if((board.crntMoveCandy.GetComponent<Candy>().angel <= 45f && board.crntMoveCandy.GetComponent<Candy>().angel >= -45f) || (board.crntMoveCandy.GetComponent<Candy>().angel <= -135f || board.crntMoveCandy.GetComponent<Candy>().angel >= 135f))
                    {
                        oppCandyScript.isRowBomb = true;
                    }
                    else
                    {
                        oppCandyScript.isColBomb = true;
                    }
                }
            }
            //board.crntMoveCandy = null;
        }
    }
}
