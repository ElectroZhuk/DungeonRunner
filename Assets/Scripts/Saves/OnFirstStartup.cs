using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFirstStartup : MonoBehaviour
{
    [SerializeField] private bool _needResetProgress;

    private string _defaultAvatarName = PlayerPrefsNames.Avatars.Human;

    private void Awake()
    {
        if (_needResetProgress)
        {
            PlayerPrefs.DeleteAll();
            //PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Money, 500);
        }

        if (PlayerPrefs.HasKey(_defaultAvatarName) == false)
        {
            PlayerPrefs.SetInt(_defaultAvatarName, 1);
            PlayerPrefs.SetString(PlayerPrefsNames.Keys.Avatar, _defaultAvatarName);
            PlayerPrefs.Save();
        }
    }
}
