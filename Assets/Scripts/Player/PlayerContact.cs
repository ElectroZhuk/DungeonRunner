using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerContact : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private bool _canTrapped;

    public UnityAction Trapped;

    private void Awake()
    {
        _canTrapped = true;
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTrapped()
    {
        _canTrapped = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (_canTrapped == false)
            return;

        if (hit.gameObject.TryGetComponent<TrapFrontSide>(out TrapFrontSide trap))
        {
            if (Vector3.Dot(hit.normal, _playerMovement.MovingVector) < -0.1f)
            {
                Trapped?.Invoke();
                OnTrapped();
            }
        }
        else if (hit.gameObject.TryGetComponent<TrapContact>(out TrapContact trapContact))
        {
            Trapped?.Invoke();
            OnTrapped();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canTrapped == false)
            return;

        if (other.gameObject.TryGetComponent<TrapContact>(out TrapContact trapContact))
        {
            Trapped?.Invoke();
            OnTrapped();
        }
    }
}
