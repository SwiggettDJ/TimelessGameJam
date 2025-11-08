using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    private CinemachineFreeLook cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
        cam.LookAt = FindObjectOfType<PlayerMovement>().GetComponentInParent<Transform>();
        cam.Follow = FindObjectOfType<PlayerMovement>().GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
