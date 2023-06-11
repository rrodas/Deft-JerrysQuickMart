using JerrysConsole;

namespace JerrysTest;

[TestClass]
public class CartTest
{
    [TestMethod]
    public void AddItem_ShouldAddSelectedItemToCart()
    {
        //Arramge
        var actualSelectedItem = new SelectedItem();
        var expectedSelectedtItem = new SelectedItem();  
        var cart = new Cart();    
        ArrangeSelectedItem(expectedSelectedtItem);

        //Act        
        CartDomain cartDomain = new CartDomain();
        var cartAdded = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);

        //Assert
        CollectionAssert.Equals(expectedSelectedtItem, cartAdded.selectedItem);

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

    [TestMethod]
    public void RemoveItem_ShouldRemoveSelectedItemToCart()
    {
        //Variables
        var expectedSelectedtItem = new SelectedItem();  
        var cart = new Cart();

        //Act        
        ArrangeSelectedItem(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        var cartRemovedItem = cartDomain.RemoveSelectedItemFromCart(cartAddedItem, expectedSelectedtItem);

        CollectionAssert.Equals(cartRemovedItem , cart);

    }

     [TestMethod]
    public void ItemsSold_ShouldCountSelectedItems(){
        //Variables
        var expectedSelectedtItem = new SelectedItem();  
        var cart = new Cart();
        int expectedCount = 2;

        //Act        
        ArrangeSelectedItem(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        
        var count = cartDomain.CountSelectedItemsInsideCart(cart);
       
        Assert.AreEqual(expectedCount, count);
       
    }

    [TestMethod]
    public void ItemsSubtotal_ShouldBringSubtotalOfSelectedItems(){
        //Variables
        var expectedSelectedtItem = new SelectedItem();  
        var cart = new Cart();
        cart.customerType = 2; //reward
        double expectedSubtotal = 7.00;

        //Act        
        ArrangeSelectedItem(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        double subTotal = cartDomain.SubTotalSelectedItemsInsideCart(cartAddedItem);

        Assert.AreEqual(expectedSubtotal, subTotal);
    }

    [TestMethod]
    public void ItemsTax_ShouldBringTaxOfSelectedItems(){
         //Variables
        var expectedSelectedtItem = new SelectedItem();  
        var cart = new Cart();
        cart.customerType = 2; //reward
        double expectedTaxes = 0.52;

        //Act        
        ArrangeSelectedIteWithTax(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        double tax = cartDomain.TaxForSelectedItemsInsideCart(cart);
        
         Assert.AreEqual(expectedTaxes, tax);

    }

    private static void ArrangeSelectedIteWithTax(SelectedItem expectedSelectedtItem)
    {
       
        var item = new Item
            {
                name = "Red Bull",
                regularPrice = 4.30,
                memberPrice = 4.00,
                taxStatus = "Taxable"
            };
        var quantity = 2;

        expectedSelectedtItem.itemSelected = item;
        expectedSelectedtItem.quantitySelected = quantity;
       
    }

     [TestMethod]
    public void CartTotal_ShouldBringTaxPlusSubtotalOfSelectedItems(){
         //Variables
        var expectedSelectedtItem = new SelectedItem();  
        var cart = new Cart();
        cart.customerType = 2; //reward
        double expectedTotal = 8.52;

        //Act        
        ArrangeSelectedIteWithTax(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        double total = cartDomain.GetTotalForSelectedItemsInsideCart(cart);

        Assert.AreEqual(expectedTotal, total);

    }
    

    [TestMethod]
    public void CartSaved_ShouldBringSavedValueForRewardCustomers(){

        var expectedSelectedtItem = new SelectedItem();  
        ArrangeSelectedItem(expectedSelectedtItem);

        var cart = new Cart();
        cart.customerType = 2; //reward
        double expectedSaved = 0.25;

        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);
        double saved = cartDomain.GetSavedAmount(cart);       

        Assert.AreEqual(expectedSaved, saved);       
        
    }
    
}

