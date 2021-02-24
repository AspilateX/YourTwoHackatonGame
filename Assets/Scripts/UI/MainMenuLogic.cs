using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FadeOut))]
public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private float introTime;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Animator camAnimator;
    [SerializeField] private Animator shipAnimator;
    public void RequestStartIntro()
    {
        mainMenu.SetActive(false);
        camAnimator.SetTrigger("StartIntro");
        shipAnimator.SetTrigger("StartIntro");
        StartCoroutine(EndIntroTimer());
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }

    private IEnumerator EndIntroTimer()
    {
        yield return new WaitForSeconds(introTime);
        GetComponent<FadeOut>().FadeScreen();
        yield return new WaitUntil(() => GetComponent<FadeOut>().isFadedOut);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
