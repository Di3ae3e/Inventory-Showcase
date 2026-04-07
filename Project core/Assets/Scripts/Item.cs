using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string itemName;
    [SerializeField] private GameObject prefab;

    public Sprite Icon => icon;
    public string ItemName => itemName;
}
