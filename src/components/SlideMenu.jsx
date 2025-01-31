import React, { useState } from 'react';
import axios from 'axios';
import '../styles/SlideMenu.css'


function SlideMenu({ isOpen, onClose }) {
  // Estado local para los campos de producto
  const [nameProduct, setNameProduct] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [stock, setStock] = useState('');
  const [image, setImage] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    const newProduct = {
      name_Product: nameProduct,
      description,
      price: parseFloat(price),
      stock: parseInt(stock),
      image
    };

    axios.post('https://localhost:7110/api/Products', newProduct)
      .then(() => {
        alert('Producto creado correctamente');
        // Limpia campos
        setNameProduct('');
        setDescription('');
        setPrice('');
        setStock('');
        setImage('');
      })
      .catch(err => {
        console.error(err);
        alert('Error al crear producto');
      });
  };

  if (!isOpen) return null; 

  return (
    <div className="slide-menu">
      <button className="close-button" onClick={onClose}>
        &times;
      </button>
      <h2>Crear Producto</h2>
      <form onSubmit={handleSubmit}>
        <label>Nombre:</label>
        <input 
          type="text"
          value={nameProduct}
          onChange={(e) => setNameProduct(e.target.value)}
          required
        />

        <label>Descripción:</label>
        <textarea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        <label>Precio:</label>
        <input 
          type="number"
          step="0.01"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          required
        />

        <label>Stock:</label>
        <input
          type="number"
          value={stock}
          onChange={(e) => setStock(e.target.value)}
          required
        />

        <label>URL de la Imagen:</label>
        <input
          type="text"
          value={image}
          onChange={(e) => setImage(e.target.value)}
        />

        <button type="submit">Agregar Producto</button>
      </form>
    </div>
  );
}

export default SlideMenu;
