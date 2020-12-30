using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraIsle : DefaultIsle, IDockable
{
    public AddonIsleItems Items { get; private set; }
    public List<CraftTask> Tasks { private set; get; }
    public OwnedItems DoneTasks { private set; get; }
    public int Counter { private set; get; } = 0;
    public int Level { private set; get; } = 0;

    public DockMode Mode { get; private set; } = DockMode.Outside;

    public delegate void DoTaskDelegate(CraftableItem item, float progress, bool doNotUpdate);
    public DoTaskDelegate OnDoTask;



    private void Awake()
    {
        Type = IsleType.Empty;
    }
    private void Start()
    {
        Tasks = new List<CraftTask>();
        DoneTasks = ScriptableObject.CreateInstance<OwnedItems>();
        IsleManager._instance.OnSeconds += DoTask;

    }

   
    public void ChangeMode(DockMode mode)
    {
        Mode = mode;
    }

    public void IncreaseLevel()
    {
        if(Level == Items.Info.Count)
            Debug.LogError("Isle level the same as max!");
        else
        {
            Level++;
        }
    }

    public class CraftTask
    {
        public CraftTask(CraftableItem item, int amount)
        {
            Item = item; Amount = amount;
        }

        public CraftableItem Item;
        public int Amount;
    }


    public void AddTask(CraftableItem item, int amount)
    {
        CraftTask task = Tasks.Find(t => t.Item == item);
        if (task != null)
            task.Amount += amount;
        else
            Tasks.Add(new CraftTask(item, amount));
    }

    public void DoTask(int secondValue)
    {
        if (Tasks.Count == 0)
            return;

        Counter++;

        CraftableItem item = Tasks[0].Item;


        if (item.CraftTime <= Counter)
        {
            DoneTasks.AddItem(item, 1);
            Counter = 0;
            Tasks[0].Amount--;

            if (Tasks[0].Amount == 0)
            {
                Tasks.RemoveAt(0);

                OnDoTask?.Invoke(item, 1, false);
            }
            else
                OnDoTask?.Invoke(item, 0, false);

        }
        else
            OnDoTask?.Invoke(item, (float)Counter / item.CraftTime, false);
    }

    public void ChangeIsleType(IsleType type)
    {
        Type = type;
        switch (type)
        {
            case IsleType.Craft:
                Items = IsleManager._instance.CraftItems;
                break;
            case IsleType.Fabric:
                Items = IsleManager._instance.FabricItems;
                break;
            default:
                Debug.LogError("Isle change type error");
                break;
        }
    }
    void OnMouseDown()
    {
        UIManager._instance.SwitchIsleUI(UIType.CraftIsle, this);
    }

    private void OnDisable()
    {
        IsleManager._instance.OnSeconds -= DoTask;
    }

    public void StartDock()
    {
        Mode = DockMode.Docking;

        //
        GameManager._instance.MainIsle.DockIsle(this);
        Mode = DockMode.Inside;
        //
    }

    public void EndDock()
    {
        throw new System.NotImplementedException();
    }
}
