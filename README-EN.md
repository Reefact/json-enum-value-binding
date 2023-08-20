# Json Enum Naming

## Presentation

**Library for customizing enumeration values for serialization / deserialization in JSON format and ASP.Net binding**

Just as the `JsonPropertyNameAttribute` allows customization of property names, `JsonEnumValueAttribute` performs the same task for enumeration values.

Reinforce the stability of your API contracts and take full control over the JSON serialization of your enumerations with the `Reefact.JsonEnumValueBinding` library. Designed for developers who demand uncompromising contract security, this library simplifies the customization of enumeration values, guaranteeing long-term compatibility with your customers.

## Main features

🔧 **Customized contract**: The `JsonEnumValueAttribute` lets you explicitly name each enumeration value. You thus maintain a clear, documented relationship between your enumeration values and the corresponding JSON values in your API contracts.

⚓ **Stability**: With the fine-grained customization offered by the `JsonEnumValueAttribute`, future changes to your enumeration values internally no longer compromise client compatibility. Simplify JSON serialization without sacrificing contract clarity and stability. Contractual security is reinforced, allowing refactorings without risk of breakage.

📚 **Documentation simplified**: The use of the `JsonEnumValueAttribute` promotes the intrinsic documentation of your contracts, offering developers using your APIs immediate insight into the intentions behind each enumeration value.

🌐 **Compatible with ASP.NET and `System.Text.Json`**: Use `JsonEnumValueAttribute` to enforce customization when binding in the ASP.NET context, ensuring that your API contracts remain robust even in complex environments.

## Usage

How to get started :

- Install the NuGet "Reefact.JsonEnumValueBinding" package for your project.
- Activate the functionality in the `Startup.cs` file

```csharp
public class Startup {

    public void ConfigureServices(IServiceCollection services) {
        services.AddControllers()
                AddJsonEnumValueBinding(); // Binding enabled
        // ...
    }
    // ...
}
```

- Apply the `JsonEnumValueAttribute` to enumeration values in your code.

```csharp
public enum ProductStatus {
    [JsonEnumValue("available")] Available,
    [JsonEnumValue("out_of_stock")] OutOfStock,
    [JsonEnumValue("discontinued")] Discontinued
}
```

Thus, the example below:

```csharp
using Microsoft.AspNetCore.Mvc;

public class Product {
    public [JsonPropertyName("name")] string Name { get; set; }
    public [JsonPropertyName("status")] string Status { get; set; }
}

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase {

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id) {
        var product = new Product {
            Name = "Widget",
            Status = ProductStatus.OutOfStock
        };

        return Ok(product); 
    }
}
```

produces the following result:

```json
{
    "name": "Widget",
    "status": "out_of_stock"
}
```

Binding is also supported:

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase {

    private readonly List<Product> _products = new List<Product> {
        new Product { Name = "Widget 1", Status = ProductStatus.OutOfStock },
        new Product { Name = "Widget 2", Status = ProductStatus.Available },
        new Product { Name = "Widget 3", Status = ProductStatus.OutOfStock }
    };

    [HttpGet("{status}")]
    public IActionResult GetProductsByStatus([FromRoute] ProductStatus status) {
        var productsByStatus = _products.Where(p => p.Status == status).ToList();

        return Ok(productsByStatus);
    }
}
```

Next call:

```ssh
curl http://localhost:5000/api/products/out_of_stock
```

produces the following result:

```json
[
    {
        "name": "Widget 1",
        "status": "out_of_stock"
    },
    {
        "name": "Widget 3",
        "status": "out_of_stock"
    }
]
```

## Conclusion

Get started today and get stable API contracts, clear documentation and peace of mind when refactoring.