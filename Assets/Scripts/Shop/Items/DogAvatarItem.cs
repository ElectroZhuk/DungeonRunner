using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAvatarItem : AvatarItem
{
    public override void Init()
    {
        NameInPlayerPrefs = PlayerPrefsNames.Avatars.Dog;
        base.Init();
    }
}
