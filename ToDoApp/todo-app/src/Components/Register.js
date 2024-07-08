import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Register = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const handleLogin=() => navigate('/');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7243/api/users/register', {
                email,
                username,
                password,
            });
            localStorage.setItem('token', response.data.token);
            alert('Register successful');
            navigate('/');
        } catch (error) {
            alert(Object.values(error.response.data.errors).join('\n'));
        }
    };

    return (
        <>
            <button onClick={handleLogin}>Login</button>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username</label>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} />
                </div>
                <div>
                    <label>Email</label>
                    <input type="text" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div>
                    <label>Password</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button type="submit">Sign up</button>
            </form>
        </>
    );
};

export default Register;
