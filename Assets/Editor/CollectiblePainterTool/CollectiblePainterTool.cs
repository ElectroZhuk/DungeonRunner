using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;
using System.Linq;

[EditorTool("CollectiblePainterTool")]
public class CollectiblePainterTool : EditorTool
{
    [SerializeField] private GameObject _collectiblePrefab;

    private List<CollectiblePoint> _collectiblePoints;
    private Collectible _activeCollectible;

    public override void OnActivated()
    {
        _collectiblePoints = FindObjectsOfType<CollectiblePoint>(true).ToList<CollectiblePoint>();

        foreach (CollectiblePoint collectiblePoint in _collectiblePoints)
            collectiblePoint.Activate();

        CollectiblePainterToolWindow.ShowWindow();
        CollectiblePainterToolWindow.CollectibleSelected += ChangeActiveCollectible;
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Event currentEvent = Event.current;

        if (currentEvent.type != EventType.MouseDown && currentEvent.type != EventType.MouseDrag)
            return;

        if (currentEvent.button != 0)
            return;

        GUIUtility.hotControl = 0;
        Ray ray = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, (1 << 3)) == false)
            return;

        if (hitInfo.collider.TryGetComponent<CollectiblePoint>(out CollectiblePoint collectiblePoint) == false)
            return;
        
        if (currentEvent.control)
            RemoveCollectible(collectiblePoint);        
        else
            AddCollectible(collectiblePoint);
    }

    public override void OnWillBeDeactivated()
    {
        foreach (CollectiblePoint collectiblePoint in _collectiblePoints)
            collectiblePoint.Deactivate();

        CollectiblePainterToolWindow.CollectibleSelected -= ChangeActiveCollectible;
        CollectiblePainterToolWindow.CloseWindow();
    }

    private void AddCollectible(CollectiblePoint collectiblePoint)
    {
        if (collectiblePoint.GetComponentsInChildren<Collectible>().Length > 0)
            return;

        float distanceFromGround = CollectiblePainterToolWindow.GetDistanceFromGround();
        Undo.RegisterCreatedObjectUndo(collectiblePoint.InstantiateCollectible(_activeCollectible, distanceFromGround), "Spawn a collectible");
    }

    private void RemoveCollectible(CollectiblePoint collectiblePoint)
    {
        Collectible[] collectibles = collectiblePoint.GetComponentsInChildren<Collectible>();

        if (collectibles.Length == 0)
            return;

        foreach (Collectible collectible in collectibles)
            Undo.DestroyObjectImmediate(collectible.gameObject);
    }

    private void ChangeActiveCollectible(Collectible collectible)
    {
        _activeCollectible = collectible;
    }
}
