namespace JerrysConsole;

public class Menu{

    int clientType;
    Cart cart;
    public Menu(){
        cart=  new Cart();
        cart.selectedItem = new List<SelectedItem>();
    }

    public void DrawClientTypeMenu(){
        bool okInput=false;
        Console.WriteLine ("Enter the number that corresponds to the type of customer: \n ");
        Console.WriteLine ("1.  Regular Customer \n");
        Console.WriteLine ("2.  Reward Member \n");        

        while(!okInput){           
            int input;
            try{
                input = int.Parse(Console.ReadLine());                                
                if(input != 1 && input != 2) {                    
                    Console.WriteLine ("Invalid selection, please try again \n");
                    }
                else{      

                    okInput=true;
                    clientType = input;
                    DrawActionsMenu();
                }

            }catch(Exception e){                
                 Console.WriteLine ("Invalid selection, please try again \n");
                 okInput=false;
            }            
        }
    }

    public void DrawActionsMenu(){
        bool okInput=false;
        Console.WriteLine ("Enter the number that corresponde to the action you want to execute: \n ");
        Console.WriteLine ("1.  Add items to cart \n");
        Console.WriteLine ("2.  Remove individual items from cart \n");
        Console.WriteLine ("3.  View cart \n");
        Console.WriteLine ("4.  Checkout and print receipt \n");
        Console.WriteLine ("5.  Cancel Transaction \n");
    
        while(!okInput){           
            int input;
            try{
                input = int.Parse(Console.ReadLine());                                
                
                if(input != 1 && input != 2 && input!=3  && input!=4 && input!=5 ){                    
                    Console.WriteLine ("Invalid selection, please try again \n");
                    }
                else{                    
                    okInput=true;
                    switch(input){
                        case 1: //add Item
                            DrawAddItemsMenu();
                            break;
                        case 2: //Remove item
                            RemoveItemMenu();
                            break;
                        case 3: //View Car
                            DrawCart();
                            Console.WriteLine("Press any key to continue");
                            Console.Read();
                            Console.Clear();
                            DrawActionsMenu();
                            break;
                        case 4: //Ceckout and print invoice
                            DrawInvoice();
                            break;
                        case 5: //Cancel Transaction
                            cart = new Cart();
                            cart.selectedItem = new List<SelectedItem>();
                            Console.WriteLine("Transaction has been canceled. Press any key too continue ");
                            Console.Read();
                            Console.Clear();
                            DrawClientTypeMenu();
                            break;
                    }
                }

            }catch(Exception e){                
                 Console.WriteLine ("Invalid selection, please try again \n");
                 okInput=false;
            }            
        }    
    }

    public void DrawAddItemsMenu(){
        bool okInput=false;        

        ShowInventoryAddItemsMenu();     
        while(!okInput){           
            var input="";
            try{
                //reading item
                input = Console.ReadLine();    
                ItemDomain itemDomain = new ItemDomain();                
                var item = itemDomain.GetItemByName(input);  
                if(item == null) {                    
                    Console.WriteLine ("Invalid selection, please try again \n");
                    }
                else{                    
                    okInput=true;
                    //reading quantity
                    var quantity = AskQuantityAddItemsMenu(input);                                                             
                    CartUpdate( item, quantity);                
                    Console.WriteLine("Item added.  Press any key to continue");
                    Console.Read();
                    Console.Clear();
                    DrawActionsMenu(); 
                }
            }catch(Exception e){  
                Console.WriteLine(e.Message);              
                 Console.WriteLine ("Invalid selection, please try again \n");
                 okInput=false;
            }            
        }
        

    }

    public void ShowInventoryAddItemsMenu(){
        Console.WriteLine ("Enter the name of the item you want to add: \n ");
        InventoryDomain inventoryDomain = new InventoryDomain();
        var listInventory = inventoryDomain.GetInventory();
        Console.WriteLine ("Item\t\t\tQuantity\t\t\tPrice");
        listInventory.ForEach(inventory =>
        {
            var name = inventory.item.name;
            var quantity = inventory.quantity;
            var price=0.0;
            if(clientType == 1){ //regular
                 price = inventory.item.regularPrice;
            }
            Console.WriteLine (name +"\t\t\t"+ quantity+ "\t\t\t"+ price);
        });

    }

    public void CartUpdate( Item item, int quantity){
      
        cart.customerType = clientType;
        
        SelectedItem selectedItem = new SelectedItem();
        selectedItem.itemSelected = item;
        selectedItem.quantitySelected = quantity;

  
                
        cart.selectedItem.Add(selectedItem);

    }

