using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum Scene
    {
        CharacterSelectScene,
        LobbyScene,
        TestScene
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    public static void Load(Scene sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Single);
        Cursor.visible = false;
    }
}
