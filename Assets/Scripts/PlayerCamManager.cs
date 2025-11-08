using Cinemachine;
using UnityEngine;

public class PlayerCamManager : MonoBehaviour
{
    [SerializeField]private CinemachineFreeLook freeLookCam;
    private float targetFov;
    private float fovChangeSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        targetFov = freeLookCam.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        HandleSmoothFov();
    }

    private void HandleSmoothFov()
    {
        // Once our current fov is close to target we snap to it
        if (Mathf.Abs(freeLookCam.m_Lens.FieldOfView - targetFov) > 0.1f)
        {
            freeLookCam.m_Lens.FieldOfView = Mathf.Lerp(freeLookCam.m_Lens.FieldOfView, targetFov, fovChangeSpeed * Time.deltaTime);
        }
        else
        {
            freeLookCam.m_Lens.FieldOfView = targetFov;
        }
    }
    public void ChangeFov(float changeAmount)
    {
        //If we get a new call before we hit target, just jump to old target so we can make the new value our next target
        if (freeLookCam.m_Lens.FieldOfView != targetFov)
        {
            freeLookCam.m_Lens.FieldOfView = targetFov;
        }
        targetFov = freeLookCam.m_Lens.FieldOfView + changeAmount;
    }

    public void EnableFreeCam(bool value)
    {
        freeLookCam.enabled = value;
    }
}
