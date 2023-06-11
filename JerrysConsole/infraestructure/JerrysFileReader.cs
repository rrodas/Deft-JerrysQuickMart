namespace JerrysConsole;

public class JerrysFileReader{
    private string fileName;
    private string filePath;

    public JerrysFileReader(string _fileName){
        fileName = _fileName;
        var currentPath = Directory.GetCurrentDirectory();         
        currentPath ="/Users/user/Documents/JerrysQuickMart/JerrysConsole/"; //from test

        filePath = currentPath + Path.DirectorySeparatorChar + fileName;  //inventory.txt        
    }

    public List<String> ReadFile(){
        List<String> lines = new List<string>();
        String line = String.Empty;
        try
        {
            StreamReader sr = new StreamReader(filePath);            
            line = sr.ReadLine();     
            lines.Add(line);            
            while (line != null)
            {                
                line = sr.ReadLine();                
                lines.Add(line);
            }            
            sr.Close();            
            return lines;
        }
        catch(Exception e)
        {
            throw e;
        }        

    }

    public int WriteFile(List<String> lines){
        
        try
        {   
            StreamWriter sw = new StreamWriter(filePath);

            lines.ForEach(line =>
            {
                sw.WriteLine(line);
            });
            sw.Close();            
            return 1;
        }
        catch(Exception e)
        {            
            Console.WriteLine("Exception: " + e.Message);
            return 0;
        }
    }
}