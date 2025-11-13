using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
//By implementing IInputAxisProvider Cinemachine will grab this script as the input provider for the camera
public class CameraInputHandler : MonoBehaviour, AxisState.IInputAxisProvider
{
    private CinemachineFreeLook freeCam;
    private float cashedInputX;
    private float cashedInputY;
    private float sensitivity = 1;
    
    void Awake()
    {
        freeCam = GetComponent<CinemachineFreeLook>();
    }
    
    private void OnLook(InputValue value)
    { 
        Vector2 input = value.Get<Vector2>();

        cashedInputX = input.x;
        cashedInputY = input.y;

    }
    
    // This is what cinemachine calls to get the axis values
    public float GetAxisValue(int axis)
    {
        switch (axis)
        {
            case 0: return cashedInputX * sensitivity;
            case 1: return cashedInputY * sensitivity;
        }

        return 0;
    }
}
