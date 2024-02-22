using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   File: BackgroundMove.cs
   Description: Represents the class that moves the backdrop of the game.
   Last Modified: February 22, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Class responsible for moving the background based on player input.
/// This allows for dynamic background movement to simulate player interaction or environmental changes.
/// </summary>
public class BackgroundMove : MonoBehaviour
{
    /// <summary>
    /// Defines the speed at which the background will move.
    /// This speed affects how quickly the background transitions during automatic movement.
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// Controls the pause state of the background movement.
    /// When set to true, background movement is halted. This is useful for interactions or paused game states.
    /// </summary>
    public static bool paused;

    /// <summary>
    /// Indicates whether the player is currently dragging the background with the mouse.
    /// This flag is used to enable manual movement of the background via mouse drag.
    /// </summary>
    private bool isDragging = false;

    /// <summary>
    /// Stores the starting Y position of the mouse when the drag begins.
    /// This is used to calculate the movement delta during the drag operation.
    /// </summary>
    private float startMouseY;

    /// <summary>
    /// Stores the Y position of the background object when the drag begins.
    /// This is used in conjunction with <see cref="startMouseY"/> to move the background vertically.
    /// </summary>
    private float startObjectY;

    /// <summary>
    /// The amount of time to wait before moving background.
    /// </summary>
    [SerializeField] private float waitTime = 3f;

    /// <summary>
    /// Controls when to move background.
    /// </summary>
    private bool doneWaiting = false;

    /// <summary>
    /// List of blocks.
    /// </summary>
    /// <typeparam name="BlockMouse">a gameobject block of mouse down.</typeparam>
    /// <returns></returns>
     [SerializeField] private List<BlockMouse> blockMouse = new List<BlockMouse>();

    /// <summary>
    /// Start is called before the first frame update.  Starts the waiting to move IEnumerator.
    /// </summary>
     void Start( )
     {
        StartCoroutine( routine: WaitToMove( ) );
     }

    /// <summary>
    /// Handles player input for dragging the background and updates the pause state.
    /// On mouse down, checks if the background was clicked and initiates dragging if so.
    /// On mouse up, stops dragging and resumes automatic movement if applicable.
    /// </summary>
    void Update()
    {
        if( doneWaiting )
        {
            bool insideBlockingArea = false;
            foreach( BlockMouse b in blockMouse )
            {
                if( b.clickedOutsideArea )
                {
                    insideBlockingArea = true;
                    break;
                }
            }

            if( !insideBlockingArea )
            {
                HandleDrag( );
            }

            if (Input.GetMouseButtonUp(button: 0))
            {
                paused = false;
                isDragging = false;
            }            
        }
    }

    /// <summary>
    /// Waits a certain amount of time before allowing background to move.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitToMove( )
    {
        gameObject.GetComponent< CanvasGroup >( ).alpha = 0f;
        float elapsedTime = 0f;
        while ( elapsedTime < waitTime )
        {
            gameObject.GetComponent< CanvasGroup >( ).alpha = Mathf.Lerp( a: 0f, b: 1f, t: elapsedTime / waitTime );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent< CanvasGroup >( ).alpha = 1f;
        yield return new WaitForSeconds( seconds: waitTime );
        doneWaiting = true;
    }

    /// <summary>
    /// Handles Player drag of background.
    /// </summary>
    private void HandleDrag()
    {
        // Calculate the distance from the camera to the GameObject along the Z axis
        float cameraToObjectDistance = Camera.main.transform.position.z - transform.position.z;

            if (Input.GetMouseButtonDown(button: 0) )
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -cameraToObjectDistance; // Set the Z distance
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(position: mousePos);
                RaycastHit2D hit = Physics2D.Raycast(origin: mouseWorldPos, direction: Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                {
                    paused = true;
                    isDragging = true;
                    startMouseY = mouseWorldPos.y;
                    startObjectY = transform.position.y;
                }
            }

            if (isDragging )
            {
                Vector3 currentMousePos = Input.mousePosition;
                currentMousePos.z = -cameraToObjectDistance; // Adjust Z distance for current mouse position
                Vector2 currentMouseWorldPos = Camera.main.ScreenToWorldPoint(position: currentMousePos);
                float deltaY = currentMouseWorldPos.y - startMouseY;
                transform.position = new Vector3(x: transform.position.x, y: startObjectY + deltaY, z: transform.position.z);
            }
    }

    /// <summary>
    /// Regularly updates the position of the background to simulate movement.
    /// This function is called every fixed framerate frame. If not paused or dragging, it moves the background towards a target position.
    /// </summary>
    void FixedUpdate()
    {
        if (doneWaiting && !paused && !isDragging)
        {
            MoveBackground();
        }
    }

    /// <summary>
    /// Moves the background towards a specified target position at the defined speed.
    /// This method calculates the new position of the background for automatic movement.
    /// </summary>
    private void MoveBackground()
    {
        Vector3 target = new Vector3(x: transform.position.x, y: transform.position.y - 5000f, z: transform.position.z);
        transform.position = Vector3.MoveTowards(current: transform.position, target, maxDistanceDelta: speed * Time.fixedDeltaTime);
    }
}
