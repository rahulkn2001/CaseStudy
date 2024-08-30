// App.jsx
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Homepage from './components/Homepage';
import Login from './components/login';
import Address from './components/Address';
import AddAddress from './components/AddAddress'
import Cart from './components/Cart';
import Orders from './components/Orders';
import Register from './components/register';
import ProtectedRoute from './components/Protectedroute';
import UpdateAddress from  './components/Address'
import UserProfile from './components/UserProfile';
import WishList from './components/Wishlist';

import ProductList from './components/Product';
import VerifyOtp from './components/verifiyotp';


const App = () => {
  return (
    <Router>
      <Routes>
        <Route path='/register'  element={<Register/>}/>
        <Route path="/" element={<Login />} />
        <Route path="/homepage" element={<ProtectedRoute><Homepage /></ProtectedRoute>} />
        <Route path="/address" element={<ProtectedRoute><Address /></ProtectedRoute>} />
        <Route path="/cart" element={<ProtectedRoute><Cart /></ProtectedRoute>} />
        <Route path="/orders" element={<ProtectedRoute><Orders /></ProtectedRoute>} />
        <Route path="/addaddress" element={<ProtectedRoute><AddAddress /></ProtectedRoute>} />
        <Route path="/updateaddress" element={<ProtectedRoute>< UpdateAddress/></ProtectedRoute>} />
        <Route path="/userprofile" element={<ProtectedRoute>< UserProfile/></ProtectedRoute>} />
        <Route path="/wishlist" element={<ProtectedRoute>< WishList/></ProtectedRoute>} />
        <Route path="/product" element={<ProductList/>} />
        <Route path="/verifyotp" element={<VerifyOtp />} />

        
      </Routes>
    </Router>
  );
};

export default App;
