import React, { useState, useEffect } from 'react';
import axios from 'axios';

function TodoList() {
    const [todos, setTodos] = useState([]);
    const [title, setTitle] = useState('');

    useEffect(() => {
        axios.get('http://localhost:7243/api/Todo')
            .then(response => console.log(response)) // setTodos(response.data)
            .catch(error => console.log(error));
    }, []);

    const addTodo = () => {
        axios.post('http://localhost:7243/api/Todo', { title, completed: false })
            .then(response => setTodos([...todos, response.data]))
            .catch(error => console.log(error));
    };

    const toggleCompletion = (id) => {
        const todo = todos.find(todo => todo.id === id);
        axios.put(`http://localhost:7243/api/Todo/${id}`, { ...todo, completed: !todo.completed })
            .then(response => setTodos(todos.map(todo => todo.id === id ? response.data : todo)))
            .catch(error => console.log(error));
    };

    return (
        <div>
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
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default TodoList;
