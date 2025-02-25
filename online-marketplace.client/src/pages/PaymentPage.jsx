import  { useState } from "react";

function PaymentPage() {
    const [loading, setLoading] = useState(false);

    const handleCheckout = async () => {
        setLoading(true);
        const cart = JSON.parse(localStorage.getItem("cart")) || [];

        try {
            const response = await fetch("https://localhost:7155/api/Payment/create-checkout-session", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(cart),
            });

            const data = await response.json();
            window.location.href = data.sessionUrl; // Redirect to Stripe checkout
        } catch (error) {
            console.error("Error during checkout:", error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="payment-container">
            <h1>Payment</h1>
            <button onClick={handleCheckout} disabled={loading}>
                {loading ? "Processing..." : "Proceed to Payment"}
            </button>
        </div>
    );
}

export default PaymentPage;
