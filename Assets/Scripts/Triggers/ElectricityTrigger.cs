using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ElectricityTrigger : MinigameTrigger
{
    [SerializeField] private Light2D mainLight;
    [SerializeField] private Light2D redLight;
    public override void AfterWin()
    {
        mainLight.intensity = 0.5f;
        redLight.intensity = 0;
    }

    protected override void AfterLose()
    {
        
    }

    void Start()
    {
        mainLight.intensity = 0;
    }
}
