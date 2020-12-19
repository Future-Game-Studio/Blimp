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
        GenerateSlots(15);

    }

    private void GenerateSlots(int circles)
    {
        int allCount = 0;
        for (int circle = 1; circle < circles; circle++)
        {
            float radius = _radius + circle * circles;
            int count = circle + 9;
            float angleStep = 360f / count;
            Vector3 mainIslePos = gameObject.transform.position;

            for (int i = 1; i < count; i++)
            {
                allCount++;

                float angle = angleStep * i * Mathf.PI / 180;

                Vector3 position = new Vector3(mainIslePos.x + (radius * Mathf.Cos(angle)), mainIslePos.y, mainIslePos.z + (radius * Mathf.Sin(angle)));
                Instantiate(_slotPrefab, position, _slotPrefab.transform.rotation, gameObject.transform);
            }

        }
        Debug.Log("Extra isles count: " + allCount);
    }
}
