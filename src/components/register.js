import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Login = () => {
  // State hooks for user input
  const [password, setPassword] = useState('12345');
  const [email, setemail] = useState('sudhanvagv44@gmail.com');
  const [firstname, setfirstname] = useState('sudhanva');
  const [lastname, setlastname] = useState('gv');
  const [searchEmail, setSearchEmail] = useState('');
  
  // State hooks for error and success messages
  const [error, seterror] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  
  const navigate = useNavigate();

  // Handlers for input changes
  const handlePasswordChange = (e) => setPassword(e.target.value);
  const handleFirstnameChange = (e) => setfirstname(e.target.value);
  const handleLastnameChange = (e) => setlastname(e.target.value);
  const handleEmailChange = (e) => setemail(e.target.value);

  // Handler for form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Submitted:', { password, firstname, email, lastname });
    callApi(password, firstname, email, lastname);
  };

  // API call for registration
  async function callApi(password, firstname, email, lastname) {
    try {
      const response = await axios.post('https://localhost:7137/api/Register/register',
        { upassword: password, Firstname: firstname, Email: email, Lastname: lastname },
        { headers: { 'Content-Type': 'application/json' } }
      );

      setSuccessMessage('Registration successful. Please check your email for verification.');
      await checkEmailVerification(email);
      
    } catch (error) {
      seterror('Registration failed: ' + (error.response?.data || error.message));
    }
  }

  async function checkEmailVerification(email) {
    try {
      const verificationResponse = await axios.get(`https://localhost:7137/api/Register/otp/${email}`);

      const { OtpToken} = verificationResponse.data;
console.log(OtpToken)
      if (OtpToken) {
        // If email is verified, navigate to login page
        navigate('/');
      } else {
        console.log(email)
        navigate('/verifyotp', { state: { email } });
      }

    } catch (error) {
      // Handle errors during OTP verification status check
      console.error('Failed to check email verification status:', error.response?.data || error.message);
      seterror('Failed to check email verification status.');
    }
  }


  return (
    <div>
      <div>Login</div>
      
      <form onSubmit={handleSubmit}>
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
        <label>
          First name
          <input
            type="text"
            name="firstname"
            value={firstname}
            onChange={handleFirstnameChange}
          />
        </label>
        <br />
        <label>
          Last name
          <input
            type="text"
            name="lastname"
            value={lastname}
            onChange={handleLastnameChange}
          />
        </label>
        <br />
        <label>
          Email
          <input
            type="text"
            name="email"
            value={email}
            onChange={handleEmailChange}
          />
        </label>
        <br />
        <button type="submit">Submit</button>
      </form>
      
      {error && <div className="error">{error}</div>}
      {successMessage && <div className="success">{successMessage}</div>}
    </div>
  );
};

export default Login;
