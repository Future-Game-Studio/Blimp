using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIsle : MonoBehaviour
{
    [SerializeField] private OwnedItems _ownedItems;
    [SerializeField] private GameObject _slotPrefab;

    private const float _radius = 25f;
    private void Start()
    {
        GenerateSlots();

    }

    private void GenerateSlots()
    {
        int allCount = 0;
        for (int circle = 1; circle < 15; circle++)
        {
            float radius = _radius + circle * 15;
            int count = circle + 9;
            float angleStep = 360f / count;
            for (int i = 1; i < count; i++)
            {
                allCount++;

                float angle = angleStep * i * 3.14f / 180;

                Vector3 position = new Vector3(gameObject.transform.position.x + (radius * Mathf.Cos(angle)), gameObject.transform.position.y, gameObject.transform.position.z + (radius * Mathf.Sin(angle)));
                Instantiate(_slotPrefab, position, _slotPrefab.transform.rotation);
            }

        }
        Debug.Log("Extra isles count: " + allCount);
    }
}
