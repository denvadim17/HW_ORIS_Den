import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import Layout from './layout/layout';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <Layout />
      </div>
    </Router>
  );
}

export default App;
