using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMinigame : IMinigame
{
    protected override void Realization()
    {
        Debug.LogError("FUCK U");
        EndMiniGame(-20);
    }
}
