public class InventorySlot 
{
    public Item ItemInSlot;
    public int ItemsInSlotCount;

    public InventorySlot(Item item, int count)
    {
        ItemInSlot = item;
        ItemsInSlotCount = count;
    }
}
