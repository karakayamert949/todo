import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    const handleRegister=() => navigate('/register');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7243/api/users/login', {
                username,
                password,
            });
            localStorage.setItem('token', response.data.token);
            alert('Login successful');
            navigate('/todo');
        } catch (error) {
            const message = error.response.data.errors ? Object.values(error.response.data.errors).join('\n') : 'Login Failed';
            alert(message);
        }
    };

    return (
        <>
            <button onClick={handleRegister}>Register</button>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username</label>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} />
                </div>
                <div>
                    <label>Password</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <button type="submit">Login</button>
            </form>
        </>
    );
};

export default Login;
