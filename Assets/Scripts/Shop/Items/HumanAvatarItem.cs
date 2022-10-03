using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAvatarItem : AvatarItem
{
    public override void Init()
    {
        NameInPlayerPrefs = PlayerPrefsNames.Avatars.Human;
        base.Init();
    }
}
