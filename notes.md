# Notes

## Waarom Blazor?

- C#
- 1 solution front/back
- delen van code front/back
  - kan ook met JS
  - gemak is in .NET wat beter
- Microsoft
- Component-based development - componenten
  - content projection is primo!

### Nadelen van Blazor

- HMR - hot module reloading  "live refresh"  MATIG  Ctrl+F5
- Tailwind - CSS-framework - inhaken op je buildproces
  - het kan wel...
- in je template <div @ref="MijnDiv"> ElementReference new Mock<ElementReference> struct

```html
<input @ref="InputDinges">   .FocusAsync()
```


## Paradigdmas der webdevelopment

- SPA - Single Page Application
  - 1 page to rule them all
  - dezelfde page, steeds nieuwe content
  - geen/heel weinig paginarefreshes
  - geen FOUC - Flash Of Unstyled Content
  - frameworks: React Angular (2MB) ~220kb Vue Svelte Lit (WC) Qwik Solid
  - hip
  - complexiteit
  - hoog niveau aan interactiviteit - gebruiksvriendelijkheid
  - grote bak aan JS
  - Blazor WebAssembly
  - Blazor Server
- SSR - Server-side rendering
  - complementair aan de SPA
  - snellere eerste paginarender
  - na eerste render is het gewoon weer een SPA
  - die interactieve delen opsturen: hydration  partial hydration streaming hydration
  - frameworks: Next.js (React) Nuxt.js (Vue) @angular/ssr SvelteKit QwikCity SolidStart
- SSG - Static site generation
  - bol.com/product/128578383/logitech-muis
  - tijdens het builden door de productcatalogus heengaan => logitech-muis-128964.html
  - libraries: Astro @angular/ssr Next.js 11ty HUGO
- MPA - Multi Page Application
  - 1996
  - multiple pages die naar elkaar linken
  - request => response
  - elke klik die je doet moet gecommuniceerd worden met server
  - HTML
  - platformen: PHP Java Spring/servlets ASP.NET Core MVC/Razor Pages
  - niet hip
  - FOUC
  - Blazor Static SSR

### Doelgroepen website/webapp

a11y - accessibility

3 doelgroepen:
- cognitief/slechthorende/slechtziende/blinde mensen
- alles ok mensen
- zoekmachines/bots/SEO/AI

## Blazor-edities

- Blazor Static SSR - code draait op server - Console.WriteLines NIET in browser
- Blazor Server - code draait op server - Console.WriteLines NIET in browser
- Blazor WebAssembly - code draait op client - Console.WriteLines wordt vertaald naar console.log() in browser

Blazor WebAssembly vs Blazor Server
- WebAssembly: geen JS, maar WebAssembly!  Assembly
  - code draait in browser
  - een heule bak aan code moet worden opgestuurd
    - al jouw C#-code
    - hele kleine geoptimaliseerde .NET => "Hello world" 7MB   25kb  3kb/s
      - compressie: gzip .gz  Brotli .br
- Server: elk klikje/tikje gaat naar de server over een WebSocket-verbinding (SignalR)
  - code draait op server
  - geen paginarefresh
  - ietsjes trager
  - veel openstaande SignalR-verbindingen
  - geen verbinding == geen bruikbare UI

## Forms

soorten forms
- registratieformulieren met 20+ velden
- belastingaangifte
- file uploads
- kleinere: filter/sorteer
- kleinere: button in een tabelrij
- Loginformulier - POST
  - GET staat in URL
  - URL's worden regelmatig gelogd
    - historie
  - proxyserver

Form actions: GET en POST

- GET
  - geen body
  - data doorgeven via URL  ?dit=iets&hoi=doei
    - 2kb?
    - geen honderden MB's
- POST
  - body <== heul groot worden
  - veel data in form
  - file upload
  - wordt niet gelogd
  - wordt ook niet geencrypt
    - body: ?dit=iets&hoi=doei


### Antiforgery token

Om XSRF te voorkomen:

```html
<form action="https://anderdomein.nl/save">
```

## Formvalidatie

[FluentValidation](https://docs.fluentvalidation.net/en/latest/) is erg tof want:

1. unittestbaar
2. rijker qua wat je aan validaties kan opnemen
3. async valideren
4. conditioneel valideren
5. nested properties

```cs
public class MyEntityValidator : AbstractValidator<MyEntity>
{
	public MyEntityValidator()
	{
		RuleFor(x => x.Title).NotEmpty().WithMessage("...");

		When(x => x.Title.Length > 20, () =>
		{
			RuleFor(
		});

		RuleForEach(x => x...)

	}
}
```

## Dependency injection

- injecteren van dependencies
  - "dingen die je nodig hebt om te werken"
- testability
- design++  SOLID
  - Single responsibility
  - Open-closed principle
  - Liskov substition principle
  - Interface segregation
  - Dependency inversion
- dezelfde instanties
- high cohesion, low coupling
  - interfaces
- het is vorm van Inversion of Control
  - praktisch - niet zelf `new ...();`

Repository
- EF Core is ook al een repository qua database-onafhankelijkheid
- kan nog steeds nuttig zijn als abstractielaag voor toegang tot data

```cs
// data dat nooit verwijderd wordt
context.Tabel.First().IsInactive = true;
context.Tabel.Where(x => x.IsInactive == false) 

// hele tabel dumpen is niet de bedoeling als er miljarden records in staan
context.Tabel.ToList();
```

Soorten dependencies:

- data access: repository
- state
- caching
- functioneel - stateless
  - logging
  - conversies
  - validators
  - kunnen deze dan niet gewoon `static`? Kan, maar is nogal irritant om te mocken
    ```cs
    DateTime.Now
    File.AppendAllTextAsync()
    ```
    - [Microsoft Fakes](https://visualstudio.microsoft.com/vs/compare/), maar is helaasch enkel voor VS Enterprise

## Visual Studio vs Rider

- Rider heeft veel sterkere mening over code formatten
- Visual Studio $$$ vs Rider $
- Visual Studio 2022 64 bit  2^64  2^32 = 2.147.xxx.xxx * 2 = 4GB
- Rider geeft aan wanneer een for="" niet matcht met een id
- Rider geeft rood bij niet-Git-toegevoegde files
  - [is wel instelbaar](https://www.jetbrains.com/help/rider/File_Status_Highlights.html)