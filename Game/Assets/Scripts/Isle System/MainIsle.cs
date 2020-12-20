using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIsle : MonoBehaviour
{
    [SerializeField] private OwnedItems _ownedItems;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private int _circlesCount;
    [SerializeField] private int _startCircle;
    [SerializeField] private List<GameObject> _isles;
    private const float _radius = 25f;
    private void Start()
    {
        _isles = new List<GameObject>();
        GenerateSlots(_circlesCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Update main isle");
            _isles.ForEach(i => Destroy(i.gameObject));
            _isles.Clear();
            GenerateSlots(_circlesCount);
        }
    }

    private void GenerateSlots(int circles)
    {
        int allCount = 0;
        for (int circle = 0; circle < circles; circle++)
        {
            float radius = _radius + (circle + 1) * 15f;
            int count = circle + _startCircle;
            float angleStep = 360f / (count + 1);
            Vector3 mainIslePos = gameObject.transform.position;

            for (int i = 1; i < count + 1; i++)
            {
                allCount++;

                float angle = angleStep * i * Mathf.PI / 180;

                Vector3 position = new Vector3(mainIslePos.x + (radius * Mathf.Cos(angle)), mainIslePos.y, mainIslePos.z + (radius * Mathf.Sin(angle)));
                GameObject isle = Instantiate(_slotPrefab, position, _slotPrefab.transform.rotation, gameObject.transform);
                _isles.Add(isle);
            }

        }
        Debug.Log("Extra isles count: " + allCount);
    }
}
