using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
   File: FindAndInit.cs
   Description: Script to handle finding and initializing different GameObjects or their components.
   Last Modified: February 4, 2024
   Last Modified By: Colby Bailey
*/

public class FindAndInit : MonoBehaviour
{
    /// <summary>
    /// Finds and initializes a GameObject and checks for null before continuing.
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject.</param>
    /// <returns>The GameObject found and initialized.</returns>
    public static GameObject InitializeGameObject( string gameObjectName, string sceneName )
    {
        GameObject gameObject = GameObject.Find(name: gameObjectName);
        if ( gameObject == null )
        {
            Debug.LogWarning( message: $"{gameObjectName} variable in {sceneName} is null!" );
            Application.Quit( );
        }
        return gameObject;
    }

    /// <summary>
    /// Finds and initializes a GameObject and checks for null before continuing.
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject.</param>
    /// <returns>The GameObject found and initialized.</returns>
    public static TextMeshProUGUI InitializeTextMeshProUGUI( string gameObjectName, string sceneName )
    {
        GameObject gameObject = InitializeGameObject( gameObjectName: gameObjectName, sceneName: sceneName );
        return gameObject.GetComponent< TextMeshProUGUI >( );
    }

    /// <summary>
    /// Finds and initializes a Button and checks for null before continuing.
    /// </summary>
    /// <param name="gameObjectName">The name of the Button.</param>
    /// <returns>The Button found and initialized.</returns>
    public static Button InitializeButton( string gameObjectName, string sceneName )
    {
        GameObject gameObject = InitializeGameObject( gameObjectName: gameObjectName, sceneName: sceneName );
        return gameObject.GetComponent< Button >( );
    }

    /// <summary>
    /// Finds and initializes a GameObject and deactivates it.
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject to set as inactive.</param>
    /// <returns>The GameObject found and initialized.</returns>
    public static GameObject FindAndDeactivate( string gameObjectName, string sceneName )
    {
        GameObject gameObject = InitializeGameObject( gameObjectName: gameObjectName, sceneName: sceneName );
        gameObject.SetActive( value: false );
        return gameObject;
    }
}
