import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const ProductList = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const location = useLocation();
  const { searchTerm } = location.state || {};

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      setError(null);

      try {
        console.log(searchTerm)
        const response = await axios.get(`https://localhost:7082/product/${searchTerm.toLowerCase()}`);
        setProducts(response.data);
      } catch (err) {
        setError('Failed to fetch products.');
      } finally {
        setLoading(false);
      }
    };

    if (searchTerm) {
      fetchProducts();
    }
  }, [searchTerm]);

  return (
    <div>
      <h1>Products</h1>
      {loading && <p>Loading...</p>}
      {error && <p>{error}</p>}
      <ul>
        {products.map((product) => (
          <li key={product.productId}>
            <strong>{product.productName}</strong> - ${product.price}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ProductList;
