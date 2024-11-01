using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerSensor : MonoBehaviour
{
    // Defines which layers will trigger the sensor.
    public LayerMask CheckWhat;

    // Flags attribute allows bitwise combination of sensor modes.
    [Flags]
    public enum SensorModes
    {
        None = 0,
        Enter = 1 << 0,
        Stay = 1 << 1,
        Exit = 1 << 2,
    }
    public SensorModes SensorMode = SensorModes.Enter | SensorModes.Exit;

    // Reference to the Collider2D component. Get on reset
    protected Collider2D _collider;

    void Start()
    {
        Initialization();
    }

    void Reset()
    {
        // Ensures the collider is set to be a trigger
        if (TryGetComponent(out _collider))
        {
            _collider.isTrigger = true;
        }
        OnReset();
    }

    protected virtual void OnReset()
    {

    }

    private void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// Override to customize update behavior.
    /// </summary>
    protected virtual void OnUpdate()
    {

    }

    /// <summary>
    /// Override to customize initialization behavior.
    /// </summary>
    protected virtual void Initialization()
    {

    }

    /// <summary>
    /// Detects when another collider enters the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object’s layer matches CheckWhat and if the sensor is set to respond to Enter events.
        if (CheckWhat == (CheckWhat | (1 << other.gameObject.layer)) && SensorMode.HasFlag(SensorModes.Enter))
        {
            OnSensor(other);
            OnSensorEnter(other);
        }
    }

    /// <summary>
    /// Detects when another collider exits the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object’s layer matches CheckWhat and if the sensor is set to respond to Exit events.
        if (CheckWhat == (CheckWhat | (1 << other.gameObject.layer)) && SensorMode.HasFlag(SensorModes.Exit))
        {
            OnSensor(other);
            OnSensorExit(other);
        }
    }

    /// <summary>
    /// Detects when another collider stays within the trigger zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object’s layer matches CheckWhat and if the sensor is set to respond to Stay events.
        if (CheckWhat == (CheckWhat | (1 << other.gameObject.layer)) && SensorMode.HasFlag(SensorModes.Stay))
        {
            OnSensor(other);
            OnSensorStay(other);
        }
    }

    /// <summary>
    /// Override in derived classes to provide custom behavior for each sensor event.
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnSensor(Collider2D other)
    {
        // General sensor logic, called for any event (enter, stay, or exit).
    }

    protected virtual void OnSensorEnter(Collider2D other)
    {
        // Logic specifically for when an object enters the trigger.
    }

    protected virtual void OnSensorExit(Collider2D other)
    {
        // Logic specifically for when an object exits the trigger.
    }

    protected virtual void OnSensorStay(Collider2D other)
    {
        // Logic specifically for when an object stays in the trigger.
    }
}
