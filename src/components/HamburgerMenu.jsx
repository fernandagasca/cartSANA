import React, { useState } from 'react';
import axios from 'axios';
import '../styles/HamburgerMenu.css'


function HamburgerMenu({ isOpen, onClose, onCreateProduct }) {
  return (
    <>     
      {isOpen && <div className="overlay" onClick={onClose} />}

      <div className={`menu-container ${isOpen ? 'open' : ''}`}>
        <button className="close-button" onClick={onClose}>
          &times;
        </button>

        <ul className="menu-list">
          <li onClick={onCreateProduct}>Crear producto</li>
        </ul>
      </div>
    </>
  );
}

export default HamburgerMenu;