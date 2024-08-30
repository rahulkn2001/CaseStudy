import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const AddressManager = () => {
    const [addresses, setAddresses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedAddress, setSelectedAddress] = useState(null);
    const [houseNumber, setHouseNumber] = useState('');

    
    const [city, setCity] = useState('');
    const [state, setState] = useState('');
    const navigate = useNavigate();
    const userid = localStorage.getItem('userid');
    const token = localStorage.getItem('token');

   
    const fetchAddresses = async () => {
        try {
            const response = await fetch(`https://localhost:7174/api/Addresses/user/${userid}`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            if (response.status === 404) {
                setError('Addresses not found. Click below to add a new address.');
                setAddresses([]);
            } else if (!response.ok) {
                throw new Error('Network response was not ok');
            } else {
                const data = await response.json();
                setAddresses(data);
            }
        } catch (error) {
            setError(error.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchAddresses();
    }, [userid, token]);

    
    const [showupdateform, setshowupdateform] = useState(false);

    const handleUpdateButtonClick = async (addressId) => {
        try {
            const response = await axios.get(`https://localhost:7174/api/Addresses/address/${addressId}`, {
                headers: { Authorization: `Bearer ${token}` }
            });

            const address = response.data;
            console.log(response.data, showupdateform);
            console.log(address[0].addressId)
            setSelectedAddress(address[0].addressId);
            setHouseNumber(address[0].houseNumber);
      setshowupdateform(!showupdateform);
            setCity(address[0].city);
            setState(address[0].state);
        } catch (err) {
            setError('Failed to fetch address ');
        }
    };

    
    const handleUpdateAddress = async (e) => {
      
        e.preventDefault();
        console.log(selectedAddress)
        const updatedAddress = {
            AddressId: selectedAddress,
            HouseNumber: parseInt(houseNumber, 10),
            City: city,
            State: state
        };

        try {
            await axios.put(`https://localhost:7174/api/Addresses/update/${selectedAddress}/${userid}`, updatedAddress, {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                }
            });

           
            await fetchAddresses();
            setSelectedAddress(null); 
        } catch (err) {
            setError('Failed to update address');
        }
    };

    const handleAddAddressClick = () => {
        navigate('/addaddress');
    };
    const deleteButtonClick = async (selectedAddress)=>{
      const res=  await axios.delete(`https://localhost:7174/api/Addresses/delete/${selectedAddress}/${userid}`,{
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`
      }});
      console.log(res)
      if(res.status==204){
        alert('Successfully Deleted')
      }


    }

    if (loading) return <p>Loading...</p>;
    if (error) return (
        <div>
            <p>{error}</p>
            <button onClick={handleAddAddressClick}>Add New Address</button>
        </div>
    );

    return (
        <div>
            {showupdateform ? (
                <div>
                    <h2>Update Address</h2>
                    <form onSubmit={handleUpdateAddress}>
                        <label>
                            House Number:
                            <input
                                type="text"
                                value={houseNumber}
                                onChange={(e) => setHouseNumber(e.target.value)}
                            />
                        </label>
                        <br />
                        <label>
                            City:
                            <input
                                type="text"
                                value={city}
                                onChange={(e) => setCity(e.target.value)}
                            />
                        </label>
                        <br />
                        <label>
                            State:
                            <input
                                type="text"
                                value={state}
                                onChange={(e) => setState(e.target.value)}
                            />
                        </label>
                        <br />
                        <button type="submit">Update Address</button>
                        <button type="button" onClick={() => setSelectedAddress(null)}>Cancel</button>
                    </form>
                </div>
            ) : (
                <div>
                    <h1>Address List</h1>
                    <ul>
                        {addresses.length > 0 ? (
                            addresses.map(address => (
                                <li key={address.addressId}>
                                    <strong>AddressID:</strong> {address.addressId}<br />
                                    <strong>House Number:</strong> {address.houseNumber}<br />
                                    <strong>City:</strong> {address.city}<br />
                                    <strong>State:</strong> {address.state}<br />
                                    <button onClick={() => handleUpdateButtonClick(address.addressId)}>Update Address</button>
                                    <button onClick={()=>deleteButtonClick(address.addressId)}>Delete Address</button>
                                </li>
                            ))
                        ) : (
                            <p>No addresses available</p>
                        )}
                    </ul>
                    <button onClick={handleAddAddressClick}>Add New Address</button>
                </div>
            )}
        </div>
    );
};

export default AddressManager;
