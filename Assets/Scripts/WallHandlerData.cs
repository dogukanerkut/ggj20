using UnityEngine;

[CreateAssetMenu(fileName = "WallHandlerData", menuName = "WallHandlerData", order = 0)]
public class WallHandlerData : ScriptableObject
{
    public int HitsRequiredToRepair;
    public Color DamageColor;
    public Color RepairColor;
    public Color RestoreColor;
}