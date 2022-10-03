using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleTinyAvatar : Avatar
{
    public override string GetSavedName()
    {
        return PlayerPrefsNames.Avatars.MaleTiny;
    }
}
