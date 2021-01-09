using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIsle : MonoBehaviour
{
    [SerializeField] private OwnedItems _ownedItems;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private int _circlesCount;
    [SerializeField] private int _startCircle;
    [SerializeField] private List<ExtraIsleSlot> _slots;
    [SerializeField] private Transform _slotCenter;
    private const float _radius = 25f;
    private void Start()
    {
        _slots = new List<ExtraIsleSlot>();
        GenerateSlots(_circlesCount);
        StartCoroutine(Rotating());
    }

    private void Update()
    {

    }

    private void GenerateSlots(int circles)
    {
        int allCount = 0;
        for (int circle = 0; circle < circles; circle++)
        {
            float radius = _radius + (circle + 1) * 15f;
            int count = circle + _startCircle;
            float angleStep = 360f / (count + 1);
            Vector3 mainIslePos = _slotCenter.position;

            for (int i = 1; i < count + 1; i++)
            {
                allCount++;

                float angle = angleStep * i * Mathf.PI / 180;

                Vector3 position = new Vector3(mainIslePos.x + (radius * Mathf.Cos(angle)), mainIslePos.y, mainIslePos.z + (radius * Mathf.Sin(angle)));
                GameObject isle = Instantiate(_slotPrefab, position, _slotPrefab.transform.rotation, _slotCenter);
                _slots.Add(isle.GetComponent<ExtraIsleSlot>());
            }

        }
        Debug.Log("Extra isles count: " + allCount);
    }

    public void DockIsle(DefaultIsle isle)
    {
        var slot = _slots.Find(s => s.IsEmpty);
        if (slot == null)
        {
            Debug.LogError("Haven`t empty slots");
            return;
        }

        slot.SetIsle(isle);
        Debug.Log("Set isle");
    }

    private IEnumerator Rotating()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            gameObject.transform.Rotate(new Vector3(0f, 0.01f, 0f));
        }
    }

    private void OnMouseDown()
    {
        CameraManager._instance.ChangeCamera(CameraManager.CameraType.MainIsle);
    }
}
