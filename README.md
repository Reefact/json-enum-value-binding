# Json Enum Naming

## Présentation

**Librairie de personnalisation des valeurs d'énumération pour la sérialisation / désérialisation au format JSON et binding ASP.Net**

Tout comme l'attribut `JsonPropertyNameAttribute` permet une personnalisation des noms de propriétés, `JsonEnumValueAttribute` accomplit la même tâche pour les valeurs d'énumérations.

Renforcez la stabilité de vos contrats d'API et prenez le contrôle total sur la sérialisation JSON de vos énumérations grâce à la librairie `Reefact.JsonEnumValueBinding`. Conçu pour les développeurs qui exigent une sécurité contractuelle sans compromis, cette librairie simplifie la personnalisation des valeurs d'énumération, garantissant ainsi une compatibilité à long terme avec vos clients.

## Fonctionnalités Principales

🔧 **Contrat sur mesure** : L'attribut `JsonEnumValueAttribute` vous permet de nommer explicitement chaque valeur d'énumération. Vous maintenez ainsi une relation claire et documentée entre vos valeurs d'énumération et les valeurs JSON correspondants dans vos contrats d'API.

⚓ **Stabilité** : Avec la personnalisation fine offerte par l'attribut `JsonEnumValueAttribute`, les modifications futures de vos valeurs d'énumération en interne ne compromettent plus la compatibilité des clients. Simplifiez la sérialisation JSON sans sacrifier la clarté ni la stabilité des contrats. La sécurité contractuelle est renforcée, permettant des refactorisations sans risque de rupture.

📚 **Documentation simplifiée** : L'utilisation de l'attribut `JsonEnumValueAttribute` favorise la documentation intrinsèque de vos contrats en offrant aux développeurs qui utilisent vos APIs un aperçu immédiat des intentions derrière chaque valeur d'énumération.

🌐 **Compatible avec ASP.NET et `System.Text.Json`** : Utilisez `JsonEnumValueAttribute` pour renforcer la personnalisation lors du binding dans le contexte ASP.NET, garantissant que vos contrats API restent robustes même dans des environnements complexes.

## Utilisation

Comment Commencer :

- Installez le package NuGet "Reefact.JsonEnumValueBinding" pour votre projet

```console
dotnet add package Reefact.JsonEnumValueBinding
```

- Activez la fonctionnalité dans le fichier `Startup.cs`

```csharp
public class Startup {

    public void ConfigureServices(IServiceCollection services) {
        services.AddControllers()
                .AddJsonEnumValueBinding(); // Activation du binding
        // ...
    }
    // ...
}
```

- Appliquez l'attribut `JsonEnumValueAttribute` aux valeurs d'énumération dans votre code.

```csharp
public enum ProductStatus {
    [JsonEnumValue("available")]     Available,
    [JsonEnumValue("out_of_stock")]  OutOfStock,
    [JsonEnumValue("discontinued")]  Discontinued
}
```

Ainsi, l'exemple ci-dessous:

```csharp
using Microsoft.AspNetCore.Mvc;

public class Product {
    public [JsonPropertyName("name")]   string Name     { get; set; }
    public [JsonPropertyName("status")] string Status   { get; set; }
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

produit le résultat suivant:

```json
{
    "name": "Widget",
    "status": "out_of_stock"
}
```

Le binding est également pris en charge:

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase {

    private readonly List<Product> _products = new List<Product>     {
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

L'appel suivant:

```ssh
curl http://localhost:5000/api/products/out_of_stock
```

produit le résultat suivant:

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

Démarrez dès aujourd'hui et obtenez des contrats d'API stables, une documentation claire et une sérénité lors de vos refactorings.