using UnityEngine;

public class DogAvatar : Avatar
{
    public override string GetSavedName()
    {
        return PlayerPrefsNames.Avatars.Dog;
    }
}
