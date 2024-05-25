using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    private PlayerControls.PlayerActions player;
    private PlayerMovement playerMovement;
    private PlayerActions playerActions;
    private PlayerLevelingUI playerLevelingUI;


    void Awake()
    {
        playerControls = new PlayerControls();
        player = playerControls.Player;

        playerMovement = GetComponent<PlayerMovement>();
        playerActions = GetComponent<PlayerActions>();
        playerLevelingUI = GetComponent<PlayerLevelingUI>();

        player.Dash.performed += ctx => playerMovement.Dash(player.Movement.ReadValue<Vector2>());
        player.Attack.performed += ctx => playerActions.Attack();
        player.OpenLevelingUI.performed += ctx => playerLevelingUI.OpenLevelingUI();

    }


    private void FixedUpdate()
    {
        playerMovement.ProcessMove(player.Movement.ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

}
