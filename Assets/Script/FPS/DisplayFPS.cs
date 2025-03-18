using TMPro;
using UnityEngine;

public class DisplayFPS : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TEXT_FPS;
    float pollingTime = 1f;
    float time;
    int frameCount;

    private void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            TEXT_FPS.text = "FPS : " + frameRate.ToString();
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
