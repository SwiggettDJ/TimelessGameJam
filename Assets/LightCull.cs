using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCull : MonoBehaviour
{
    private GameObject player;
    private Light lightComponent;

    private float distance = 30f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lightComponent = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            lightComponent.enabled = true;
        }
        else
        {
            lightComponent.enabled = false;
        }
    }
}
