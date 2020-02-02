﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    Playing,
    Paused,
    Options,
    GameOver
}

public enum PlantType
{
    Staple,
    Water
}

public class SceneManager : MonoBehaviour
{
    // Manager scripts
    private UIManager uiManager;
    private AudioManager audioManager;
    private ConveyorBelt conveyorBelt;

    // Gamestate variables
    private int lives;
    private int score;
    private GameState currentState;


    // Tools
    private StapleMachine stapleMachine;
    private WaterTool waterTool;

    // Start is called before the first frame update
    void Start()
    {
        // Game states
        currentState = GameState.Title;
        lives = 3;
        score = 0;

        // Get managers
        uiManager = gameObject.GetComponent<UIManager>();
        audioManager = gameObject.GetComponent<AudioManager>();
        conveyorBelt = gameObject.GetComponent<ConveyorBelt>();

        // Perform setups on managers
        conveyorBelt.Setup();
        UpdateUI();

        // Get machines
        stapleMachine = FindObjectOfType<StapleMachine>();
        waterTool = FindObjectOfType<WaterTool>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Title:
                if (Input.GetKeyDown("space"))
                {
                    currentState = GameState.Playing;
                    uiManager.SetState(currentState);
                }
                break;

            case GameState.Playing:

                UpdateConveyor();
                UseTools();
                UpdateUI();

                // Out of lives, game over
                if (lives <= 0)
                {
                    currentState = GameState.GameOver;
                    uiManager.SetState(currentState);
                }

                // Toggle pause menu
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = GameState.Paused;
                    uiManager.TogglePause();
                }
                break;

            case GameState.Paused:
                // I'm actually not sure what happens here because 
                break;

            case GameState.GameOver:
                if (Input.GetKeyDown("space"))
                {
                    ResetGame();
                }
                break;
        }
    }

    private void ResetGame()
    {
        conveyorBelt.RemoveAllPlants();

        score = 0;
        lives = 3;

        currentState = GameState.Title;
        uiManager.SetState(currentState);

        UpdateUI();
    }

    private void UpdateUI()
    {
        uiManager.SetScore(score);
        uiManager.SetLives(lives);
    }

    // Handles the plants and updates score/lives as appropriate
    private void UpdateConveyor()
    {
        conveyorBelt.SpawnPlants();
        conveyorBelt.MovePlants();
        conveyorBelt.RemovePlants();

        score += conveyorBelt.UpdateScore();
        lives -= conveyorBelt.UpdateLives();
    }

    // Controls the various tools
    private void UseTools()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stapleMachine.PressStaple();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            waterTool.SpawnWater();
        }
    }


}
