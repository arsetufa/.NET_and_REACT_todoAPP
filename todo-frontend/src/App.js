import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';

const url = 'http://localhost:5053/api/todo';

function App() {
    const [todos, setTodos] = useState([]);
    const [description, setDescription] = useState('');

    const fetchTodos = async () => {
        const result = await axios(url);
        setTodos(result.data);
    };

    useEffect(() => {
        fetchTodos();
    }, []);

    const addTodo = async () => {
        const result = await axios.post(url, {
            "description": description
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        });

        setTodos([...todos, result.data]);
        setDescription('');
    };

    const updateTodo = async (todo) => {
        const result = await axios.put(`${url}/${todo.id}`, {
            "id": todo.id,
            "description": todo.description,
            "isComplted": true
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        console.log(result);
        fetchTodos();
    };

    const deleteTodo = async (todo) => {
        const result = await axios.delete(`${url}/${todo.id}`);
        console.log(result.status);
        fetchTodos();

    };

    return (
        <div className="App">
            <h1>Todo List</h1>
            <input
                value={description}
                onChange={e => setDescription(e.target.value)}
                placeholder="Add a new task..."
            />

            <button onClick={addTodo}>Add</button>
            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        {todo.description}
                        {todo.isComplted ? <button>DONE</button> : <button onClick={() => updateTodo(todo)}>Complete</button>}
                        <button onClick={() => deleteTodo(todo)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
