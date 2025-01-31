import React from 'react';
import '../styles/Navbar.css'


function Navbar({ onToggleMenu }) {
  return (
    <nav className="navbar">
      <div className="navbar-left">
        {/* Botón hamburguesa */}
        <button className="hamburger-btn" onClick={onToggleMenu}>
          <span></span>
          <span></span>
          <span></span>
        </button>
        <h2 className="navbar-title">CartSANA</h2>
      </div>      
     <a className="git-logo" href='https://github.com/fernandagasca/cartSANA'>
     <img 
        src="https://cdn-icons-png.flaticon.com/512/25/25231.png"       
        alt="Git Logo" 
        
      />
      </a>      
    </nav>
  );
}

export default Navbar;

