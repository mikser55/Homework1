using UnityEngine;

public class PlayerInput
{
    private static PlayerInputs _instance = null;

    public static PlayerInputs Get
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerInputs();

            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatic()
    {
        _instance = null;
    }
}