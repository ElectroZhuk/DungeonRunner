using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAvatar : Avatar
{
    public override string GetSavedName()
    {
        return PlayerPrefsNames.Avatars.Human;
    }
}
