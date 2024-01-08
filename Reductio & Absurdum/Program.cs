List<ProductType> category = new List<ProductType>()
{
    new ProductType()
    {
        Name = "Apparel",
        Id = 1
    },
    new ProductType()
    {
        Name = "Potions",
        Id = 2
    },
    new ProductType()
    {
        Name = "Enchanted Objects",
        Id = 3
    },
    new ProductType()
    {
        Name = "Wands",
        Id = 4
    }
};

List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "Witch Hat",
        Price = 15.00M,
        Available = true,
        ProductTypeId = 1,
        DateStocked = new DateTime(2022, 12, 1)
    },
    new Product()
    {
        Name = "Love Potion",
        Price = 12.00M,
        Available = true,
        ProductTypeId = 2,
        DateStocked = new DateTime(2023, 2, 4)
    },
    new Product()
    {
        Name = "Enchanted Book",
        Price = 20.00M,
        Available = true,
        ProductTypeId = 3,
        DateStocked = new DateTime(2023, 10, 5)
    },
    new Product()
    {
        Name = "Wizard Wand",
        Price = 15.00M,
        Available = true,
        ProductTypeId = 4,
        DateStocked = new DateTime(2022, 5, 14)
    }
};

//Create a main menu with working options to view all products,
//add a product to the inventory, delete a product from the inventory,
//and update a product's details

string greeting = @"Welcome to Reductio & Absurdum,
          Your one stop shop for all things magic!";

Console.WriteLine(greeting);
MainMenu();

void MainMenu()
{
    string choice = null;
    while (choice == null)
    {
        Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. View All Products
                        2. Add A Product To The Inventory
                        3. Delete A Product From The Inventory
                        4. Update A Product's Details
                        5. View Products By Category
                        6. View Available Products
                        ");

        choice = Console.ReadLine();

        if (choice == "0")
        {
            Console.WriteLine("Have a hocus pocus day! Goodbye!");
        }
        else if (choice == "1")
        {
            ViewProductDetails();
        }
        else if (choice == "2")
        {
            NewProduct();
        }
        else if (choice == "3")
        {
            DeleteProduct();
        }
        else if (choice == "4")
        {
            UpdateAProduct();
        }
        else if (choice == "5")
        {
            ProductsByCategory();
        }
        else if (choice == "6")
        {
            List<Product> unsoldProducts = products.Where(p => p.Available).ToList();
            foreach (Product prod in unsoldProducts)
            {
                Console.WriteLine(prod.Name);
            }
            MainMenu();
        }
    }
}

void ViewProductDetails()
{
    ListProducts();

    Product chosen = null;
    while (chosen == null)
    {
        Console.WriteLine("Please enter a product number:");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosen = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please enter an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do better!");
        }
    }

    Console.WriteLine(@$"You Chose:
    {chosen.Name}, which {(chosen.Available ? $"has been available for {chosen.DaysOnShelf} days" : "is not available")},
    and the price is ${chosen.Price}.");
    MainMenu();
}


void ListProducts()
{
    decimal totalValue = 0.0M;
    foreach (Product product in products)
    {
        if (product.Available)
        { 
            totalValue += product.Price;
        }
    }

    Console.WriteLine($"Total Inventory Value = ${totalValue}");
    Console.WriteLine("Products: ");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }
}

