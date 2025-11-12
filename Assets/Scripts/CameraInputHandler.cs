using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraInputHandler : MonoBehaviour
{
    private CinemachineFreeLook freeCam;
    // Start is called before the first frame update
    void Awake()
    {
        freeCam = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLook(InputValue value)
    { 
        Vector2 input = value.Get<Vector2>();
        print(input);
        freeCam.m_XAxis.Value = input.x;
        freeCam.m_YAxis.Value = input.y;
        
    }
}
