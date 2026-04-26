using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool isStacable = true;
    [SerializeField] private int maxStackSize = 1;

    public Sprite Icon => icon;
    public string ItemName => itemName;
    public bool IsStackable => isStacable;
    public int MaxStackSize => maxStackSize;
}
