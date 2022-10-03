using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTinyAvatarItem : AvatarItem
{
    public override void Init()
    {
        NameInPlayerPrefs = PlayerPrefsNames.Avatars.FemaleTiny;
        base.Init();
    }
}
