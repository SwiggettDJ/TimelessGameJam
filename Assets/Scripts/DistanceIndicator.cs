using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceIndicator : MonoBehaviour
{
    private float currentTime;
    //private CharacterController playerController;
    private float speedModifier = 20f;
    private float fogModifier = 6f;
    private float fogSmooth = 0.8f;
    private float fogDistance;
    
    private float lerpSpeed = 10f;
    [SerializeField]
    private AnimationCurve lerpCurve;
    private float curveTime = 0.5f;
    
    private Camera mainCam;
    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        mainCam = Camera.main;
        
        /*playerController = GetComponentInParent<CharacterController>();

        transform.localScale = TimeManager.instance.currentLoopTime * playerController.velocity;*/
    }

    private void Update()
    {
        float cameraDistance = Vector3.Distance(transform.position, mainCam.transform.position);
        
        currentTime = TimeManager.instance.currentLoopTime;

        if (curveTime < 0.75f)
        {
            float t = lerpCurve.Evaluate(curveTime / 0.5f);
            if (t < 0)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale*0.2f, Time.unscaledDeltaTime * lerpSpeed * Mathf.Abs(t));
                fogDistance = Mathf.Lerp(fogDistance, fogDistance*0.2f, Time.unscaledDeltaTime * lerpSpeed * Mathf.Abs(t));
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, currentTime * speedModifier * Vector3.one, Time.unscaledDeltaTime * lerpSpeed * t);
                fogDistance = Mathf.Lerp(fogDistance, currentTime * fogModifier + cameraDistance, Time.unscaledDeltaTime * lerpSpeed * t);
            }
            
            curveTime += Time.unscaledDeltaTime;
        }
        else
        {
            transform.localScale = currentTime * speedModifier * Vector3.one;
            fogDistance = currentTime * fogModifier + cameraDistance;
        }
        RenderSettings.fogStartDistance = fogDistance;
        RenderSettings.fogEndDistance = fogDistance + fogDistance*fogSmooth;
    }

    private void ChangeLerpSpeed()
    {
        curveTime = 0f;
    }

    private void OnEnable()
    {
        TimeManager.OnTimeUpgraded += ChangeLerpSpeed;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeUpgraded -= ChangeLerpSpeed;
    }
}
