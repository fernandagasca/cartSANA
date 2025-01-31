import React, { useState } from 'react';
import axios from 'axios';
import '../styles/CreateProductModal.css'


function CreateProductModal({ visible, onClose }) {
  // Estados del formulario
  const [nameProduct, setNameProduct] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [stock, setStock] = useState('');
  const [image, setImage] = useState('');

  if (!visible) {
    return null; 
  }

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
        // Limpia los campos
        setNameProduct('');
        setDescription('');
        setPrice('');
        setStock('');
        setImage('');
        
        onClose();
      })
      .catch(err => {
        console.error(err);
        alert('Error al crear producto');
      });
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <button className="modal-close" onClick={onClose}>
          &times;
        </button>
        <h2>Crear Producto</h2>
        <form onSubmit={handleSubmit}>
          <label>Nombre del producto</label>
          <input 
            type="text" 
            value={nameProduct}
            onChange={(e) => setNameProduct(e.target.value)}
            required
          />

          <label>Descripción</label>
          <textarea 
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            rows={3}
          />

          <label>Precio</label>
          <input 
            type="number" 
            step="0.01"
            value={price}
            onChange={(e) => setPrice(e.target.value)}
            required
          />

          <label>Stock</label>
          <input 
            type="number" 
            value={stock}
            onChange={(e) => setStock(e.target.value)}
            required
          />

          <label>URL de la Imagen</label>
          <input 
            type="text"
            value={image}
            onChange={(e) => setImage(e.target.value)}
          />

          <button type="submit" className="btn-submit">Agregar Producto</button>
        </form>
      </div>
    </div>
  );
}

export default CreateProductModal;
