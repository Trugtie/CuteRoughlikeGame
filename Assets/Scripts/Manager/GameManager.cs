using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void WaveTransitionCallback()
    {
        if (Player.Instance.HasLevelUp())
        {
            Debug.Log("Display transition panel");
        }
        else
        {
            Debug.Log("Display Shop");
        }
    }
}
