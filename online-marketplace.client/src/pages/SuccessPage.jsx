import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

function SuccessPage() {
    const navigate = useNavigate();

    useEffect(() => {
        localStorage.removeItem("cart"); // Clear cart after payment
        setTimeout(() => navigate("/products"), 3000);
    }, [navigate]);

    return (
        <div className="success-container">
            <h1>Payment Successful!</h1>
            <p>Thank you for your purchase. Redirecting to products...</p>
        </div>
    );
}

export default SuccessPage;
