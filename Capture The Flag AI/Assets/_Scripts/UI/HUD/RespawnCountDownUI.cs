using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RespawnCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
    private int countDown;

    private void Awake()
    {
        text.text = "";
    }

    public void RespawnCountDown(int time)
    {
        countDown = time;
        StartCoroutine(TickRespawnTimer());
    }
    
    IEnumerator TickRespawnTimer()
    {
        for (int i = countDown; i > 0; i--)
        {
            text.text = $"Respawn in {i}";
            yield return waitForSeconds;
        }
        text.text = "";
    }
}