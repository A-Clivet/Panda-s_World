using System.Collections.Generic;
using UnityEngine;

public enum ControlScheme
{
    Keyboard
}

public class CustomInputSystem : MonoBehaviour
{
    private static Dictionary<string, KeyCode> _keyMapping = new Dictionary<string, KeyCode>();
    private static Dictionary<string, string> _axisMapping = new Dictionary<string, string>();

    private void Awake()
    {
        InitializeDefaultKey();
        InitializeDefaultAxis();

        DontDestroyOnLoad(gameObject);
    }

    private void InitializeDefaultKey()
    {
        //Player 1
        _keyMapping["Jump1"] = KeyCode.Space;
        _keyMapping["Fire1"] = KeyCode.Q;
    }

    private void InitializeDefaultAxis()
    {
        _axisMapping["Horizontal1"] = "Horizontal";

        _axisMapping["Horizontal2"] = "Horizontal2";
    }

    public static bool GetKeyDown(string action, ControlScheme controlScheme)
    {
        if (_keyMapping.ContainsKey(action))
        {
            return Input.GetKeyDown(_keyMapping[action]);
        }


        return false;
    }

    public static float GetAxis(string action, ControlScheme controlScheme)
    {
        if (_axisMapping.ContainsKey(action))
        {
            return Input.GetAxis(_axisMapping[action]);
        }

        return 0f;
    }

    public static void RebindKey(string action, KeyCode newKey)
    {
        if (_keyMapping.ContainsKey(action))
        {
            if (!CheckKeyDuplicate(newKey))
                _keyMapping[action] = newKey;
        }
    }

    private static bool CheckKeyDuplicate(KeyCode newKey)
    {
        foreach (KeyValuePair<string, KeyCode> entry in _keyMapping)
        {
            if (entry.Value == newKey)
            {
                return true;
            }
        }

        return false;
    }

    public static KeyCode GetBoundKey(string action)
    {
        if (_keyMapping.ContainsKey(action))
        {
            return _keyMapping[action];
        }

        return KeyCode.None;
    }
}