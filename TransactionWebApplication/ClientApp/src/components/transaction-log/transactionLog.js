import React, { useEffect }  from 'react';
import { connect } from 'react-redux';
import { getTransactions } from '../../actions';
import './transactionLog.css';

const TransactionLog = ({ items, getTransactions }) => {

  useEffect(() => {
    getTransactions();
  }, []);

  const renderRow = (item, index) => {
    const { id, type, status, amount, balance, message, effectiveDate } = item;
    return (
      <tr key={id}>
        <td>{index + 1}</td>
        <td>{type}</td>
        <td>{status}</td>
        <td>${amount}</td>
        <td>${balance}</td>
        <td>{message}</td>
        <td>{effectiveDate}</td>
      </tr>
    );
  };

  return (
    <div className="shopping-cart-table">
      <h2>Transaction Log</h2>
      <table className="table">
        <thead>
          <tr>
            <th>#</th>
            <th>Type</th>
            <th>Status</th>
            <th>Amount</th>
            <th>Balance</th>
            <th>Message</th>
            <th>Date</th>
          </tr>
        </thead>

        <tbody>
        { items.map(renderRow) }
        </tbody>
      </table>
    </div>
  );
};

const mapStateToProps = state => ({
  items: state.transactions
});

const mapDispatchToProps = dispatch => ({
  getTransactions: () => dispatch(getTransactions())
});

export default connect(mapStateToProps, mapDispatchToProps)(TransactionLog);
