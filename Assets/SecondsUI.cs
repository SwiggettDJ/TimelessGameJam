using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SecondsUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.OnTimeUpgraded += PlayAnim;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayAnim()
    {
        GetComponent<ParticleSystem>().Play();
    }

    void OnDestroy()
    {
        TimeManager.OnTimeUpgraded -= PlayAnim;
    }
}
