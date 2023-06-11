using JerrysConsole;

namespace JerrysTest;

[TestClass]
public class InvoiceTest
{
    [TestMethod]
    public void GetInvoice_ShouldGetInvoice()    
    {
        var cart = new Cart();
        cart.customerType = 2;
        cart.cash = 20;
        var expectedSelectedtItem = new SelectedItem();  
        
        ArrangeSelectedItem(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        
        InvoiceRepository invoiceRepository = new InvoiceRepository();
        int success = invoiceRepository.SaveInvoice(cart);        
        
        Assert.IsTrue(success == 1);

    }

    private static void ArrangeSelectedItem(SelectedItem expectedSelectedtItem)
    {
       
        var item = new Item
            {
                name = "Milk",
                regularPrice = 3.75,
                memberPrice = 3.50,
                taxStatus = "tax-Exempt"
            };
        var quantity = 1;

        expectedSelectedtItem.itemSelected = item;
        expectedSelectedtItem.quantitySelected = quantity;
       
    }


}