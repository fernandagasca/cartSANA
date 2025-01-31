import js from "@eslint/js"
import axios from "axios"
import { useState } from "react"
import { useEffect } from "react"
import '../styles/ProductList.css'

function ProductList({ onAddToCart }) {
    const [products, setProducts] = useState([]);
    const [quantities, setQuantities] = useState({}); 
    // quantities = { productId: number }
  
    useEffect(() => {
      // Llamada al endpoint que devuelve la lista de productos
      axios.get('https://localhost:7110/api/Products')
        .then((response) => {
          setProducts(response.data);
        })
        .catch((error) => {
          console.error('Error al obtener productos:', error);
        });
    }, []);
  
    // Manejar cambios en el input de cantidad
    const handleQuantityChange = (productId, value) => {
      // Asegurarnos de que sea mínimo 1
      const intValue = parseInt(value, 10) || 1; 
      setQuantities((prev) => ({
        ...prev,
        [productId]: intValue,
      }));
    };
  
    // Al hacer clic en "Agregar al carrito"
    const handleAddClick = (product) => {
      const qty = quantities[product.id] || 1; 
      onAddToCart(product, qty);
    };
  
    return (
      <div className="product-list-container">
        <h1 className="product-list-title">Product List</h1>
  
        <div className="product-cards">
          {products.map((product) => {
            const quantityValue = quantities[product.id] || 1; 
            return (
              <div className="product-card" key={product.id}>
                <img
                  src={product.image}
                  alt={product.name_Product}
                  className="product-image"
                />
                <h3>{product.name_Product}</h3>
                <p className="description">{product.description}</p>
                <p className="price">Precio: ${product.price}</p>
                <p className="stock">Stock disponible: {product.stock}</p>
                
                <label htmlFor={`qty-${product.id}`}>Cantidad:</label>
                <input
                  id={`qty-${product.id}`}
                  type="number"
                  min="1"
                  max={product.stock}
                  value={quantityValue}
                  onChange={(e) => handleQuantityChange(product.id, e.target.value)}
                  style={{ width: '60px', marginRight: '8px' }}
                />
  
                <button
                  className="add-to-cart-btn"
                  onClick={() => handleAddClick(product)}
                >
                  Agregar al carrito
                </button>
              </div>
            );
          })}
        </div>
      </div>
    );
  }
  
  export default ProductList;
  