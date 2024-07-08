import React from 'react';
import TodoList from './Components/TodoList';
import Login from './Components/Login';
import ProtectedRoute from './Components/ProtectedRoute';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/todo" element={<ProtectedRoute><TodoList /></ProtectedRoute>} />
            </Routes>
        </Router>
    );
}

export default App;
