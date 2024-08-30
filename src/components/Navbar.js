import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import './Navbar.css';
import { useNavigate } from 'react-router-dom';

const Navbar = () => {
  const [categories, setCategories] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [dropdownVisible, setDropdownVisible] = useState(false);
  const navigate = useNavigate();
  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const res = await axios.get('https://localhost:7174/api/Categories');
        setCategories(res.data);
      } catch (error) {
        console.log(error);
      }
    };

    fetchCategories();
  }, []);

  const searchproduct =()=>{
    console.log(searchTerm)
    navigate('/product', { state: { searchTerm } });
  }

  const handleInputChange = (event) => {
    setSearchTerm(event.target.value);
    setDropdownVisible(true);
  };

  const handleOptionClick = (category) => {
    setSearchTerm(category.categoryName);
    setDropdownVisible(false);
  };

  const handleClickOutside = (event) => {
    if (!event.target.closest('.dropdown-container')) {
      setDropdownVisible(false);
    }
  };

  useEffect(() => {
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  return (
    <nav>
      <ul>
        <li><Link to="/address">Address</Link></li>
        <li><Link to="/cart">Cart</Link></li>
        <li><Link to="/orders">Orders</Link></li>
        <li><Link to="/wishlist">Wishlist</Link></li>
      </ul>
      <div className="position-relative dropdown-container">
        <input
          className="form-control me-2"
          type="text"
          placeholder="Search"
          value={searchTerm}
          onChange={handleInputChange}
          onClick={() => setDropdownVisible(true)}
        />
        {dropdownVisible && (
          <div className="position-absolute w-100 mt-1 bg-white border rounded shadow">
            {categories
              .filter(category => category.categoryName.toLowerCase().includes(searchTerm.toLowerCase()))
              .map(category => (
                <div
                  key={category.categoryId}
                  className="p-2 cursor-pointer"
                  onClick={() => handleOptionClick(category)}
                >
                  {category.categoryName}
                </div>
              ))}
            {categories.filter(category => category.categoryName.toLowerCase().includes(searchTerm.toLowerCase())).length === 0 && (
              <div className="p-2 text-muted">No results</div>
            )}
          </div>
        )}
        <button className="btn btn-outline-success mt-2" onClick={searchproduct} type="submit">Search</button>

      </div>
      <Link to="/userprofile" className="profile-link">
        <div className="profile-circle">
          
          <span className="profile-icon">👤</span> 
        </div>
      </Link>
    </nav>
  );
};

export default Navbar;
