using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimePiece : MonoBehaviour
{
    void Awake()
    {
        TimeManager.OnDisableCollectedTime += DisableIfCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimeManager.instance.CollectTimePiece(transform.position);
            gameObject.SetActive(false);
        }
    }

    private void DisableIfCollected(List<Vector3> collectedPos)
    {
        foreach (Vector3 pos in collectedPos)
        {
            if (transform.position == pos)
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    private void OnDestroy()
    {
        TimeManager.OnDisableCollectedTime -= DisableIfCollected;
    }
}