    public int AskQuantityAddItemsMenu(String itemName){
        bool okInput=false;
        Console.WriteLine ("Enter the quantiy you want to buy for " + itemName +" : \n ");  
            
        int input =0;
        while(!okInput){                                  
            try{                
                input = int.Parse(Console.ReadLine());                                                                    
                okInput=true;   
            }catch(Exception e){                                
                Console.WriteLine ("Invalid selection, please try again \n");                                
                okInput=false;  
            }                       
        }        
        return input;
    }


    private void RemoveItemMenu()
    {
        bool okInput=false;
        DrawCart();
        Console.WriteLine ("Enter the item's name to be removed: \n ");
       
        while(!okInput){           
            int input;
            try{
                var itemToBeRemoved = Console.ReadLine();                                
                var selectedItemFound = cart.selectedItem.Find( selectedItem =>
                    selectedItem.itemSelected.name == itemToBeRemoved
                );
                
                if(selectedItemFound== null) {                    
                    Console.WriteLine ("Invalid selection, please try again \n");
                }
                else{      
                    cart.selectedItem.Remove(selectedItemFound);
                    okInput=true;
                    Console.WriteLine ("Item has been removed, press any key to continue\n");
                    Console.Read();
                    Console.Clear();
                    DrawActionsMenu();
                }

            }catch(Exception e){                
                 Console.WriteLine ("Invalid selection, please try again \n");
                 okInput=false;
            }            
        }
    }

    public void  DrawCart(){
        if(cart.selectedItem.Count==0){
            Console.WriteLine("The Cart is Empty, press any key to continue");    
            Console.Read();
            Console.Clear();
            DrawActionsMenu();
            return;
        }
        
        Console.WriteLine("ITEM\t\tQUANTITY");

        cart.selectedItem.ForEach(selectedItem =>
            {
                var name = selectedItem.itemSelected.name;
                var quantity = selectedItem.quantitySelected;      
                Console.WriteLine(name +"\t\t" + quantity);          
            }
        );
    }

    public void DrawInvoice()
    {
        
        Console.WriteLine("Your cart has generated with the following values: \n");
        DrawPreInvoice();

        bool okInput = false;
        Console.WriteLine("Select one option : \n ");
        Console.WriteLine("1.  Continue with the payment \n");
        Console.WriteLine("2.  Cancel Transaction\n");

        while (!okInput)
        {
            int input;
            try
            {
                input = int.Parse(Console.ReadLine());
                if (input != 1 && input != 2)
                {
                    Console.WriteLine("Invalid selection, please try again \n");
                }
                else
                {
                    okInput = true;
                    if (input == 1)
                    {
                        //read the payment
                        ValidatePayment();
                        CheckOutProcess();
                        Console.WriteLine("Find your final Invoice: \n");
                        DrawPreInvoice();
                        Console.WriteLine("\nPress any key to continue: \n");
                        Console.Read();
                        Console.Clear();
                        DrawClientTypeMenu();
                    }
                    else
                    {
                        okInput = false;
                        Console.Clear();
                        DrawActionsMenu();
                    }

                    DrawActionsMenu();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid selection, please try again \n");
                okInput = false;
            }
        }
    }

    private void DrawPreInvoice()
    {
        var cartDomain = new CartDomain();
        var lines = cartDomain.ViewCart(cart);
        Console.Clear();
        
        lines.ForEach(line => Console.WriteLine(line));
        Console.WriteLine("\n");
    }

    public void ValidatePayment(){
         bool okInput=false;
        Console.WriteLine ("Enter cash: \n ");
        
        while(!okInput){           
            double input;
            try{
                input = Double.Parse(Console.ReadLine());    
                CartDomain cartDomain = new CartDomain();
                var totalToPay = cartDomain.GetTotalForSelectedItemsInsideCart(cart);
                if(input < totalToPay) {                    
                    Console.WriteLine ("Cash entered is not enough, please try again \n");
                    }
                else{     
                    okInput=true;
                    cart.cash =  input;                                    
                }

            }catch(Exception e){                
                 Console.WriteLine ("Invalid selection, please try again \n");
                 okInput=false;
            }            
        }

    }

    public void CheckOutProcess(){
        var cartDomain = new CartDomain();
        cartDomain.CheckOutProcess(cart);
    }


}