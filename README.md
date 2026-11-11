# SearchNarutoAPI

Aplicação **Blazor WebAssembly (.NET 10)** que consome a [Dattebayo API](https://dattebayo-api.onrender.com) e permite listar (paginado) e buscar personagens de Naruto por nome.

Para cada personagem são exibidos: nome, imagem, clã/afiliação (`personal.clan` / `personal.affiliation`), lista de jutsus e patente ninja (`rank.ninjaRank`).

## Executar

```bash
dotnet run
```

> A API gratuita no Render pode levar alguns segundos para responder na primeira chamada.
