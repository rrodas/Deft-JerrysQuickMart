namespace JerrysConsole;

using System;
using System.Globalization;

public class InvoiceRepository{

    private JerrysFileReader fileReader;
    private String formattedDate;
    private String readabledDate;
    
    public InvoiceRepository(){

        DateTime thisDay = DateTime.Today;
        var parts = thisDay.ToString("d").Split('/');
        var day = parts[0];
        var month = parts[1];
        var year = parts[2];
        formattedDate = day+month+year;

        var readableMonth = DateTime.Now.ToString("MMMM") ;
        readabledDate =readableMonth +" " + day + "," +year;


        var fileName = "transaction_00001-"+ formattedDate +".txt";
        fileReader = new JerrysFileReader(fileName);
             
    }
    
    public List<String> CreateFileInvoiceStructure(Cart cart){
            var lines = new List<String>();
            lines.Add(readabledDate);
            lines.Add("TRANSACTION: 000001");
            lines.Add("ITEM\tQUANTITY\tUNIT_PRICE\tTOTAL");
            var selectedItemList = cart.selectedItem;
            selectedItemList.ForEach(selectedItem=>
            {
                var name = selectedItem.itemSelected.name;
                var quantity = selectedItem.quantitySelected;
                double unitPrice = 0.0;
                if(cart.customerType == 1)//regular
                {
                    unitPrice = selectedItem.itemSelected.regularPrice;

                }else{ //reward
                    unitPrice = selectedItem.itemSelected.memberPrice;
                }
                var total = quantity * unitPrice;

                var line = name + "\t" + quantity + "\t\t\t" + 
                unitPrice.ToString(CultureInfo.InvariantCulture)  
                    + "\t\t\t" + total.ToString(CultureInfo.InvariantCulture);
                lines.Add(line);                
            });
            lines.Add("*************");

            CartDomain cartDomain = new CartDomain();
            
            lines.Add("TOTAL NUMBER OF ITEMS SOLD: " + cartDomain.CountSelectedItemsInsideCart(cart) );    
            lines.Add("SUBTOTAL: $" + cartDomain.SubTotalSelectedItemsInsideCart(cart) );  
            lines.Add("TAX: $" + cartDomain.TaxForSelectedItemsInsideCart(cart) );  
            lines.Add("TOTAL: $" + cartDomain.GetTotalForSelectedItemsInsideCart(cart) );  
            lines.Add("CASH: $" + cart.cash ); 
            double change =  cart.cash - cartDomain.GetTotalForSelectedItemsInsideCart(cart);
            lines.Add("CHANGE: $" +  change);  

            lines.Add("*****************"); 
            double savedAmount =  cartDomain.GetSavedAmount(cart);
            lines.Add("YOU SAVED: $" + savedAmount );  

            return lines;
    }

    public int SaveInvoice(Cart cart){
        var lines = CreateFileInvoiceStructure(cart);
        int success = fileReader.WriteFile(lines);
        return success;
        
    }
}