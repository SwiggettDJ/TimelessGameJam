using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    
    private float maxLoopTime;
    private float currentLoopTime;
    
    
    void Awake()
    {
        instance = this;
    }

    
    void Update()
    {
        currentLoopTime += Time.deltaTime;
        if (currentLoopTime >= maxLoopTime)
        {
            
        }
    }
}
