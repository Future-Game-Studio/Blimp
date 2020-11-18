using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesIsle : MonoBehaviour
{
    [SerializeField] private OwnedItems _items;
    [SerializeField] private Item _itemType;
    [SerializeField] private int _maxAmount;
    private void Start()
    {
        _items = new OwnedItems();
        StartCoroutine(Refreshing());
    }

    private void Update()
    {

    }

    //ref coroutine
    private IEnumerator Refreshing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            if(_maxAmount > _items._summaryAmount)
                _items.AddItem(_itemType, 1);
        }
    }
}
