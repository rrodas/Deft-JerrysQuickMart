using JerrysConsole;

namespace JerrysTest;

[TestClass]
public class InventoryTest
{
    [TestMethod]
    public void GetInventory_ShouldGetInventory()
    {
        //Arramge
        var actualInventory = new List<Inventory>();
        var expectedInventory = new List<Inventory>();
        ArrangeInventory(expectedInventory);
        InventoryDomain inventoryDomain = new InventoryDomain();

        //Act        
        actualInventory = inventoryDomain.GetInventory();

        //Assert
        CollectionAssert.Equals(expectedInventory, actualInventory);

    }

    private static void ArrangeInventory(List<Inventory> expectedInventory)
    {
        expectedInventory.Add(new Inventory
        {
            item = new Item
            {
                name = "Milk",
                regularPrice = 3.75,
                memberPrice = 3.50,
                taxStatus = "tax-Exempt"
            },
            quantity = 5
        });

        expectedInventory.Add(new Inventory
        {
            item = new Item
            {
                name = "Red Bull",
                regularPrice = 4.30,
                memberPrice = 4.00,
                taxStatus = "Taxable"
            },
            quantity = 10
        });
    }

     [TestMethod]
    public void SaveInventory_ShouldUpdateInventoryAfterCheckOut()
    {
        var cart = new Cart();
        var expectedSelectedtItem = new SelectedItem();  
        
        ArrangeSelectedItem(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);

        InventoryDomain inventoryDomain = new InventoryDomain();        
        var actualInventory = inventoryDomain.UpdateInventory(cartAddedItem);

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
    public void UpdateInventory_ShouldWriteFileInventory()
    {
        var inventory = new List<Inventory>();
        var cart = new Cart();
        var expectedSelectedtItem = new SelectedItem();         
        
        ArrangeSelectedItem(expectedSelectedtItem);
        CartDomain cartDomain = new CartDomain();
        var cartAddedItem = cartDomain.AddSelectedItemToCart(cart,expectedSelectedtItem);

        InventoryDomain inventoryDomain = new InventoryDomain();        
        inventory = inventoryDomain.UpdateInventory(cartAddedItem);

        var iv = new InventoryRepository();
        int success = iv.SaveInventory(inventory);
        
        Assert.IsTrue(success == 1);
    }
        
}

