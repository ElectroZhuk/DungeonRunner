using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedLevelLoader : MonoBehaviour
{
    public void LoadSavedLevel()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNames.Keys.Level) == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Level, 1);
            PlayerPrefs.Save();
        }

        SceneTransition.SwitchToScene(PlayerPrefs.GetInt(PlayerPrefsNames.Keys.Level));
    }
}
