import React from 'react';
import '../styles/Cart.css';

function Cart({ 
  cartItems, 
  incrementQuantity, 
  decrementQuantity, 
  removeFromCart,
  onProcessOrder           
}) {
  // Calcular el total de todo el carrito
  const totalPrice = cartItems.reduce((acc, item) => {
    return acc + (item.product.price * item.quantity);
  }, 0);

  return (
    <div className="cart-container">
      <h2>Mi Carrito</h2>

      {cartItems.length === 0 && <p>No hay productos en el carrito.</p>}

      {cartItems.map((item) => (
        <div key={item.product.id} className="cart-item">
          <div className="cart-info">
            <span className="product-name">{item.product.name_Product}</span>
            <span className="product-price">
              (${item.product.price.toFixed(2)} c/u)
            </span>
          </div>

          <div className="cart-controls">
            <button onClick={() => decrementQuantity(item.product.id)}>-</button>
            <span className="quantity">{item.quantity}</span>
            <button onClick={() => incrementQuantity(item.product.id)}>+</button>
          </div>

          <span className="subtotal">
            Subtotal: ${(item.product.price * item.quantity).toFixed(2)}
          </span>

          <button 
            className="remove-btn" 
            onClick={() => removeFromCart(item.product.id)}
          >
            Eliminar
          </button>
        </div>
      ))}

      {cartItems.length > 0 && (
        <div className="cart-total">
          <h3>Total: ${totalPrice.toFixed(2)}</h3>         
          <button 
            className="process-order-btn" 
            onClick={onProcessOrder}
          >
            Procesar Orden
          </button>
        </div>
      )}
    </div>
  );
}

export default Cart;
