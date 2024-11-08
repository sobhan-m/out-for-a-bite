using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter
{
    public float maxValue { get; private set; }
    public float minValue { get; private set; }
    public float currentValue { get; private set; }

    public Meter(float minValue, float maxValue, float currentValue = 0)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.currentValue = currentValue;
    }

    /// <summary>
    /// Fill the meter by <c>val</c>.
    /// </summary>
    public void FillMeter(float val)
    {
        if (val < 0)
        {
            throw new System.ArgumentOutOfRangeException("Meter.FillMeter(): The passed value must be non-negative. Use Meter.EmptyMeter() to reduce.");
        }
        currentValue = Mathf.Clamp(currentValue += val, minValue, maxValue);
    }

    /// <summary>
    /// Empty the meter by <c>val</c>.
    /// </summary>
    public void EmptyMeter(float val)
    {
        if (val < 0)
        {
            throw new System.ArgumentOutOfRangeException("Meter.EmptyMeter(): The passed value must be non-negative. Use Meter.FillMeter() to increase.");
        }
        currentValue = Mathf.Clamp(currentValue -= val, minValue, maxValue);
    }

    public bool IsFull()
    {
        return currentValue >= maxValue;
    }

    public bool IsEmpty()
    {
        return currentValue <= minValue;
    }

    /// <summary>
    /// Fill the meter up to the <c>maxValue</c>.
    /// </summary>
    public void FillMeter()
    {
        currentValue = maxValue;
    }

    /// <summary>
    /// Empty the meter down to <c>minValue</c>.
    /// </summary>
    public void EmptyMeter()
    {
        currentValue = minValue;
    }

}