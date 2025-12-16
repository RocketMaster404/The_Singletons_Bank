# The Singletons Bank

This document describes the structure and architecture for **The Singletons Bank**, a console-based banking application written in C#. Below is an overview of the project's classes, objects, and their areas of responsibility.

## 1. Program Flow and Core
These classes handle the application's startup, lifecycle, and central data storage.

*   **`Program.cs`**
    *   Contains the `Main` method, which is the program's entry point.
    *   Calls `RunProgram.Run()` to start the application.

*   **`RunProgram.cs`**
    *   Handles the main loop of the program ("Game loop").
    *   Initializes the system (creates test users, calculates interest).
    *   Controls the flow between the login view and the specific views for customer (`Customer`) or administrator (`Admin`).
    *   Runs the transaction queue at regular intervals.

*   **`Bank.cs`**
    *   Acts as an in-memory database (runtime) and central hub.
    *   Holds a static list of all users (`_users`).
    *   Handles login logic (`LogIn`) and user validation.
    *   Contains methods to create new users and run monthly interest calculations.

## 2. Users and Roles
The application uses inheritance to handle different types of permissions.

*   **`User.cs` (Abstract base class)**
    *   Basic properties for all users: username, password, login attempts, and if the account is blocked.
    *   Contains logic for login checks (`Logincheck`).

*   **`Customer.cs` (Inherits from `User`)**
    *   Represents the bank's customers.
    *   Holds lists for the customer's regular accounts (`_accounts`), savings accounts (`_savingAccounts`), and loans (`_loans`).
    *   Has an inbox (`_inbox`) for messages from the bank (e.g., regarding loans).
    *   Contains logic to calculate creditworthiness (`CredibilityCalculator`) and handle loan proposals.

*   **`Admin.cs` (Inherits from `User`)**
    *   Represents the bank's administrators.
    *   Has permission to view and manage incoming loan applications (`Loantickets`).
    *   Can create new users and unblock locked user accounts.
    *   Can send invoices and messages to customers.

## 3. Accounts and Finance
Classes that handle money, currencies, and bank accounts.

*   **`Account.cs`**
    *   Base class for bank accounts.
    *   Generates unique account numbers.
    *   Handles balance (`_balance`), currency (`_currency`), and deposits/withdrawals.

*   **`SavingAccount.cs` (Inherits from `Account`)**
    *   Specialized account type for savings.
    *   Has an interest rate (`_interestRate`) and logic to apply interest to the balance.

*   **`Loan.cs`**
    *   Represents a loan.
    *   Contains information about the loan amount and interest.
    *   Contains static logic to apply for loans and check if a customer is allowed to take a loan (based on income/assets).

*   **`Currency.cs`**
    *   Handles exchange rates (SEK, USD, EUR).
    *   Contains methods to convert amounts between different currencies (`ConvertCurrency`).
    *   Allows admins to change exchange rates.

## 4. Transaction System
A system to handle transfers, history, and queues.

*   **`Transaction.cs`**
    *   Handles the logic for executing transfers (both internal between own accounts and external to other users).
    *   Responsible for printing transaction history to the console.
    *   Validates that sufficient funds exist and that account numbers are correct.

*   **`TransactionHistory.cs`**
    *   A data model (object) that saves information about a completed transaction (sender, receiver, amount, date, type).

*   **`PendingTransaction.cs`**
    *   A data model for a transaction that is currently in the queue waiting to be executed.

*   **`TransactionQueue.cs`**
    *   Simulates the bank's transaction time.
    *   Holds a queue (`Queue`) of pending transactions.
    *   `RunQueue()` processes the queue and completes the transfer of money between accounts.

## 5. Interfaces and Tools
Helper classes to handle the console interface.

*   **`Menu.cs`**
    *   Contains all menu outputs (Login, Main Menu Customer, Main Menu Admin).
    *   Handles the `switch` statements that control what happens when the user makes a selection in the menu.

*   **`Utilities.cs`**
    *   Static helper class.
    *   Handles input from the user (ensures numbers are entered when required, etc.).
    *   Manages color changes in the console and printing of ASCII art.

## 6. Database and File Handling
Classes that handle persistence by saving and loading data from text files, so information is not lost when the program is turned off.

*   **`DatabaseLogins.cs`**
    *   Responsible for saving and reading user information from the file `Logins.txt`.
    *   Handles usernames, passwords, administrator status, and if the user is blocked.
    *   Ensures the user list is updated at program start.

*   **`DatabaseAccounts.cs`**
    *   Responsible for saving and reading financial data from the file `Accounts.txt`.
    *   Handles storage of customers' regular accounts (`Account`), savings accounts (`SavingAccount`), and loans (`Loan`).
    *   Ensures the correct accounts and loans are linked to the correct customer (`Customer`) when data is loaded into the system.
