using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;

[EditorTool("Custom snap move", typeof(CustomSnap))]
public class CustomSnappingTool : EditorTool
{
    private Transform _oldTarget;
    private CustomSnapEndPoint[] _worldPoints;
    private CustomSnapStartPoint _ownPoint;

    public float _snapDistance = 0.5f;
    public Texture2D ToolIcon;

    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = ToolIcon,
                text = "Custom snap move tool",
                tooltip = "Move and snap"
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((CustomSnap)target).transform;

        if (targetTransform != _oldTarget)
        {
            _worldPoints = FindObjectsOfType<CustomSnapEndPoint>();
            _ownPoint = ((CustomSnap)target).GetComponentInChildren<CustomSnapStartPoint>();
            _oldTarget = targetTransform;
        }
        
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);
            
        if (newPosition == targetTransform.position)
        {
            MoveWithSnapping(targetTransform, newPosition);
        }
        else
        {
            Undo.RecordObject(targetTransform, "Move with custom snap tool");
            targetTransform.position = newPosition;
        }
    }

    private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
    {
        Vector3 bestPosition = newPosition;
        float closestDistance = float.PositiveInfinity;

        foreach (var point in _worldPoints)
        {
            if (point != targetTransform.GetComponentInChildren<CustomSnapEndPoint>())
            {
                Vector3 targetPosition = point.transform.position - (_ownPoint.transform.position - targetTransform.position);
                float distance = Vector3.Distance(targetPosition, targetTransform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPosition = targetPosition;
                }
            }
        }

        if (closestDistance <= _snapDistance)
        {
            Undo.RecordObject(targetTransform, "Move with custom snap tool");
            targetTransform.position = bestPosition;
        }
        else
        {
            targetTransform.position = newPosition;
        }
    }
}
