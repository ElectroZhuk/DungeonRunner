using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAvatarItem : AvatarItem
{
    public override void Init()
    {
        NameInPlayerPrefs = PlayerPrefsNames.Avatars.Devil;
        base.Init();
    }
}
