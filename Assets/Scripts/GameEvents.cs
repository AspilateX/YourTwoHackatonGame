using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<float> onHealthBarChanged;
    public event Action<float> onMiniGameEnded;
    public event Action<KeyCode> onKeyHelperRequest;
    public event Action onKeyHelperHideRequest;
    public event Action onPlayerHitInteractButton;
    public event Action onPlayerHitGrabButton;
    public event Action onPlayerFixedModule;
    //public event Action onPlayerHitUseItemButton;

    public void FixModule()
    {
        onPlayerFixedModule?.Invoke();
    }
    public void HitInteract()
    {
        onPlayerHitInteractButton?.Invoke();
    }

    //public void HitUseItem()
    //{
    //    onPlayerHitUseItemButton?.Invoke();
    //}
    public void HitGrab()
    {
        onPlayerHitGrabButton?.Invoke();
    }
    public void ChangeHealthBar(float newValue)
    {
        onHealthBarChanged?.Invoke(newValue);
    }
    public void EndMiniGame(float rewardAmount)
    {
        onMiniGameEnded?.Invoke(rewardAmount);
    }
    public void RequestKeyHelper(KeyCode key)
    {
        onKeyHelperRequest?.Invoke(key);
    }

    public void RequestHideKeyHelper()
    {
        onKeyHelperHideRequest?.Invoke();
    }
}
