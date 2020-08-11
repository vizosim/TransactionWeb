# TransactionWeb
Transaction Web application

To run application:

Open /dist folder and run TransactionWebApplication

Follow by this url https://localhost:5001 to load UI

API available by this url https://localhost:5001/api/

Commit new transaction
POST - https://localhost:5001/api/transaction/commit
{
  "type": "credit",
  "amount": 11
}

GET https://localhost:5001/api/transaction
Retrieve transaction history

Get current balance
https://localhost:5001/api/transaction/balance
{
    "balance": 101
}

