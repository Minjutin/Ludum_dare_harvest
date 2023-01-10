using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region PROPERTIES

    #region Delegates
    #endregion

    TileManager tileManager;

    [Header("Player GameObject")]
    [SerializeField]public GameObject player;
    int playerInt;

    [HideInInspector]
    public PlayerMovement playerMovement;

    //Animator controller
    Animator animator;

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
        if (!player) { Debug.Log("No Player Found!"); }

        playerMovement = player.GetComponent<PlayerMovement>();

        // Find other references
        playerInteraction = player.GetComponent<PlayerInteraction>();

        //Find animator
        animator = player.transform.Find("Graphics").transform.Find("SpriteHolder").GetComponent<Animator>();

        //Find player
        playerInt = this.GetComponent<PlayerStats>().playerNumber;
        Debug.Log(playerInt);
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

        if(playerInt == 1)
        {
            // Check W / Up
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Move", true);
                animator.SetBool("Up", true); ws_input += 1;
            }

            // Check S / Down
            if (Input.GetKey(KeyCode.S))
            { animator.SetBool("Move", true); animator.SetBool("Up", false); ws_input += -1; }

            // Check A / Left
            if (Input.GetKey(KeyCode.A))
            { animator.SetBool("Move", true); ad_input += -1; }

            // Check D / Right
            if (Input.GetKey(KeyCode.D))
            { animator.SetBool("Move", true); ad_input += 1; }

            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)
                && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Up", false); animator.SetBool("Move", false);
            }
        }

        if(playerInt == 2)
        {
            // Check W / Up
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("Move", true);
                animator.SetBool("Up", true); ws_input += 1;
            }

            // Check S / Down
            if (Input.GetKey(KeyCode.DownArrow))
            { animator.SetBool("Move", true); animator.SetBool("Up", false); ws_input += -1; }

            // Check A / Left
            if (Input.GetKey(KeyCode.LeftArrow))
            { animator.SetBool("Move", true); ad_input += -1; }

            // Check D / Right
            if (Input.GetKey(KeyCode.RightArrow))
            { animator.SetBool("Move", true); ad_input += 1; }

            if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)
                && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)){
                animator.SetBool("Up", false); animator.SetBool("Move", false);
            }

        }

        // Add the input values to the Movement Vector
        moveInput = new Vector3(ad_input, 0f, ws_input);

        // Normalize the Vector
        moveInput = moveInput.normalized;

    }

    private void GetInteractionInputs()
    {
        if(playerInt == 1)
        {
            // Detect Keypress
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInteraction.InteractionButtonPressed(
                                        tileManager.GetTileCreatureIsOn(player.transform.position));
                // Above gets the tile
            }
        }

        if(playerInt == 2)
        {
            // Detect Keypress
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                playerInteraction.InteractionButtonPressed(
                                        tileManager.GetTileCreatureIsOn(player.transform.position));
                // Above gets the tile

            }
        }

    }

    private void GetInventoryInput()
    {
        if(playerInt == 1)
        {
            // Detect Keypress
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Change Inventory slot
                playerInteraction.ChangeSlot();
            }
        }

        if(playerInt == 2)
        {
            // Detect Keypress
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                // Change Inventory slot
                playerInteraction.ChangeSlot();
            }
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
