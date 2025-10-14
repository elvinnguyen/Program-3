using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // Static instance of the Game Manager, can be access from anywhere
    public int score = 0; // Player score
    bool isGameOver;

    void Awake()
    {
        // if it doesn't exist
        if (instance == null)
        {
            // Set the instance to the current object (this)
            instance = this;
        }
        // There can only be a single instance of the game manager
        else if (instance != this)
        {
            // Destroy the current object, so there is just one manager
            Destroy(gameObject);
        }
        // Don't destroy this object when loading scenes
        DontDestroyOnLoad(gameObject);
    }

    // Increase score
    public void IncreaseScore(int amount)
    {
        score += amount;
        print("New Score: " + score.ToString());
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        GameOverRoutine();
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        Time.timeScale = 0f;
        SceneManager.LoadScene("Game Over");
    }
}
