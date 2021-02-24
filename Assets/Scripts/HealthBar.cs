using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpSlow;
    [SerializeField] private float DeathSpeed = 1;
    public float slowBarSpeed;

    public static HealthBar current;
    
    public const float MIN_HP = 0;
    public const float MAX_HP = 100;
    public float currentHealth;
    private float currentHealthSlow;
    private float t = 0;

    private void Awake()
    {
        current = this;
    }
    private void OnEnable()
    {
        GameEvents.current.onHealthBarChanged += ChangeHealthBar;
        GameEvents.current.onMiniGameEnded += AddHealth;
    }
    private void OnDisable()
    {
        GameEvents.current.onHealthBarChanged -= ChangeHealthBar;
        GameEvents.current.onMiniGameEnded -= AddHealth;
    }


    private void ChangeHealthBar(float newValue)
    {
        currentHealth = Mathf.Min(Mathf.Max(newValue, MIN_HP), MAX_HP);
    }

    private void AddHealth(float amount)
    {
        currentHealth = Mathf.Min(Mathf.Max(currentHealth + amount, MIN_HP), MAX_HP);
        if (amount < 0)
            t = 0;
    }

    private void Start()
    {
        currentHealth = MAX_HP;
        currentHealthSlow = MAX_HP;

        GameData.lastGameMaxHP = currentHealth;
    }

    private void Update()
    {
        if (currentHealthSlow != currentHealth)
        {
            currentHealthSlow = Mathf.Lerp(currentHealthSlow, currentHealth, t);
            t += slowBarSpeed * Time.deltaTime;
        }
        hpBar.fillAmount = currentHealth / MAX_HP;
        hpSlow.fillAmount = currentHealthSlow / MAX_HP;

        currentHealth -= Time.deltaTime * DeathSpeed;

        if (currentHealthSlow <= 0)
        {
            currentHealth = 0;
            currentHealthSlow = 0;
            DeathLogic();
        }

        
    }

    private void DeathLogic()
    {
        WinLoseLogic.current.Lose();
    }

}
