export const fetchTransactions = async () => {
    const res = await fetch('api/transaction');
    const body = await res.json();
    return body;
}