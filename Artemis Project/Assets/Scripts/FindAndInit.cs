using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
   File: FindAndInit.cs
   Last Modified: February 17, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Script to handle finding and initializing different GameObjects or their components.
/// </summary>
public class FindAndInit : MonoBehaviour
{
    /// <summary>
    /// Finds and initializes a GameObject and checks for null before continuing.
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject.</param>
    /// <param name="scriptName">The name of the Script GameObject is in.</param>
    /// <returns>The GameObject found and initialized.</returns>
    public static GameObject InitializeGameObject( string gameObjectName, string scriptName )
    {
        GameObject gameObject = GameObject.Find(name: gameObjectName);
        if ( gameObject == null )
        {
            Debug.LogWarning( message: $"{gameObjectName} variable in {scriptName} is null!" );
            SaveSystem.SaveToDisk( );
            Application.Quit( );
        }
        return gameObject;
    }

    /// <summary>
    /// Finds and initializes a GameObject and checks for null before continuing.
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject.</param>
    /// <returns>The GameObject found and initialized.</returns>
    public static TextMeshProUGUI InitializeTextMeshProUGUI( string gameObjectName, string scriptName )
    {
        GameObject gameObject = InitializeGameObject( gameObjectName: gameObjectName, scriptName: scriptName );
        return gameObject.GetComponent< TextMeshProUGUI >( );
    }

    /// <summary>
    /// Finds and initializes a Button and checks for null before continuing.
    /// </summary>
    /// <param name="gameObjectName">The name of the Button.</param>
    /// <returns>The Button found and initialized.</returns>
    public static Button InitializeButton( string gameObjectName, string scriptName )
    {
        GameObject gameObject = InitializeGameObject( gameObjectName: gameObjectName, scriptName: scriptName );
        return gameObject.GetComponent< Button >( );
    }

    /// <summary>
    /// Finds and initializes a GameObject and deactivates it.
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject to set as inactive.</param>
    /// <returns>The GameObject found and initialized.</returns>
    public static GameObject FindAndDeactivate( string gameObjectName, string scriptName )
    {
        GameObject gameObject = InitializeGameObject( gameObjectName: gameObjectName, scriptName: scriptName );
        gameObject.SetActive( value: false );
        return gameObject;
    }
}
