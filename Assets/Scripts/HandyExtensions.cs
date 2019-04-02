using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HandyExtensions
{

    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }

    private static Func<int, UnityEngine.Object> m_FindObjectFromInstanceID = null;

    static HandyExtensions()
    {
        var methodInfo = typeof(UnityEngine.Object)
            .GetMethod("FindObjectFromInstanceID",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        if (methodInfo == null)
            Debug.LogError("FindObjectFromInstanceID was not found in UnityEngine.Object");
        else
            m_FindObjectFromInstanceID = (Func<int, UnityEngine.Object>)Delegate.CreateDelegate(typeof(Func<int, UnityEngine.Object>), methodInfo);
    }
    public static UnityEngine.Object FindObjectFromInstanceID(int aObjectID)
    {
        if (m_FindObjectFromInstanceID == null)
            return null;
        return m_FindObjectFromInstanceID(aObjectID);
    }
}
