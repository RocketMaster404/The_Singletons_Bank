# The Singletons Bank

Detta dokument beskriver strukturen och arkitekturen för **The Singletons Bank**, en konsolbaserad bankapplikation skriven i C#. Nedan följer en genomgång av projektets klasser, objekt och deras ansvarsområden.

Länk till Trello: https://trello.com/b/P8tRXuvY/bankomat-sut25

## 1. Programflöde och Kärna
Dessa klasser hanterar applikationens start, livscykel och centrala datalagring.

*   **`Program.cs`**
    *   Innehåller `Main`-metoden som är programmets startpunkt.
    *   Anropar `RunProgram.Run()` för att starta applikationen.

*   **`RunProgram.cs`**
    *   Hanterar huvudloopen för programmet ("Game loop").
    *   Initierar systemet (skapar testanvändare, räknar ränta).
    *   Styr flödet mellan inloggningsvyn och de specifika vyerna för kund (`Customer`) eller administratör (`Admin`).
    *   Kör transaktionskön med jämna mellanrum.

*   **`Bank.cs`**
    *   Agerar databas i minnet (runtime) och centralt nav.
    *   Håller en statisk lista över alla användare (`_users`).
    *   Hanterar inloggningslogik (`LogIn`) och validering av användare.
    *   Innehåller metoder för att skapa nya användare och köra månatliga ränteberäkningar.

## 2. Användare och Roller
Applikationen använder arv för att hantera olika typer av behörigheter.

*   **`User.cs` (Abstrakt basklass)**
    *   Grundläggande egenskaper för alla användare: användarnamn, lösenord, inloggningsförsök och om kontot är blockerat.
    *   Innehåller logik för inloggningskontroll (`Logincheck`).

*   **`Customer.cs` (Ärver av `User`)**
    *   Representerar bankens kunder.
    *   Håller listor för kundens vanliga konton (`_accounts`), sparkonton (`_savingAccounts`) och lån (`_loans`).
    *   Har en inkorg (`_inbox`) för meddelanden från banken (t.ex. om lån).
    *   Innehåller logik för att beräkna kreditvärdighet (`CredibilityCalculator`) och hantera låneförslag.

*   **`Admin.cs` (Ärver av `User`)**
    *   Representerar bankens administratörer.
    *   Har behörighet att se och hantera inkomna låneansökningar (`Loantickets`).
    *   Kan skapa nya användare och avblockera låsta användarkonton.
    *   Kan skicka fakturor och meddelanden till kunder.

## 3. Konton och Ekonomi
Klasser som hanterar pengar, valutor och bankkonton.

*   **`Account.cs`**
    *   Basklass för bankkonton.
    *   Genererar unika kontonummer.
    *   Hanterar saldo (`_balance`), valuta (`_currency`) och insättningar/uttag.

*   **`SavingAccount.cs` (Ärver av `Account`)**
    *   Specialiserad kontotyp för sparande.
    *   Har en räntesats (`_interestRate`) och logik för att applicera ränta på saldot.

*   **`Loan.cs`**
    *   Representerar ett lån.
    *   Innehåller information om lånebelopp och ränta.
    *   Innehåller statisk logik för att ansöka om lån och kontrollera om en kund får ta lån (baserat på inkomst/tillgångar).

*   **`Currency.cs`**
    *   Hanterar växelkurser (SEK, USD, EUR).
    *   Innehåller metoder för att konvertera belopp mellan olika valutor (`ConvertCurrency`).
    *   Tillåter admin att ändra växelkurser.

## 4. Transaktionssystem
Ett system för att hantera överföringar, historik och köer.

*   **`Transaction.cs`**
    *   Hanterar logiken för att utföra överföringar (både interna mellan egna konton och externa till andra användare).
    *   Ansvarar för att skriva ut transaktionshistorik till konsolen.
    *   Validerar att täckning finns och att kontonummer är korrekta.

*   **`TransactionHistory.cs`**
    *   En datamodell (objekt) som sparar information om en genomförd transaktion (avsändare, mottagare, belopp, datum, typ).

*   **`PendingTransaction.cs`**
    *   En datamodell för en transaktion som ligger i kö och väntar på att genomföras.

*   **`TransactionQueue.cs`**
    *   Simulerar bankens transaktionstid.
    *   Håller en kö (`Queue`) av väntande transaktioner.
    *   `RunQueue()` bearbetar kön och slutför överföringen av pengar mellan konton.

## 5. Gränssnitt och Verktyg
Hjälpklasser för att hantera konsolgränssnittet.

*   **`Menu.cs`**
    *   Innehåller alla utskrifter av menyer (Inloggning, Huvudmeny Kund, Huvudmeny Admin).
    *   Hanterar `switch`-satserna som styr vad som händer när användaren gör ett val i menyn.

*   **`Utilities.cs`**
    *   Statisk hjälpklass.
    *   Hanterar inmatning från användaren (säkerställer att man skriver in siffror när det krävs, etc.).
    *   Sköter färgändringar i konsolen och utskrift av ASCII-art.

## 6. Databas och Filhantering
Klasser som hanterar persistens genom att spara och ladda data från textfiler, så att information inte går förlorad när programmet stängs av.

*   **`DatabaseLogins.cs`**
    *   Ansvarar för att spara och läsa in användarinformation från filen `Logins.txt`.
    *   Hanterar användarnamn, lösenord, administratörsstatus samt om användaren är blockerad.
    *   Säkerställer att användarlistan uppdateras vid programstart.

*   **`DatabaseAccounts.cs`**
    *   Ansvarar för att spara och läsa in finansiell data från filen `Accounts.txt`.
    *   Hanterar lagring av kunders vanliga konton (`Account`), sparkonton (`SavingAccount`) och lån (`Loan`).
    *   Ser till att rätt konton och lån kopplas till rätt kund (`Customer`) när datan laddas in i systemet.
