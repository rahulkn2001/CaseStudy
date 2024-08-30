import React, { useEffect, useState } from 'react';
import axios from 'axios';

const UserProfile = () => {
  const [user, setUser] = useState({
    userId: null,
    firstname: '',
    lastname: '',
    email: '',
    upassword: ''
  });
  const [editMode, setEditMode] = useState(false);
  const [error, setError] = useState('');

  const userid = localStorage.getItem('userid');
  const token = localStorage.getItem('token');

  useEffect(() => {
    if (!userid || !token) {
      setError('User not authenticated.');
      return;
    }

    const fetchUserData = async () => {
      try {
        const response = await axios.get(`https://localhost:7082/authenticate/${userid}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
       
        setUser(response.data);
      } catch (err) {
        console.error('Error fetching user data:', err);
        setError('Failed to fetch user data.');
      }
    };

    fetchUserData();
  }, [userid, token]); // Add dependencies

  const handleChange = (e) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  };

  const handleSave = async () => {
    try {
      await axios.put(`https://localhost:7082/authenticate/${userid}`, user, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
      alert('Successfully Updated')
      setEditMode(false);
    } catch (err) {
      console.error('Error updating user data:', err);
      setError('Failed to update profile. Please try again.');
    }
  };

  return (
    <div className="profile-container">
      <h2>User Profile</h2>
      {error && <div className="error-message">{error}</div>}
      <div className="profile-form">
        <label>
          First Name:
          <input
            type="text"
            name="firstname"
            value={user.firstname}
            onChange={handleChange}
            disabled={!editMode}
          />
        </label>
        <label>
          Last Name:
          <input
            type="text"
            name="lastname"
            value={user.lastname}
            onChange={handleChange}
            disabled={!editMode}
          />
        </label>
        <label>
          Email:
          <input
            type="email"
            name="email"
            value={user.email}
            onChange={handleChange}
            disabled={!editMode}
          />
        </label>
        <label>
          Password:
          <input
            type="password"
            name="upassword"
            value={user.upassword}
            onChange={handleChange}
            disabled={!editMode}
          />
        </label>
        {editMode ? (
          <div>
            <button onClick={handleSave}>Save</button>
            <button onClick={() => setEditMode(false)}>Cancel</button>
          </div>
        ) : (
          <button onClick={() => setEditMode(true)}>Edit</button>
        )}
      </div>
    </div>
  );
};

export default UserProfile;
