using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FpsCounter : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;

    private float frameCount;

    private float timer;
    
    private float updateInterval = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            textDisplay.text = frameCount/updateInterval + " FPS";
            timer = 0;
            frameCount = 0;
        }
    }
}
