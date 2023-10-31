using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Morrison,Brooke 
/// 10/31/23
/// This script controls the main game scene and game over screen loading when the appropriate buttons are pressed
/// </summary>
public class gameOver : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }


}
