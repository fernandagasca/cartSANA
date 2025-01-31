import React from 'react';
import axios from 'axios'; 
import '../styles/OrderSummaryModal.css';

function OrderSummaryModal({ isOpen, onClose, cartItems, customerId }) {
  if (!isOpen) {
    return null; 
  }

  // Calcular total nuevamente
  const total = cartItems.reduce((acc, item) => {
    return acc + item.product.price * item.quantity;
  }, 0);

  // Función para confirmar
  const handleConfirmOrder = async () => {
    try {
      const orderData = {
        customerId: 1, 
        items: cartItems.map((item) => ({
          productId: item.product.id, 
          quantity: item.quantity,
        })),
      };

      // Enviar la orden al backend
      const response = await axios.post('https://localhost:7110/api/Orders/ProcessOrder', orderData);

      if (response.status === 200) {
        alert('¡Orden procesada con éxito!');
        onClose();
      }
    } catch (error) {
      console.error('Error al procesar la orden:', error);
      alert('Error al procesar la orden. Inténtalo de nuevo.');
    }
  };

  return (
    <div className="order-summary-overlay">
      <div className="order-summary-modal">
        <button className="close-button" onClick={onClose}>
          &times;
        </button>
        <h2>Resumen de la Orden</h2>

        <div className="summary-list">
          {cartItems.map((item) => (
            <div key={item.product.id} className="summary-item">
              <span>{item.product.name_Product}</span>
              <span>Cant: {item.quantity}</span>
              <span>Precio c/u: ${item.product.price.toFixed(2)}</span>
              <strong>
                Subtotal: ${(item.product.price * item.quantity).toFixed(2)}
              </strong>
            </div>
          ))}
        </div>

        <h3>Total: ${total.toFixed(2)}</h3>

        <button 
          className="confirm-btn" 
          onClick={handleConfirmOrder}
        >
          Confirmar Orden
        </button>
      </div>
    </div>
  );
}

export default OrderSummaryModal;
