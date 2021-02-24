using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTrigger : MinigameTrigger
{
    [SerializeField] private AmbientLogic ambient;
    [SerializeField] private int id;
    protected override void AfterLose()
    {
        
    }

    public override void AfterWin()
    {
        StartCoroutine(ambient.ActivateTrust());
    }
}
