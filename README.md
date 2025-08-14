# ;PL - Szybkie pisanie polskich znaków diakrytycznych

**;PL** to narzędzie do szybkiego pisania polskich znaków diakrytycznych bez używania kombinacji Alt+klawisz. Bazuje na projekcie [Tajpi](https://github.com/CapacitorSet/tajpi) dostosowanym do języka polskiego.

## Jak to działa

Aplikacja pozwala na szybkie wpisywanie polskich znaków diakrytycznych poprzez kombinacje z średnikiem:

- `;a` → `ą`
- `;c` → `ć` 
- `;e` → `ę`
- `;l` → `ł`
- `;n` → `ń`
- `;o` → `ó`
- `;s` → `ś`
- `;x` → `ź`
- `;z` → `ż`

## Po co? 

Przeznaczone dla pisarzy, dziennikarzy i wszystkich osób, które piszą bardzo dużo tekstu w języku polskim, chcąc jednocześnie uniknąć **cieśni nadgarstka** ([Carpal Tunnel Syndrome](https://pl.wikipedia.org/wiki/Zesp%C3%B3%C5%82_cieśni_nadgarstka)).

### Problem z tradycyjnym pisaniem polskich znaków

Standardowe metody wpisywania polskich znaków diakrytycznych:
- **Klawisz AltGr + litera** - wymaga trzymania AltGr i może być niewygodne przy szybkim pisaniu. Dodatkowo wymaga precyzyjnej synchronizacji naciśnięcia AltGr z drugim klawiszem. W przypadku osób piszących powyżej 150 WPM zaczyna to być problemem. 
- **Autokorekta** - spowalnia pisanie i nie zawsze działa poprawnie

### Przykłady użycia

- `szko;la` → `szkoła`
- `;zd;xb;lo` → `źdźbło`  
- `w;edka` → `wędka`
- `krzy;z` → `krzyż`

### Funkcje specjalne

- `;;` → `;` (escape - wstawia zwykły średnik)
- `;q`, `;w`, `;r` itp. → `q`, `w`, `r` (litery bez polskiego odpowiednika)
- `Shift+;` → `:` (zwykły dwukropek)

## Instalacja i uruchomienie

1. Pobierz lub sklonuj repozytorium
2. Otwórz folder `Szybkopis`
3. Uruchom `dotnet build` lub `dotnet run`
4. Aplikacja pojawi się w zasobniku systemowym jako ikona z polską flagą i napisem ";PL"

## Obsługa

- **Lewy klik** na ikonie w zasobniku - włącz/wyłącz aplikację
- **Prawy klik** - pokaż menu z opcjami
- **Zamknięcie** - wybierz "Wyjście" z menu lub zamknij przez menedżer zadań

## Wymagania

- Windows 10/11
- .NET 9.0 (Windows Desktop)
- Uprawnienia administratora mogą być wymagane dla hooków klawiatury

## O projekcie

Projekt jest inspirowany aplikacją [Tajpi](https://github.com/CapacitorSet/tajpi) stworzoną dla języka esperanto. Oryginalna wersja VB6 została przepisana od zera w C# .NET 9.

## Autorzy i podziękowania

- Oryginalny projekt **Tajpi**: [CapacitorSet](https://github.com/CapacitorSet/tajpi)
- Adaptacja polska **;PL**: Utworzona z pomocą Claude (Anthropic)

## Licencja

Ten projekt zachowuje ducha open source oryginalnego Tajpi. Kod źródłowy jest dostępny do modyfikacji i dystrybucji.

---

# ;PL - Fast Polish Diacritics Typing Tool

**;PL** is a tool for fast typing of Polish diacritical characters without using Alt+code combinations. Based on the [Tajpi](https://github.com/CapacitorSet/tajpi) project by CapacitorSet, but adapted for the Polish language.

## How it works

The application allows fast input of Polish diacritical characters through semicolon combinations:

- `;a` → `ą`
- `;c` → `ć` 
- `;e` → `ę`
- `;l` → `ł`
- `;n` → `ń`
- `;o` → `ó`
- `;s` → `ś`
- `;x` → `ź`
- `;z` → `ż`

### Usage examples

- `szko;la` → `szkoła` (school)
- `;zd;xb;lo` → `źdźbło` (blade)
- `w;e;dka` → `wędka` (fishing rod)
- `krzy;z` → `krzyż` (cross)

### Special functions

- `;;` → `;` (escape - inserts regular semicolon)
- `;q`, `;w`, `;r` etc. → `q`, `w`, `r` (letters without Polish equivalent)
- `Shift+;` → `:` (regular colon)

## Installation and running

1. Download or clone the repository
2. Open the `Szybkopis` folder
3. Run `dotnet build` or `dotnet run`
4. The application will appear in the system tray as an icon with Polish flag and ";PL" text

## Usage

- **Left click** on tray icon - enable/disable the application
- **Right click** - show options menu
- **Exit** - select "Wyjście" from menu or close via task manager

## Requirements

- Windows 10/11
- .NET 9.0 (Windows Desktop)
- Administrator privileges may be required for keyboard hooks

## About the project

The project is inspired by the [Tajpi](https://github.com/CapacitorSet/tajpi) application created for Esperanto. Our implementation was rewritten from scratch in C# .NET 9 and adapted for Polish users' needs.

### Differences from Tajpi

- Adapted for Polish diacritical characters
- Rewritten in C# for better performance and Windows compatibility
- Simplified user interface  
- Faster character processing using SendInput API

## Authors and acknowledgments

- Original **Tajpi** project: [CapacitorSet](https://github.com/CapacitorSet/tajpi)
- Polish adaptation **;PL**: Created with assistance from Claude (Anthropic)

## License

This project maintains the open source spirit of the original Tajpi. Source code is available for modification and distribution.