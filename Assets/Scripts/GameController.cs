using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public GameObject[] balls;
    private int score = 0;
    private UIDocument uiDocument;
    private Label scoreLabel;
    private Label gameOverLabel;

    public static GameController instance;
    private void Awake()
    {
        instance = this;
        uiDocument = FindObjectOfType<UIDocument>();
        if (uiDocument != null)
        {
            scoreLabel = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
            gameOverLabel = uiDocument.rootVisualElement.Q<Label>("GameOverLabel");
        }
    }
    
    public void RegisterCollision(GameObject a, GameObject b)
    {
        Vector3 spawnPosition = (a.transform.position + b.transform.position) / 2;
        int size = a.GetComponent<FallingObject>().size;
        // Muutettu score privaatiksi ja käytetään apufunktiota joka samalla päivittää UI:n
        //score += (size * 100) * 2;
        AddScore((size * 100) * 2);
        //Debug.Log(score);
        Instantiate(instance.balls[size], spawnPosition, Quaternion.identity, this.gameObject.transform);
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = "Score: " + score;
        }
    }
    public void GameOver()
    {
        gameOverLabel.style.display = DisplayStyle.Flex;
        Debug.Log("GAME OVER!!!");
    }
}
