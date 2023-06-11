using JerrysConsole;

namespace JerrysTest;

[TestClass]
public class ItemTest
{
    [TestMethod]
    public void GetItem_ShouldGetItemByName()
    {
        //Arramge
        var actualItem = new Item();
        var expectedItem = new Item();
        
        ArrangeItem(expectedItem);
        var itemDomain = new ItemDomain();

        //Act        
        actualItem = itemDomain.GetItemByName("Milk");
       
        //Assert
        CollectionAssert.Equals(expectedItem, actualItem);

    }

    private static void ArrangeItem(Item expectedItem)
    {
       expectedItem.name = "Milk";
       expectedItem.regularPrice= 3.75;
       expectedItem.memberPrice = 3.50;
       expectedItem.taxStatus = "Tax-Exempt";
    }

    [TestMethod]
    public void GetItem_ShouldGetItemIsTaxable()
    {
        //Arramge
        int actualStatus = 0;
        int expectedStatus = 0;
                
         //Act        
        ItemDomain itemDomain = new ItemDomain();
        actualStatus = itemDomain.ItemIsTaxable("Milk");

        //Assert
        Assert.AreEqual(expectedStatus,actualStatus );

    }

    [TestMethod]
    public void GetItem_ShouldGetItemPriceDependingOnTypeOfClient()
    {
        //Arramge
        double actualPrice = 0;
        double expectedPrice = 3.50;
        const int clientType = 2;//this is a rewarded client
                
         //Act        
        var itemDomain = new ItemDomain();
        actualPrice = itemDomain.ItemPriceForCustomerType(clientType, "Milk");

        //Assert
        Assert.AreEqual(expectedPrice, actualPrice);

    }

    [TestMethod]
    public void GetItem_ShouldGetTotalItemPriceForQuantityAndTypeOfClient()
    {
        //Arramge
        double actualTotal = 0;
        double expectedTotal = 7.00;
        const int quantity = 2;
        const int clientType = 2;        
         //Act        
        var itemDomain = new ItemDomain();
        actualTotal = itemDomain.TotalItemPriceForQuantityAndTypeOfClient(quantity, clientType, "Milk");

        //Assert
        Assert.AreEqual(expectedTotal, actualTotal);

    }


}

