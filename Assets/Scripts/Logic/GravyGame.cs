using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GravyGame : IMinigame
{
    public List<Image> Icons = new List<Image>();
    public GameObject Floor;
  
    public Sprite player;
    public Sprite clear;
    public Sprite luke;
    public Sprite wall;
    private int MaxIcons;
    private int playericon=29;
    private bool isMoove=false;
    public int lvl = 1;
    public float time = 10;
    public TextMeshProUGUI TimeOnAnswer;
    bool isStart = false;
    [SerializeField]
    private Sprite playerA;
    [SerializeField]
    private Sprite playerD;
    [SerializeField]
    private Sprite playerW;
    [SerializeField]
    private GameObject Game;
    [SerializeField]
    private Image ArrowW;
    [SerializeField]
    private Image ArrowA;
    [SerializeField]
    private Image ArrowD;
    [SerializeField]
    private Image ArrowS;

    protected override void Realization()
    {
        
        StartGame();
    }
    // Start is called before the first frame update
    void StartGame()
    {
        lvl = 1;  
        MaxIcons = Floor.transform.childCount;
        for (int i = 0; i < MaxIcons; i++) { Icons.Add(Floor.transform.GetChild(i).gameObject.GetComponent<Image>()); Icons[i].sprite = clear; Icons[i].tag = "Untagged"; Icons[i].color = new Vector4(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 1); }
        Icons[3].sprite = wall;
        Icons[11].sprite = wall;
        Icons[33].sprite = wall;
        Icons[34].sprite = wall;
        Icons[35].sprite = wall;
        Icons[3].tag = "wall";
        Icons[11].tag = "wall";
        Icons[33].tag = "wall";
        Icons[34].tag = "wall";
        Icons[35].tag = "wall";
        playericon = 29;
        Icons[29].sprite = player;
        Icons[44].sprite = luke;
        Icons[44].tag = "Finish";
        Icons[44].color = new Vector4(2 / 255.0f, 255 / 255.0f, 0 / 255.0f, 1);
        isStart = true;
        Game.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            time -= Time.deltaTime;
            if(time<=0)
            {
                EndMiniGame(-20);
                isStart = false;
                Game.SetActive(false);

            }
            if ((Icons[playericon].tag == "Finish") && !isMoove)
            {
                time = 10;
                Winn();
            }
            if ((Input.GetKeyDown(KeyCode.D)) && (playericon < (MaxIcons - 1)) && (!isMoove))
            {

                isMoove = true;
                StartCoroutine(Dpress());


            }
            if ((Input.GetKeyDown(KeyCode.A)) && ((playericon - 1) > -1) && (!isMoove))
            {

                isMoove = true;
                StartCoroutine(Apress());
            }
            if ((Input.GetKeyDown(KeyCode.W)) && ((playericon - 8) > -1) && (!isMoove))
            {

                isMoove = true;
                StartCoroutine(Wpress());
            }
            if ((Input.GetKeyDown(KeyCode.S)) && (playericon < (MaxIcons - 8)) && (!isMoove))
            {

                isMoove = true;
                StartCoroutine(Spress());
            }
            TimeOnAnswer.text = (int)time + " ";
        }
    }
    IEnumerator Dpress()
    {
        GreenON(ArrowD);
        while (((playericon + 1) < 48) && (Icons[playericon + 1].gameObject.tag != "wall") && ((playericon + 1) % 8 != 0))
        {
           
           
                if (Icons[playericon].tag == "Finish")
                    Icons[playericon].sprite = luke;
                else
                    Icons[playericon].sprite = clear;
                playericon++;
                Icons[playericon].sprite = player;
            yield return new WaitForSeconds(0.06f);

            

        }
        GreenOFF(ArrowD);
        isMoove = false;
        Icons[playericon].sprite = playerD;

    }
    IEnumerator Apress()
    {
        GreenON(ArrowA);
        while (((playericon - 1) > -1) && (Icons[playericon - 1].gameObject.tag != "wall") && ((playericon - 1) % 8 != 7))
        {
            if (Icons[playericon].tag == "Finish")
                Icons[playericon].sprite = luke;
            else
                Icons[playericon].sprite = clear;
            playericon--;
            Icons[playericon].sprite = player;
            yield return new WaitForSeconds(0.06f);
        }
        Icons[playericon].sprite = playerA;
        isMoove = false;
        GreenOFF(ArrowA);
    }
    IEnumerator Wpress()
    {
        GreenON(ArrowW);
        while (((playericon - 8) > -1) && (Icons[playericon - 8].gameObject.tag != "wall"))
        {
            if (Icons[playericon].tag == "Finish")
                Icons[playericon].sprite = luke;
            else
                Icons[playericon].sprite = clear;
            playericon -= 8;
            Icons[playericon].sprite = player;
            yield return new WaitForSeconds(0.06f);
        }
        isMoove = false;
        Icons[playericon].sprite = playerW;
        GreenOFF(ArrowW);
    }
    IEnumerator Spress()
    {
        GreenON(ArrowS);
        while (((playericon + 8) < MaxIcons) && (Icons[playericon + 8].gameObject.tag != "wall"))
        {
            if (Icons[playericon].tag == "Finish")
                Icons[playericon].sprite = luke;
            else
                Icons[playericon].sprite = clear;
            playericon += 8;
            Icons[playericon].sprite = player;
            yield return new WaitForSeconds(0.06f);
        }
         isMoove = false;
        GreenOFF(ArrowS);
    }
    public void Winn()
    {
        lvl++;
        switch (lvl)
        {
            case (2):lvl2();break;
            case (3):lvl3();break;
            case (4):lvl4();break;
            default:Pobeda(); break;
        }
 

    }
    void Pobeda()
    {
        isStart = false;

        EndMiniGame(+20);

        Game.SetActive(false);
    }
    

   
    public void lvl2()
    {
     
       
        for (int i = 0; i < 48; i++)
        {
            Icons[i].sprite = clear;
            Icons[i].tag = "Untagged";
        }
        Icons[0].sprite = player;
        playericon = 0;
        Icons[5].sprite = wall;
        Icons[5].tag = "wall";
        Icons[20].sprite = wall;
        Icons[20].tag = "wall";
        Icons[38].sprite = wall;
        Icons[38].tag = "wall";
        for (int i = 8; i < 11; i++)
        {
            Icons[i].sprite = wall;
            Icons[i].tag = "wall";
        }
        for (int i = 25; i < 28; i++)
        {
            Icons[i].sprite = wall;
            Icons[i].tag = "wall";
        }
        Icons[44].sprite = luke;
        Icons[44].color = new Vector4(2 / 255.0f, 255 / 255.0f, 0 / 255.0f, 1);
        Icons[44].tag = "Finish";


    }
    public void lvl3()
    {

        for (int i = 0; i < 48; i++)
        {
            Icons[i].sprite = clear;
            Icons[i].tag = "Untagged";
            Icons[i].color = new Vector4(255 / 255.0f, 255/ 255.0f, 255 / 255.0f, 1);
        }

        Icons[27].sprite = player;
        playericon = 27;
        Icons[28].sprite = luke;
        Icons[28].tag="Finish";
        Icons[28].color = new Vector4(2 / 255.0f, 255 / 255.0f, 0 / 255.0f, 1);
        Icons[29].sprite = wall;
        Icons[29].tag = "wall";

        Icons[3].sprite = wall;
        Icons[3].tag = "wall";
        Icons[6].sprite = wall;
        Icons[6].tag = "wall";
        Icons[8].sprite = wall;
        Icons[8].tag = "wall";
        Icons[21].sprite = wall;
        Icons[21].tag = "wall";
        Icons[23].sprite = wall;
        Icons[23].tag = "wall";
        Icons[44].sprite = wall;
        Icons[44].tag = "wall";

    }
    public void lvl4()
    {
        for (int i = 0; i < 48; i++)
        {
            Icons[i].sprite = clear;
            Icons[i].tag = "Untagged";
            Icons[i].color = new Vector4(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 1);
        }
        Icons[17].sprite = player;
        playericon = 17;
        Icons[37].sprite = luke;
        Icons[37].tag = "Finish";
        Icons[37].color = new Vector4(2 / 255.0f, 255 / 255.0f, 0 / 255.0f, 1); Icons[5].sprite = wall;
        Icons[5].tag = "wall";

        Icons[7].sprite = wall;
        Icons[7].tag = "wall";
        Icons[9].sprite = wall;
        Icons[9].tag = "wall";
        Icons[20].sprite = wall;
        Icons[20].tag = "wall";
        Icons[23].sprite = wall;
        Icons[23].tag = "wall";
        Icons[24].sprite = wall;
        Icons[24].tag = "wall";
        Icons[28].sprite = wall;
        Icons[28].tag = "wall";
        Icons[29].sprite = wall;
        Icons[29].tag = "wall";
        Icons[33].sprite = wall;
        Icons[33].tag = "wall";
        Icons[35].sprite = wall;
        Icons[35].tag = "wall";
        Icons[36].sprite = wall;
        Icons[36].tag = "wall";
        Icons[46].sprite = wall;
        Icons[46].tag = "wall";



    }
    public void GreenON(Image Arrow)
    {
        Arrow.color = new Vector4(2 / 255.0f, 255 / 255.0f, 0 / 255.0f, 1);
    }
    public void GreenOFF(Image Arrow)
    {
        Arrow.color = new Vector4(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 1);
    }
}
