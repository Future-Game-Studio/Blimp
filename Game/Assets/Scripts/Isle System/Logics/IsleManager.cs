using UnityEngine;

public class IsleManager : MonoBehaviour
{
    public static IsleManager _instance { private set; get; }

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
    }





    #region Dock System
    public void StartDock()
    {

    }


    public void EndDock()
    {

    }
    #endregion
}

