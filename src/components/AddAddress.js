import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


const AddAddress = () => {
    const [HouseNumber, setHouseNumber] = useState('');
    const [City, setCity] = useState('');
    const [State, setState] = useState('');
    const navigate = useNavigate();
   

    const handleSubmit = async (e) => {
        e.preventDefault();
        const Userid = localStorage.getItem('userid');
        const address = {
            UserId:parseInt(Userid, 10),
            HouseNumber: parseInt(HouseNumber, 10),
            City,
            State,
        };

        try {
            const token = localStorage.getItem('token');
            const response = await axios.post('https://localhost:7174/api/Addresses/add-address', address, 
              {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                }
            });
            console.log('Address added:', response.data);
            
            navigate('/address')

        } catch (error) {
            console.error('There was an error adding the address!', error);
        }
    };

    return (
        <div>
            <h2>Add Address</h2>
            <form onSubmit={handleSubmit}>
                
                <label>
                    HouseNumber:
                    <input type="text" value={HouseNumber} onChange={(e) => setHouseNumber(e.target.value)} />
                </label>
                <br />
                <label>
                    City:
                    <input type="text" value={City} onChange={(e) => setCity(e.target.value)} />
                </label>
                <br />
                <label>
                    State:
                    <input type="text" value={State} onChange={(e) => setState(e.target.value)} />
                </label>
                <br />
                
                <button type="submit">Add Address</button>
            </form>
        </div>
    );
};
export default AddAddress;