import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';


import Address from './Address';

const Login = () => {
  // State hooks for userid and password
  const [Email, setEmail] = useState('raaj@gmail.com');
  const [password, setPassword] = useState('12345');
  const [error,seterror]=useState('problem creates error');
  const navigate = useNavigate();
  // Handler for password input change
  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  // Handler for form submission
  const  handleSubmit = (e) => {
    e.preventDefault();
    // Handle form submission logic here
    console.log('Submitted:', { Email, password });
    callapi(Email,password)
    
  };
 async  function callapi(Email,password){
    try {
        const response = await axios.post('https://localhost:7082/authenticate',
          { Email: Email,upassword: password, Firstname:'John' }, 
          { headers: { 'Content-Type': 'application/json' } }
        );
        console.log(response.data)
        const token = response.data.token;
        const userid=response.data.userId;
        console.log(token,'------',userid);
        localStorage.setItem('token', token);
        localStorage.setItem('userid',userid);
        navigate('/homepage');
      } catch (error) {
        seterror('Invalid username or password');
      }
  }
  

  return (
    <div>
      <div>Login</div>
      
      <form onSubmit={handleSubmit}>
        <label>
          Email
          <input
            type="text"
            name="Email"
            value={Email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </label>
        <br />
        <label>
          Password
          <input
            type="password"
            name="password"
            value={password}
            onChange={handlePasswordChange}
          />
        </label>
        <br />
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default Login;
