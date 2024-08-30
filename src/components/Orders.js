import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Orders = () => {
  const [orders, setOrders] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Retrieve userId from localStorage
    const userId = localStorage.getItem('userid');

    // Check if userId is available
    if (!userId) {
      setError('User ID is not available.');
      setLoading(false);
      return;
    }

    // Function to fetch orders
    const fetchOrders = async () => {
      try {
        // API call to fetch orders using the userId
        const response = await axios.get(`https://localhost:7162/gateway/order`, {
          params: { userId }, // Send userId as a query parameter if needed
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('token')}` // Include token if required
          }
        });
        setOrders(response.data);
      } catch (err) {
        setError('Failed to fetch orders.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, []); // Empty dependency array ensures this effect runs once when component mounts

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Orders</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {orders.length > 0 ? (
        <ul>
          {orders.map((order, index) => (
            <li key={index}>
              Order ID: {order.orderId}, Order Date: {order.orderDate}, Status: {order.status}
            </li>
          ))}
        </ul>
      ) : (
        <p>No orders found</p>
      )}
    </div>
  );
};

export default Orders;
