using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BrazierEmberHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform _target;
    private MeshRenderer _meshRenderer;
    private int _rimAmountId = Shader.PropertyToID("_delta");
    void Start()
    {
        _meshRenderer = _target.GetComponent<MeshRenderer>();
    }

    public void Burn(float time)
    {
        _meshRenderer.sharedMaterial.DOFloat(1, _rimAmountId, time * 0.5f)
        .OnComplete(() => _meshRenderer.sharedMaterial.DOFloat(0, _rimAmountId, 0).SetDelay(time * 0.5f));
    }
}
