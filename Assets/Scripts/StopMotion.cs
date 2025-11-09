using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StopMotion : MonoBehaviour
{
    private float updateInterval = 1/3f; // Framerate
    private Animator animator;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer >= updateInterval)
        {
            animator.SetTrigger("FrameUpdate");
            timer = 0;
        }
    }
}
