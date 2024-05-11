import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import './PokemonCard.css';

function PokemonCard() {
  const { id } = useParams();
  const [pokemonInfo, setPokemonInfo] = useState(null);

  useEffect(() => {
    async function fetchPokemonInfo() {
      try {
        const response = await fetch(`https://pokeapi.co/api/v2/pokemon/${id}`);
        const data = await response.json();
        setPokemonInfo(data);
      } catch (error) {
        console.error('Error fetching Pokemon info:', error);
      }
    }

    fetchPokemonInfo();
  }, [id]);

  if (!pokemonInfo) {
    return <p>Loading...</p>;
  }

  const filteredStats = pokemonInfo.stats.filter(stat =>
    ['hp', 'attack', 'defense', 'speed'].includes(stat.stat.name)
  );

  const filteredMoves = pokemonInfo.moves.filter(move =>
    move.version_group_details.some(detail => detail.level_learned_at > 0)
  );
  const filteredAbilities = pokemonInfo.abilities.filter(ability =>
    ability.is_hidden === false
  );


  const types = pokemonInfo.types.map(type => type.type.name).join(', ');

  return (
    <div className="app">
      <div className='head1'>
        <Link to="/" className="back-link">
          <img src="https://thypix.com/wp-content/uploads/2020/04/white-arrow-99.png" className='img'></img>
        </Link>
      </div>
      <div className='main'>
        <div className='left'>
          <div className='card'>
          <div className='frame'>
          <div className='poke-info'>
            <p className='poke-id'>#{pokemonInfo.id}</p>
            <h4 className='poke-name'>{pokemonInfo.name}</h4>
          </div>
            <div className='card-type'>
              <div className="types">
                {pokemonInfo.types.map((type, index) => (
               <span key={index} className={`type ${type.type.name}`}>
                 {type.type.name}

                </span>
                  ))}
                </div>

              </div>
            </div>

            <div className='frame'>
              <div className='stats'>
                <ul>
                  {filteredStats.map((stat, index) => (
                    <li key={index} className='default-text'>{stat.stat.name} 
                      <div className="progress-bar">
                      <div
                        className={`progress ${stat.stat.name.toLowerCase()}`}
                        style={{ width: `${(stat.base_stat)}%` }}
                      >
                      </div>
                      </div>
                    </li>
                  ))}
                </ul>
              </div>
              <img src={pokemonInfo.sprites.other['official-artwork'].front_default} alt={`Pokemon ${pokemonInfo.name}`} className='poke-img' />
            </div>
          </div>

          <div className='card'>
            <h4 className='default-text'>Breeding</h4>
            <div className='breeding'>
              <div>
                <p className='values-name'>Height</p>
                <p className='values'>{pokemonInfo.height / 10} m</p>
              </div>
              <div>
                <p className='values-name'>Weight</p>
                <p className='values'>{pokemonInfo.weight / 10} kg</p>
              </div>
            </div>
          </div>
          <div className='card'>
            <h4 className='default-text'>Moves</h4>
            <ul id='move-card'>
              {filteredMoves.map((move, index) => (
                <li key={index} className='moves'>{move.move.name}</li>
              ))}
            </ul>
          </div>
          <div className='card'>
          <h4 className='default-text'>Abilities</h4>
          <ul id='ability-card'>
            {filteredAbilities.map((ability, index) => (
              <li key={index} className='abilities'>{ability.ability.name}</li>
            ))}
          </ul>
        </div>
    </div>
    </div>
    </div>
  );
}
export default PokemonCard;
