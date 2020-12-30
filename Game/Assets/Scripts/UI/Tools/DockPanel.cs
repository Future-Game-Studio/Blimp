using UnityEngine;
using UnityEngine.UI;

public class DockPanel : MonoBehaviour
{
    [SerializeField] private Button _dockButton;
    public Button DockButton { get => _dockButton; }
    [SerializeField] private Button _exitButton;
    public Button ExitButton { get => _exitButton; }


}
