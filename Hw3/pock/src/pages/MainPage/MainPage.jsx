import React, { useState, useEffect } from 'react';
import SearchBar from '../SearchBar';
import Pokemons from '../Pokemons';
import './MainPage.css';
import '../../App.css';

function MainPage() {
  const [pokemonList, setPokemonList] = useState([]);
  const [filteredPokemon, setFilteredPokemon] = useState([]);
  const [searchError, setSearchError] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('https://pokeapi.co/api/v2/pokemon?limit=154');
        const data = await response.json();
        const detailedPokemonList = await Promise.all(data.results.map(async (pokemon) => {
          const pokemonResponse = await fetch(pokemon.url);
          const pokemonData = await pokemonResponse.json();
          return {
            id: pokemonData.id,
            name: pokemonData.name,
            image: `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/${pokemonData.id}.png`,
            types: pokemonData.types,
          };
        }));
        setPokemonList(detailedPokemonList);
      } catch (error) {
        console.error('Error fetching Pokemon data:', error);
      }
    };

    fetchData();
  }, []);

  const handleSearch = (searchTerm) => {
    if (searchTerm.trim() === '') {
      setFilteredPokemon([]);
      setSearchError(false);
    } else {
      const filteredList = pokemonList.filter(pokemon => pokemon.name.toLowerCase().includes(searchTerm.toLowerCase()));

      if (filteredList.length > 0) {
        setFilteredPokemon(filteredList);
        setSearchError(false);
      } else {
        setFilteredPokemon([]);
        setSearchError(true);
      }
    }
  };

  return (
    <div className="app">
      <header className='head'>
        <h1 className='main-name'>Who are you looking for?</h1>
        <SearchBar onSearch={handleSearch} />
      </header>
      <main className="main-content">
        {searchError ? (
          <div>
            <h3 className='error'>Oops! No results found.</h3>
            <p className='error'>Sorry, the Pokemon you're looking for is not in this list.</p>
            <img src='https://projectpokemon.org/images/sprites-models/homeimg/poke_capture_0025_001_mo_n_00000000_f_n.png' alt='Error Pokemon' className='img-error' />
          </div>
        ) : (
          <div className="pokemon-list" id="poke-container">
            {filteredPokemon.length > 0
              ? filteredPokemon.map((pokemon, index) => (
                  <Pokemons key={index} pokemon={pokemon} />
                ))
              : pokemonList.map((pokemon, index) => (
                  <Pokemons key={index} pokemon={pokemon} />
                ))}
          </div>
        )}
      </main>
    </div>
  );
}

export default MainPage;
