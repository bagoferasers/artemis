using UnityEngine;

/*
   File: BlockMouse.cs
   Description: Represents the class prevents a player from Mouse Down input on game object.
   Last Modified: February 22, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Prevents a Player from Mouse Down input on gameObject.
/// </summary>
public class BlockMouse : MonoBehaviour
{
    /// <summary>
    /// Checks whether Player has clicked inside gameObject or not.
    /// </summary>
    public bool clickedOutsideArea = false;

    /// <summary>
    /// Checks for Player Mouse down and changes clickedOutsideArea accordingly.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(button: 0)) // Check if left mouse button is clicked
        {
            Vector2 tapPos = Camera.main.ScreenToWorldPoint(position: Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(origin: tapPos, direction: Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log(message: "Clicked on blocking area. Input blocked.");
                clickedOutsideArea = true;
            }
        }

        if( Input.GetMouseButtonUp( button: 0 ) )
        {
            clickedOutsideArea = false;
        }
    }
}
