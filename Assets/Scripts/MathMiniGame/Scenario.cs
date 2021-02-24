using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scenario : IMinigame
{
    public List<GameObject> IconsOfChoise = new List<GameObject>();
    public List<Text> Answers = new List<Text>();

    public List<Vector3> IconsOfChoisePosition= new List<Vector3>();

    public AudioSource myFx;
    public AudioClip hoverFx;
    public TextMeshProUGUI Answer;
    public TextMeshProUGUI TimeOnAnswer;
    public float time = 10;
    public int correctcell=2;
    private int countlvl=1;
    public int maxlvl;
    bool timerpause=false;
    public GameObject CanvasMath;
    private bool isStart=false;
    
    // Start is called before the first frame update

    protected override void Realization()
    {
        
        StartGame();
    }

    public void StartGame()
    {
        activeIncon = 2;
        isStart = true;
        IconsOfChoise[0].SetActive(false);
        IconsOfChoise[1].SetActive(false);
        IconsOfChoise[2].SetActive(true);
        IconsOfChoise[3].SetActive(false);
       
       
       
        for (int i = 0; i < 4; i++)
            Answers[i].text = " ";

        DoQuestion();
        timerpause = false;
    }
    private bool isDuration=false;
    public float duration=0;
    private float duration2 = 1;
    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            if (isDuration)
            {
                StartCoroutine(WPressKey());
            }
            if (countlvl == maxlvl)
            {
                Winn();
                countlvl = 1;
            }
            if ((Input.GetKeyDown(KeyCode.D)) && !(IconsOfChoise[3].activeSelf) && (!timerpause))
            {
                
                MoveRight();
            }
            if ((Input.GetKeyDown(KeyCode.A)) && !(IconsOfChoise[0].activeSelf) && !(timerpause))
            {
              
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.W) && (!timerpause))
            {
               
                for (int i = 0; i < 4; i++)
                    Answers[i].text = " ";
               
                isDuration = true;

            }
            if(!timerpause)time -= Time.deltaTime;
            
            TimeOnAnswer.text = (int)time + " ";
            if (time < 0)
            {
                Lose();
            }
        }
    }

    IEnumerator WPressKey()
    {
        Vector3 defolt = new Vector3(IconsOfChoise[activeIncon].transform.position.x, IconsOfChoise[activeIncon].transform.position.y, IconsOfChoise[activeIncon].transform.position.z);
       
        isDuration = false;
        timerpause = true;
        int i = 1;
        while (i < 62)
        {
            IconsOfChoise[activeIncon].transform.Translate(0, 4, 0);
            yield return new WaitForSeconds(0.02f);
            i++;
        }
        IconsOfChoise[activeIncon].transform.position = defolt;
        Debug.Log(defolt);
        if (activeIncon == correctcell)
            Complite();
        else Lose();
    }

    private int activeIncon=2;
    private void MoveRight()
    {
        IconsOfChoise[activeIncon].SetActive(false);
        activeIncon++;
        IconsOfChoise[activeIncon].SetActive(true);

    }
    private void MoveLeft()
    {
        IconsOfChoise[activeIncon].SetActive(false);
        activeIncon--;
        IconsOfChoise[activeIncon].SetActive(true);

    }
    private void DoQuestion()
    {
       
        int a = Random.Range(1, 10);
        int b = Random.Range(1, 10);
        int rightansver = a * b;
        Answer.text = a.ToString() + "*" + b.ToString() + "=";
        int newcorrectcell;
        do
        {
            newcorrectcell = Random.Range(0, 4);
        } while (newcorrectcell == correctcell);
        correctcell = newcorrectcell;

        Answers[correctcell].text = rightansver.ToString();
        int wronganswer;
        for (int i = 0; i < 4; i++)
        {
            if (i != correctcell)
            {
                 wronganswer = rightansver + ((-1) ^ Random.Range(1, 2)) * Random.Range(1, 15);
                if (wronganswer < 0) wronganswer *= (-1);
                if ((wronganswer == rightansver)&&(wronganswer==0)) wronganswer++;
                Answers[i].text = wronganswer.ToString();
            }
        }
        chek();
        time = 10;
    }
    public void Complite()
    {
        timerpause = false;
        myFx.PlayOneShot(hoverFx);
        for (int i = 0; i < 4; i++)
            Answers[i].text = " ";
        //анимация 
        countlvl++;
        DoQuestion();

    }
    public void Lose()
    {
        isStart = false;
        EndMiniGame(-20);
        CanvasMath.SetActive(false);
       
    }
    public void Winn()
    {
        isStart = false;
        EndMiniGame(25);
        CanvasMath.SetActive(false);
    }
    public void chek()
    {
        bool isDiferent = true;//изначально считаем что все ответы верны
        int wronganswer;
        int rightansver = int.Parse(Answers[correctcell].text);
        for (int i=0; i<4 ; i++)
        { if (i != correctcell)
            { int j = 0;
                while(!isDiferent || (j<4))
                {
                    if(j==4)
                    {
                        j = 0;
                        isDiferent = true;
                    }
                    if ( (j != correctcell) && (j!=i) )
                    {
                        if ( (Answers[i].text == Answers[j].text) || (Answers[correctcell].text==Answers[i].text))
                        {
                            wronganswer = rightansver + ((-1) ^ Random.Range(1, 2)) * Random.Range(1, 15);
                            if (wronganswer < 0) wronganswer *= (-1);
                            if (wronganswer == rightansver) wronganswer++;
                            Answers[i].text = wronganswer.ToString();
                            isDiferent = false;//нашлось совпадение
                        }
                    }
                    j++;

                }
            }
        }
    }
}
