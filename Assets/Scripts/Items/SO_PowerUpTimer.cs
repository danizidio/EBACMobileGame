using UnityEngine;

[CreateAssetMenu()]
public class SO_PowerUpTimer : ScriptableObject
{
    [SerializeField] float _maxTime;
    public float MaxTime { get {  return _maxTime; } }
}
