using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    [SerializeField] private Transform _customizationContainer;

    private void Awake()
    {
        EnableSelectedAvatar(GetSelectedOrDeafaultAvatar());
    }

    private Avatar GetSelectedOrDeafaultAvatar()
    {
        Avatar[] avatars = _customizationContainer.GetComponentsInChildren<Avatar>();

        if (PlayerPrefs.HasKey(PlayerPrefsNames.Keys.Avatar))
        {
            string avatarName = PlayerPrefs.GetString(PlayerPrefsNames.Keys.Avatar);

            foreach (Avatar avatar in avatars)
            {
                if (avatar.GetSavedName() == avatarName)
                    return avatar;
            }
        }
        else
        {
            PlayerPrefs.SetString(PlayerPrefsNames.Keys.Avatar, avatars[0].GetSavedName());
            PlayerPrefs.Save();
        }
        
        return avatars[0];
    }

    private void EnableSelectedAvatar(Avatar selectedAvatar)
    {
        foreach (Avatar avatar in _customizationContainer.GetComponentsInChildren<Avatar>())
        {
            avatar.gameObject.SetActive(avatar == selectedAvatar);
        }
    }
}
