using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scene
{
    public Scenes SceneName;
    public int BuildIndex;
}

public enum Scenes
{
    MainMenu,

    TestEnvironment,
    Level1,
}
