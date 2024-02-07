using UnityEngine;
using TMPro;
/*
   File: MenuScene.cs
   Description: Script to handle the Main Menu Scene.
   Last Modified: February 7, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Script to handle the Main Menu.
/// </summary>
public class MenuScene : MonoBehaviour
{
    /// <summary>
    /// The TextMeshProUGUI component that will hold the top player's score.
    /// </summary>
    private TextMeshProUGUI topPlayerScoreText;

    /// <summary>
    /// Initializes top player score to UI.
    /// </summary>    
    void Start( )
    {
        topPlayerScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "TopPlayerScore", sceneName: "MenuScene.cs" );
        if( PlayerPrefs.GetString( key: "FirstTime" ) != "yes" )
        {
            SaveSystem.SetString( name: "FirstTime", val: "yes" );
            SaveSystem.SaveToDisk( );
        }
        topPlayerScoreText.text = SaveSystem.GetInt( name: "TopPlayerScore" ).ToString( );
    }
}
