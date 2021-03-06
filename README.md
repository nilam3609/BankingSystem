# BankingSystem

# Solution: 

This project is created in C# .NET Core using Angular as frontend and MSSQL for database. 
Prerequisite: Visual Studio 2019, .NET Core SDK 3.xx,Angular. This project can also run in VSCode.
Architecture - 
Considering the seperation of concern and purpose, the architecture is created in below layers.
* OnlineBanking - This project holds APIs as well as Angular commponent. (Reactive forms, Bootstrap). This also includes a middelware which would handle exception and return message to frontend.
* OnlineBanking.Domain- This project holds all the entity for database and DTOs for APIs.
* OnlineBanking.Service- This projects holds AccountService and ClientService along with its interface. The main purpose of this layer is to hold all the business logic.
* OnlineBanking.Repository - This project is responsible for all the connection between Database and Service. Repository also contains the migrations files which are generated by EntityFramework based on changes in domain. SQL server is used along with entity framework.
* OnlineBanking.UnitTest - This project holds all the important unit tests for AccountService and AccountRepository.(Unit testing using Nunit ,Moq)


# How it works :
When the application starts, using migration- a database would be generated and required tables would be created along with the seeding of Bank info.
Purpose of Entity - 
* Bank - It would hold all the info about different banks
* Client- Stores different details about client including new UserId,Address etc.
* User - Stores details about user for login - username , id etc.
* Account - Stores info about account number, accountId, Balance, ClientId linked to this account
* AnnualInterest - Stores master data aboue available account type and its relevant annual interest rates and deposit period days
* DepositAccountSettings- Stores details about account for particular client and a foreign key to annual interest table for account settings and frequency for deposit. 

This way by seperating into different entity would make system more dynamic and robust which would help in further modification.

# Functionalities :
* Create Client- includes creating user and adding client for particular banks
* Open new account- Here client would be able to create multiple accounts by selecting avaible types of account - Savings/Deposit and choose different interest rates and deposit periods
* Deposit - Client can deposit amount to particular account. 
* Withdraw - Client would be able withdraw amount in given scenarios - If the account is a Savings Account and client has sufficient balance OR if account is a DepositAccount it would be on or after maturity period. ASSUMPTION - User is allowed to deposit money into Deposit Account anytime.
* DepositInterest-  Inorder to achieve this ,there can be Scheduler created which would run daily. The purpose here would be to calculate interest based on the type of accounts in the bank and update balance. ASSUMPTION - Creating Scheduler is not a part of assignment here but there is already a method would calculate all the interest and balance.

# Conclusion : 
This is still lot of room for improvement here in terms of UI design, adding authorization and login.




