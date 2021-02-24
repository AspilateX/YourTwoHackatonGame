using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SinMGLogic : IMinigame
{
    public struct Task
    {
        public string question;
        public string[] answers;
        public uint rightAnswerID;
        public int[] asteroidAngles;

        public Task(string qstn, string[] aswrs, uint id, int[] angles) : this()
        {
            question = qstn;
            answers = aswrs;
            rightAnswerID = id;
            asteroidAngles = angles;
        }
    }

    private static string s = char.ConvertFromUtf32(0x221A);

    public List<Task> tasks = new List<Task>() // Добавить больше не синусов
    {
        new Task("Sin(60)", new string[4] { s+"2/2", "1/2", s + "3/2", "1" }, 2, new int[4] {45, 30, 60, 90}),
        new Task("Sin(30)", new string[4] { s+"2/2", "1/2", s + "3/2", "0" }, 1, new int[4] {45, 30, 60, 0}),
        new Task("Sin(45)", new string[4] { s+"2/2", "1/2", s + "3/2", "1" }, 0, new int[4] {45, 30, 60, 90}),
        new Task("Sin(90)", new string[4] { s+"2/2", "1/2", "0", "1" }, 3, new int[4] {45, 30, 0, 90}),

        new Task("Sin(-60)", new string[4] { "-" + s + "3/2", s + "3/2", "1/2", "-1/2" }, 0, new int[4] {-60, 60, 30, -30}),
        new Task("Sin(-30)", new string[4] { "-" + s + "3/2", s + "3/2", "1/2", "-1/2" }, 3, new int[4] {-60, 60, 30, -30}),
        new Task("Sin(-45)", new string[4] {s + "2/2", "-" + s + "2/2", "1/2", "-1/2" }, 1, new int[4] {45, -45, 30, -30}),
        new Task("Sin(-90)", new string[4] {s + "2/2", "-" + s + "2/2", "1", "-1" }, 3, new int[4] {45, -45, 90, -90}),

        new Task("Sin(120)", new string[4] { s + "3/2", "1/2", s + "2/2", "-" + s + "2/2" }, 0, new int[4] {120, 150, 135, 225}),
        new Task("Sin(135)", new string[4] { s + "3/2", "1/2", s + "2/2", "-" + s + "2/2" }, 2, new int[4] {120, 150, 135, 225}),
        new Task("Sin(150)", new string[4] { s+"3/2", "1/2", "-" + s + "3/2", "-1/2" }, 1, new int[4] {120, 150, 240, 210}),
        new Task("Sin(210)", new string[4] { s+"3/2", "1/2", "-" + s + "3/2", "-1/2" }, 3, new int[4] {120, 150, 240, 210}),
        new Task("Sin(225)", new string[4] { s + "3/2", "-1/2", s + "2/2", "-" + s + "2/2" }, 3, new int[4] {120, 210, 135, 225}),
        new Task("Sin(240)", new string[4] { s+"3/2", "1/2", "-" + s + "3/2", "-1/2" }, 2, new int[4] {120, 150, 240, 210}),
        new Task("Sin(300)", new string[4] { "-" + s + "2/2", "1/2", "-" + s + "3/2", "-1/2" }, 2, new int[4] {315, 30, 300, 330}),
        new Task("Sin(315)", new string[4] { "-" + s + "2/2", "1/2", "-" + s + "3/2", "-1/2" }, 0, new int[4] {315, 30, 300, 330}),
        new Task("Sin(330)", new string[4] { "-" + s + "2/2", "1/2", "-" + s + "3/2", "-1/2" }, 3, new int[4] {315, 30, 300, 330}),

        new Task("Cos(60)", new string[4] { s+"2/2", "1/2", s + "3/2", "1" }, 1, new int[4] {45, 60, 30, 90}),
        new Task("Cos(30)", new string[4] { s+"2/2", "1/2", s + "3/2", "0" }, 2, new int[4] {45, 60, 30, 0}),
        new Task("Cos(45)", new string[4] { s+"2/2", "1/2", s + "3/2", "1" }, 0, new int[4] {45, 60, 30, 90}),
        new Task("Cos(90)", new string[4] { s+"2/2", "1/2", "1", "0" }, 3, new int[4] {45, 60, 0, 90}),

        new Task("Tan(30)", new string[4] { s+"3/3", s+"3", "1", "0" }, 0, new int[4] {30+180, 60+180, 45+180, 0+180}),
        new Task("Tan(45)", new string[4] { s+"3/3", s+"3", "1", "0" }, 2, new int[4] {30, 60, 45, 0}),
        new Task("Tan(60)", new string[4] { s+"3/3", s+"3", "1", "0" }, 1, new int[4] {30+180, 60+180, 45+180, 0+180}),
        new Task("Tan(0)", new string[4] { s+"3/3", s+"3", "1", "0" }, 3, new int[4] {30, 60, 45, 0}),

    };

    [SerializeField] private Button[] answerButtons = new Button[4];
    [SerializeField] private Text QuestionField;
    [SerializeField] Transform center;
    [SerializeField] private float asteroidDistance;
    [SerializeField] private float timeToAnswer;
    [SerializeField] private float distanceToDestroyAsteroid;
    [SerializeField] private float bulletTime;
    [Space]
    [SerializeField] GameObject asteroidPref;
    [SerializeField] private GameObject bulletPref;
    private int asteroidsCount = 6;
    [SerializeField] private Text asteroidsCounter;
    [SerializeField] private Image beeper;
    [SerializeField] private AudioSource beepAS;
    [SerializeField] private Sprite redBeep;
    [SerializeField] private Sprite greenBeep;
    [SerializeField] private Sprite offBeep;
    [Space]
    [SerializeField] private AudioSource shotAS;
    [SerializeField] private AudioSource expAS;
    [SerializeField] private GameObject game;
    private Transform asteroid;
    private bool playerAnswerIsCorrect;
    private int playerAnswer;
    IEnumerator co;

    private Task currentTask;


    protected override void Realization()
    {
        game.SetActive(true);
        asteroidsCount = 6;
        GenerateTask();
    }

    private void GenerateTask()
    {
        asteroidsCounter.text = asteroidsCount.ToString();
        beeper.sprite = offBeep;
        playerAnswerIsCorrect = false;
        currentTask = tasks[Random.Range(0, tasks.Count)];
        QuestionField.text = currentTask.question;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentTask.answers[i];
        }
        asteroid = Instantiate(asteroidPref, center.position, Quaternion.identity, center).transform;
        asteroid.position = center.position + Vector3.right * asteroidDistance;
        asteroid.RotateAround(center.position, Vector3.forward, currentTask.asteroidAngles[currentTask.rightAnswerID]);
        asteroid.rotation = new Quaternion(0, 0, 0, 0);
        co = Timer();
        StartCoroutine(co);
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }
    }

    private IEnumerator Timer()
    {
        float timeLeft = timeToAnswer;
        float beepTime = timeLeft / 15f;
        beepAS.Play();
        while (timeLeft > Time.deltaTime)
        {
            if (asteroid != null)
            {
                asteroid.position = Vector3.Lerp(asteroid.position, center.position, Time.deltaTime / timeLeft);
                timeLeft -= Time.deltaTime;
                beepTime -= Time.deltaTime;
                if (beepTime <= Time.deltaTime)
                {
                    beepAS.Play();
                    beepTime = timeLeft / 15f;
                    if (beepTime < 0.1f)
                        beepTime = 0.1f;
                    StartCoroutine(BeepTimer(0.05f));
                }
                yield return null;
            }
            else
            {
                CorrectAnswerLogic();
                yield break;
            }
        }
        Destroy(asteroid.gameObject);
        IncorrectAnswerLogic();
    }

    private IEnumerator BeepTimer(float beepTime)
    {
        beeper.sprite = redBeep;
        yield return new WaitForSeconds(beepTime);
        beeper.sprite = offBeep;
    }

    private IEnumerator ShotRoutine()
    {
        float timeLeft = bulletTime;
        Transform bullet = Instantiate(bulletPref, center.position, Quaternion.identity, center).transform;
        shotAS.Play();
        bullet.position = center.position + Vector3.right * asteroidDistance;
        bullet.RotateAround(center.position, Vector3.forward, currentTask.asteroidAngles[playerAnswer]);
        bullet.rotation = new Quaternion(0, 0, 0, 0);
        Vector3 targetPos = bullet.position;
        bullet.position = center.position;

        while (timeLeft > Time.deltaTime)
        {
            bullet.position = Vector3.Lerp(bullet.position, targetPos, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
            if (playerAnswerIsCorrect && Vector3.Distance(bullet.position, asteroid.position) < distanceToDestroyAsteroid)
            {
                expAS.Play();
                Destroy(asteroid.gameObject);
                Destroy(bullet.gameObject);
                yield break;
            }
            yield return null;
        }
        Destroy(bullet.gameObject);
    }    

    public void GetAnswer(int id)
    {
        playerAnswer = id;
        if (id == currentTask.rightAnswerID)
            playerAnswerIsCorrect = true;
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
        StartCoroutine(ShotRoutine());
    }

    private void CorrectAnswerLogic()
    {
        Debug.Log("CORRECT!");
        asteroidsCount--;
        if (asteroidsCount > 0)
            GenerateTask();
        else
        {
            game.SetActive(false);
            EndMiniGame(25);
        }
    }

    private void IncorrectAnswerLogic()
    {
        Debug.Log("INCORRECT!");
        game.SetActive(false);
        EndMiniGame(-15);
        AmbientLogic.current.CreateExplosion(1);
    }

    private void MakeShot()
    {

    }
}
