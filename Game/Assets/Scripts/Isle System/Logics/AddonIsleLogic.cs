using UnityEngine;

public class AddonIsleLogic : MonoBehaviour
{
    public static AddonIsleLogic _instance { private set; get; }
    [SerializeField] private AddonIsleItems _craftIsleItems;
    [SerializeField] private AddonIsleItems _fabricIsleItems;
    public AddonIsleItems CraftItems { get { return _craftIsleItems; } }
    public AddonIsleItems FabricItems { get { return _fabricIsleItems; } }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

