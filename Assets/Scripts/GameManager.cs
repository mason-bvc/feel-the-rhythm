using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Mason: hah
    public static event Action<GameManager> OnInitializedGlobalInstance;

    public void Start()
    {
        // Mason: hah
        OnInitializedGlobalInstance?.Invoke(this);
    }
}
