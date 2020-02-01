using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WallHandlerData _data;
    [SerializeField] private WallBreakHandler _wallBreakHandler;
    [SerializeField] List<Container> _linkedContainers = new List<Container>();
    private float _currentRepairAmount;

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
}
