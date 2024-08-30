// ProductList.js
import React, { useEffect, useState } from 'react';
import axios from 'axios';

const WishList = ({ userId }) => {
  const [productNames, setProductNames] = useState([]);
  const [error, setError] = useState(null);
  const userid = localStorage.getItem('userid');
  useEffect(() => {
    const fetchProductNames = async () => {
      try {
        const response = await axios.get(`https://localhost:7082/wishlist/${userid}`);
        setProductNames(response.data);
      } catch (error) {
        setError('Failed to fetch product names.');
        console.error(error);
      }
    };

    fetchProductNames();
  }, [userId]);

  return (
    <div>
      <h1>Product List</h1>
      {error && <p>{error}</p>}
      <ul>
        {productNames.map((name, index) => (
          <li key={index}>{name}</li>
        ))}
      </ul>
    </div>
  );
};

export default WishList;
