namespace JerrysConsole;

public class Cart
{
    public int number{get; set;}
    public DateTime date{get; set;}
    public List<SelectedItem>? selectedItem {get; set;}
    public int itemsSold{get; set;}
    public double subTotal{get; set;}
    public double tax{get; set;}
    public double total{get; set;}
    public double cash{get; set;}
    public double change{get; set;}
    public double saved{get; set;}
    public int customerType{get; set;}  //1: Regular, 2: Reward
   
    
    
}
