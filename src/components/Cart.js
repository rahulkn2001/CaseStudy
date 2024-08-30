import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Cart = () => {
  const [productNames, setProductNames] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Function to fetch products for the current user
    const fetchProducts = async () => {
      try {
        const token = localStorage.getItem('token');
        const userId = localStorage.getItem('userid'); // Assuming userId is stored in localStorage

        // Check if userId is present
        if (!userId) {
          setError('User ID is missing.');
          return;
        }

        const response = await axios.get(`https://localhost:7174/api/Cart/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
        
        setProductNames(response.data);
        setError(null);
      } catch (err) {
        setError('Failed to fetch product names.');
        setProductNames([]);
      }
    };

    // Fetch products when component mounts
    fetchProducts();
  }, []); // Empty dependency array ensures this runs once on component mount

  return (
    <div>
      <h1>Product List</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <ul>
        {productNames.length > 0 ? (
          productNames.map((name, index) => <li key={index}>{name}</li>)
        ) : (
          <p>No products found</p>
        )}
      </ul>
    </div>
  );
};

export default Cart;
