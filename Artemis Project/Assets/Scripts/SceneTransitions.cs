using UnityEngine.SceneManagement;

/*
   File: SceneTransitions.cs
   Description: Script to handle scene transitions.
   Last Modified: January 27, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Represents the class that holds a Scene object.
/// </summary>
public class SceneTransitions
{   
    /// <summary>
    /// Represents a Scene object.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// The name of the Scene object.
        /// </summary>
        private string nameOfScene;

        /// <summary>
        /// The constructor for a Scene object.
        /// </summary>
        /// <param name="nameOfScene">The name of the Scene object.</param>
        public Scene( string nameOfScene )
        {
            this.nameOfScene = nameOfScene;
        }

        /// <summary>
        /// Changes the Scene to the name of the Scene object.
        /// </summary>
        public void ChangeScene( )
        {
            SceneManager.LoadScene( sceneName: this.nameOfScene );
        }
    }
}
