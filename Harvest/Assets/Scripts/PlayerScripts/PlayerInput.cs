using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region PROPERTIES

    #region Delegates
    #endregion

    TileManager tileManager;

    [Header("Player GameObject")]
    GameObject player;

    PlayerMovement playerMovement;

    // Player Gameplay
    PlayerInteraction playerInteraction;


    // Moving
    Vector3 moveInput = Vector3.zero;

    #endregion


    #region SET-UP

    private void Start()
    {
        // Get TileManager
        tileManager = FindObjectOfType<TileManager>();

        // Get Player
        player = GameObject.Find("Player");
        if (!player) { Debug.Log("No Player Found!"); }

        playerMovement = player.GetComponent<PlayerMovement>();

        // Find other references
        playerInteraction = player.GetComponent<PlayerInteraction>();
    }

    private void Update()
    {
        ListenForInputs();

        // Execute Movement Inputs
        ExecuteInputs();
    }
    #endregion

    #region INPUTS
    private void ListenForInputs()
    {
        GetMoveInputs();

        GetInventoryInput();
        GetInteractionInputs();
    }

    private void GetMoveInputs()
    {

        // Reset Movement Vector
        moveInput = Vector3.zero;

        // Get the WASD inputs
        float ws_input = 0;
        float ad_input = 0;

        #region Keypress detection
        // Check W / Up
        if (Input.GetKey(KeyCode.W))
        { ws_input += 1; }

        // Check S / Down
        if (Input.GetKey(KeyCode.S))
        { ws_input += -1; }

        // Check A / Left
        if (Input.GetKey(KeyCode.A))
        { ad_input += -1; }

        // Check D / Right
        if (Input.GetKey(KeyCode.D))
        { ad_input += 1; }
        #endregion

        // Add the input values to the Movement Vector
        moveInput = new Vector3(ad_input, 0f, ws_input);

        // Normalize the Vector
        moveInput = moveInput.normalized;

    }

    private void GetInteractionInputs()
    {
        // Detect Keypress
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInteraction.InteractionButtonPressed(
                                    tileManager.GetTilePlayerIsOn(player.transform.position));
                                    // Above gets the tile

        }
    }

    private void GetInventoryInput()
    {
        // Detect Keypress
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Change Inventory slot
            playerInteraction.ChangeSlot();
        }
    }
    #endregion

    #region EXECUTION
    private void ExecuteInputs()
    {
        SendMoveInputs(moveInput);
    }

    private void SendMoveInputs(Vector3 moveVector)
    {
        if (moveVector == Vector3.zero) /* There was no movement */ { return; }

        if (playerMovement == null) /* There is no Player */ { return; }

        // Send Inputs
        playerMovement.MovePlayer(moveInput);

        //Debug.Log("MoveInput: " + moveInput);

    }

    #endregion
}
