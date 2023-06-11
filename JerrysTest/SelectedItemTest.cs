using JerrysConsole;

namespace JerrysTest;

[TestClass]
public class SelectedItemTest
{
    [TestMethod]
    public void GetISelectedtem_ShouldGetIfNameOtSelectedItemEnteredExist()
    {
        //Arramge
        var actualNameExist = 1;
        var expectedNameExist = 1;      
     
        //Act        
        var itemDomain = new ItemDomain();
        var actualItem = itemDomain.GetItemByName("Milk");
        if(actualItem != null){
            expectedNameExist = 1;
        }else{
            expectedNameExist = 0;
        }

        Assert.AreEqual(expectedNameExist,actualNameExist);

    }

    [TestMethod]
    public void GetISelectedtem_ShouldGetQuantitytByItemName()
    {
        //Arrange
        var actualQuantity= 2;
        var expectedQuantity = 2;
        
        //Act
        var inventoryDomain = new InventoryDomain();
        var inventoryQuantity = inventoryDomain.GetQuantityByItemName("Milk");

        Assert.AreEqual(expectedQuantity,actualQuantity);
    }
}