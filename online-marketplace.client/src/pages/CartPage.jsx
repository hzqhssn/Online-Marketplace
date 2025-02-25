import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./CartPage.css"; // Import styles

function CartPage() {
    const [cart, setCart] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const storedCart = JSON.parse(localStorage.getItem("cart")) || [];
        setCart(storedCart);
    }, []);

    const updateQuantity = (productId, newQuantity) => {
        const updatedCart = cart.map((item) =>
            item.id === productId ? { ...item, quantity: newQuantity } : item
        );
        setCart(updatedCart);
        localStorage.setItem("cart", JSON.stringify(updatedCart));
    };

    const removeItem = (productId) => {
        const updatedCart = cart.filter((item) => item.id !== productId);
        setCart(updatedCart);
        localStorage.setItem("cart", JSON.stringify(updatedCart));
    };

    const getTotal = () => {
        return cart.reduce((total, item) => total + item.price * item.quantity, 0).toFixed(2);
    };

    return (
        <div className="cart-container">
            <h1>Shopping Cart</h1>
            {cart.length === 0 ? (
                <p>Your cart is empty.</p>
            ) : (
                <>
                    <ul className="cart-items">
                        {cart.map((item) => (
                            <li key={item.id} className="cart-item">
                                <img src={item.imageUrl} alt={item.name} />
                                <div>
                                    <h3>{item.name}</h3>
                                    <p>${item.price.toFixed(2)}</p>
                                    <input
                                        type="number"
                                        min="1"
                                        value={item.quantity}
                                        onChange={(e) => updateQuantity(item.id, parseInt(e.target.value))}
                                    />
                                    <button onClick={() => removeItem(item.id)}>Remove</button>
                                </div>
                            </li>
                        ))}
                    </ul>
                    <h2>Total: ${getTotal()}</h2>
                    <button onClick={() => navigate("/checkout")}>Proceed to Checkout</button>
                </>
            )}
        </div>
    );
}

export default CartPage;
