using UnityEngine;

#if UNITY_EDITOR
[ExecuteAlways]
[RequireComponent(typeof(SphereCollider))]
public class CollectiblePoint : MonoBehaviour
{
    private SphereCollider _collider;
    private bool _isActivated = false;
    private Transform _startPoint;
    private Transform _finishPoint;

    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.enabled = false;
    }

    public void Activate()
    {
        _isActivated = true;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _isActivated = false;
        _collider.enabled = false;
    }

    public void SetPoints(Transform startPoint, Transform finishPoint)
    {
        _startPoint = startPoint;
        _finishPoint = finishPoint;
    }

    public Object InstantiateCollectible(Collectible collectible, float distanceFromGround)
    {
        Vector3 position = transform.position + new Vector3(0, distanceFromGround, 0);
        GameObject instantiatedCollectible = Instantiate(collectible.gameObject, position, collectible.gameObject.transform.rotation, transform);
        Vector3 rotation = new Vector3(instantiatedCollectible.transform.eulerAngles.x, 0, instantiatedCollectible.transform.eulerAngles.z);
        instantiatedCollectible.transform.LookAt(instantiatedCollectible.transform.position + CollectibleLookAtVector());
        rotation.y = instantiatedCollectible.transform.eulerAngles.y;
        instantiatedCollectible.transform.rotation = Quaternion.Euler(rotation);

        return instantiatedCollectible;
    }

    private Vector3 CollectibleLookAtVector()
    {
        return (_finishPoint.position - _startPoint.position).normalized;
    }

    private void OnDrawGizmos()
    {
        if (_isActivated)
        {
            Gizmos.color = Color.cyan;
            Gizmos.color = new Color(0, 1, 1, 0.3f);
            Gizmos.DrawSphere(transform.position, _collider.radius);
        }
    }
}
#endif
