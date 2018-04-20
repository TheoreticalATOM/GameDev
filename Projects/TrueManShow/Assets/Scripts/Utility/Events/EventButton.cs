using Sirenix.OdinInspector;
using UnityEngine.Events;

public class EventButton : SerializedMonoBehaviour
{
    public UnityEvent OnButtonPressed;

    [Button()]
    public void FireEvent()
    {
        OnButtonPressed.Invoke();
    }
    
}
