using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private const string DEFAULT_PLAYER_RESOURCES_PATH = "GameplayUtils/PlayerController";

    private static PlayerController mainPlayer;

    private PlayerInput inputHandler;

    public static PlayerController MainPlayer
    {
        get
        {
            if (mainPlayer == null)
            {
                mainPlayer = SpawnPlayer();
                DontDestroyOnLoad(mainPlayer);
            }

            return mainPlayer;
        }
    }

    private static PlayerController SpawnPlayer()
    {
        //PlayeConroller cargado como PlayerController y lo instancio
        PlayerController prefab = Resources.Load<PlayerController>(DEFAULT_PLAYER_RESOURCES_PATH);
        if (prefab == null)
        {
            throw new NullReferenceException(
                message: $"There is no default player prefab at path {DEFAULT_PLAYER_RESOURCES_PATH}");
        }
        return Instantiate<PlayerController>(prefab);
    }


    private void Awake()
    {
        if (mainPlayer == null)
        {
            mainPlayer = this;
            DontDestroyOnLoad(this);
        }
        else if (mainPlayer != this)
        {
            Destroy(gameObject);
        }

        inputHandler = GetComponent<PlayerInput>();



    }

    //dejar de controlar el personaje y empezar a controlar el carro y cosas como emparentar el personaje al carro
    public void ControlActor(Actor controlledActor)
    {
        controlledActor.BindInputActions(inputHandler);
    }
}
