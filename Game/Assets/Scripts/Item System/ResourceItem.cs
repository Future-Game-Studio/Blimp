using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "New Resource Item", menuName = "Inventory System/Items/Resource", order = 51)]
public class ResourceItem : Item
{
    public override string ColouredName => throw new System.NotImplementedException();

    private void Awake()
    {
        _type = ItemType.Recovering;
    }

}
