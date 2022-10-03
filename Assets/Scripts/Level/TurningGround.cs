using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningGround : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;
    [SerializeField] private Transform _centerOfCircle;

    public Vector3 GetMovingVector(Vector3 contactPosition, float stepSize)
    {
        Vector3 horisontalDirection = (_startPoint.position - _centerOfCircle.position).normalized;
        Vector3 verticalDirection = (_finishPoint.position - _centerOfCircle.position).normalized;
        float radius = Vector3.Distance(_centerOfCircle.position, contactPosition);
        float rotationFromStartAngle = 2 * Mathf.Asin((stepSize + Vector3.Distance(contactPosition, _centerOfCircle.position + horisontalDirection * radius)) / (2 * radius));
        Vector3 nextPosition = (horisontalDirection * Mathf.Cos(rotationFromStartAngle) * radius + verticalDirection * Mathf.Sin(rotationFromStartAngle) * radius) + _centerOfCircle.position;
        Vector3 movingVector = (nextPosition - contactPosition).normalized;
        movingVector.y = 0;

        return movingVector;
    }
}
