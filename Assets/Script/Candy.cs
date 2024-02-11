using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Candy : MonoBehaviour
{
    public int rows, cols, targetX, targetY,prevRow, PrevCols;
    Vector2 firstPos, secondPos;
    public float angel;
    Board board;
    Find_Match find_match;
    public GameObject oppCandy;
    public bool isMatched, isRowBomb, isColBomb;
    public static int teddyCnt=1;
    int cntteddy;
    Canvas_Manager canvasManager;
    // Start is called before the first frame update
    void Start()
    {
        cntteddy = teddyCnt;
        board = FindObjectOfType<Board>();
        find_match = FindObjectOfType<Find_Match>();
        canvasManager = GetComponent<Canvas_Manager>();
    }

    // Update is called once per frame
    private void Update()
    {
        targetX = cols;
        targetY = rows;
        if(Mathf.Abs(targetX - transform.position.x) > 0f)
        {
            Vector2 pos = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, pos, 0.5f);
            if (this.gameObject != board.allCandies[cols, rows])
            {
                board.allCandies[cols, rows] = this.gameObject;
            }
        }
        if(Mathf.Abs(targetY - transform.position.y) > 0f)
        {
            Vector2 pos = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(pos, transform.position, 0.5f);
            if(this.gameObject != board.allCandies[cols, rows])
            {
                board.allCandies[cols, rows] = this.gameObject;
            }
        }

        StartCoroutine(checkMatchCandy());
        find_match.findMatchesCandies(targetX, targetY);
        //findMatchCandy();
        checkPosTeddy();
    }

    private void OnMouseDown()
    {
        board.crntMoveCandy = this.gameObject;
        firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("firstPos = " + firstPos);
    }

    private void OnMouseUp()
    {
        secondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("secondPos = "+secondPos);
        checkAngel();
    }

    void checkAngel()
    {
        if (Mathf.Abs(secondPos.x - firstPos.x) > 0f || Mathf.Abs(secondPos.y - firstPos.y) > 0f)
        {
            Vector2 offSet = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
            angel = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;
            //Debug.Log("angel = " + angel);

            candyMoveDirection();
        }
    }   

    void candyMoveDirection()
    {
        if (angel >= 135 || angel <= -135)
        {
            prevRow = rows;
            PrevCols = cols;
            oppCandy = board.allCandies[cols - 1, rows];
            oppCandy.GetComponent<Candy>().cols += 1;
            oppCandy.name = "(" + cols + "," + rows + ")";
            cols -= 1;
            this.name = "(" + cols + "," + rows + ")";
            //Debug.Log("left");
        }
        else if (angel <= 45f && angel >= -45f)
        {
            prevRow = rows;
            PrevCols = cols;
            oppCandy = board.allCandies[cols + 1, rows];
            oppCandy.GetComponent<Candy>().cols -= 1;
            oppCandy.name = "(" + cols + "," + rows + ")";
            cols += 1;
            this.name = "(" + cols + "," + rows + ")";
            //Debug.Log("right");
        }
        else if (angel >= 45f && angel <= 135f)
        {
            prevRow = rows;
            PrevCols = cols;
            //oppCandy = board.allCandies[cols, rows - 1];
            //rows -= 1;
            //oppCandy.GetComponent<Candy>().rows += 1;
            oppCandy = board.allCandies[cols, rows + 1];
            oppCandy.GetComponent<Candy>().rows -= 1;
            oppCandy.name = "(" + cols + "," + rows + ")";
            rows += 1;
            this.name = "(" + cols + "," + rows + ")";
            //Debug.Log("up");
        }
        else if (angel >= -135f && angel <= -45f)
        {
            prevRow = rows; 
            PrevCols = cols;
            oppCandy = board.allCandies[cols, rows - 1];
            oppCandy.GetComponent<Candy>().rows += 1;
            oppCandy.name = "(" + cols + "," + rows + ")";
            rows -= 1;
            this.name = "(" + cols + "," + rows + ")";
            //Debug.Log("down");
        }
        StartCoroutine(checkMatchCandy());
    }

    //void findMatchCandy()
    //{
    //    if (cols > 0 && cols < board.cols - 1)
    //    {
    //        GameObject curntCandy = board.allCandies[cols, rows];
    //        if (curntCandy != null)
    //        {
    //            GameObject leftCandy = board.allCandies[cols - 1, rows];
    //            GameObject rightCandy = board.allCandies[cols + 1, rows];

    //            if (leftCandy != null && rightCandy != null)
    //            {
    //                if (leftCandy.tag == curntCandy.tag && rightCandy.tag == curntCandy.tag)
    //                {
    //                    isMatched = true;
    //                    leftCandy.GetComponent<Candy>().isMatched = true;
    //                    rightCandy.GetComponent<Candy>().isMatched = true;
    //                }
    //            }
    //        }
    //    }

    //    if (rows > 0 && rows < board.rows - 1)
    //    {
    //        GameObject curntCandy = board.allCandies[cols, rows];
    //        if (curntCandy != null)
    //        {
    //            GameObject upCandy = board.allCandies[cols, rows + 1];
    //            GameObject downCandy = board.allCandies[cols, rows - 1];

    //            if (upCandy != null && downCandy != null)
    //            {
    //                if (upCandy.tag == curntCandy.tag && downCandy.tag == curntCandy.tag)
    //                {
    //                    isMatched = true;
    //                    upCandy.GetComponent<Candy>().isMatched = true;
    //                    downCandy.GetComponent<Candy>().isMatched = true;
    //                }
    //            }
    //        }
    //    }

    //}

    IEnumerator checkMatchCandy()
    {
        yield return new WaitForSeconds(0.5f);
        if(oppCandy!=null)
        {
            if(!isMatched && !oppCandy.GetComponent<Candy>().isMatched)
            {
                oppCandy.GetComponent<Candy>().rows = rows;
                oppCandy.GetComponent<Candy>().cols = cols;
                rows = prevRow;
                cols = PrevCols;
                oppCandy = null;
            }
            else
            {
                board.destroyMatchCandy();
                oppCandy = null;
            }
        }
    }
    void checkPosTeddy()
    {
        if (this.tag == "Teddy" && rows==0)
        {
            isMatched = true;
            Canvas_Manager.lvlTeddyNo[0]--;
            board.destroyMatchCandy();
            if (Canvas_Manager.lvlTeddyNo[0] > 0)
            {
                board.generateTeddy();
            }
            else
            {
                //call Win;
            }
        }
        //if(this.tag=="Teddy" && rows<=2)
        //{
        //    board.generateTeddy();
        //}
    }

}
