using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        NavigationData.OnLoading?.Invoke();
    }
}
