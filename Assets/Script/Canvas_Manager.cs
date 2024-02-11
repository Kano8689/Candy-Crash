using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Manager : MonoBehaviour
{
    public GameObject lvlTeddyImg;
    public static int[] lvlTeddyNo = {3};
    public Text teddyNoDisp;
    public Sprite Teddy;
    Board board;
    // Start is called before the first frame update
    void Start()
    {
        board = FindAnyObjectByType<Board>();
        //teddyNoDisp.GetComponent<Text>().text = lvlTeddyNo[0].ToString();
        //Instantiate(Teddy, lvlTeddyImg.transform);
    }

    // Update is called once per frame
    void Update()
    {
        teddyNoDisp.GetComponent<Text>().text = lvlTeddyNo[0].ToString();
    }
}
