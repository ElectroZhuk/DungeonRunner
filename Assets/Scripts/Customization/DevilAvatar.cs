using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAvatar : Avatar
{
    public override string GetSavedName()
    {
        return PlayerPrefsNames.Avatars.Devil;
    }
}
