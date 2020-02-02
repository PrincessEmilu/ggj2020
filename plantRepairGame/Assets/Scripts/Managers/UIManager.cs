using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Texture titleScreen;

    public Canvas pauseMenu;

    private GameState currentState;
    private GUIStyle defaultStyle;
    private GUIContent titleContent;

    private int score;
    private int lives;

    public void Start()
    {
        currentState = GameState.Title;

        // Setup GUI style
        defaultStyle = new GUIStyle();
        defaultStyle.fontSize = 48;
        defaultStyle.alignment = TextAnchor.MiddleCenter;

        titleContent = new GUIContent("Press SPACE to play", titleScreen);
    }

    public void SetState(GameState gameState)
    {
        currentState = gameState;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void TogglePause()
    {
        if (currentState != GameState.Paused)
        {
            currentState = GameState.Paused;
            pauseMenu.enabled = true;
        }
        else
        {
            currentState = GameState.Playing;
        }
    }

    private void OnGUI()
    {
        switch (currentState)
        {
            case GameState.Title:
                GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), titleScreen, ScaleMode.StretchToFill);
                GUI.Label(new Rect(Screen.width / 2, Screen.height - Screen.height / 3, 200, 100), "Press SPACE to play", defaultStyle);
                GUI.EndClip();
                break;

            case GameState.Playing:
                GUI.Box(new Rect(0, 0, Screen.width / 3, Screen.height / 3), "Score: " + score + "\nLives: " + lives, defaultStyle);
                break;

            case GameState.Paused:

                break;

            case GameState.GameOver:
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Game Over!\nPress SPACE to restart", defaultStyle);
                break;
        }
    }
}
