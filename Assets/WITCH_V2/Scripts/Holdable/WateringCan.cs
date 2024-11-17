using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WateringEvent
{

}

public class WateringCan : HoldableObject
{
    [SerializeField] float ResetPositionDuration = .4f;

    private Vector3 _initialPos;
    private Quaternion _initialRot;
    private Vector3 _posOnRelease;

    protected override void Initialization()
    {
        base.Initialization();
        _initialPos = transform.position;
        _initialRot = transform.rotation;
    }

    protected override void OnReleased()
    {
        base.OnReleased();
    }

    public void ResetPosition()
    {
        _posOnRelease = transform.position;
        StartCoroutine(ResetPositionRoutine(ResetPositionDuration));
        EventBus.TriggerEvent(new WateringEvent());
    }

    IEnumerator ResetPositionRoutine(float translateDuration)
    {
        float t = 0;
        while (t < translateDuration)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(_posOnRelease, _initialPos, t/translateDuration);
        }
    }
}
