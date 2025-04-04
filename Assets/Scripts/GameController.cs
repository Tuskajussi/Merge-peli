using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public GameObject[] balls;
    private int score = 0;
    private UIDocument uiDocument;
    private Label scoreLabel;
    //private Label gameOverLabel;
    private Button newGameButton;
    private Button exitButton;
    private VisualElement gameOverScreen;

    public static GameController instance;
    private void Awake()
    {
        instance = this;
        uiDocument = FindObjectOfType<UIDocument>();
        Time.timeScale = 1; // muutetaan timeScale 1:ksi, jotta peli käynnistyy oikein uudelleenaloitettaessa
        if (uiDocument != null)
        {
            scoreLabel = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
            //gameOverLabel = uiDocument.rootVisualElement.Q<Label>("GameOverLabel");
            gameOverScreen = uiDocument.rootVisualElement.Q<VisualElement>("GameOverScreen");
            newGameButton = uiDocument.rootVisualElement.Q<Button>("NewGameButton");
            exitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");
            newGameButton.clicked += NewGame;
            exitButton.clicked += Exit;
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
        gameOverScreen.style.display = DisplayStyle.Flex;
        Debug.Log("GAME OVER!!!");
    }
    private void NewGame()
    {
        // get the current scene name 
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    private void Exit()
    {
        Application.Quit();
    }
}
