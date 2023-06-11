namespace JerrysConsole;

public class InventoryDomain
{
    InventoryRepository repository;

    public InventoryDomain(){
        repository= new InventoryRepository();
    }
    public List<Inventory> GetInventory()
    {
        var inventoryList = repository.GetInventory();
        return inventoryList;
    }

   
    public int GetQuantityByItemName(string itemName){
        
        var repository = new InventoryRepository();
        var inventoryList = repository.GetInventory();
        var quantity = inventoryList.Find(inventory => inventory.item.name== itemName).quantity;               
        return quantity;     
    }

    public List<Inventory> UpdateInventory(Cart cart){
        var inventoryList = repository.GetInventory();

        var listSelectedItem = cart.selectedItem;

        listSelectedItem.ForEach(selectedItem =>
        {
           var name = selectedItem.itemSelected.name;
           int quantityCart = selectedItem.quantitySelected;

           var inventoryForItemName =inventoryList.Find(inventory => inventory.item.name== name);
           inventoryForItemName.quantity -= quantityCart;
                      
        });

        return inventoryList;
    }

}