var products = new List<Product>
{
    new(1, "Defter", "Kırtasiye", 40m, 5),
    new(2, "Kalem", "Kırtasiye", 12m, 10),
    new(3, "Kupa", "Ev", 65m, 3)
};

foreach (var product in products.OrderBy(p => p.Name))
    Console.WriteLine($"{product.Id}: {product.Name} - {product.Price:C} - stok {product.Stock}");

Console.Write("Ürün kodu: ");
if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Geçersiz kod."); return; }
var selected = products.FirstOrDefault(p => p.Id == id);
if (selected is null) { Console.WriteLine("Ürün bulunamadı."); return; }
Console.Write("Adet: ");
if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
{ Console.WriteLine("Geçersiz adet."); return; }
try
{
    selected.Order(quantity);
    Console.WriteLine($"Sipariş toplamı: {selected.Price * quantity:C}; kalan stok: {selected.Stock}");
}
catch (InvalidOperationException error) { Console.WriteLine(error.Message); }

internal sealed class Product(int id, string name, string category, decimal price, int stock)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Category { get; } = category;
    public decimal Price { get; } = price;
    public int Stock { get; private set; } = stock;
    public void Order(int quantity)
    {
        if (quantity > Stock) throw new InvalidOperationException("Yeterli stok yok.");
        Stock -= quantity;
    }
}
