using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

/*
   File: SceneTransitions.cs
   Last Modified: February 25, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Holds methods that transition to different Scenes.
/// </summary>
public class SceneTransitions : MonoBehaviour
{
    /// <summary>
    /// The CanvasGroup alpha to fade to.
    /// </summary>
    [SerializeField] private float toFadeAlpha = 1f;

    /// <summary>
    /// The CanvasGroup alpha to fade from.
    /// </summary>

    [SerializeField] private float fromFadeAlpha = 0f;

    /// <summary>
    /// The time it should take to fade the CanvasGroup alpha.
    /// </summary>

    [SerializeField] private float fadeTime = 1f;

    /// <summary>
    /// Start is called before the first frame update. Checks for SaveSystem file before continuing Scene.
    /// </summary>
    public void Initialize()
    {
        SaveSystem.CheckForSaveSystem();
    }

    /// <summary>
    /// When quitting application forcefully, save data first.
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveSystem.SetBool(name: "StageFinish", val: false);
        Debug.Log(message: "Exiting Game!");
        SaveSystem.SetBool(name: "FirstLaunch", val: false);
        SaveSystem.SaveToDisk();
    }

    /// <summary>
    /// Fades CanvasGroup alpha to a value over a certain amount of time.
    /// </summary>
    private IEnumerator FadeOut()
    {
        GameObject backgroundNoiseGameObject = FindAndInit.InitializeGameObject( gameObjectName: "BackgroundNoise", scriptName: "SceneTransitions.cs" );
        AudioSource audioSourceBackground = backgroundNoiseGameObject.GetComponent< AudioSource >( );
        GameObject menuHandlerGameObject = FindAndInit.InitializeGameObject( gameObjectName: "MainMenuHandler", scriptName: "SceneTransitions.cs" );
        AudioSource audioSourceMenu = menuHandlerGameObject.GetComponent< AudioSource >( );
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            this.gameObject.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(a: fromFadeAlpha, b: toFadeAlpha, t: elapsedTime / fadeTime);
            audioSourceBackground.volume = Mathf.Lerp(a: toFadeAlpha, b: fromFadeAlpha, t: elapsedTime / fadeTime);
            audioSourceMenu.volume = Mathf.Lerp(a: toFadeAlpha, b: fromFadeAlpha, t: elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        this.gameObject.GetComponent<CanvasGroup>().alpha = toFadeAlpha;
        audioSourceBackground.volume = fromFadeAlpha;
        audioSourceMenu.volume = fromFadeAlpha;
        ExitGame( );
    }

    /// <summary>
    /// Checks to see if a Scene exists before continuing. Exits application if null.
    /// </summary>
    /// <param name="sceneName"></param>
    private static void CheckIfSceneExists(string sceneName)
    {
        bool exists = false;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(buildIndex: i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
            {
                exists = true;
            }
        }

        if (exists == false)
        {
            Debug.LogWarning(message: $"sceneName Scene does not exist. Exiting application!");
            SaveSystem.SaveToDisk();
            Application.Quit();
        }
    }

    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public static void MainMenuScene()
    {
        if( ProgressBar.Instance != null )
            Destroy( obj: ProgressBar.Instance );
        CheckIfSceneExists(sceneName: "Main");
        SceneManager.LoadScene(sceneName: "Main" );
    }

    /// <summary>
    /// The method that transitions the Scene to EndGame.
    /// </summary>
    public static void EndGameScene(bool won)
    {
        if( ProgressBar.Instance != null )
            Destroy( obj: ProgressBar.Instance );
        if (won == true)
        {
            SaveSystem.SetBool(name: "Won", val: true);
        }
        else
        {
            SaveSystem.SetBool(name: "Won", val: false);
        }
        CheckIfSceneExists(sceneName: "EndGame");
        SceneManager.LoadScene(sceneName: "EndGame" );
    }

    /// <summary>
    /// The method that transitions the Scene to Credits manually via Buttons.
    /// </summary>
    public static void CreditsScene()
    {
        if( ProgressBar.Instance != null )
            Destroy( obj: ProgressBar.Instance );
        CheckIfSceneExists(sceneName: "Credits");
        SceneManager.LoadScene(sceneName: "Credits" );
    }

    /// <summary>
    /// The method that transitions the Scene to Credits at the end of a Game.
    /// </summary>
    public static void EndGameCreditsScene()
    {
        if( ProgressBar.Instance != null )
            Destroy( obj: ProgressBar.Instance );
        CheckIfSceneExists(sceneName: "Credits");
        SceneManager.LoadScene(sceneName: "Credits" );
    }

    /// <summary>
    /// The method that transitions the Scene to the main game.
    /// </summary>
    public static void Play1Scene()
    {
        CheckIfSceneExists(sceneName: "Play1");
        SceneManager.LoadScene(sceneName: "Play1" );
    }

    /// <summary>
    /// The method that transitions the Scene to settings.
    /// </summary>
    public static void SettingsScene()
    {
        CheckIfSceneExists(sceneName: "Settings");
        SceneManager.LoadScene(sceneName: "Settings" );
    }

    /// <summary>
    /// The method that exits the game and quits the application.
    /// </summary>
    public static void ExitGame()
    {
        SaveSystem.SetBool(name: "StageFinish", val: false);
        Debug.Log(message: "Exiting Game!");
        SaveSystem.SetBool(name: "FirstLaunch", val: false);
        SaveSystem.SaveToDisk();
        Application.Quit();
    }

    /// <summary>
    /// Fades out Scene when selecting the Exit Button.
    /// </summary>
    public void FadeOutAndExitGame()
    {
        SaveSystem.SetBool(name: "StageFinish", val: false);
        StartCoroutine(routine: FadeOut());
        SaveSystem.SetBool(name: "FirstLaunch", val: false);
        SaveSystem.SaveToDisk();
    }
}
