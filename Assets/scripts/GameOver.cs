using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject winScreen; // Reference to the win screen UI

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the Player collided
        {
            Time.timeScale = 0f; // Pause the game
            winScreen.SetActive(true); // Display the win screen
        }
    }

    // Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    // Method to quit the game
    public void QuitGame()
    {
        Time.timeScale = 1f; // Unpause in case quitting during pause
        Application.Quit(); // Quit the application
        Debug.Log("Game Quit"); // This will show in the console during testing
    }
}