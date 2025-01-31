import React, { useState } from 'react';
import './App.css';
import Navbar from './components/Navbar';
import ProductList from './components/ProductList';
import Cart from './components/Cart';
import OrderSummaryModal from './components/OrderSummaryModal'; // <-- Nuevo import

function App() {
  // Estado global del carrito: array de objetos { product, quantity }
  const [cartItems, setCartItems] = useState([]);

  // Estado para abrir/cerrar el modal de resumen de pedido
  const [orderModalOpen, setOrderModalOpen] = useState(false);

  // Agregar un producto con una cantidad deseada
  const addToCart = (product, desiredQty) => {
    setCartItems((prevCart) => {
      const existingItem = prevCart.find(
        (item) => item.product.id === product.id
      );
      if (existingItem) {
        // Calcula la nueva cantidad
        const newQuantity = existingItem.quantity + desiredQty;
        // Validar stock
        if (newQuantity > product.stock) {
          alert('No hay suficiente stock para agregar esa cantidad.');
          return prevCart;
        }
        // Actualiza la cantidad en el carrito
        return prevCart.map((item) =>
          item.product.id === product.id
            ? { ...item, quantity: newQuantity }
            : item
        );
      } else {
        // Si no existe en el carrito, validamos stock
        if (desiredQty > product.stock) {
          alert('No hay suficiente stock.');
          return prevCart;
        }
        // Agregamos un nuevo ítem
        return [...prevCart, { product, quantity: desiredQty }];
      }
    });
  };

  // Incrementar la cantidad de un producto
  const incrementQuantity = (productId) => {
    setCartItems((prevCart) =>
      prevCart.map((item) => {
        if (item.product.id === productId) {
          if (item.quantity + 1 > item.product.stock) {
            alert('Has alcanzado el stock máximo disponible para este producto.');
            return item;
          }
          return { ...item, quantity: item.quantity + 1 };
        }
        return item;
      })
    );
  };

  // Decrementar la cantidad de un producto
  const decrementQuantity = (productId) => {
    setCartItems((prevCart) =>
      prevCart.map((item) => {
        if (item.product.id === productId && item.quantity > 1) {
          return { ...item, quantity: item.quantity - 1 };
        }
        return item;
      })
    );
  };

  // Eliminar producto del carrito
  const removeFromCart = (productId) => {
    setCartItems((prevCart) =>
      prevCart.filter((item) => item.product.id !== productId)
    );
  };


  // Cuando el usuario presione "Procesar Orden" en el carrito
  const handleProcessOrder = () => {
    // Simplemente abrimos el modal de resumen
    setOrderModalOpen(true);
  };

  // Para cerrar el modal de resumen de pedido
  const handleCloseOrderModal = () => {
    setOrderModalOpen(false);
  };

  const [menuOpen, setMenuOpen] = useState(false);

  
const handleToggleMenu = () => {
  setMenuOpen(!menuOpen);
};


  return (
    <>
      <Navbar />

      <div className="main-content">
        <ProductList onAddToCart={addToCart} />

        <Cart
          cartItems={cartItems}
          incrementQuantity={incrementQuantity}
          decrementQuantity={decrementQuantity}
          removeFromCart={removeFromCart}

          // Pasamos la función que abre el modal
          onProcessOrder={handleProcessOrder}
        />
      </div>
      
      <OrderSummaryModal
        isOpen={orderModalOpen}
        onClose={handleCloseOrderModal}
        cartItems={cartItems}
      />
    </>
    
  );
}

export default App;
