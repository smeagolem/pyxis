using UnityEngine;
using System;

public abstract class Feedback<Self, T> : MonoBehaviour
{
    public T value;
    public static event Action<T> emitted;

    void OnEnable()
    {
        Emit(value);
    }

    public static void Emit(T value)
    {
        emitted?.Invoke(value);
    }
}