void NewProduct()
{
    Product newProduct = new Product()
    {
        Name = "",
        Price = 0M,
        Available = true,
        ProductTypeId = -1
    };

    string response = "";
    decimal price = 0M;
    bool validPrice = false;


    Console.WriteLine("What is the name of the new product?");
    while (response == "")
    {
        response = Console.ReadLine();
        newProduct.Name += response;
    }

    Console.WriteLine($"What is the price of the {newProduct.Name}?");
    response = Console.ReadLine();

    validPrice = decimal.TryParse(response, out price);

        if (validPrice == true)
        {
            newProduct.Price += price;
        }
        else
        {
        Console.WriteLine("Sorry, you entered an invalid price, please try again.");
        }

    string choose = null;
    while (choose == null)
    {
    Console.WriteLine(@$"Choose a category for {newProduct.Name}:
                        1. Apparel
                        2. Potions
                        3. Enchanted Objects
                        4. Wands");

    choose = Console.ReadLine();

    if (choose == "1")
        {
            newProduct.ProductTypeId = 1;
        }
    else if (choose == "2")
        {
            newProduct.ProductTypeId = 2;
        }
    else if (choose == "3")
        {
            newProduct.ProductTypeId = 3;
        }
    else if (choose == "4")
        {
            newProduct.ProductTypeId = 4;
        }

        Console.WriteLine($@"Thank you for adding your new product!
            You've added {newProduct.Name}, which costs ${newProduct.Price}
            and is currently available.");
    }

    products.Add(newProduct);
    MainMenu();
}

void DeleteProduct()
{

    Console.WriteLine("Please select which product you would like to delete.");

    ListProducts();

    Product chosen = null;
    string answer = "";
    while (chosen == null)
    {
        Console.WriteLine("Please enter a product number:");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosen = products[response - 1];
            Console.WriteLine(@$"Are you sure you want to delete {chosen.Name}?
                                    1. Yes
                                    2. No");

            answer = Console.ReadLine();

            if (answer == "1")
            {
                products.Remove(chosen);
                Console.WriteLine($"You have successfully deleted {chosen.Name}!");
                MainMenu();
            }
            else if (answer == "2")
            {
                 MainMenu();
            }
            else
            {
                Console.WriteLine("Sorry, you've entered an invalid entry.");
                MainMenu();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please enter an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do better!");
        }
    }
}

void UpdateAProduct()
{

    Console.WriteLine("Select a product to update:");

    ListProducts();

    string answer = "";
    Product chosen = null;
    while (chosen == null)
    {
        Console.WriteLine("Please enter a product number:");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosen = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please enter an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do better!");
        }
    }

    Console.WriteLine(@$"Select what you would like to update:
                            1. Name: {chosen.Name}
                            2. Price: {chosen.Price}
                            3. Available: {chosen.Available}
                            4. Category: {chosen.ProductTypeId}");

    answer = Console.ReadLine();
    bool validNumber = false;
    decimal price = 0M;

    if (answer == "1")
    {
        Console.WriteLine("Please enter the new name of the product.");
        answer = Console.ReadLine();
        chosen.Name = answer;
        Console.WriteLine($"Your product name has been updated to {answer}!");
        MainMenu();
    }
    else if (answer == "2")
    {
        Console.WriteLine("Please enter the updated price of the product.");
        answer = Console.ReadLine();
        validNumber = decimal.TryParse(answer, out price);

        if (validNumber == true)
        {
            chosen.Price = price;
            Console.WriteLine($"Your product price has been updated to ${price}!");
            MainMenu();
        }
        else
        {
            Console.WriteLine("Sorry, you entered an invalid price, please try again.");
        }
    }
    else if (answer == "3")
    {
        Console.WriteLine(@"Is this product available?
                                1. Yes
                                2. No");
        answer = Console.ReadLine();

        if (answer == "1")
        {
            chosen.Available = true;
        }
        else if (answer == "2")
        {
            chosen.Available = false;
        }

        Console.WriteLine($"You've updated your product to {(chosen.Available ? "available" : "not Available")}");
        MainMenu();
    }
    else if (answer == "4")
    {
        Console.WriteLine(@$"Please select the updated category for your product:
                        1. Apparel
                        2. Potions
                        3. Enchanted Objects
                        4. Wands");

        answer = Console.ReadLine();

        if (answer == "1")
        {
            chosen.ProductTypeId = 1;
        }
        else if (answer == "2")
        {
            chosen.ProductTypeId = 2;
        }
        else if (answer == "3")
        {
            chosen.ProductTypeId = 3;
        }
        else if (answer == "4")
        {
            chosen.ProductTypeId = 4;
        }

        Console.WriteLine($"You've updated your product to category {chosen.ProductTypeId}");
        MainMenu();
    }
}

void ProductsByCategory()
{
    string answer = "";
    Console.WriteLine(@"Please select a category to view:
                        1. Apparel
                        2. Potions
                        3. Enchanted Objects
                        4. Wands");

    answer = Console.ReadLine();

    if (answer == "1")
    {
        sort(1);
        MainMenu();
    }
    else if (answer == "2")
    {
        sort(2);
        MainMenu();
    }
    else if (answer == "3")
    {
        sort(3);
        MainMenu();
    }
    else if (answer == "4")
    {
        sort(4);
        MainMenu();
    }

    void sort(int response)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].ProductTypeId == response)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name}");
            }
        }
    }
}
