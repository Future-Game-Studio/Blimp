using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftIsle : DefaultIsle
{
    [SerializeField] private AddonIsleItems _items;
    public AddonIsleItems Items { get => _items; }
    public List<CraftTask> Tasks { private set; get; }
    public OwnedItems DoneTasks { private set; get; }
    public int Counter { private set; get; }
    public int Level { private set; get; }


    public delegate void IntAndFloatThrow(CraftableItem item, float progress);
    public IntAndFloatThrow OnDoTask;


    //Debug
    [SerializeField] CraftableItem _item;

    private void Awake()
    {
        Type = IsleType.Craft;
        Level = 0;
        Counter = 0;
    }
    private void Start()
    {
        Tasks = new List<CraftTask>();
        DoneTasks = ScriptableObject.CreateInstance<OwnedItems>();
        IsleManager._instance.OnSeconds += DoTask;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
            AddTask(_item, 3);
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

                OnDoTask?.Invoke(item, 1);
            }
            else
                OnDoTask?.Invoke(item, 0);

        }
        else
            OnDoTask?.Invoke(item, (float)Counter / item.CraftTime);
    }

    void OnMouseDown()
    {
        UIManager._instance.SwitchIsleUI(UIType.CraftIsle, this);
    }

    private void OnDisable()
    {
        IsleManager._instance.OnSeconds -= DoTask;
    }
}
