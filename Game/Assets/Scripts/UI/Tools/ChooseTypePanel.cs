using UnityEngine;
using UnityEngine.UI;

public class ChooseTypePanel : MonoBehaviour
{
    [SerializeField] private Button _craft;
    public Button CraftButton { get => _craft; }
    [SerializeField] private Button _fabric;
    public Button FabricButton { get => _fabric; }
    [SerializeField] private Button _exit;
    public Button ExitButton { get => _exit; }
}
