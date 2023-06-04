using TimeCounter;

public class DestroyGameObject : Timer
{

    void Start()
    {
        SetTimer(GetTimer,() => Destroy(gameObject));
    }

    private void Update()
    {
        CountDown();
    }
}
