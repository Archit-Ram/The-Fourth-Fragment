using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameInputScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\
    public float TimeGap = 40f;
    static Dictionary<string, KeyCode> KeyMaps;
    static string[] Actions = new string[3] {
        "Forward",
        "Backward",
        "Jump",
    };
    static KeyCode[,] ValidCombinations = new KeyCode[12, 3] {
    {
        KeyCode.D,
        KeyCode.A,
        KeyCode.W,
    },
    {
        KeyCode.F,
        KeyCode.S,
        KeyCode.E,
    },
    {
        KeyCode.G,
        KeyCode.D,
        KeyCode.R,
    },
    {
        KeyCode.H,
        KeyCode.F,
        KeyCode.T,
    },
    {
        KeyCode.J,
        KeyCode.G,
        KeyCode.Y,
    },
    {
        KeyCode.K,
        KeyCode.H,
        KeyCode.U,
    },
    {
        KeyCode.L,
        KeyCode.J,
        KeyCode.I,
    },
    {
        KeyCode.C,
        KeyCode.Z,
        KeyCode.S,
    },
    {
        KeyCode.V,
        KeyCode.X,
        KeyCode.D,
    },
    {
        KeyCode.B,
        KeyCode.C,
        KeyCode.F,
    },
    {
        KeyCode.N,
        KeyCode.V,
        KeyCode.G,
    },
    {
        KeyCode.M,
        KeyCode.B,
        KeyCode.H,
    },
    };
    static KeyCode[] Maps = new KeyCode[3] {
        KeyCode.D,
        KeyCode.A,
        KeyCode.W,
    };
    GameInputScript()
    {
        Init();
    }
    void Start()
    {
        StartCoroutine(CallRandomizerEveryTimeGap());
    }

    private static void Init()
    {
        KeyMaps = new Dictionary<string, KeyCode>();
        for (int i = 0; i < 3; i++)
        {
            KeyMaps.Add(Actions[i], Maps[i]);
        }
    }

    static System.Random random = new System.Random();
    IEnumerator CallRandomizerEveryTimeGap()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeGap-0.25f);
            //SwapInputs();
            FindAnyObjectByType<PlayerControllerConfusion>().enabled = false;
            int random_index = UnityEngine.Random.Range(0, 11);
            print(random_index);
            KeyMaps["Forward"] = ValidCombinations[random_index, 0];
            KeyMaps["Backward"] = ValidCombinations[random_index, 1];
            KeyMaps["Jump"] = ValidCombinations[random_index, 2];
            yield return new WaitForSeconds(0.25f);
            FindAnyObjectByType<PlayerControllerConfusion>().enabled = true;

        }
    }

    public static void SetKeyMap(string Act, KeyCode Input)
    {
        if (!KeyMaps.ContainsKey(Act))
        {
            throw new ArgumentException("Action is not part of the game");
        }
        KeyMaps[Act] = Input;
    }
    public static bool getKeyDown(string Act)
    {
        return Input.GetKeyDown(KeyMaps[Act]);
    }

    public static float GetAxis(string positiveAction, string negativeAction)
    {
        float axisValue = 0f;
        if (Input.GetKey(KeyMaps[positiveAction]))
        {
            axisValue += 1f;
        }
        if (Input.GetKey(KeyMaps[negativeAction]))
        {
            axisValue -= 1f;
        }
        return axisValue;
    }
    public static bool GetKey(string Action)
    {
        return Input.GetKey(KeyMaps[Action]);
    }
    public static bool GetKeyDown(string Action)
    {
        return Input.GetKeyDown(KeyMaps[Action]);
    }
    //static System.Random random = new System.Random(); 

    void SwapInputs()
    {
        List<KeyCode> RandomKeyCodes = new List<KeyCode>();

        for (KeyCode keyCode = KeyCode.Alpha0; keyCode <= KeyCode.Alpha9; keyCode++)
        {
            RandomKeyCodes.Add(keyCode);
        }

        for (KeyCode keyCode = KeyCode.A; keyCode <= KeyCode.Z; keyCode++)
        {
            RandomKeyCodes.Add(keyCode);
        }
        List<KeyCode> availableKeyCodes = new List<KeyCode>();
        RandomKeyCodes.Add(KeyCode.Space);
        foreach (KeyCode keyCode in RandomKeyCodes)
        {
            if (!KeyMaps.ContainsValue(keyCode))
            {
                availableKeyCodes.Add(keyCode);
            }
        }
        HashSet<KeyCode> AllotKeys = new HashSet<KeyCode>();
        while (AllotKeys.Count < 3)
        {
            int i = random.Next(RandomKeyCodes.Count);
            AllotKeys.Add(RandomKeyCodes[i]);
        }
        int j = 0;
        foreach (KeyCode member in AllotKeys)
        {
            KeyMaps[Actions[j]] = member;
            j++;
        }
        Debug.Log($"{KeyMaps["Jump"]},{KeyMaps["Backward"]},{KeyMaps["Forward"]}");
    }
}