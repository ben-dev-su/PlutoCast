namespace PlutoCast.Desktop.Models;

public class Category(int categoryId, string categoryName)
{
    public int CategoryId { get; set; } = categoryId;
    public string CategoryName { get; set; } = categoryName;
}