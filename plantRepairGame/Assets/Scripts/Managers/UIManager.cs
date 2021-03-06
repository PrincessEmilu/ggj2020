﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Texture titleScreen;

    public Canvas pauseMenu;

    private GameState currentState;
    private GUIStyle defaultStyle;
    private GUIStyle scoreStyle;
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

        scoreStyle = new GUIStyle();
        scoreStyle.fontSize = 48;
        scoreStyle.alignment = TextAnchor.MiddleLeft;
        scoreStyle.normal.textColor = Color.white;


       
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
                
                GUI.EndClip();
                break;

            case GameState.Playing:
                GUI.Label(new Rect(0, Screen.height - Screen.height /3, Screen.width / 3, Screen.height / 3), "Score: " + score + "\nLives: " + lives, scoreStyle);
                break;

            case GameState.Paused:
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Paused!", defaultStyle);
                break;

            case GameState.GameOver:
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Game Over!\nPress SPACE to restart", defaultStyle);
                break;
        }
    }
}
