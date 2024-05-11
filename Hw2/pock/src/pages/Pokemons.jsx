import React from 'react';
import { Link } from 'react-router-dom';
import './Pokemons.css';

function Pokemons({ pokemon }) {
  const { id, name, image, types } = pokemon;

  function PokemonTypes() {
    return (
      <ul className='types'>
        {types.map((type, index) => (
          <li key={index} className={`type-card ${type.type.name}`}>
            {type.type.name}
          </li>
        ))}
      </ul>
    );
  }

  return (
    <Link to={`/pokemon/${id}`} className="pokemon-card">
      <div className="pokemon-info">
        <h2 className="pokemon-name">{name}</h2>
        <p className="pokemon-id">#{id}</p>
      </div>
      <img className="pokemon-image" src={image} alt={name} />
      <PokemonTypes />
    </Link>
  );
}

export default Pokemons;
