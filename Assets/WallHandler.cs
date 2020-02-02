using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
public class WallHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WallHandlerData _data;
    [SerializeField] private WallBreakHandler _wallBreakHandler;
    [SerializeField] List<Container> _linkedContainers = new List<Container>();
    private MeshRenderer _renderer;
    private float _currentRepairAmount;
    int _rimAmountId = Shader.PropertyToID("_delta");
    int _targetColorId = Shader.PropertyToID("_RimColor");
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();

    }
    public bool AttemptToRepair()
    {
        if (HasResourceToRepair() && !IsWallAlreadySolid())
        {
            Repair();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Repair()
    {
        _currentRepairAmount++;
        if (_currentRepairAmount >= _data.HitsRequiredToRepair)
        {
            _currentRepairAmount = 0;
            foreach (var item in _linkedContainers)
            {
                if (item.ItemCount < 0) continue;
                item.RemoveItem();
            }
            _wallBreakHandler.Repair();
        }

        RepairFX();

    }
    private bool HasResourceToRepair()
    {
        int totalStones = _linkedContainers.Sum(x => x.ItemCount);
        return totalStones > 0;
    }
    private bool IsWallAlreadySolid()
    {
        return _wallBreakHandler.IsWallSolid;
    }
    public void ShineBabyShine()
    {
        _renderer.material.SetColor(_targetColorId, _data.DamageColor);
        DOTween.Kill(_renderer.material, true);
        _renderer.material.DOFloat(1, _rimAmountId, .75f).From(0).SetEase(Ease.InQuint);
    }
    public void RepairFX()
    {
        _renderer.material.SetColor(_targetColorId, _data.RepairColor);
        DOTween.Kill(_renderer.material, true);
        _renderer.material.DOFloat(1, _rimAmountId, .3f).From(0).SetEase(Ease.InQuint);
    }
    public void RestoreFX()
    {
        _renderer.material.SetColor(_targetColorId, _data.RestoreColor);
        DOTween.Kill(_renderer.material, true);
        _renderer.material.DOFloat(1, _rimAmountId, .3f).From(0).SetEase(Ease.InQuint);
    }
    private void OnDisable()
    {
        DOTween.Kill(_renderer.material, true);
    }
}
