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
        TestScene,
        EndScreen
    }

    private void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        instance = this;
        //Cursor.visible = false;
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        TimeManager.OnAllUpgrades += WinGame;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        TimeManager.OnAllUpgrades -= WinGame;
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

    private void WinGame()
    {
        Load(SceneEnum.EndScreen);
    }
}
