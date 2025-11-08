using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum Scene
    {
        CharacterSelectScene,
        LobbyScene,
        TestScene
    }

    public static void Load(Scene sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Single);
    }
}
