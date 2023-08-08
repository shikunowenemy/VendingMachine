public class Good
{
    private string _name;
    private int _price;
    private int _amount;

    public Good(string name, int cost, int amount)
    {
        this._name = name;
        this._price = cost;  
        this._amount = amount;
    }

    public string Name
    {
        get 
        {
            return _name; 
        }
    }

    public int Price
    {
        get 
        { 
            return _price; 
        } 
    }

    public int Amount
    {
        get 
        { 
            return _amount; 
        }

        set 
        {
            if (_amount > 0) _amount = value;
            else Console.WriteLine($"{_name} закончился");
        }   
    }
}
