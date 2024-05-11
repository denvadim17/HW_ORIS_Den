import React, { useState } from 'react';
import './SearchBar.css';

const SearchBar = ({ onSearch }) => {
  const [searchTerm, setSearchTerm] = useState('');

  const handleInputChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleSearch = () => {
    onSearch(searchTerm);
  };

  return (
    <div className="search-bar">
      <input
        className="search-bar-input"
        type="text"
        placeholder="E.g. Pikachu"
        value={searchTerm}
        onChange={handleInputChange}
      />
      <button className="search-bar-button" onClick={handleSearch}>GO</button>
    </div>
  );
};

export default SearchBar;