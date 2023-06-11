namespace JerrysConsole;

using System;
public class ItemDomain
{
   
    public ItemDomain(){
   
    }

    public Item? GetItemByName(string itemName)
    {
        InventoryRepository repository = new InventoryRepository();   
        var inventoryList = repository.GetInventory();

        Item? item = inventoryList.Find(inventory => inventory.item.name== itemName).item;
        if(item==null) return null;
                      
        return  item;
    }

    public int ItemIsTaxable(string itemName){
        var itemEncontrado = GetItemByName(itemName);
        if(itemEncontrado==null) return 0;
        if(itemEncontrado.taxStatus == "Taxable"){
            return 1;
        }else{
            return 0;
        }
    }

    public double ItemPriceForCustomerType(int customerType, string itemName){
        var itemEncontrado = GetItemByName(itemName);
        if(itemEncontrado==null)return 0;
        if(customerType == 1){ //regular
            return itemEncontrado.regularPrice;
        }else{ //reward
            return itemEncontrado.memberPrice;
        }
    }

    public double TotalItemPriceForQuantityAndTypeOfClient(int quantity, int customerType, string itemName){
        double priceForCustomer = ItemPriceForCustomerType(customerType,itemName);
        return priceForCustomer * quantity;
    }
}