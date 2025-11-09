using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public static Action OnSceneLoadedCustom;
    public enum SceneEnum
    {
        CharacterSelectScene,
        LobbyScene,
        TestScene
    }
    
    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Load(SceneEnum sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Single);
        Cursor.visible = false;
    }
    
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        Cursor.visible = false;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnSceneLoadedCustom?.Invoke();
    }
}
