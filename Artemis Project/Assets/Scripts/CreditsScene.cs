using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

/*
   File: CreditsScene.cs
   Description: Script to handle the Credits Scene.
   Last Modified: February 4, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the Credits Scene.
/// </summary>
public class CreditsScene : MonoBehaviour
{
    /// <summary>
    /// A list of all sources.
    /// </summary>
    /// <typeparam name="string">A source in the List.</typeparam>
    private List< string > sourcesList = new List< string >( );

    /// <summary>
    /// The text on the UI of the sources.
    /// </summary>
    private TextMeshProUGUI sourcesText;

    /// <summary>
    /// Start is called before the first frame update. Initializes
    /// TextMeshProUGUI component, loads the sources to the sourcesList, and displays
    /// sources to the UI.
    /// </summary>
    void Start( )
    {
        sourcesText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "SourcesText", sceneName: "CreditsScene.cs" );
        LoadCredits( );
        DisplaySources( );
    }

    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public void Back( )
    {
        new SceneTransitions.Scene( nameOfScene: "Main" ).ChangeScene( );
    }

    /// <summary>
    /// Finds the path to each .csv and then adds a source to the sourcesList for each.
    /// </summary>
    private void LoadCredits( )
    {
        string basePath;
        if( Application.isEditor )
        {
            basePath = Path.Combine( Application.dataPath, "Sources" );
        }
        else
        {
            basePath = Path.Combine( Application.streamingAssetsPath, "Sources" );
        }
        ReadCSVAndStore( csvPath: Path.Combine( basePath, "sources.csv" ) );
    }

    /// <summary>
    /// Reads a .csv and stores it into a source for the sourcesList.
    /// </summary>
    /// <param name="csvPath">Path to where .csv files are located in game directory.</param>
    private void ReadCSVAndStore( string csvPath )
    {
        //initialize reader
        StreamReader reader = new StreamReader( path: csvPath );

        string lineRead;
        
        //loop through sources and store in list.
        while( ( lineRead = reader.ReadLine( ) ) != "//.end.//" )
        {
            sourcesList.Add( item: lineRead );
            sourcesList.Add( item: "\n" );
        }
    }

    /// <summary>
    /// Displays the sources in the sourcesList to the UI.
    /// </summary>
    private void DisplaySources( )
    {
        foreach( string source in sourcesList )
        {
            sourcesText.text += source;
        }
    }
}
