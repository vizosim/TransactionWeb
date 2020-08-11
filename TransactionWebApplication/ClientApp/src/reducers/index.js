
const initialState = {
  transactions: [],
  loading: true,
  error: null
};

const reducer = (state = initialState, action) => {

  switch (action.type) {
    case 'FETCH_TRANSACTIONS_REQUEST':
      return {
        ...state,
        transactions: [],
        loading: true,
        error: null
      };

    case 'FETCH_TRANSACTIONS_SUCCESS':
      return {
        ...state,
        transactions: action.payload,
        loading: false,
        error: null
      };

    case 'FETCH_TRANSACTIONS_FAILURE':
      return {
        ...state,
        transactions: [],
        loading: false,
        error: action.payload
      };

    default:
      return state;
  }
};

export default reducer;
