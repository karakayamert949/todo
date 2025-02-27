import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function TodoList() {
    const [todos, setTodos] = useState([]);
    const [title, setTitle] = useState('');
    const navigate = useNavigate();
    const handleExit = () => {
        // Clear any authentication tokens or user data from local storage
        localStorage.removeItem('token');
        // Navigate to the login page
        navigate('/');
    };

    useEffect(() => {
        axios.get('https://localhost:7243/api/Todo')
            .then(response => setTodos(response.data)) // setTodos(response.data)
            .catch(error => console.log(error));
    });

    const addTodo = () => {
        axios.post('https://localhost:7243/api/Todo', { title, completed: false })
            .then(response => setTodos([...todos, response.data]))
            .catch(error => console.log(error));
    };

    const toggleCompletion = (id) => {
        const todo = todos.find(todo => todo.id === id);
        axios.put(`https://localhost:7243/api/Todo/${id}`, { ...todo, completed: !todo.completed })
            .then(response => setTodos(todos.map(todo => todo.id === id ? response.data : todo)))
            .catch(error => console.log(error));
    };

    const deleteTodo = (id) => {
        axios.delete(`https://localhost:7243/api/Todo/${id}`)
            .then(response => console.log(`Todo with ID ${id} deleted successfully.`))
            .catch(error => console.log(error));
    };

    return (
        <div>
            <button onClick={handleExit}>Exit</button>
            <h1>Todo List</h1>
            <input value={title} onChange={(e) => setTitle(e.target.value)} />
            <button onClick={addTodo}>Add</button>
            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        <span style={{ textDecoration: todo.completed ? 'line-through' : 'none' }}>
                            {todo.title}
                        </span>
                        <button onClick={() => toggleCompletion(todo.id)}>Toggle</button>
                        <button onClick={() => deleteTodo(todo.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default TodoList;
