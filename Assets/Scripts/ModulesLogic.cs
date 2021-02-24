using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModulesLogic : MonoBehaviour
{
    public static ModulesLogic current;

    private List<MinigameTrigger> modules = new List<MinigameTrigger>();

    public int fixedModulesAmount = 0;
    public int modulesAmount;


    private void Awake()
    {
        current = this;
    }

    private void OnEnable()
    {
        GameEvents.current.onPlayerFixedModule += RegisterFixedModule;
    }
    private void OnDisable()
    {
        GameEvents.current.onPlayerFixedModule -= RegisterFixedModule;
    }

    private void Start()
    {
        modulesAmount = GameObject.FindGameObjectsWithTag("Minigame").Length;
    }

    private void RegisterFixedModule()
    {
        fixedModulesAmount++;
    }

    private void Update()
    {
        if (fixedModulesAmount >= modulesAmount)
        {
            GetComponent<WinLoseLogic>().Win();
        }
    }
}
