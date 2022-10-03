using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleTinyAvatarItem : AvatarItem
{
    public override void Init()
    {
        NameInPlayerPrefs = PlayerPrefsNames.Avatars.MaleTiny;
        base.Init();
    }
}
