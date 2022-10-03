using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarItem : Item
{
    protected string NameInPlayerPrefs;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(NameInPlayerPrefs) == false)
        {
            PlayerPrefs.SetInt(NameInPlayerPrefs, 0);
            PlayerPrefs.Save();
        }
    }

    public virtual void Init()
    {
        if (PlayerPrefs.HasKey(NameInPlayerPrefs) == false)
        {
            PlayerPrefs.SetInt(NameInPlayerPrefs, 0);
            PlayerPrefs.Save();
        }
    }

    public override void Buy()
    {
        PlayerPrefs.SetInt(NameInPlayerPrefs, 1);
        PlayerPrefs.Save();
    }

    public void Select()
    {
        PlayerPrefs.SetString(PlayerPrefsNames.Keys.Avatar, NameInPlayerPrefs);
    }

    public bool IsSelected()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNames.Keys.Avatar))
        {
            return PlayerPrefs.GetString(PlayerPrefsNames.Keys.Avatar) == NameInPlayerPrefs;
        }

        return false;
    }

    public override bool IsBought()
    {
        return PlayerPrefs.GetInt(NameInPlayerPrefs) == 1;
    }
}
