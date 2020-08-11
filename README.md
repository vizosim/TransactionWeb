# TransactionWeb
Transaction Web application

### To run application:

* Go to /dist directory and run TransactionWebApplication.exe
* Follow by this url https://localhost:5001 to load UI

API available by this url https://localhost:5001/api/

Commit new transaction<br>
POST - https://localhost:5001/api/transaction/commit
{
  "type": "credit",
  "amount": 100
}

Retrieve transaction history<br>
GET https://localhost:5001/api/transaction

Get current balance<br>
GET https://localhost:5001/api/transaction/balance
{
}
