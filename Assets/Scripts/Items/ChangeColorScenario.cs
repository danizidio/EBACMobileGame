using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ChangeColorScenario : MonoBehaviour
{
    [SerializeField] Color _startColor = Color.white;
    [SerializeField] Color _originalColor;

    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public IEnumerator ChangeColor()
    {
        _originalColor = _meshRenderer.material.color;

        _meshRenderer.material.color = _startColor;
        
        yield return new WaitForSeconds(.5f);

        _meshRenderer.material.DOColor(_originalColor, 3f);
    }
}
