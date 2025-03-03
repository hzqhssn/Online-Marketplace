﻿import { useState } from "react";
import { useNavigate } from "react-router-dom";

function LoginPage() {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
    });

    const [message, setMessage] = useState(null);
    const navigate = useNavigate(); // ✅ Hook to navigate to another page

    const handleInputChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("https://localhost:7155/api/Auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    email: formData.email,
                    password: formData.password,
                }),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Login failed");
            }

            const data = await response.json();
            localStorage.setItem("token", data.token);
            localStorage.setItem("userId", data.userId);

            setMessage("Login successful!");

            // ✅ Redirect to profile page after 1.5 seconds
            setTimeout(() => navigate("/profile"), 1500);
        } catch (error) {
            setMessage(error.message);
        }
    };

    return (
        <div className="login-container">
            <h1>Login</h1>
            {message && <p className="message">{message}</p>}

            <form onSubmit={handleSubmit}>
                <label>Email: <input type="email" name="email" value={formData.email} onChange={handleInputChange} required /></label>
                <label>Password: <input type="password" name="password" value={formData.password} onChange={handleInputChange} required /></label>
                <button type="submit">Login</button>
            </form>
        </div>
    );
}

export default LoginPage;
