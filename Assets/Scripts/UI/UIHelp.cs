using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIHelp : MonoBehaviour
{
    [SerializeField] private Image HelpButtonImage;
    [SerializeField] private TextMeshProUGUI HelpButtonLetter;
    [Space]
    [SerializeField] private Image HelpMassagePanel; // У HelpPanel должен быть дочерний объект - TextMeshPro
    private TextMeshProUGUI HelpText;
    public static UIHelp current;
    [Space]
    [SerializeField] private TextMeshProUGUI modulesText;
    [SerializeField] private TextMeshProUGUI fireText;
    [Space]
    [SerializeField] private GameObject BigHelpGO;
    [SerializeField] private TextMeshProUGUI BigHelpText;

    private bool isAlreadyShowing = false;

    private void OnEnable()
    {
        GameEvents.current.onKeyHelperRequest += ShowKeyHelp;
        GameEvents.current.onKeyHelperHideRequest += HideKeyHelp;
    }

    private void OnDisable()
    {
        GameEvents.current.onKeyHelperRequest -= ShowKeyHelp;
        GameEvents.current.onKeyHelperHideRequest -= HideKeyHelp;
    }

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        HideKeyHelp();
        CloseHelpMassage();
        HelpText = HelpMassagePanel.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        HelpText.autoSizeTextContainer = true;

        // Пример вызова
        //ShowHelpMassage("Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. " +
        //    "Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово. Word, Слово.", 10);
    }

    private void Update()
    {
        GameData.lastGameHPAmount = HealthBar.current.currentHealth;
        GameData.lastGameMaxHP = 100;
        GameData.lastGameFixedMoulesCount = ModulesLogic.current.fixedModulesAmount;
        GameData.lastGameMaxModules = ModulesLogic.current.modulesAmount;

        modulesText.text = GameData.lastGameFixedMoulesCount.ToString() + "/" + GameData.lastGameMaxModules.ToString();
    }

    private void ShowKeyHelp(KeyCode key)
    {
        HelpButtonImage.gameObject.SetActive(true);
        HelpButtonLetter.gameObject.SetActive(true);

        HelpButtonLetter.text = key.ToString();
    }
    private void HideKeyHelp()
    {
        HelpButtonImage.gameObject.SetActive(false);
        HelpButtonLetter.gameObject.SetActive(false);
    }

    public void ShowHelpMassage(string massage, float time)
    {
        if (!isAlreadyShowing)
            StartCoroutine(ShowingHelp(massage, time));
    }

    private IEnumerator ShowingHelp(string massage, float time)
    {
        isAlreadyShowing = true;
        HelpMassagePanel.gameObject.SetActive(true);
        HelpText.text = massage;
        yield return new WaitForSeconds(time);
        HelpText.text = null;
        isAlreadyShowing = false;
        CloseHelpMassage();
    }
    public void CloseHelpMassage()
    {
        HelpMassagePanel.gameObject.SetActive(false);
    }

    public void RequestBigHelp(string text)
    {
        Time.timeScale = 0f;
        BigHelpGO.SetActive(true);
        BigHelpText.text = text;
    }

    public void HideBigHelp()
    {
        Time.timeScale = 1f;
        BigHelpGO.SetActive(false);
    }

}
