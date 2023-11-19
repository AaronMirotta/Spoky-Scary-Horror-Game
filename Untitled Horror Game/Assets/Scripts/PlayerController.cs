using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //2d player movement
    //player can move up and down on any part of the floor
    //wasd movement and point & click movement
    
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get { return instance; }
    }
    
    private enum MovementMethod { Keyboard, PointClick };

    [SerializeField]
    private MovementMethod currentMovementMethod;

    [SerializeField]
    private float speed;

    public PlayerInputActions playerControls;

    private InputAction move;
    private InputAction interact;
    private InputAction inventory;

    private bool isGrounded;

    //ground detection
    [SerializeField]
    private Vector3 groundPos;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private float groundDist;
    [SerializeField]
    private LayerMask groundMask;

    //interaction
    [SerializeField]
    private Vector2 interactablePosOffset;

    [SerializeField]
    private GameObject interactIconPrefab;

    [SerializeField]
    private GameObject interactIcon;
    [SerializeField]
    private float interactRange;

    private Rigidbody2D rb;

    private Vector3 playerTargetPos;

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;

        inventory = playerControls.Player.Inventory;
        inventory.Enable();
        inventory.performed += OpenInventory;
    }
    private void OnDisable()
    {
        move.Disable();
        interact.Disable();
        inventory.Disable();
    }
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerTargetPos = transform.position;
    }
    private void Update()
    {
        isGrounded = GroundDetect();

        if (currentMovementMethod == MovementMethod.Keyboard) { KeyboardMovement(); }

        //
        InteractionIcon();

    }
    private void InteractionIcon()//shows icon on over the closest object to the player
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange, ~LayerMask.GetMask("Player"));

        Collider2D closest = GetClosestInteract(hits);
        
        if(closest == null) { return; }
        
        if(interactIcon == null)
        {
            if(closest.GetComponent<IInteractable>() != null)
            {
                interactIcon = Instantiate(interactIconPrefab, closest.transform);
            }
        }
        else
        {
            if(Vector2.Distance(transform.position, closest.ClosestPoint(transform.position)) <= interactRange)
            {
                if(closest.GetComponent<IInteractable>() == null)
                {
                    GameObject temp = interactIcon;
                    interactIcon = null;
                    Destroy(temp.gameObject);
                }
                else
                {
                    interactIcon.transform.parent = closest.transform;
                    interactIcon.transform.position = closest.transform.position;
                }
            }
            else
            {
                //player is too far from object to interact
                GameObject temp = interactIcon;
                interactIcon = null;
                Destroy(temp.gameObject);
            }
        }
    }
    private Collider2D GetClosestInteract(Collider2D[] objects)
    {
        Collider2D tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 curPos = transform.position;
        foreach(Collider2D t in objects)
        {
            float dist = Vector3.Distance(t.transform.position, curPos);
            if(dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    private void Interact(InputAction.CallbackContext context)
    {
        //interact with object parented to InteractIcon
        Transform parent = interactIcon.transform.parent;

        Debug.Log(parent.name);

        if(parent.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
    private void OpenInventory(InputAction.CallbackContext context)
    {
        if (UIManager.Instance.isInventoryOpen)
        {
            UIManager.Instance.CloseInventory();
        }
        else
        {
            UIManager.Instance.OpenInventory();
        }
    }
    private void KeyboardMovement()
    {
        float moveX = move.ReadValue<Vector2>().x;
        float moveY = move.ReadValue<Vector2>().y;



        if (isGrounded)
        {
            rb.velocity = new Vector2(moveX, moveY / 2) * speed;
        }
        else
        {
            if (moveY > 0)
            {
                moveY = 0;
            }
            rb.velocity = new Vector2(moveX, moveY / 2) * speed;
        }
    }

    private bool GroundDetect()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position + groundPos, groundRadius, Vector2.down, groundDist, groundMask);

        bool grounded;

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Floor")
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
        else
        {
            grounded = false;
        }

        return grounded;
    }

    private void OnLevelWasLoaded(int level)
    {
        playerTargetPos = transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + groundPos, groundRadius);

        //interaction radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}