using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTinyAvatar : Avatar
{
    public override string GetSavedName()
    {
        return PlayerPrefsNames.Avatars.FemaleTiny;
    }
}
