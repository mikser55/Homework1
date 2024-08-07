using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        Product iPhone12 = new Product("iPhone 12");
        Product iPhone11 = new Product("iPhone 11");
        Warehouse warehouse = new Warehouse();
        Shop shop = new Shop(warehouse);

        warehouse.DeliverProduct(iPhone12, 10);
        warehouse.DeliverProduct(iPhone11, 1);
        warehouse.DisplayInventory();
        Cart cart = shop.CreateCart();

        try
        {
            cart.AddProduct(iPhone12, 4);
            cart.AddProduct(iPhone11, 3);
        }
        catch (InvalidOperationException error)
        {
            Console.WriteLine(error.Message);
        }

        cart.DisplayContents("Корзина: ");
        Order order = cart.PlaceOrder();
        Console.WriteLine(order.PaymentLink);

        try
        {
            cart.AddProduct(iPhone12, 9);
        }
        catch (InvalidOperationException error)
        {
            Console.WriteLine(error.Message);
        }

        Console.ReadKey();
    }
}

public class Product
{
    public Product(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }
}

public class Warehouse
{
    private readonly Dictionary<Product, int> _inventory = new Dictionary<Product, int>();

    public void DeliverProduct(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        if (_inventory.ContainsKey(product))
            _inventory[product] += quantity;
        else
            _inventory.Add(product, quantity);
    }

    public void DisplayInventory()
    {
        Console.WriteLine("Warehouse inventory:");

        foreach (var item in _inventory)
            Console.WriteLine($"{item.Key.Name} - {item.Value} units");

        Console.WriteLine();
    }

    public bool TryGetProduct(Product product, int requestedQuantity, out int availableQuantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (requestedQuantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(requestedQuantity));

        if (_inventory.TryGetValue(product, out availableQuantity) && availableQuantity >= requestedQuantity)
            return true;

        return false;
    }

    public void RemoveProduct(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        if (_inventory.ContainsKey(product))
        {
            _inventory[product] -= quantity;

            if (_inventory[product] <= 0)
                _inventory.Remove(product);
        }
        else
        {
            throw new InvalidOperationException($"Product {product.Name} not found in inventory");
        }
    }
}

public class Shop
{
    private readonly Warehouse _warehouse;

    public Shop(Warehouse warehouse)
    {
        _warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
    }

    public Cart CreateCart()
    {
        return new Cart(_warehouse);
    }
}

public class Cart
{
    private readonly Dictionary<Product, int> _products = new Dictionary<Product, int>();
    private readonly Warehouse _warehouse;

    public Cart(Warehouse warehouse)
    {
        _warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
    }

    public void DisplayContents(string title)
    {
        Console.WriteLine(title);

        foreach (var product in _products)
            Console.WriteLine($"{product.Key.Name} - Quantity: {product.Value}");

        Console.WriteLine();
    }

    public void AddProduct(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        if (_warehouse.TryGetProduct(product, quantity, out int availableQuantity))
        {
            if (_products.ContainsKey(product))
                _products[product] += quantity;
            else
                _products.Add(product, quantity);
        }
        else
        {
            throw new InvalidOperationException($"Not enough {product.Name} in stock. Available: {availableQuantity}");
        }
    }

    public Order PlaceOrder()
    {
        foreach (var item in _products)
            _warehouse.RemoveProduct(item.Key, item.Value);

        DisplayContents("Order placed: ");
        _products.Clear();

        return new Order();
    }
}

public class Order
{
    public string PaymentLink { get; } = "Thank you for your purchase!";
}