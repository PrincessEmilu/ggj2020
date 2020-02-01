using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Title,
    Playing,
    Paused,
    Options,
    GameOver
}

enum PlantType
{
    Staple
}

public class SceneManager : MonoBehaviour
{
    // Manager scripts
    private UIManager uiManager;
    private AudioManager audioManager;
    private ConveyorBelt conveyorBelt;

    private GameState currentState;

    private StapleMachine stapleMachine;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Playing;

        // Get managers
        uiManager = gameObject.GetComponent<UIManager>();
        audioManager = gameObject.GetComponent<AudioManager>();
        conveyorBelt = gameObject.GetComponent<ConveyorBelt>();

        conveyorBelt.Setup();

        // Get machines
        stapleMachine = FindObjectOfType<StapleMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Title:
                break;

            case GameState.Playing:
                UpdateConveyor();
                UseTools();
                break;

            case GameState.GameOver:
                break;
        }
    }

    private void UpdateConveyor()
    {
        conveyorBelt.SpawnPlants();
        conveyorBelt.MovePlants();
        conveyorBelt.RemovePlants();
    }

    private void UseTools()
    {
        if (Input.GetKeyDown("space"))
        {

        }
    }
}
