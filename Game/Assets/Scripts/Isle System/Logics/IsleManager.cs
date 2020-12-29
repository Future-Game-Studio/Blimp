using UnityEngine;
using System.Collections;


public class IsleManager : MonoBehaviour
{
    public static IsleManager _instance { private set; get; }
    public delegate void SecondsThrow(int value);
    public SecondsThrow OnSeconds;

    #region isles logic
    [SerializeField] private AddonIsleItems _craftIsleItems;
    [SerializeField] private AddonIsleItems _fabricIsleItems;
    public AddonIsleItems CraftItems { get { return _craftIsleItems; } }
    public AddonIsleItems FabricItems { get { return _fabricIsleItems; } }
    #endregion

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(InvokeInSecond(1));
    }

    private IEnumerator InvokeInSecond(int second)
    {
        while (true)
        {
            yield return new WaitForSeconds(second);
            OnSeconds?.Invoke(second);
        }
    }

}

