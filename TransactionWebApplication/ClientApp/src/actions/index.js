import { fetchTransactions } from '../services/transactionService';

const transactionsRequested = () => {
  return {
    type: 'FETCH_TRANSACTIONS_REQUEST'
  }
};

const transactionsLoaded = (transactions) => {
  return {
    type: 'FETCH_TRANSACTIONS_SUCCESS',
    payload: transactions
  };
};

const getTransactions = () => async dispatch => {
  dispatch(transactionsRequested());
  const result = await fetchTransactions();
  dispatch(transactionsLoaded(result));
};

export {
  getTransactions
};