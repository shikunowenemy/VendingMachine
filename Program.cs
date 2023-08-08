#region Initialization of base variables

int balance = 0;
List<int> coinTypes = new List<int>() { 1, 2, 5, 10 };
coinTypes.Sort();
List<Good> goods = new List<Good>()
{
    new Good("Шоколадка", 80, 2),
    new Good("Чипсы", 125, 5),
    new Good("Кока-кола", 94, 3),
    new Good("Сок", 78, 3),
    new Good("Печенья", 45, 5),
    new Good("Дудка", 899, 1),
    new Good("Сухарики", 24, 2),
    new Good("Плов", 219, 2),
    new Good("Шашлык", 349, 0),
    new Good("Салфетки", 15, 2)
};

#endregion
void PrintError()
{
    Console.WriteLine("Некорректные параметры, попробуйте еще раз");
}

void ShowBalance()
{
    Console.WriteLine($"Баланс: {balance} руб.");
}

#region AddMoney Methods
int InputMoney(string inputMessage)
{
    Console.Write(inputMessage);
    bool isCorrectInput = int.TryParse(Console.ReadLine(), out int inputedMoney);
    if (isCorrectInput == false || inputedMoney < 0)
    {
        PrintError();
        return InputMoney(inputMessage);
    }
    else
    {
        return inputedMoney;
    }
}

void PayCard()
{
    int inputedMoney = InputMoney("Введите сумму: ");
    balance += inputedMoney;
    Console.WriteLine($"{inputedMoney} руб. добавлены на баланс");
}

void PayCoins()
{
    int coinsValue = 0;
    foreach (int coinType in coinTypes)
    {
        coinsValue += InputMoney($"Введите количество монет номиналом \"{coinType}\": ") * coinType;
    }
    balance += coinsValue;
    Console.WriteLine($"{coinsValue} руб. добавлены на баланс");
}

#endregion
void AddMoney()
{
    Console.WriteLine("Тип оплаты: монеты или карта?");
    string paymentType = Console.ReadLine().ToLower().Trim();

    if (paymentType == "карта")
    {
        PayCard();
    }
    else if (paymentType == "монеты")
    {
        PayCoins();
    }
    else
    {
        PrintError();
    }
}


void GetChange()
{
    if (balance <= 0)
    {
        Console.WriteLine("Невозможно выдать сдачу.");
        return;
    }
    List<int> reversedCointTypes = coinTypes;
    reversedCointTypes.Reverse();
    Console.WriteLine($"К сдаче: ");
    foreach (int coinType in reversedCointTypes)
    {
        int changeCoins = balance / coinType;
        balance -= changeCoins * coinType;
        if (changeCoins != 0) Console.WriteLine($"{changeCoins} номиналом {coinType}");
    }
}

void ShowGoods()
{
    if (goods.Count == 0)
    {
        Console.WriteLine("Автомат пустой.");
        return;
    }

    Console.WriteLine($"id\tНаименование\tСтоимость\tКоличество");

    for (int i = 0; i < goods.Count; i++)
    {
        string goodInformation = String.Format($"{i,2}\t{goods[i].Name,12}\t{goods[i].Price,9}\t{goods[i].Amount,10}");
        Console.WriteLine(goodInformation);
    }
}

void BuyGood(int id, int count)
{
    if (count <= 0)
    {
        PrintError();
        return;
    }

    if (id < 0 || id > goods.Count)
    {
        Console.WriteLine("Данной позиции не существует.");
        return;
    }

    Good selectedGood = goods[id];

    if (count > selectedGood.Amount)
    {
        Console.WriteLine("В автомате нет такого количества товаров.");
        return;
    }

    int cost = count * selectedGood.Price;

    if (cost > balance)
    {
        Console.WriteLine($"На балансе не хватает {cost-balance} руб. для покупки");
        return;
    }

    Console.WriteLine($"Вы купили {count} шт. \"{selectedGood.Name}\" за {cost} рублей");

    balance -= cost;
    selectedGood.Amount -= count;
}

#region Main

Console.WriteLine(
"Введите команду: \n" +
"   * AddMoney: добавляет на баланс автомата денег;\n" +
"   * GetChange: обнуляет баланс и выдает сдачу монетами номиналом 1, 2, 5 и 10 рублей, от большей монеты к меньшей;\n" +
"   * ShowGoods: Отображает столбиком все товары и их количество в автомате;\n" +
"   * BuyGood {id} {count}: автомат выдает товар в определенном количестве и снимает с баланса деньги.");

while (true)
{
    ShowBalance();
    Console.WriteLine();

    string userInput = Console.ReadLine();
    string[] userCommands = userInput.Split();
    switch (userCommands[0].ToLower())
    {
        case "addmoney":
            AddMoney();
            break;
        case "getchange":
            GetChange();
            break;
        case "showgoods":
            ShowGoods();
            break;
        case "buygood":
            if (userCommands.Length != 3)
            {
                PrintError();
                break;
            }

            bool isCorrectInputId = int.TryParse(userCommands[1], out int id);
            bool isCorrectInputCount = int.TryParse(userCommands[2], out int count);

            if (isCorrectInputId == false || isCorrectInputCount == false)
            {
                PrintError();
                break;
            }

            BuyGood(id, count);
            break;
        default:
            PrintError();
            break;
    }

    Console.WriteLine("\nВведите команду:");
    continue;
}

#endregion