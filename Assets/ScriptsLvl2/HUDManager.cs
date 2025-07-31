using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text powerUpText;
    public Text scoreText;

    private float powerUpTimeLeft = 0f;
    private int score = 0;

    void Update()
    {
       
        if (powerUpTimeLeft > 0f)
        {
            powerUpTimeLeft -= Time.deltaTime;
            powerUpText.text = $"Power-Up: {powerUpTimeLeft:F1}s";
        }
        else
        {
            powerUpText.text = "";
        }

        
        score += Mathf.FloorToInt(Time.deltaTime * 10); 
        scoreText.text = $"Score: {score}";
    }

    public void ActivatePowerUpTimer(float duration)
    {
        powerUpTimeLeft = duration;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
