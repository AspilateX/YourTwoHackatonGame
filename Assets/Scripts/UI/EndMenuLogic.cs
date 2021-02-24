using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuLogic : MonoBehaviour
{
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private TextMeshProUGUI HeaderText;
    [SerializeField] private TextMeshProUGUI modulesInfoText;
    [SerializeField] private TextMeshProUGUI hpInfoText;

    [Space]
    [SerializeField] private float openMenuTime;

    [SerializeField] private bool isWin;


    private void Start()
    {
        RequestEndMenu(isWin);
    }

    public void RequestEndMenu(bool isPlayerWin)
    {
        if (isPlayerWin)
            HeaderText.text = "Корабль был починен";
        else
            HeaderText.text = "Корабль был разрушен";

        modulesInfoText.text = GameData.lastGameFixedMoulesCount.ToString() + "/" + GameData.lastGameMaxModules.ToString();
        hpInfoText.text = (Mathf.Round(GameData.lastGameHPAmount).ToString() + "/" + Mathf.Round(GameData.lastGameMaxHP).ToString());

        StartCoroutine(OpenMenu());
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("CutScene_Intro", LoadSceneMode.Single);
    }

    public void SkipIntro()
    {
        endGameMenu.GetComponent<Animator>().SetTrigger("OpenMenu");
    }

    private IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(openMenuTime);
        endGameMenu.GetComponent<Animator>().SetTrigger("OpenMenu");
    }
}
