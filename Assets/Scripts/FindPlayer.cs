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
        cam.LookAt = FindObjectOfType<PlayerNetwork>().GetComponentInParent<Transform>();
        cam.Follow = FindObjectOfType<PlayerNetwork>().GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
