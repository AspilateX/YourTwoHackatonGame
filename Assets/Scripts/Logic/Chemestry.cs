using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chemestry : IMinigame
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public GameObject panel;
    [SerializeField]
    private List<OnFollderClick> Edit = new List<OnFollderClick>();
    public List<Text> Per = new List<Text>();
    public GameObject Pers;
    public int clickid;
    public GameObject Edits;
    int lvl=1;
    private int MaxIcons;
    private int hp = 2;
    string[] RightAnswer = new string[6];
     public  bool[] RightEdit = new bool[6];
    [SerializeField]
    private GameObject Game;
    bool FormulaOneIsRight = false;
    [SerializeField]
    private Image WaterOneH;
    [SerializeField]
    private Image WaterOneW;
    float WaterOn1=0;
    [SerializeField]
    private Image WaterTwoH;
    [SerializeField]
    private Image WaterTwoW;
    float WaterOn2 = 0;
    bool FormulaTwoIsRight = false;
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private Transform forbubble;
    [SerializeField]
    private Transform forbubbleleft;
    [SerializeField]
    private GameObject lamp1;
    public Sprite bipoff;
    public Sprite bipon;
    bool change = true;
    public bool start=false;
    protected override void Realization()
    {
        Game.SetActive(true);
        
        StartGame();
        RightEdit[1] = true;
        start = true;

    }
    // Start is called before the first frame update
   

    void StartGame()
    {

        
        MaxIcons = Edits.transform.childCount;
        for (int i = 0; i < MaxIcons; i++) Edit.Add(Edits.transform.GetChild(i).gameObject.GetComponent<OnFollderClick>());

   
        MaxIcons = Pers.transform.childCount;
        for (int i = 0; i < MaxIcons; i++) Per.Add(Pers.transform.GetChild(i).gameObject.GetComponent<Text>());
       

        lamp1.GetComponent<Image>().sprite = bipon;
        hp = 2;
        lvl = 1;


        WaterOneH.fillAmount = 1f;
        WaterTwoH.fillAmount = 1f;
        WaterOneW.fillAmount = 0.265f;
        WaterTwoW.fillAmount = 0.265f;
        Per[0].text = "Сa";
        Per[1].text = "O";
        Per[2].text = "2";
        Per[3].text = "CaO";


        Per[4].text = "Fe";
        Per[5].text = "Cl";
        Per[6].text = "2";
        Per[7].text = "FeCl";
        Per[8].text = "3";
        Per[9].gameObject.SetActive(true);
        RightAnswer[4] = "1";

        Edit[1].gameObject.SetActive(false);
        panel.SetActive(false);
        RightAnswer[0] = "2";
        RightAnswer[1] = "1";
        RightAnswer[2] = "2";
        RightAnswer[3] = "2";
        RightAnswer[4] = "3";
        RightAnswer[5] = "2";
        for (int i = 0; i < 6; i++)
        {
            RightEdit[i] = false;
        }
        MaxIcons = Edits.transform.childCount;
        for (int i = 0; i < MaxIcons; i++)
        {
            Edit[i].SetText("1");
        }

        FormulaOneIsRight = false;
        FormulaTwoIsRight = false;


    }

    // Update is called once per frame
    public void ChangeTextById(string newtext)
    {
        
        Edit[clickid].SetText(newtext);
        ChekOnRight();
        ClosePanel();

    }
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
    void ClosePanel()
    {
        panel.SetActive(false);
    }
    void ChekOnRight()
    {
        if (Edit[clickid].text.text != RightAnswer[clickid]) { hp--; lamp1.GetComponent<Image>().sprite = bipoff; RightEdit[clickid] = false; } else RightEdit[clickid] = true;
    }
    void Update()
    {
        if (start)
        { 
            if (hp == 0)
            {
                start = false;
                EndMiniGame(-20);
                
                Game.SetActive(false);
            }
            if (RightEdit[0] && RightEdit[1] && RightEdit[2] && RightEdit[3] && RightEdit[4] && RightEdit[5] && change)
            {
                StartCoroutine(Wait());

            }
            if (RightEdit[0] && RightEdit[1] && RightEdit[2] && !FormulaOneIsRight)
            {
                StartCoroutine(SpawnBubles(forbubble));
                StartCoroutine(RigthFormulaOne());
            }
            if (RightEdit[3] && RightEdit[4] && RightEdit[5] && !FormulaTwoIsRight)
            {
                StartCoroutine(SpawnBubles(forbubbleleft));
                StartCoroutine(RigthFormulaTwo());
            }
        }
    }
    IEnumerator Wait()
    {
        change = false;
        yield return new WaitForSeconds(4f);
        
        ChangeLvl();
    }
    IEnumerator RigthFormulaOne()
    {
        FormulaOneIsRight = true;
        while (WaterOneW.fillAmount<1)
        {
            
            WaterOn1 = 0.1f;
            yield return new WaitForSeconds(0.05f);
            WaterOneH.fillAmount -= WaterOn1;
            Debug.Log(WaterOneH.fillAmount);
            WaterOneW.fillAmount += WaterOn1 * 0.735f;
           

        }
        
    }
    IEnumerator SpawnBubles(Transform spawn)
    {
        myFx.PlayOneShot(hoverFx);
        int i =1;
        while (i < 15)
        {
            for(int j=0;j<Random.RandomRange(1,4);j++)
            Instantiate(bubble, spawn.position + new Vector3(Random.Range(0, 30) , 0, 0), spawn.rotation).transform.SetParent(Game.transform);
            yield return new WaitForSeconds(0.2f);
            i++;
        }
    }
    IEnumerator RigthFormulaTwo()
    {
        FormulaTwoIsRight = true;
        while (WaterTwoW.fillAmount < 1)
        {
            WaterOn2 = 0.05f;
            yield return new WaitForSeconds(0.1f);
            WaterTwoH.fillAmount -= WaterOn2;
            Debug.Log(WaterOneH.fillAmount);
            WaterTwoW.fillAmount += WaterOn2 * 0.735f;


        }

    }
    void ChangeLvl()
    {
        lvl++;
        switch(lvl)
        {
            case (2):lvl2();change = true; break;
            case (3):lvl3(); change = true; break;
            default: Winn(); break;

        }
    }
    void Winn()
    {
        EndMiniGame(+20);
        start = false;
        Game.SetActive(false);
    }
    void lvl2()
    {
        WaterOneH.fillAmount = 1f;
        WaterTwoH.fillAmount = 1f;
        WaterOneW.fillAmount = 0.265f;
        WaterTwoW.fillAmount = 0.265f;
        Per[0].text = "Na";
        Per[1].text = "Cl";
        Per[2].text = "2";
        Per[3].text = "NaCl";
        Per[4].text = "Al";
        Per[5].text = "Cl";
        Per[6].text = "2";
        Per[7].text = "AlCl";
        Per[8].text = "3";
        MaxIcons = Edits.transform.childCount;
        for (int i=0;i<MaxIcons;i++)
        {
            Edit[i].SetText("1");
        }
        Edit[1].gameObject.SetActive(true);
        for (int i=0;i<6;i++)
        {
            RightEdit[i] = false;
        }
        RightEdit[1] = true;
        FormulaOneIsRight = false;
        FormulaTwoIsRight = false;

    }
    void lvl3()
    {
        WaterOneH.fillAmount = 1f;
        WaterTwoH.fillAmount = 1f;
        WaterOneW.fillAmount = 0.265f;
        WaterTwoW.fillAmount = 0.265f;
        Per[0].text = "Li";
        Per[1].text = "O";
        Per[2].text = "2";
        Per[3].text = "Li";
        Per[10].gameObject.SetActive(true);
        Per[11].gameObject.SetActive(true);
        RightAnswer[0] = "4";
        RightAnswer[1] = "1";
        RightAnswer[2] = "2";

        Per[4].text = "FeCl";
        Per[5].text = "Cl";
        Per[6].text = "2";
        Per[7].text = "FeCl";
        Per[8].text = "3";
        Per[9].gameObject.SetActive(true);
        RightAnswer[4] = "1";

        MaxIcons = Edits.transform.childCount;
        for (int i = 0; i < MaxIcons; i++)
        {
            Edit[i].SetText("1");
        }
        Edit[1].gameObject.SetActive(true);
        for (int i = 0; i < 6; i++)
        {
            RightEdit[i] = false;
        }
        RightEdit[1] = true;
        RightEdit[4] = true;
        FormulaOneIsRight = false;
        FormulaTwoIsRight = false;
    }
}
