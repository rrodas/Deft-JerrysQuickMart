namespace JerrysConsole;

public class CartDomain
{
    public CartDomain(){
  
    }

    public Cart AddSelectedItemToCart(Cart cart, SelectedItem _selectedItem){        
        if(cart.selectedItem == null) cart.selectedItem = new List<SelectedItem>();
        cart.selectedItem.Add(_selectedItem) ;
        return cart;
    }

    public Cart RemoveSelectedItemFromCart(Cart cart, SelectedItem _selectedItem){     
        if(cart != null){
            if(cart.selectedItem == null) cart.selectedItem = new List<SelectedItem>();
            cart.selectedItem.Remove(_selectedItem);
        }                        
        return cart;
    }

    public int CountSelectedItemsInsideCart(Cart cart){
        var count = 0;
        if(cart.selectedItem == null) cart.selectedItem = new List<SelectedItem>();
        count = cart.selectedItem.Count();
        return  count;
        
    }

    public double SubTotalSelectedItemsInsideCart(Cart cart){
        
        var listSelectedItems = cart.selectedItem;
        double subTotal = 0;

        listSelectedItems.ForEach(item => {        
            var name = item.itemSelected.name;   
            var quantity = item.quantitySelected;
            ItemDomain itemDomain = new ItemDomain();
            var total =itemDomain.TotalItemPriceForQuantityAndTypeOfClient(quantity,cart.customerType,name);
            subTotal  += total;
        });

        return subTotal;
    }
    public double TaxForSelectedItemsInsideCart(Cart cart){
        
        var listSelectedItems = cart.selectedItem;
        double tax = 0;

        listSelectedItems.ForEach(selectedItem => {        
            var name = selectedItem.itemSelected.name;   
            var quantity = selectedItem.quantitySelected;
            ItemDomain itemDomain = new ItemDomain();
            int taxeable =itemDomain.ItemIsTaxable(name);
            if(taxeable == 1){
                var total =itemDomain.TotalItemPriceForQuantityAndTypeOfClient(quantity,cart.customerType,name);
                tax +=  total * 0.065;
            }            
        });

        return tax;

    }

    public double GetTotalForSelectedItemsInsideCart(Cart cart){
        double subTotal = SubTotalSelectedItemsInsideCart(cart);
        double tax = TaxForSelectedItemsInsideCart(cart);
        double total = subTotal + tax;
        return total;
    }

     public double GetSavedAmount(Cart cart){
        double amountsaved = 0;

        double total = GetTotalForSelectedItemsInsideCart(cart);
       
        if(cart.customerType == 2){ //reward
            cart.customerType = 1; //
            double totalForRegularCustomer = GetTotalForSelectedItemsInsideCart(cart);
            amountsaved = totalForRegularCustomer -total;
        }else{
            amountsaved = 0;
        }
        
        return amountsaved;
    }

    public List<string> ViewCart(Cart cart){
        
        return new InvoiceRepository().CreateFileInvoiceStructure(cart)  ;
    }

    public void CheckOutProcess(Cart cart){
        var inventoryDomain = new InventoryDomain();
        var inventory = inventoryDomain.UpdateInventory(cart);
        new InventoryRepository().SaveInventory(inventory);

        new InvoiceRepository().SaveInvoice(cart);
    }


}