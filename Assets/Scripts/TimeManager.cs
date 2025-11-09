using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    
    //public static event Action OnTimeChange;
    
    private float maxLoopTime = 2f;
    public float currentLoopTime;
    private float timeIncreaseAmount = 2f;
    
    private List<Vector3> collectedTimePos = new List<Vector3>();
    
    public static event Action<List<Vector3>> OnDisableCollectedTime;
    public static event Action OnTimeUpgraded;
    
    void Awake()
    {
        instance = this;
        SceneLoader.OnSceneLoadedCustom += SetUp;
        PlayerMovement.OnPlayerWalk += TimeControl;
    }
    

    private void SetUp()
    {
        currentLoopTime = maxLoopTime;

        if(collectedTimePos.Count > 0)
        {
            OnDisableCollectedTime?.Invoke(collectedTimePos);
        }
    }
    void Update()
    {
        currentLoopTime -= Time.deltaTime;
        if (currentLoopTime <= 0)
        {
            SceneLoader.instance.Reload();
        }
    }

    private void TimeControl(bool timeFlow)
    {
        if (timeFlow)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0.01f;
        }
    }
    public void CollectTimePiece(Vector3 timePiecePos)
    {
        collectedTimePos.Add(timePiecePos);
        UpgradeMaxTime();
    }
    private void UpgradeMaxTime()
    {
        maxLoopTime += timeIncreaseAmount;
        currentLoopTime += timeIncreaseAmount;
        OnTimeUpgraded?.Invoke();
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadedCustom -= SetUp;
    }
}
