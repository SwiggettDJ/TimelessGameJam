using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner : MonoBehaviour
{
   private void Awake()
   {
      if (TimeManager.instance == null)
      {
         GameObject timeManager = new GameObject("TimeManager");
         timeManager.AddComponent<TimeManager>();
         DontDestroyOnLoad(timeManager);
      }

      if (SceneLoader.instance == null)
      {
         GameObject sceneloader = new GameObject("SceneLoader");
         sceneloader.AddComponent<SceneLoader>();
         DontDestroyOnLoad(sceneloader);
      }
   }
}
