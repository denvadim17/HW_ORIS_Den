import React from "react";
import MainPage from "../pages/MainPage/MainPage";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import '../pages/Pokemons.css';
import '../pages/SearchBar.css';
import '../pages/MainPage/MainPage.css';
import PokemonCard from "../pages/MainPage/components/PokemonCard";
import '../App.css';

const Layout = () => {
  return (
      <div>
          <header></header>
          <main>
              <div className="wrapper">
                  <Routes>
                      <Route exact path="/" element={<MainPage />} />
                      <Route path="/pokemon/:id" element={<PokemonCard />} />
                  </Routes>
              </div>
          </main>
      </div>
  );
}

export default Layout;
