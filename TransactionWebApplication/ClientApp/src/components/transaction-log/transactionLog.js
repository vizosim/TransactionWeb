import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { getTransactions } from '../../actions';
import { Accordion, Card } from 'react-bootstrap';
import './transactionLog.css';

const TransactionLog = ({ items, getTransactions }) => {

  useEffect(() => {
    getTransactions();
  }, []);

  const renderRow = (item, index) => {
    const { id, type, status, amount, balance, message, effectiveDate } = item;

    const isCredit = type === 'Credit';
    const rowstyle = { color: isCredit ? 'green' : 'red' };
    const amountStyle = isCredit ? '+' : '-';

    return (
      <Card key={id}>
        <Accordion.Toggle as={Card.Header} eventKey={index + 1}>
          <Card.Body>
            <Card.Title>{type}</Card.Title>
            <Card.Subtitle className="mb-2" style={rowstyle}>{amountStyle} ${amount}</Card.Subtitle>
          </Card.Body>
        </Accordion.Toggle>
        <Accordion.Collapse eventKey={index + 1}>
          <Card.Body>
            <Card.Title>{status}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted">Balance: ${balance}</Card.Subtitle>
            <Card.Subtitle className="mb-2 text-muted">{effectiveDate}</Card.Subtitle>
            <Card.Text>
              {message}
            </Card.Text>
          </Card.Body>
        </Accordion.Collapse>
      </Card>
    );
  };

  return (
    <div className="shopping-cart-table">
      <h2>Transaction Log</h2>

      <Accordion defaultActiveKey="0">
        {items.map(renderRow)}
      </Accordion>
    </div >
  );
};

const mapStateToProps = state => ({
  items: state.transactions
});

const mapDispatchToProps = dispatch => ({
  getTransactions: () => dispatch(getTransactions())
});

export default connect(mapStateToProps, mapDispatchToProps)(TransactionLog);
