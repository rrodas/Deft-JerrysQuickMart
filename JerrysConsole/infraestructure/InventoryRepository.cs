namespace JerrysConsole;

using System;
using System.Globalization;

public class InventoryRepository{

    private JerrysFileReader fileReader;
    
    public InventoryRepository(){
        fileReader = new JerrysFileReader("inventory.txt");
             
    }

    public List<Inventory> GetInventory(){
        var inventoryList = new List<Inventory>();

        var lines = fileReader.ReadFile();
        lines.ForEach(l =>
        {
            var inventory = loadInventory(l);
            if(inventory != null) 
                inventoryList.Add(inventory);
        });
        return inventoryList;

    }

    private Inventory? loadInventory(string line)
    {
        if(string.IsNullOrEmpty(line)) return null;
        
        var parts = line.Split(',');
            
        var productQuantity = parts[0];
        var productQuantityParts = productQuantity.Split(':');
        var itemName = productQuantityParts[0];
        var quantityProduct = Convert.ToInt32(productQuantityParts[1]);

        var inventory = new Inventory{
            quantity = quantityProduct,
            item = new Item{
                name = itemName,
                regularPrice = Convert.ToDouble(parts[1], CultureInfo.InvariantCulture),
                memberPrice = Convert.ToDouble(parts[2], CultureInfo.InvariantCulture),
                taxStatus = parts[3]
            }
        };

        return inventory;
        
    }
    public List<String> CreateFileInventoryStructure(List<Inventory> inventoryList){
            var lines = new List<String>();
            inventoryList.ForEach(inventoryList =>
            {
                var name = inventoryList.item.name;
                double regularPrice = inventoryList.item.regularPrice;
                double memberPrice = inventoryList.item.memberPrice;
                var taxStatus = inventoryList.item.taxStatus;
                int quantity = inventoryList.quantity;
                var line = name + ":" + quantity + "," + regularPrice.ToString(CultureInfo.InvariantCulture) 
                + "," + memberPrice.ToString(CultureInfo.InvariantCulture) + "," + taxStatus;
                lines.Add(line);                
            });
            return lines;
    }

    public int SaveInventory(List<Inventory> inventoryList){
        var lines = CreateFileInventoryStructure(inventoryList);
        int success = fileReader.WriteFile(lines);
        return success;
        
    }
}