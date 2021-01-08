using UnityEngine;
using System.Collections;


public class IsleManager : MonoBehaviour
{
    public static IsleManager _instance { private set; get; }
    [SerializeField] private GameObject _ropePrefab;
    [SerializeField] private Item _ropeItem;
    public Item RopeItem { get => _ropeItem; }

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

    public RopeBridge Rope { private set; get; } = null;
    public bool IsDocking { get => Rope != null; }

    public RopeBridge CreateDockConnection(Transform firstConnecion, Transform secondConnection)
    {
        if (Rope != null)
            Debug.LogError("Rope object removing error");

        var ropeObj = Instantiate(_ropePrefab, Vector3.zero, Quaternion.identity);
        Rope = ropeObj.GetComponent<RopeBridge>();

        Rope.SetPoints(firstConnecion, secondConnection);

        return Rope;
    }

    public void DeleteRopeConnection()
    {
        if (Rope == null)
            Debug.LogError("Rope deleting error");

        Destroy(Rope.gameObject);
    }

    public void StartDock(DefaultIsle isle, Transform ropeConnection)
    {
        StartCoroutine(Docking(isle, ropeConnection));
    }

    private IEnumerator Docking(DefaultIsle defaultIsle, Transform ropeConnection)
    {
        var isle = defaultIsle.gameObject;
        var player = GameManager._instance.Player;

        Transform mainIsle = GameManager._instance.MainIsle.transform;
        Transform start = player.ConnectionPoint;
        Transform end = ropeConnection;

        var rope = CreateDockConnection(start, end); ;

        float maxDistance = rope.RepoSegLen * rope.SegmentLength;

        Rigidbody rb = isle.GetComponent<Rigidbody>();

        float expSpeed = player.lastSpeed;
        while (true)
        {
            float lastSpeed = GameManager._instance.Player.lastSpeed;
            float speed = lastSpeed > 0 ? lastSpeed : 10;

            isle.transform.LookAt(start);

            //Check for moving
            if (Vector3.Distance(start.position, end.position) > maxDistance - 0.25f)
            {
                Debug.Log("Move");
                rb.velocity = isle.transform.forward * speed;
                expSpeed = speed;
            }
            else
            {
                Debug.Log(expSpeed);
                rb.velocity = isle.transform.forward * expSpeed;
                expSpeed = Mathf.Lerp(expSpeed - 0.15f > 0 ? expSpeed - 0.15f : 0, expSpeed, 0.1f);
            }

            //Check for docking
            if (Vector3.Distance(mainIsle.position, isle.transform.position) < 20f)
            {
                rb.velocity = rb.angularVelocity = Vector3.zero;
                EndDock(defaultIsle);
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void EndDock(DefaultIsle isle)
    {
        DeleteRopeConnection();
        GameManager._instance.MainIsle.DockIsle(isle);
        isle.gameObject.GetComponent<IDockable>().EndDock();
    }
}

