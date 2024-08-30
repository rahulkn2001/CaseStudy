import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate, useLocation } from 'react-router-dom';

function VerifyOtp() {
  const navigate = useNavigate();
  const location = useLocation();
  
  const [OtpToken, setOtpToken] = useState('');
  const [error, setError] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

  // Retrieve email from location state or default to empty string
  const { email } = location.state || {};
console.log(email)
  // Handle OTP input change
  const handleOtpChange = (e) => {
    setOtpToken(e.target.value);
  };

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!email) {
      setError('Email is missing.');
      return;
    }

    try {
      // POST request to verify OTP
      const response = await axios.get(
        `https://localhost:7137/api/Register/otp/${email}`,
        {OtpToken}
        
      );
      const { otpToken: serverOtpToken } = response.data;

      console.log(serverOtpToken+'  '+OtpToken)
      
      if (serverOtpToken === OtpToken) {
        
        try {
            // PUT request to update email verification status
            const response = await axios.put(
              'https://localhost:7137/api/Register/otp',
              { email } 
            );
      
            if (response.status === 200) {
              setSuccessMessage('Email verification status updated successfully!');
              navigate('/');
            } else {
              setError('Failed to update email verification status.');
            }
          } catch (error) {
            setError('Failed to update email verification status: ' + (error.response?.data?.message || error.message));
          }
          navigate('/');
      }
        
      else {
        setError('Entered OTP is incorrect.');
      }
     
    } catch (error) {
      // Handle errors
      setError('OTP verification failed: ' + (error.response?.data || error.message));
    }
  };

  return (
    <div className="otp-verification">
      <h1>Verify OTP</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="otp">Enter OTP</label>
          <input
            type="text"
            id="otp"
            name="otp"
            value={OtpToken}
            onChange={handleOtpChange}
            required
          />
        </div>
        <button type="submit">Verify OTP</button>
      </form>
      {successMessage && <p className="success-message">{successMessage}</p>}
      {error && <p className="error-message">{error}</p>}
    </div>
  );
}

export default VerifyOtp;
