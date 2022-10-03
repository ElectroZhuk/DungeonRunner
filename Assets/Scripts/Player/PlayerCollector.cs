using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerLevelCollectible), typeof(PlayerMoney))]
public class PlayerCollector : MonoBehaviour
{
    private PlayerLevelCollectible _level;
    private PlayerMoney _money;

    private void Awake()
    {
        _level = GetComponent<PlayerLevelCollectible>();
        _money = GetComponent<PlayerMoney>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Collectible>(out Collectible collectible))
        {
            if (collectible is LevelCollectible levelCollectible)
            {
                if (_level.CanAdd(levelCollectible) && levelCollectible.IsCollected == false)
                {
                    _level.Add(levelCollectible);
                    levelCollectible.Collect();
                }
            }
            else if (collectible is Money money)
            {
                if (_money.CanAdd(money) && money.IsCollected == false)
                {
                    _money.Add(money);
                    money.Collect();
                }
            }
        }
    }
}
